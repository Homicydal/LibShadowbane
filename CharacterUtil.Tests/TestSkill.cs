using System;
using System.Collections.Generic;
using System.Text;
using LibShadowbane.CharacterUtil;

namespace LibShadowbane.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class TestSkill
    {
        [SetUp]
        protected void SetUp() { }

        [Test]
        public void TestBase()
        {
            Character toon = new Character();
            toon.SetBaseStat(Stat.Dexterity, 40);
            toon.SetBaseStat(Stat.Strength, 45);
            toon.SetBaseStat(Stat.Intelligence, 50);

            toon.TrainSkill("Axe", 0);
            Assert.That(toon.GetSkill("Axe").UntrainedSkill, Is.EqualTo(16));
            
            toon.TrainSkill("Bargaining", 0);
            Assert.That(toon.GetSkill("Bargaining").UntrainedSkill, Is.EqualTo(17));
            
            toon.TrainSkill("Wear Armor, Light", 0);
            Assert.That(toon.GetSkill("Wear Armor, Light").UntrainedSkill, Is.EqualTo(16));
            
            toon.SetBaseStat(Stat.Intelligence, 60);

            Assert.That(toon.GetSkill("Axe").UntrainedSkill, Is.EqualTo(18));
            Assert.That(toon.GetSkill("Bargaining").UntrainedSkill, Is.EqualTo(19));
            Assert.That(toon.GetSkill("Wear Armor, Light").UntrainedSkill, Is.EqualTo(17));

            toon.TrainSkill("Running", 0);
            Assert.That(toon.GetSkill("Running").UntrainedSkill, Is.EqualTo(0));
        }

        [Test]
        public void TestBuffed(){
            Character toon2 = new Character();
            toon2.SetBaseStat(Stat.Dexterity, 25);
            toon2.SetBaseStat(Stat.Intelligence, 145);
            toon2.TrainSkill("Wear Armor, Light", 61);
            Assert.That(toon2.GetSkill("Wear Armor, Light").UntrainedSkill, Is.EqualTo(29));
            Assert.That(toon2.GetSkill("Wear Armor, Light").UnbuffedSkill, Is.EqualTo(100));
            toon2.AddEffect(Effects.Get("Blessing of Dexterity", 35));
            toon2.AddEffect(Effects.Get("Charm of Illumination (Intelligence)", 35));
            Assert.That(toon2.GetSkill("Wear Armor, Light").BuffedSkill, Is.EqualTo(117m));
        }
    }
}