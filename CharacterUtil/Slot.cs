using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public enum Slot
    {
        RightHand,
        LeftHand,
        Amulet,
        RightRing,
        LeftRing,
        Helm,
        Chest,
        Sleeves,
        Gloves,
        Leggings,
        Boots,
    }

    ///<Summary>
    /// A target for mapping an EquipmentType to one or more slots. Some items
    /// like one-handed weapons are ValidIn multiple slots, while other items
    /// like a robe occupies multiple slots. One field or the other should be
    /// set, with ValidIn being preferred
    ///</Summary>
    public class SlotMap
    {
        public EquipmentType EquipmentType { get; set; }
        public List<Slot> ValidIn { get; set; }
        public List<Slot> Occupies { get; set; }
    }



    ///<Summary>
    /// A mapping of equipment type to slots
    ///</Summary>
    public static class Slots
    {
        private static readonly Dictionary<EquipmentType, SlotMap> equipmentSlots;

        public static SlotMap Get(EquipmentType itemType) => equipmentSlots[itemType];

        static Slots()
        {
            equipmentSlots = EntityReader.EntityMap<EquipmentType, SlotMap>("EquipmentSlots.json", map => map.EquipmentType);
        }
    }
}
