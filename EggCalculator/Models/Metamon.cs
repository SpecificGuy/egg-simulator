using EggCalculator.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.EventCalendar;

namespace EggCalculator.Models
{
    public enum Rarity
    {
        NORMAL = 0,
        RARE = 1,
        SUPER_RARE = 2,
        SUPER_SUPER_RARE = 3
    }

    public enum League
    {
        APPRENTICE = 0,
        INTERMEDIATE = 1,
        ADVANCED = 2
    }

    [Serializable]
    public class Metamon
    {
        public Rarity Rarity { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public League League { get; set; }

        public Metamon(Rarity rarity, int level, string label, int experience = 0)
        {
            Rarity = rarity;
            Level = level;
            Experience = experience;
            League = League.APPRENTICE;
        }

        public int PlayMatch (DateTime date)
        {
            Random rand = new Random();
            double matchResult = rand.NextDouble();
            int fragments;

            if (matchResult < Rates.GetMatchWinningRate((int)Rarity))  //metamon won the game
            {
                fragments = Rates.GetWinningFragments((int)Rarity, Level, date);
                GainExperience(Rates.GetWinningExperience());
            } else
            {
                fragments = Rates.GetLosingFragments((int)Rarity, Level, date);
                GainExperience(Rates.GetLosingExperience());
            }

            return fragments;
        }

        private void GainExperience (int exp)
        {
            if (Experience + exp < 100)
                Experience += exp;
            else
                Experience = 100;
        }
        private void ResetExperience ()
        {
            Experience = 0;
        }

        public bool LevelUp()
        {
            if (CanLevelUp())
            {
                Level += 1;
                League = GetLeague();
                ResetExperience();
                return true;
            }

            return false;
        }

        public bool CanLevelUp()
        {
            return Experience >= Rates.ExperienceLimit && (Level + 1) <= 60;
        }
        public League GetLeague()
        {
            if (Level > 40)
                return (League)2;
            if (Level > 20)
                return (League)1;
            return 0;

        }
    }
}
