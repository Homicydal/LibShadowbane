using System;
using System.Collections.Generic;
using System.Text;

namespace LibShadowbane.CharacterUtil
{
    public enum Stat
    {
        Strength,
        StrengthMax,
        Dexterity,
        DexterityMax,
        Intelligence,
        IntelligenceMax,
        Spirit,
        SpiritMax,
        Constitution,
        ConstitutionMax,
        AttackRating,
        AttackSpeed,
        Defense, // the character's defense
        ArmorDefense, // defense that gets added to/multiplied by armor
        ShieldDefense, // defense that gets added to/multiplied by shield
        FlatDefense, // flat bonuses to defense, modified by stance/rune only
        Health,
        HealthRegen,
        Stamina,
        StaminaRegen,
        Mana,
        ManaRegen,
        MovementSpeed,
        AttackDamage,
        WeaponDamage,
        PowerDamage,
    }

    /* things that exist but I don't care about yet: */
    // duration of effect
    // dot (mana, hp, stamina)
    // movement prevention (root)
    // power prevention (powerblock)
    // powerblock immunity
    // ground
    // movement prevention/action prevention (stun)
    // stun immunity
    // silence
    // healing immunity (shadowmantle)
}
