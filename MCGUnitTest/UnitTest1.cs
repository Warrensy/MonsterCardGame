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
        public void Test_createRandToken_output()
        {
            //ARRANGE
            var con = new ConnectionForm();

            //ACT
            var testToken = con.createRandToken();

            //ASSERT
            Assert.AreEqual("1", testToken);
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
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            battle.Fight(newCard1, newCard2);

            //ASSERT

        }
    }
}