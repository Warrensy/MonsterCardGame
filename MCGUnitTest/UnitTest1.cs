using NUnit.Framework;
using MonsterCardGame;
using System.IO;
using System;

namespace MCGUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            int x = 3;
            Assert.AreEqual(3, x);
        }
      
        [Test]
        public void GetPassword_Test()
        {
            //ARRANGE
            var stringReader = new StringReader("12345");
            Console.SetIn(stringReader);

            //ACT
            var testpassword = ConnectionForm.Testinput();

            //ASSERT
            Assert.AreEqual("12345", testpassword);
        }
        [Test]
        public void Test_Fight_FireSpell_VS_WaterSpell()
        {
            //ARRANGE
            MonsterCard FireSpell = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            MonsterCard loserCard = battle.Fight(FireSpell, WaterSpell);

            //ASSERT
            Assert.AreEqual(loserCard, FireSpell);
        }
        [Test]
        public void Test_Fight_Monster_VS_Monster()
        {
            //ARRANGE
            MonsterCard FireMachine = new MonsterCard(4, "TestFireCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard WaterDragon = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Dragon, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();
            MonsterCard LoserPrediction = null;

            //ACT
            MonsterCard result = battle.Fight(FireMachine, WaterDragon);

            //ASSERT
            Assert.AreEqual(LoserPrediction, result);
        }
        [Test]
        public void Test_Fight_FireMonsterCard_VS_WaterSpellCard()
        {
            //ARRANGE
            MonsterCard FireMachine = new MonsterCard(4, "TestFireMonsterCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.Machine, Card.ElementType.Water, 22);
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Fire, 23);
            Logic battle = new Logic();
            MonsterCard LoserPrediction = WaterSpell;

            //ACT
            MonsterCard result = battle.Fight(FireMachine, WaterSpell);

            //ASSERT
            Assert.AreEqual(LoserPrediction, result); 
        }
        [Test]
        public void Attack_of_Card_with_boost()
        {
            //ARRANGE
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            Logic battle = new Logic();
            newCard1._Boost = true;

            //ACT
            int dmgWithBoost = newCard1.Attack();
            int dmgNormal = newCard1._dmg;

            //ASSERT
            Assert.AreNotEqual(dmgWithBoost, dmgNormal);
        }
        [Test]
        public void function()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }
        [Test]
        public void Logic_Test_CheckForSpell_With_SpellCards()
        {
            //ARRANGE
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            bool result = battle.CheckForSpell(newCard1._Type, newCard2._Type);

            //ASSERT
            Assert.AreEqual(result, true);
        }
        [Test]
        public void Logic_Test_CheckForSpell_With_MonsterCard()
        {
            //ARRANGE
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Dragon, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            bool result = battle.CheckForSpell(newCard1._Type, newCard2._Type);

            //ASSERT
            Assert.AreEqual(result, false);
        }
        [Test]
        public void Logic_Test_CheckForSpell_With_MonsterCard_and_SpellCard()
        {
            //ARRANGE
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Dragon, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            bool result = battle.CheckForSpell(newCard1._Type, newCard2._Type);

            //ASSERT
            Assert.AreEqual(result, true);
        }
        [Test]
        public void CheckElements()
        {
            //ARRANGE
            MonsterCard FireSpellCard = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard WaterSpellCard = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            battle.CheckElements(ref FireSpellCard, ref WaterSpellCard);
            
            //ASSERT
            if(WaterSpellCard._Boost == true && FireSpellCard._Nerf == false)
            {
                Assert.Pass();
            }
        }
    }
}