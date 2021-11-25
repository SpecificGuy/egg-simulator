using EggCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EggCalculator.Models
{
    public class DailyResult : NotifyPropertyChanged
    {
        private int eggsMinted;

        public int EggsMinted
        {
            get { return eggsMinted; }
            set { eggsMinted = value;
                OnPropertyChanged();
            }
        }

        private int grossDailyCost;

        public int GrossDailyCost
        {
            get { return grossDailyCost; }
            set { grossDailyCost = value;
                OnPropertyChanged();
            }
        }

        private int grossDailyGain;

        public int GrossDailyGain
        {
            get { return grossDailyGain; }
            set { grossDailyGain = value;
                OnPropertyChanged();
            }
        }
    }
}
