using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    ///<Summary>
    /// DamageType is a type of damage that can be cast or, more importantly for
    /// the character, resisted.
    ///</Summary>
    public class DamageType : IEntity
    {
        public string Name { get; set; }
    }

    public static class DamageTypes
    {
        private static readonly Dictionary<string, DamageType> damageTypes;

        public static DamageType Get(string damageTypeName)
        {
            return damageTypes[damageTypeName];
        }

        static DamageTypes()
        {
            damageTypes = EntityReader.EntityNames<DamageType>("DamageTypes.json");
        }
    }
}
