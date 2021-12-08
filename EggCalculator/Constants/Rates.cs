using EggCalculator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EggCalculator.Constants
{
    public class Rates
    {
        //public static int MatchLimit = 20;
        //public static int ExperienceLimit = 100;
        //public static int EggPrice = 110000;
        //public static int NormalMetamonPrice = 540000;
        //public static int PotionPrice = 17000;
        //public static int EggFragmentQuantity = 1000;
        //public static int MinimunRacaRevenue = 200000;
        //public static int MetamonCountRevenueStart = 30;
        //public static int MetamonCountPotionBuyStart = 2;

        private static int experienceOffset = 200;
        [JsonProperty]
        public static int ExperienceOffset
        {
            get { return experienceOffset ; }
            set { experienceOffset =  value;
                NotifyStaticPropertyChanged();
            }
        }


        private static int matchLimit = 20;
        [JsonProperty]
        public static int MatchLimit
        {
            get { return matchLimit; }
            set { matchLimit = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int baseExperience = 100;
        [JsonProperty]
        public static int BaseExperience
        {
            get { return baseExperience; }
            set { baseExperience = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int eggPrice = 110000;
        [JsonProperty]
        public static int EggPrice
        {
            get { return eggPrice; }
            set { eggPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int normalMetamonPrice = 700000;
        [JsonProperty]
        public static int NormalMetamonPrice
        {
            get { return normalMetamonPrice; }
            set { normalMetamonPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int rareMetamonPrice = 5200000;
        [JsonProperty]
        public static int RareMetamonPrice
        {
            get { return rareMetamonPrice; }
            set { rareMetamonPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int superRareMetamonPrice = 40000000;
        [JsonProperty]
        public static int SuperRareMetamonPrice
        {
            get { return superRareMetamonPrice; }
            set { superRareMetamonPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int superSuperRareMetamonPrice = 120000000;
        [JsonProperty]
        public static int SuperSuperRareMetamonPrice
        {
            get { return superSuperRareMetamonPrice; }
            set { superSuperRareMetamonPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int potionPrice = 13000;
        [JsonProperty]
        public static int PotionPrice
        {
            get { return potionPrice; }
            set { potionPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int yellowDiamondPrice = 52000;
        [JsonProperty]
        public static int YellowDiamondPrice
        {
            get { return yellowDiamondPrice; }
            set
            {
                yellowDiamondPrice = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int violetDiamondPrice = 310000;
        [JsonProperty]
        public static int VioletDiamondPrice
        {
            get { return violetDiamondPrice; }
            set
            {
                violetDiamondPrice = value;
                NotifyStaticPropertyChanged();
            }
        }


        private static int blackDiamondPrice = 150000;
        [JsonProperty]
        public static int BlackDiamondPrice
        {
            get { return blackDiamondPrice; }
            set
            {
                blackDiamondPrice = value;
                NotifyStaticPropertyChanged();
            }
        }


        private static int eggFragmentQuantity = 1000;
        [JsonProperty]
        public static int EggFragmentQuantity
        {
            get { return eggFragmentQuantity; }
            set {
                eggFragmentQuantity = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int minimumRacaRevenue = 200000;
        [JsonProperty]
        public static int MinimumRacaRevenue
        {
            get { return minimumRacaRevenue; }
            set { minimumRacaRevenue = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int metamonCountRevenueStart = 30;
        [JsonProperty]
        public static int MetamonCountRevenueStart
        {
            get { return metamonCountRevenueStart; }
            set { metamonCountRevenueStart = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int metamonCountPotionBuyStart = 2;
        [JsonProperty]
        public static int MetamonCountPotionBuyStart
        {
            get { return metamonCountPotionBuyStart; }
            set { metamonCountPotionBuyStart = value;
                NotifyStaticPropertyChanged();
            }
        }


        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        [JsonProperty]
        public static double[] MatchWinningRates { get; } = new double[4] { 0.7, 0.8, 0.85, 0.90 };
        [JsonProperty]
        public static int[] MatchExperience { get; } = new int[2] { 5, 3 };
        //public static int[] MatchRacaCost { get; } = new int[3] { 0, 0, 0 };

        private static double marketTaxPercentage = 3.0;
        [JsonProperty]
        public static double MarketTaxPercentage
        {
            get { return marketTaxPercentage; }
            set { marketTaxPercentage = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int[] matchRacaCost = new int[3] { 0, 0, 0 };
        [JsonProperty]
        public static int[] MatchRacaCost
        {
            get { return matchRacaCost; }
            set { matchRacaCost = value;
                NotifyStaticPropertyChanged();
            }
        }

        private static int GetLeague(int level)
        {
            if (level > 40)
                return 2;
            if (level > 20)
                return 1;
            return 0;

        }
        
        public static int GetMatchCost(int level)
        {
            return MatchRacaCost[GetLeague(level)];
        }
        public static double GetWinningFragments(Rarity rarity, int level)
        {
            //int league = GetLeague(level);
            //if (date >= new DateTime(2021, 12, 25))
            //    return FragmentsWinningRatesSixthNerf[rarity,league];
            //if (date >= new DateTime(2021, 12, 20))
            //    return FragmentsWinningRatesFifthNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 15))
            //    return FragmentsWinningRatesFourthNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 10))
            //    return FragmentsWinningRatesThirdNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 5))
            //    return FragmentsWinningRatesSecondNerf[rarity, league];
            //if (date >= new DateTime(2021, 11, 27))
            //    return FragmentsWinningRatesFirstNerf[rarity, league];

            switch (rarity)
            {
                case Rarity.NORMAL:
                    return 20 + 0.5 * (level - 1);
                case Rarity.RARE:
                    return 40 + 2 * (level - 1);
                case Rarity.SUPER_RARE:
                    return 64 + 4 * (level - 1);
                case Rarity.SUPER_SUPER_RARE:
                    return 100 + 9 + (level - 1);
                default:
                    return 0;
            }

            //return FragmentsWinningRates[(int)rarity, league];
        }

        public static double GetLosingFragments(Rarity rarity, int level)
        {
            //int league = GetLeague(level);
            //if (date >= new DateTime(2021, 12, 25))
            //    return FragmentsLosingRatesSixthNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 20))
            //    return FragmentsLosingRatesFifthNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 15))
            //    return FragmentsLosingRatesFourthNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 10))
            //    return FragmentsLosingRatesThirdNerf[rarity, league];
            //if (date >= new DateTime(2021, 12, 5))
            //    return FragmentsLosingRatesSecondNerf[rarity, league];
            //if (date >= new DateTime(2021, 11, 27))
            //    return FragmentsLosingRatesFirstNerf[rarity, league];

            //return FragmentsLosingRates[(int)rarity, league];

            switch (rarity)
            {
                case Rarity.NORMAL:
                    return 10 + 0.25 * (level - 1);
                case Rarity.RARE:
                    return 20 + 1 * (level - 1);
                case Rarity.SUPER_RARE:
                    return 32 + 2 * (level - 1);
                case Rarity.SUPER_SUPER_RARE:
                    return 50 + 4.5 + (level - 1);
                default:
                    return 0;
            }
        }

        public static double GetMatchWinningRate(Rarity rarity)
        {
            return MatchWinningRates[(int)rarity];
        }

        public static int GetWinningExperience()
        {
            return MatchExperience[0];
        }
        public static int GetLosingExperience()
        {
            return MatchExperience[1];
        }

        public static int GetPotionPrice(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    return PotionPrice;
                case Rarity.RARE:
                    return YellowDiamondPrice;
                case Rarity.SUPER_RARE:
                    return VioletDiamondPrice;
                case Rarity.SUPER_SUPER_RARE:
                    return BlackDiamondPrice;
                default:
                    return 0;
            }
        }

        public static int GetMetamonPrice(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    return NormalMetamonPrice;
                case Rarity.RARE:
                    return RareMetamonPrice;
                case Rarity.SUPER_RARE:
                    return SuperRareMetamonPrice;
                case Rarity.SUPER_SUPER_RARE:
                    return SuperSuperRareMetamonPrice;
                default:
                    return 0;
            }
        }

    }
}
