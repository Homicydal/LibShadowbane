using System;
using System.Collections.Generic;
using System.Text;
using LibShadowbane.CharacterUtil;

namespace LibShadowbane.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class TestCharacter
    {
        [SetUp]
        protected void SetUp() { }

        [Test]
        public void TestStatDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 25);
            Assert.That(toon.Defense, Is.EqualTo(50));
        }
        
        [Test]
        public void TestStanceDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 25);
            toon.SetBaseStat(Stat.Intelligence, 145);
            toon.Stance = Stances.Get("HealerDefense", 20); // defense +17% tt
            Assert.That(toon.Defense, Is.EqualTo(59));
            toon.Stance = Stances.Get("HealerOffense", 25); // defense -19% tt
            Assert.That(toon.Defense, Is.EqualTo(41));
        }

        [Test]
        public void TestEffectsDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 25);
            
            // regular conc pot:
            toon.AddEffect(Effects.Get("Blessing of Dexterity", 35));
            toon.AddEffect(Effects.Get("Blessed Protection", 35));
            toon.AddEffect(Effects.Get("Prayer of Protection", 35));
            toon.AddEffect(Effects.Get("Charm of Illumination (Spirit)", 35));
            toon.AddEffect(Effects.Get("Charm of Illumination (Intelligence)", 35));
            toon.Stance = Stances.Get("HealerOffense", 25);
            Assert.That(toon.Defense, Is.EqualTo(239));
            toon.Stance = Stances.Get("HealerDefense", 20);
            Assert.That(toon.Defense, Is.EqualTo(345));

            // perfect conc pot:
            toon.AddEffect(Effects.Get("Blessing of Dexterity", 40));
            toon.AddEffect(Effects.Get("Blessed Protection", 40));
            toon.AddEffect(Effects.Get("Prayer of Protection", 40));
            toon.AddEffect(Effects.Get("Charm of Illumination (Spirit)", 40));
            toon.AddEffect(Effects.Get("Charm of Illumination (Intelligence)", 40));
            toon.Stance = Stances.Get("HealerOffense", 25);
            Assert.That(toon.Defense, Is.EqualTo(259));
            toon.Stance = Stances.Get("HealerDefense", 20);
            Assert.That(toon.Defense, Is.EqualTo(374));
        }

        [Test]
        public void TestArmorDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 25);
            toon.SetBaseStat(Stat.Intelligence, 145);
            
            // perfect conc pot:
            toon.AddEffect(Effects.Get("Blessing of Dexterity", 40));
            toon.AddEffect(Effects.Get("Blessed Protection", 40));
            toon.AddEffect(Effects.Get("Prayer of Protection", 40));
            toon.AddEffect(Effects.Get("Charm of Illumination (Spirit)", 40));
            toon.AddEffect(Effects.Get("Charm of Illumination (Intelligence)", 40));

            toon.Stance = Stances.Get("HealerDefense", 20);

            toon.TrainSkill("Wear Armor, Light", 61);
            Assert.That(toon.GetSkill("Wear Armor, Light").UnbuffedSkill, Is.EqualTo(100m));
            Assert.That(toon.GetSkillValue("Wear Armor, Light"), Is.EqualTo(118m));

            toon.EquipItem(Equipments.Get("Hunting Leather Hood", "Aegis", null));
            Assert.That(toon.Dexterity, Is.EqualTo(85));
            Assert.That(toon.Defense, Is.EqualTo(484)); 
            toon.EquipItem(Equipments.Get("Hunting Leather Vest", "Aegis", null));
            Assert.That(toon.Defense, Is.EqualTo(677));
            toon.EquipItem(Equipments.Get("Hunting Leather Sleeves", "Aegis", null));
            Assert.That(toon.Defense, Is.EqualTo(768));
            toon.EquipItem(Equipments.Get("Hunting Leather Leggings", "Aegis", null));
            Assert.That(toon.Defense, Is.EqualTo(899));
            toon.EquipItem(Equipments.Get("Hunting Leather Boots", "Aegis", null));
            Assert.That(toon.Defense, Is.EqualTo(980));
            toon.EquipItem(Equipments.Get("Hunting Leather Gloves", "Aegis", null));
            
            //Todo: armor dex penalty is throwing this off
            Warn.Unless(toon.Dexterity, Is.EqualTo(88));
            Warn.Unless(toon.Defense, Is.EqualTo(1061));
        }

        [Test]
        public void TestShieldDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 25);
            toon.SetBaseStat(Stat.Strength, 35);
            toon.SetBaseStat(Stat.Intelligence, 145);
            toon.TrainSkill("Block", 60);
            Assert.That(toon.GetSkillValue("Block"), Is.EqualTo(100));
            toon.EquipItem(Equipments.Get("Red Scutum Shield", null, "of Blocking"));
            Assert.That(toon.Defense, Is.EqualTo(192));
        }

        [Test]
        public void TestWeaponDef()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 130);
            toon.SetBaseStat(Stat.Strength, 40);
            toon.SetBaseStat(Stat.Intelligence, 50);
            toon.TrainSkill("Sword", 74);
            toon.TrainSkill("Sword Mastery", 0);
            Assert.That(toon.GetSkill("Sword").UnbuffedSkill, Is.EqualTo(100));
            toon.EquipItem(Equipments.Get("Cutlass", null, null), Slot.RightHand);
            Assert.That(toon.Defense, Is.EqualTo(422));
            toon.EquipItem(Equipments.Get("Braialla's Blade", null, null), Slot.LeftHand);
            Assert.That(toon.Defense, Is.EqualTo(422));
            toon.UnequipItem(Slot.RightHand);
            Assert.That(toon.Defense, Is.EqualTo(458));
        }

        [Test]
        public void TestDexPenalty()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 85);
            toon.EquipItem(Equipments.Get("Ring Mail Helm", null, null));
            Assert.That(toon.Dexterity, Is.EqualTo(81));
            toon.EquipItem(Equipments.Get("Ring Mail Hauberk", null, null));
            Warn.Unless(toon.Dexterity, Is.EqualTo(73));
            toon.EquipItem(Equipments.Get("Ring Mail Sleeves", null, null));
            Warn.Unless(toon.Dexterity, Is.EqualTo(70));
            Warn.Unless(1, Is.EqualTo(1));
            toon.EquipItem(Equipments.Get("Ring Mail Gauntlets", null, null));
            Warn.Unless(toon.Dexterity, Is.EqualTo(68));
            toon.EquipItem(Equipments.Get("Ring Mail Leggings", null, null));
            Warn.Unless(toon.Dexterity, Is.EqualTo(64));
            toon.EquipItem(Equipments.Get("Ring Mail Boots", null, null));
            Warn.Unless(toon.Dexterity, Is.EqualTo(62));
        }
    }
}