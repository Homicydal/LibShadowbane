using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class Skill : IEntity
    {
        public string Name { get; set; }
        public Stat? PrimaryStat { get; set; } = Stat.Intelligence;
        public Stat SecondaryStat { get; set; }
    }

    public class TrainedSkill
    {
        public Func<Stat, int> GetBaseStat { private get; set; }
        public Func<Stat, int> GetBuffedStat { private get; set; }
        private int _trains;
        public Skill Skill { get; private set; }
        public int Trains { get; set; } = 0;

        private double BaseSkill(Func<Stat, int> statGetter)
        {
            if (statGetter == null)
            {
                throw new Exception("Skill calculation requires GetBaseStat/GetBuffedStat injection");
            }

            if (Skill.PrimaryStat == null)
            {
                return 0; // pretty much just for running
            }

            double stat_value(Stat? stat) => ((statGetter((Stat)stat) - 40) * 0.01f) + 1;
            var pri = stat_value(Skill.PrimaryStat);
            var sec = stat_value(Skill.SecondaryStat);
            var statBase = pri*.6 + sec*.4;
            var result = Math.Pow(statBase, 1.5) * 15.0;
            return result;
        }

        public int TrainValue {
            get {
                if (Trains <= 10) 
                { 
                    return Trains * 2; // two for one
                } 
                else if (Trains <= 90) 
                {
                    return Trains + 10; // one for one
                }
                else if (Trains <= 134) 
                { 
                    return (Trains - 90) / 2 + 100; // one for two
                }
                else 
                { 
                    return (Trains - 134) / 3 + 122; // one for three
                }
            }
        }

        public int UntrainedSkill => (int)BaseSkill(GetBaseStat);
        public int UntrainedBuffedSkill => (int)BaseSkill(GetBuffedStat);

        public int UnbuffedSkill => UntrainedSkill + TrainValue;
        public int BuffedSkill => UntrainedBuffedSkill + TrainValue;

        //public int MaxTrains => GetStat(Stat.Intelligence);

        internal TrainedSkill(Skill skill, int trains=0)
        {
            Skill = skill;
            Trains = trains;
        }
    }

    public static class Skills
    {
        private static readonly Dictionary<string, Skill> skills;

        public static Skill Get(string skillName) => skills[skillName];

        public static TrainedSkill Get(string skillName, int trains) 
            => new TrainedSkill(Get(skillName), trains);

        static Skills()
        {
            skills = EntityReader.EntityNames<Skill>("Skills.json");
        }
    }
}
