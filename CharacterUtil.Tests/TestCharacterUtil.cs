using System;
using System.Collections.Generic;
using System.Text;
using LibShadowbane.CharacterUtil;

namespace LibShadowbane.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class TestCharacterUtil
    {
        [SetUp]
        protected void SetUp() { }

        [Test]
        public void TestEffect()
        {
            RankedEffect effect = Effects.Get("Prayer of Protection", 40);
            Assert.That(effect.GetModifier(Stat.Defense), Is.EqualTo(50));
            
            effect.Rank = 35;
            Assert.That(effect.GetModifier(Stat.Defense), Is.EqualTo(45));

            Assert.That(effect.GetModifier(Stat.Strength), Is.EqualTo(0));

            Character toon = new Character();
            Assert.That(toon.GetEffectModifier(Stat.Defense), Is.EqualTo(0));
            toon.AddEffect(effect);
            Assert.That(toon.GetEffectModifier(Stat.Defense), Is.EqualTo(45));
        }

        [Test]
        public void TestEquipment()
        {
            Character toon = new Character();
            toon.EquipItem(Equipments.Get("Ring", "Ornamented", "of Thurin"), Slot.LeftHand);
            Assert.That(toon.GetEquipmentModifier(Stat.FlatDefense), Is.EqualTo(30));
            toon.EquipItem(Equipments.Get("Ring", "Ornamented", "of Thurin"), Slot.RightHand);
            Assert.That(toon.GetEquipmentModifier(Stat.FlatDefense), Is.EqualTo(60));
            toon.EquipItem(Equipments.Get("Hunting Leather Vest", "Aegis", null), Slot.Chest);
            Assert.That(toon.ArmorWeight, Is.EqualTo(4));
            Assert.That(toon.GetEquipmentModifier(Stat.ArmorDefense), Is.EqualTo(49));
        }
    }
}
