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
    public class AccountState
    {
        public int RacaBalance { get; set; }
        public double FragmentBalance { get; set; }
        public int PotionBalance { get; set; }
        public int YellowDiamondBalance { get; set; }
        public int VioletDiamondBalance { get; set; }
        public int BlackDiamondBalance { get; set; }
        public int EggBalance { get; set; }
        public int RacaRevenue { get; set; }
        public ObservableCollection<Metamon> Metamons { get; set; }
        public AccountState Copy()
        {
            AccountState temp = new AccountState
            {
                RacaBalance = RacaBalance.DeepClone(),
                FragmentBalance = FragmentBalance.DeepClone(),
                PotionBalance = PotionBalance.DeepClone(),
                YellowDiamondBalance = YellowDiamondBalance.DeepClone(),
                VioletDiamondBalance = VioletDiamondBalance.DeepClone(),
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
                    VioletDiamondBalance++;
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
                    if (VioletDiamondBalance > 0)
                        VioletDiamondBalance--;
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
                    return VioletDiamondBalance;
                case Rarity.SUPER_SUPER_RARE:
                    return BlackDiamondBalance;
                default:
                    return 0;
            }
        }
    }
}
