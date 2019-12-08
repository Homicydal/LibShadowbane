using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    internal class EquipmentManager
    {
        private readonly Dictionary<Slot, RolledEquipment> slots = new Dictionary<Slot, RolledEquipment>() {
            [Slot.RightHand] = null,
            [Slot.LeftHand] = null,
            [Slot.Amulet] = null,
            [Slot.RightRing] = null,
            [Slot.LeftRing] = null,
            [Slot.Helm] = null,
            [Slot.Chest] = null,
            [Slot.Sleeves] = null,
            [Slot.Gloves] = null,
            [Slot.Leggings] = null,
            [Slot.Boots] = null,
        };

        private readonly List<Slot> armorSlots = new List<Slot>() {
            Slot.Helm, Slot.Chest, Slot.Sleeves, Slot.Gloves, Slot.Leggings, Slot.Boots
        };
        
        internal void EquipItem(RolledEquipment item, Slot? slot=null)
        {
            SlotMap slotMap = Slots.Get(item.Base.EquipmentType);
            if (slot == null)
            {
                if (slotMap.Occupies != null)
                {
                    slot = slotMap.Occupies[0];
                }
                else if (slotMap.ValidIn != null && slotMap.ValidIn.Count == 1)
                {
                    slot = slotMap.ValidIn[0];
                }
                else
                {
                    throw new Exception("Slot not provided and could not infer");
                }
            }
            
            if (slotMap.Occupies != null)
            {
                foreach (Slot checkSlot in slotMap.Occupies)
                {
                    if (slots[checkSlot] != null)
                    {
                        throw new Exception("Required slot not empty");
                    }
                }
            }

            if (slots[(Slot)slot] != null)
            {
                throw new Exception("Specified slot not empty");
            }

            // todo: any other requirement checks

            slots[(Slot)slot] = item;
        }

        internal void UnequipItem(Slot slot)
        {
            slots[slot] = null;
        }

        internal decimal GetModifier(Stat stat)
        {
            decimal bonus = 0;
            foreach (KeyValuePair<Slot, RolledEquipment> item in slots)
            {
                bonus += item.Value?.GetModifier(stat) ?? 0;
            }

            return bonus;
        }

        internal int GetEquipmentDefense(Func<Skill, decimal?> GetSkillValue)
        {
            float defense = 0f;
            
            var rightHand = slots[Slot.RightHand];
            var leftHand = slots[Slot.LeftHand];
            
            // shield:
            if (leftHand?.Base.EquipmentType == EquipmentType.Shield)
            {
                var baseDefense = (float)leftHand.GetModifier(Stat.ShieldDefense);
                float skill = (float?)GetSkillValue(leftHand.Base.PrimarySkill) ?? 0f;
                defense += baseDefense * (skill * .01f + 1);
            }

            // weapon:
            var weapon = rightHand != null ? rightHand
                       : leftHand?.Base.EquipmentType == EquipmentType.OneHandedWeapon ? leftHand
                       : null;
            defense += (float?)GetSkillValue(weapon?.Base.PrimarySkill) ?? 0f / 2;
            defense += (float?)GetSkillValue(weapon?.Base.SecondarySkill) ?? 0f / 2;

            // armor:
            foreach (Slot slot in armorSlots)
            {
                if (slots[slot] == null) { continue; }

                var baseDefense = (float)slots[slot].GetModifier(Stat.ArmorDefense);
                float skill = (float?)GetSkillValue(slots[slot].Base.PrimarySkill) ?? 0f;
                defense += baseDefense * (skill * 0.02f + 1);
            }
            return (int)Math.Round(defense, MidpointRounding.AwayFromZero);
        }

        internal int ArmorWeight()
        {
            int weight = 0;
            foreach (Slot slot in armorSlots)
            {
                weight += slots[slot]?.Base.Weight ?? 0;
            }
            return weight;
        }
    }
}