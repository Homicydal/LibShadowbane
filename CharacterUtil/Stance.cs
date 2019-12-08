using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public class Stance : IEntity
    {
        public string Name { get; set; }
        public decimal DefenseBase { get; set; } = 0;
        public decimal DefenseRange { get; set; } = 0;
    }

    public class RankedStance
    {
        public Stance Stance { get; private set; }
        public int Rank { get; set; }

        public decimal DefenseModifier => Stance.DefenseBase + Rank/40m * Stance.DefenseRange;

        internal RankedStance(Stance stance, int rank)
        {
            Stance = stance;
            Rank = rank;
        }
    }

    public static class Stances
    {
        private static readonly Dictionary<string, Stance> stances;

        public static Stance Get(string stanceName) => stances[stanceName];

        public static RankedStance Get(string stanceName, int rank) 
            => new RankedStance(Get(stanceName), rank);

        static Stances()
        {
            stances = EntityReader.EntityNames<Stance>("Stances.json");
        }
    }
}
