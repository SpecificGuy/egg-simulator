using EggCalculator.Constants;
using EggCalculator.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.EventCalendar;

namespace EggCalculator.Models
{
    public class Simulation : NotifyPropertyChanged, ICalendarEvent
    {
        private DateTime? dateFrom;

        public DateTime? DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime? dateTo;

        public DateTime? DateTo
        {
            get { return dateTo; }
            set { dateTo = value;
                OnPropertyChanged();
            }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value;
                OnPropertyChanged();
            }
        }

        private AccountState accountState;

        public AccountState AccountState
        {
            get { return accountState; }
            set { accountState = value;
                OnPropertyChanged("AccountState");
            }
        }

        private DailyResult dailyResult;
        [JsonIgnore]
        public DailyResult DailyResult
        {
            get { return dailyResult; }
            set { dailyResult = value;
                OnPropertyChanged();
            }
        }

        private GameResult gameResult;
        [JsonIgnore]
        public GameResult GameResult
        {
            get { return gameResult; }
            set { gameResult = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        public ObservableCollection<String> Logs { get; set; } = new ObservableCollection<String>();
        [JsonIgnore]
        public bool MainEvent { get; set; } = false;

        public Simulation()
        {
            DailyResult = new DailyResult();
            GameResult = new GameResult();
        }
        public GameResult PlayGame(bool autoBuyPotions, bool autoLevelUp)
        {
            GameResult gr = new GameResult
            {
                MetamonLeagueUp = 0,
                MetamonLevelUp = 0,
                Fragments = 0,
                MatchCost = 0,
                Interrupted = false,
                PotionCost = 0,
                MatchCount = 0
            };

            foreach (Metamon metamon in AccountState.Metamons)
            {
                for (int i = 0; i < Rates.MatchLimit; i++)
                {
                    if (AccountState.RacaBalance - gr.MatchCost - Rates.GetMatchCost(metamon.Level) > 0)
                    {
                        gr.MatchCost += Rates.GetMatchCost(metamon.Level);
                        gr.Fragments += metamon.PlayMatch();
                        gr.MatchCount++;

                        if (autoLevelUp && AccountState.Metamons.Count >= Rates.MetamonCountPotionBuyStart && metamon.CanLevelUp())
                        {
                            if(autoBuyPotions && AccountState.GetPotionBalance(metamon.Rarity) <= 0)
                                gr.PotionCost += BuyPotions(metamon.Rarity);
                            
                            if(AccountState.DecreasePotionBalance(metamon.Rarity))
                            {
                                League before = metamon.GetLeague();
                                if(metamon.LevelUp())
                                {
                                    League after = metamon.GetLeague();
                                    if (before != after)
                                        gr.MetamonLeagueUp++;
                                    gr.MetamonLevelUp++;
                                }
                            }
                        }
                    }
                    else
                    {
                        gr.Interrupted = true;
                        break;
                    }
                }
            }

            AccountState.FragmentBalance += gr.Fragments;
            AccountState.RacaBalance -= gr.MatchCost;
            DailyResult.GrossDailyCost += gr.MatchCost;

            GameResult = gr;
            LogGameResult(gr);
            return gr;
        }
        internal int BuyPotions(Rarity rarity)
        {
            int potPrice = Rates.GetPotionPrice(rarity);
            if (AccountState.RacaBalance > potPrice)
            {
                AccountState.IncreasePotionBalance(rarity);
                AccountState.RacaBalance -= potPrice;
                DailyResult.GrossDailyCost += potPrice;
                return potPrice;
            }
            return 0;
        }
        private int MinimunRacaWalletDeposit()
        {
            int minimumRacaMatchesFee = AccountState.Metamons.Select(x => Rates.GetMatchCost(x.Level) * Rates.MatchLimit).Sum();
            int minuminPotionsFee = AccountState.Metamons.Select(x => Rates.GetPotionPrice(x.Rarity)).Sum();

            return minuminPotionsFee + minimumRacaMatchesFee;
        }
        internal int BuyMetamon(Rarity rarity, bool manual = false)
        {
            int minimumDeposit = MinimunRacaWalletDeposit();
            int metamonPrice = Rates.GetMetamonPrice(rarity);
            if (manual || AccountState.RacaBalance > metamonPrice + minimumDeposit)
            {
                AccountState.Metamons.Add(new Metamon(rarity, 1, "Metamon"));
                AccountState.RacaBalance -= metamonPrice;
                DailyResult.GrossDailyCost += metamonPrice;
                Logs.Add($"[{(manual ? "MANUAL" : "AUTO")}][1 METAMON] Bought a metamon for {metamonPrice}.");

                return 1;
            }
            return 0;
        }
        internal void LogInitialState()
        {
            Logs.Add($"[START DAY RACA BALANCE] {AccountState.RacaBalance}");
            Logs.Add($"[START DAY FRAGMENT BALANCE] {AccountState.FragmentBalance}");

        }
        internal int MintEggs()
        {
            if (AccountState.FragmentBalance / Rates.EggFragmentQuantity > 0)
            {
                int eggQuantity = (int)AccountState.FragmentBalance / Rates.EggFragmentQuantity;

                AccountState.EggBalance += eggQuantity;
                AccountState.FragmentBalance = AccountState.FragmentBalance % Rates.EggFragmentQuantity;
                Logs.Add($"[{eggQuantity} EGGS MINTED] Potential value {eggQuantity * Rates.EggPrice} RACA.");

                DailyResult.EggsMinted = eggQuantity;
                return eggQuantity;
            }

            return 0;
        }
        internal long GetRevenue()
        {
            int minimumDeposit = MinimunRacaWalletDeposit();

            if (AccountState.Metamons.Count >= Rates.MetamonCountRevenueStart && AccountState.RacaBalance > Rates.MinimumRacaRevenue + minimumDeposit * 2)
            {
                long calculatedRaca = AccountState.RacaBalance - minimumDeposit * 2;

                AccountState.RacaRevenue += calculatedRaca;
                AccountState.RacaBalance = AccountState.RacaBalance - calculatedRaca;
                Logs.Add($"[PAYDAY] Converted {calculatedRaca} RACA.");

                return calculatedRaca;
            }

            return 0;
        }
        internal void SellEggs()
        {
            if (AccountState.EggBalance > 0)
            {
                int totalSellGain = (int) (AccountState.EggBalance * (Rates.EggPrice - Rates.EggPrice * Rates.MarketTaxPercentage/100));
                DailyResult.GrossDailyGain += totalSellGain;
                Logs.Add($"[{AccountState.EggBalance} EGGS SOLD] Total gain {totalSellGain} RACA.");
                AccountState.RacaBalance += totalSellGain;
                AccountState.EggBalance = 0;
            }
        }
        internal void LogGameResult(GameResult gr)
        {
            //Logs.Add($"[FRAGMENT FARM] Gained {gr.Fragments}");
            //Logs.Add($"[URACA] Match cost {gr.MatchCost}");

            if (gr.Interrupted)
                Logs.Add($"[FUND EXPIRED] You don't have enough RACA to play");

            if (gr.MetamonLevelUp > 0)
                Logs.Add($"[AUTO][{gr.MetamonLevelUp} LEVELUP] Spent {gr.PotionCost} RACA for potions.");
            if (gr.MetamonLeagueUp > 0)
                Logs.Add($"[AUTO][{gr.MetamonLeagueUp} LEAGUEUP] Metamons now gain more fragments.");

        }
    }
}
