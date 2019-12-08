using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    internal class EffectManager
    {
        private readonly List<RankedEffect> effects = new List<RankedEffect>();

        internal bool AddEffect(RankedEffect newEffect)
        {
            foreach (RankedEffect existing in effects)
            {
                if (existing.Effect.StackCategory == newEffect.Effect.StackCategory)
                {
                    if (newEffect.Rank >= existing.Rank)
                    {
                        effects.Remove(existing);
                        effects.Add(newEffect);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            effects.Add(newEffect);
            return true;
        }

        internal decimal GetEffectModifier(Stat stat)
        {
            decimal bonus = 0;
            foreach (RankedEffect effect in effects)
            {
                bonus += effect.GetModifier(stat);
            }

            return bonus;
        }
    }
}
