using EggCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EggCalculator.Models
{
    public class GameResult : NotifyPropertyChanged
    {
        private int metamonLevelUp;

        public int MetamonLevelUp
        {
            get { return metamonLevelUp; }
            set { metamonLevelUp = value;
                OnPropertyChanged();
            }
        }

        private int metamonLeagueUp;

        public int MetamonLeagueUp
        {
            get { return metamonLeagueUp; }
            set { metamonLeagueUp = value;
                OnPropertyChanged();
            }
        }

        private double fragments;

        public double Fragments
        {
            get { return fragments; }
            set { fragments = value;
                OnPropertyChanged();
            }
        }

        private int matchCost;

        public int MatchCost
        {
            get { return matchCost; }
            set { matchCost = value;
                OnPropertyChanged();
            }
        }

        private int potionCost;

        public int PotionCost
        {
            get { return potionCost; }
            set { potionCost = value;
                OnPropertyChanged();
            }
        }

        private int matchCount;

        public int MatchCount
        {
            get { return matchCount; }
            set { matchCount = value;
                OnPropertyChanged();
            }
        }

        private bool interrupted;

        public bool Interrupted
        {
            get { return interrupted; }
            set { interrupted = value;
                OnPropertyChanged();
            }
        }

        //int metamonLevelUp = 0;
        //int metamonLeagueUp = 0;
        //int initialFragmentBalance = FragmentBalance;
        //int totalMatchCost = 0;
        //bool gameInterrupted = false;
    }
}
