using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class EffectComponent
    {
        public Stat Stat { get; set; }
        public decimal BaseValue { get; set; }
        public decimal Rank40Bonus { get; set; }
    }

    public class Effect : IEntity
    {
        public string Name { get; set; }
        public string StackCategory { get; set; }
        public List<string> Tags { get; set; }
        public List<EffectComponent> Components { get; set; }
    }

    public class RankedEffect
    {
        public Effect Effect { get; private set; }
        public int Rank { get; set; } = 0;
        
        public decimal GetModifier(Stat stat)
        {
            foreach(EffectComponent component in Effect.Components)
            {
                if (component.Stat == stat)
                {
                    return component.BaseValue + (component.Rank40Bonus / 40 * Rank);
                }
            }

            return 0;
        }

        internal RankedEffect(Effect effect, int rank)
        {
            Effect = effect;
            Rank = rank;
        }
    }

    public static class Effects
    {
        private static readonly Dictionary<string, Effect> effects;

        public static Effect Get(string effectName) => effects[effectName];

        public static RankedEffect Get(string effectName, int rank) 
            => new RankedEffect(Get(effectName), rank);

        static Effects()
        {
            effects = EntityReader.EntityNames<Effect>("Effects.json");
        }
    }
}
