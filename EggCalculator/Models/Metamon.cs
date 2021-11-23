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

        public double PlayMatch (DateTime date)
        {
            Random rand = new Random();
            double matchResult = rand.NextDouble();
            double fragments;

            if (matchResult < Rates.GetMatchWinningRate(Rarity))  //metamon won the game
            {
                fragments = Rates.GetWinningFragments(Rarity, Level, date);
                GainExperience(Rates.GetWinningExperience());
            } else
            {
                fragments = Rates.GetLosingFragments(Rarity, Level, date);
                GainExperience(Rates.GetLosingExperience());
            }

            return fragments;
        }

        private void GainExperience (int exp)
        {
            if (Experience + exp < ExperienceNeededForLevelUp() + Rates.ExperienceOffset)
                Experience += exp;
            else
                Experience = ExperienceNeededForLevelUp() + Rates.ExperienceOffset;
        }
        private void ConsumeExperience()
        {
            Experience = Experience - ExperienceNeededForLevelUp();
        }

        public bool LevelUp()
        {
            if (CanLevelUp())
            {
                Level += 1;
                League = GetLeague();
                ConsumeExperience();
                return true;
            }

            return false;
        }

        public int ExperienceNeededForLevelUp()
        {
            return Rates.BaseExperience + (Level - 1) * 5;
        }

        public bool CanLevelUp()
        {
            return Experience >= ExperienceNeededForLevelUp() && (Level + 1) <= 60; //Update from 
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
