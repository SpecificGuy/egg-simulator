using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EggCalculator.Models
{
    public class GameResult
    {
        public int MetamonLevelUp { get; set; }
        public int MetamonLeagueUp { get; set; }
        public int Fragments { get; set; }
        public int MatchCost { get; set; }
        public int PotionCost { get; set; }
        public int MatchCount { get; set; }
        public bool Interrupted { get; set; }
        //int metamonLevelUp = 0;
        //int metamonLeagueUp = 0;
        //int initialFragmentBalance = FragmentBalance;
        //int totalMatchCost = 0;
        //bool gameInterrupted = false;
    }
}
