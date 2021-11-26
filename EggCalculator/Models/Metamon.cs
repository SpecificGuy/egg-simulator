using EggCalculator.Constants;
using EggCalculator.Utility;
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
    public class Metamon : NotifyPropertyChanged
    {
        public Rarity Rarity { get; set; }
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value;
                OnPropertyChanged();
            }
        }

        private int experience;

        public int Experience
        {
            get { return experience; }
            set { experience = value;
                OnPropertyChanged();

            }
        }
        public League League { get; set; }

        public Metamon(Rarity rarity, int level, string label, int experience = 0)
        {
            Rarity = rarity;
            Level = level;
            Experience = experience;
            League = League.APPRENTICE;
        }

        public double PlayMatch ()
        {
            Random rand = new Random();
            double matchResult = rand.NextDouble();
            double fragments;

            if (matchResult < Rates.GetMatchWinningRate(Rarity))  //metamon won the game
            {
                fragments = Rates.GetWinningFragments(Rarity, Level);
                GainExperience(Rates.GetWinningExperience());
            } else
            {
                fragments = Rates.GetLosingFragments(Rarity, Level);
                GainExperience(Rates.GetLosingExperience());
            }

            return fragments;
        }

        public void GainExperience (int exp)
        {
            if ((Experience + exp) < ExperienceNeededForLevelUp() + Rates.ExperienceOffset)
                Experience += exp;
            else
                Experience = ExperienceNeededForLevelUp() + Rates.ExperienceOffset;
        }
        private void ConsumeExperience()
        {
            int experienceNeeded = ExperienceNeededForLevelUp();
            Experience = Experience - experienceNeeded;
        }

        public bool LevelUp()
        {
            if (CanLevelUp())
            {
                ConsumeExperience();
                Level += 1;
                League = GetLeague();
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
            int experienceNeeded = ExperienceNeededForLevelUp();
            return Experience >= experienceNeeded && (Level + 1) <= 60; //Update from 
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
