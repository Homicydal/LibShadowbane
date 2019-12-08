using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    internal class SkillManager
    {
        private readonly Dictionary<Skill, TrainedSkill> skills = new Dictionary<Skill, TrainedSkill>();

        internal void AddSkill(TrainedSkill skill)
        {
            skills[skill.Skill] = skill;
        }

        internal TrainedSkill GetSkill(Skill skill)
        {
            if (skill != null && skills.ContainsKey(skill))
            {
                return skills[skill];
            }
            return null;
        }

        internal void TrainSkill(string skill, int trains, Func<Stat, int> GetBaseStat, Func<Stat, int> GetBuffedStat)
        {
            // until runes grant, adds a skill and then sets trains:
            TrainedSkill toTrain = GetSkill(skill);
            if (toTrain == null)
            {
                toTrain = Skills.Get(skill, 0);
                toTrain.GetBaseStat = GetBaseStat;
                toTrain.GetBuffedStat = GetBuffedStat;
                AddSkill(toTrain);
            }
            toTrain.Trains = trains;
        }

        internal TrainedSkill GetSkill(string skill) => GetSkill(Skills.Get(skill));

        internal decimal? GetSkillValue(Skill skill) => GetSkill(skill)?.BuffedSkill;
        internal decimal? GetSkillValue(string skill) => GetSkill(Skills.Get(skill))?.BuffedSkill;
    }
}
