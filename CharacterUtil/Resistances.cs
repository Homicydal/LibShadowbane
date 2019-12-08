using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class Resistances : Dictionary<DamageType, int>
    {
        public string DamageType { get; set; }
        public int Resistance { get; set; }
    }
}
