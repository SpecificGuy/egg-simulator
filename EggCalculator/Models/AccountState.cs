using EggCalculator.Constants;
using EggCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.EventCalendar;

namespace EggCalculator.Models
{
    public class AccountState : NotifyPropertyChanged
    {
        private long racaBalance;

        public long RacaBalance
        {
            get { return racaBalance; }
            set { racaBalance = value;
                OnPropertyChanged();
            }
        }

        private double fragmentBalance;

        public double FragmentBalance
        {
            get { return fragmentBalance; }
            set { fragmentBalance = value;
                OnPropertyChanged();
            }
        }

        private int potionBalance;

        public int PotionBalance
        {
            get { return potionBalance; }
            set { potionBalance = value;
                OnPropertyChanged();
            }
        }

        private int yellowDiamondBalance;

        public int YellowDiamondBalance
        {
            get { return yellowDiamondBalance; }
            set { yellowDiamondBalance = value;
                OnPropertyChanged();
            }
        }

        private int purpleDiamondBalance;

        public int PurpleDiamondBalance
        {
            get { return purpleDiamondBalance; }
            set { purpleDiamondBalance = value;
                OnPropertyChanged();
            }
        }

        private int blackDiamondBalance;

        public int BlackDiamondBalance
        {
            get { return blackDiamondBalance; }
            set { blackDiamondBalance = value;
                OnPropertyChanged();
            }
        }

        private int eggBalance;

        public int EggBalance
        {
            get { return eggBalance; }
            set { eggBalance = value;
                OnPropertyChanged();
            }
        }

        private long racaRevenue;

        public long RacaRevenue
        {
            get { return racaRevenue; }
            set { racaRevenue = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Metamon> Metamons { get; set; }
        public AccountState Copy()
        {
            AccountState temp = new AccountState
            {
                RacaBalance = RacaBalance.DeepClone(),
                FragmentBalance = FragmentBalance.DeepClone(),
                PotionBalance = PotionBalance.DeepClone(),
                YellowDiamondBalance = YellowDiamondBalance.DeepClone(),
                PurpleDiamondBalance = PurpleDiamondBalance.DeepClone(),
                BlackDiamondBalance = BlackDiamondBalance.DeepClone(),
                EggBalance = EggBalance.DeepClone(),
                RacaRevenue = RacaRevenue.DeepClone(),
                Metamons = Metamons.DeepClone()
        };

            return temp;
        }


        internal bool IncreasePotionBalance(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    PotionBalance++;
                    break;
                case Rarity.RARE:
                    YellowDiamondBalance++;
                    break;
                case Rarity.SUPER_RARE:
                    PurpleDiamondBalance++;
                    break;
                case Rarity.SUPER_SUPER_RARE:
                    BlackDiamondBalance++;
                    break;
                default:
                    return false;
            }
            return true;
        }

        internal bool DecreasePotionBalance(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    if (PotionBalance > 0)
                        PotionBalance--;
                    else
                        return false;
                    break;
                case Rarity.RARE:
                    if (YellowDiamondBalance > 0)
                        YellowDiamondBalance--;
                    else
                        return false;
                    break;
                case Rarity.SUPER_RARE:
                    if (PurpleDiamondBalance > 0)
                        PurpleDiamondBalance--;
                    else
                        return false;
                    break;
                case Rarity.SUPER_SUPER_RARE:
                    if (BlackDiamondBalance > 0)
                        BlackDiamondBalance--;
                    else
                        return false;
                    break;
                default:
                    return false;
            }
            return true;
        }

        internal int GetPotionBalance(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    return PotionBalance;
                case Rarity.RARE:
                    return YellowDiamondBalance;
                case Rarity.SUPER_RARE:
                    return PurpleDiamondBalance;
                case Rarity.SUPER_SUPER_RARE:
                    return BlackDiamondBalance;
                default:
                    return 0;
            }
        }
    }
}
