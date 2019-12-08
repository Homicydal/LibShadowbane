using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public enum EquipmentType
    {
        OneHandedWeapon,
        TwoHandedWeapon,
        MainHandWeapon,
        Shield,
        Hood,
        Robe,
        SleevelessRobe,
        Amulet,
        Ring,
        Helm,
        Chest,
        Sleeves,
        Gloves,
        Leggings,
        Boots,
    }

    public class Equipment : IEntity
    {
        private Skill primarySkill;
        private Skill secondarySkill;

        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }

        public decimal SkillRequirement { get; set; }
        
        public string PrimarySkillName {
            get => primarySkill.Name;
            set => primarySkill = Skills.Get(value);
        }
        public Skill PrimarySkill { get => primarySkill; }

        public string SecondarySkillName {
            get => secondarySkill.Name;
            set => secondarySkill = Skills.Get(value);
        }
        public Skill SecondarySkill { get => secondarySkill; }

        public int Weight { get; set; }

        public List<StatAffixComponent> StatComponents { get; set; }

        public decimal GetModifier(Stat stat)
        {
            if (StatComponents == null) 
            {
                return 0;
            }

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

    public class RolledEquipment
    {
        public Equipment Base { get; private set; }
        public Affix Prefix { get; private set; }
        public Affix Suffix { get; private set; }
        
        public decimal GetModifier(Stat stat)
        {
            return (Prefix?.GetModifier(stat) ?? 0) 
                 + (Suffix?.GetModifier(stat) ?? 0)
                 + (Base.GetModifier(stat));
        }

        internal RolledEquipment(Equipment baseItem, Affix prefix=null, Affix suffix=null)
        {
            Base = baseItem;
            Prefix = prefix;
            Suffix = suffix;
        }
    }

    public static class Equipments // ugh English.
    {
        private static readonly Dictionary<string, Equipment> equipment;

        public static Equipment Get(string baseName) => equipment[baseName];

        public static RolledEquipment Get(string baseName, string prefixName, string suffixName)
        {
            var prefix = prefixName == null ? null : Affixes.Get(prefixName);
            var suffix = suffixName == null ? null : Affixes.Get(suffixName);
            return new RolledEquipment(Get(baseName), prefix, suffix);
        }

        static Equipments()
        {
            equipment = EntityReader.EntityNames<Equipment>("Equipment.json");
        }
    }
}
