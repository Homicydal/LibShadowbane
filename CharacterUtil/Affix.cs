using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class StatAffixComponent
    {
        public Stat Stat { get; set; }
        public decimal Value { get; set; }
    }

    public enum AffixType
    {
        Prefix,
        Suffix
    }

    public class Affix : IEntity
    {
        public string Name { get; set; }
        public AffixType Type { get; set; }
        public List<StatAffixComponent> StatComponents { get; set; }

        public decimal GetModifier(Stat stat)
        {
            foreach (StatAffixComponent component in StatComponents)
            {
                if (component.Stat == stat)
                {
                    return component.Value;
                }
            }

            return 0;
        }
    }

    public static class Affixes
    {
        private static readonly Dictionary<string, Affix> affixes;

        public static Affix Get(string affixName) => affixes[affixName];

        static Affixes()
        {
            affixes = EntityReader.EntityNames<Affix>("Affixes.json");
        }
    }
}
