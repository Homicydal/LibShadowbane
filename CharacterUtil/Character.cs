using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class Character
    {
        private readonly EquipmentManager equipmentManager = new EquipmentManager();
        private readonly EffectManager effectManager = new EffectManager();
        private readonly SkillManager skillManager = new SkillManager();

        private readonly Dictionary<Stat, int> baseStats = new Dictionary<Stat, int>() {
            [Stat.Dexterity] = 40,
            [Stat.DexterityMax] = 100,
            [Stat.Strength] = 40,
            [Stat.StrengthMax] = 100,
            [Stat.Intelligence] = 40,
            [Stat.IntelligenceMax] = 100,
            [Stat.Spirit] = 40,
            [Stat.SpiritMax] = 100,
            [Stat.Constitution] = 40,
            [Stat.ConstitutionMax] = 100,
        };

        public RankedStance Stance { get; set; }

        // Equipment:
        public decimal GetEquipmentModifier(Stat stat) => equipmentManager.GetModifier(stat);
        public decimal GetEquipmentDefense() => equipmentManager.GetEquipmentDefense(GetSkillValue);
        public void EquipItem(RolledEquipment item, Slot? slot=null) => equipmentManager.EquipItem(item, slot);
        public void UnequipItem(Slot slot) => equipmentManager.UnequipItem(slot);
        public int ArmorWeight => equipmentManager.ArmorWeight();

        // Effects:
        public decimal GetEffectModifier(Stat stat) => effectManager.GetEffectModifier(stat);
        public void AddEffect(RankedEffect newEffect) => effectManager.AddEffect(newEffect);

        // Skills:
        public void AddSkill(TrainedSkill skill) => skillManager.AddSkill(skill);
        public TrainedSkill GetSkill(Skill skill) => skillManager.GetSkill(skill);
        public TrainedSkill GetSkill(string skill) => skillManager.GetSkill(skill);
        public decimal? GetSkillValue(Skill skill) => skillManager.GetSkillValue(skill); // later + bonuses
        public decimal? GetSkillValue(string skill) => skillManager.GetSkillValue(skill);
        public void TrainSkill(string skill, int trains)
            => skillManager.TrainSkill(skill, trains, GetBaseStat, GetBuffedStat);
        
        // Stats:
        public void SetBaseStat(Stat attribute, int value)
        {
            baseStats[attribute] = value;
        }

        public int GetBaseStat(Stat stat) => baseStats[stat];

        public int GetBuffedStat(Stat stat)
        {
            var total = baseStats[stat];
            total += (int) GetEffectModifier(stat);
            total += (int) GetEquipmentModifier(stat);
            if (stat == Stat.Dexterity) // stupid snowflake dex.
            {
                decimal penalty = 1 - (0.004m * ArmorWeight);
                total = (int) Math.Floor(total * penalty);
            }
            return total;
        }

        // Add bonuses later from runes:
        public int Strength => (int) GetBuffedStat(Stat.Strength);
        public int Dexterity => (int) GetBuffedStat(Stat.Dexterity);
        public int Intelligence => (int) GetBuffedStat(Stat.Intelligence);
        public int Spirit => (int) GetBuffedStat(Stat.Spirit);
        public int Consitution => (int) GetBuffedStat(Stat.Constitution);

        public int Defense
        {
            get
            {
                decimal defense = Dexterity * 2;
                defense += GetEquipmentDefense();
                // defense += (Shield Armor * (shield skill * .03))
                // defense += ((Primary Weapon Skill + Secondary Weapon Skill)/2))
                defense += GetEquipmentModifier(Stat.FlatDefense);
                defense += GetEffectModifier(Stat.Defense);
                defense *= Stance?.DefenseModifier ?? 1.0m;
                return (int)Math.Round(defense, MidpointRounding.AwayFromZero);
            }
        }
        
        // ATR calculated per weapon/spell
        
        public int Health { get; }
        public int Stam { get; }
        public int Mana { get; }
        public int HealthRegen { get; }
        public int ManaRegen { get; }
        public int StamRegen { get; }

        public int Resistance(DamageType damageType) => 0; // todo
    }
}
