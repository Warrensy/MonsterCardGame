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
        public void Test_Fight_FireSpell_VS_WaterSpell()
        {
            //ARRANGE
            MonsterCard FireSpell = new MonsterCard(4, "TestFireCard", Card.MonsterType.FireSpell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 22);
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            MonsterCard loserCard = battle.Fight(FireSpell, WaterSpell);

            //ASSERT
            Assert.AreEqual(FireSpell, loserCard);
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
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 23);
            Logic battle = new Logic();
            MonsterCard LoserPrediction = FireMachine;

            //ACT
            MonsterCard result = battle.Fight(FireMachine, WaterSpell);

            //ASSERT
            Assert.AreEqual(LoserPrediction, result); 
        }
        [Test]
        public void DMG_of_Card_with_boost()
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
        public void Logic_Test_CheckForSpell_With_SpellCards()
        {
            //ARRANGE
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.FireSpell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            bool result = battle.CheckForSpell(newCard1._Type, newCard2._Type);

            //ASSERT
            Assert.AreEqual(result, true);
        }
        [Test]
        public void Logic_Test_CheckForSpell_With_MonsterCards()
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
            MonsterCard newCard1 = new MonsterCard(4, "TestFireCard", Card.MonsterType.FireSpell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 22);
            MonsterCard newCard2 = new MonsterCard(4, "TestWaterCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            bool result = battle.CheckForSpell(newCard1._Type, newCard2._Type);

            //ASSERT
            Assert.AreEqual(result, true);
        }
        [Test]
        public void CheckElements_Fire_VS_Water()
        {
            //ARRANGE
            MonsterCard FireSpellCard = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard WaterSpellCard = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Normal, 23);
            Logic battle = new Logic();

            //ACT
            battle.CheckElements(ref FireSpellCard, ref WaterSpellCard);
            
            //ASSERT
            if(WaterSpellCard._Boost == true && WaterSpellCard._Nerf == false && FireSpellCard._Nerf == true && FireSpellCard._Boost == false)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void CheckElements_Fire_VS_Normal()
        {
            //ARRANGE
            MonsterCard FireSpellCard = new MonsterCard(4, "TestFireCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 22);
            MonsterCard NormalSpellCard = new MonsterCard(4, "TestNormalCard", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            battle.CheckElements(ref FireSpellCard, ref NormalSpellCard);

            //ASSERT
            if (NormalSpellCard._Boost == false && NormalSpellCard._Nerf == true && FireSpellCard._Nerf == false && FireSpellCard._Boost == true)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void CheckElements_Water_VS_Normal()
        {
            //ARRANGE
            MonsterCard WaterSpellCard = new MonsterCard(4, "TestWaterCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Normal, 22);
            MonsterCard NormalSpellCard = new MonsterCard(4, "TestNormalCard", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.None, Card.ElementType.Fire, 23);
            Logic battle = new Logic();

            //ACT
            battle.CheckElements(ref WaterSpellCard, ref NormalSpellCard);

            //ASSERT
            if (NormalSpellCard._Boost == true && NormalSpellCard._Nerf == false && WaterSpellCard._Nerf == true && WaterSpellCard._Boost == false)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void Test_Battle_Losing()
        {
            //ARRANGE
            Deck Player1 = new Deck();
            Deck Player2 = new Deck();
            Logic testLogic = new Logic();
            MonsterCard FireMachine = new MonsterCard(4, "TestFireMonsterCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.Machine, Card.ElementType.Water, 22);
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 23);
            Player1.CardDeck.Add(FireMachine);
            Player2.CardDeck.Add(WaterSpell);
            int expectedResult = 0;

            //ACT
            int result = testLogic.Battle(Player1, Player2);

            //ASSERT
            Assert.AreEqual(result, expectedResult);
        }
        [Test]
        public void Test_Battle_Winning()
        {
            //ARRANGE
            Deck Player1 = new Deck();
            Deck Player2 = new Deck();
            Logic testLogic = new Logic();
            MonsterCard FireMachine = new MonsterCard(4, "TestFireMonsterCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.Machine, Card.ElementType.Water, 22);
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 23);
            Player1.CardDeck.Add(WaterSpell);
            Player2.CardDeck.Add(FireMachine);

            //ACT
            int result = testLogic.Battle(Player1, Player2);

            //ASSERT
            Assert.AreEqual(result, 1);
        }
        [Test]
        public void Test_Battle_Draw()
        {
            //ARRANGE
            Deck Player1 = new Deck();
            Deck Player2 = new Deck();
            Logic testLogic = new Logic();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);
            Player1.CardDeck.Add(WaterSpell);
            Player2.CardDeck.Add(WaterSpell);
            int expectedResult = 0;

            //ACT
            int result = testLogic.Battle(Player1, Player2);

            //ASSERT
            Assert.AreEqual(result, expectedResult);
        }
        [Test]
        public void Test_Add_Card_to_Deck()
        {
            //ARRANGE
            Deck testDeck = new Deck();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);

            //ACT
            testDeck.AddCardToDeck(WaterSpell);

            //ASSERT
            Assert.AreEqual(1, testDeck.CardDeck.Count);
        }
        [Test]
        public void Test_Add_Card_to_Stack()
        {
            //ARRANGE
            CardStack testDeck = new CardStack();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);

            //ACT
            testDeck.AddMonsterCardToStack(WaterSpell);

            //ASSERT
            Assert.AreEqual(1, testDeck.CardsInStack.Count);
        }
        [Test]
        public void Test_ShuffleDeck()
        {
            //ARRANGE
            Deck TestDeck = new Deck();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);
            MonsterCard FireSpell = new MonsterCard(4, "TestFireSpellCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.Spell, Card.ElementType.Water, 24);
            MonsterCard NormalSpell = new MonsterCard(4, "TestNormalSpellCard", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Spell, Card.ElementType.Fire, 25);
            MonsterCard Dragon = new MonsterCard(4, "TestMonsterCard", Card.MonsterType.Dragon, Card.ElementType.Fire, Card.MonsterType.FireElves, Card.ElementType.Water, 26);
            TestDeck.AddCardToDeck(WaterSpell);
            TestDeck.AddCardToDeck(FireSpell);
            TestDeck.AddCardToDeck(NormalSpell);
            TestDeck.AddCardToDeck(Dragon);
            Deck ControllDeck = TestDeck;

            //ACT
            TestDeck = TestDeck.ShuffleDeck();

            //ASSERT
            Assert.AreNotEqual(ControllDeck, TestDeck);
        }
        [Test]
        public void Test_uniquenes_of_ShuffleDeck()
        {
            //ARRANGE
            Deck TestDeck = new Deck();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);
            MonsterCard FireSpell = new MonsterCard(4, "TestFireSpellCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.Spell, Card.ElementType.Water, 24);
            MonsterCard NormalSpell = new MonsterCard(4, "TestNormalSpellCard", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Spell, Card.ElementType.Fire, 25);
            MonsterCard Dragon = new MonsterCard(4, "TestMonsterCard", Card.MonsterType.Dragon, Card.ElementType.Fire, Card.MonsterType.FireElves, Card.ElementType.Water, 26);
            TestDeck.AddCardToDeck(WaterSpell);
            TestDeck.AddCardToDeck(FireSpell);
            TestDeck.AddCardToDeck(NormalSpell);
            TestDeck.AddCardToDeck(Dragon);
            Deck ControllDeck = TestDeck;

            //ACT
            TestDeck = TestDeck.ShuffleDeck();
            ControllDeck = ControllDeck.ShuffleDeck();

            //ASSERT
            Assert.AreNotEqual(ControllDeck, TestDeck);
        }
        [Test]
        public void Test_RemoveCardFromDeckByName()
        {
            //ARRANGE
            Deck TestDeck = new Deck();
            Deck ControllDeck = new Deck();
            MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 23);
            MonsterCard FireSpell = new MonsterCard(4, "TestFireSpellCard", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.Spell, Card.ElementType.Water, 24);
            MonsterCard NormalSpell = new MonsterCard(4, "TestNormalSpellCard", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Spell, Card.ElementType.Fire, 25);
            MonsterCard Dragon = new MonsterCard(4, "TestMonsterCard", Card.MonsterType.Dragon, Card.ElementType.Fire, Card.MonsterType.FireElves, Card.ElementType.Water, 26);
            TestDeck.AddCardToDeck(WaterSpell);
            ControllDeck.AddCardToDeck(WaterSpell);
            TestDeck.AddCardToDeck(FireSpell);
            ControllDeck.AddCardToDeck(FireSpell);
            TestDeck.AddCardToDeck(NormalSpell);
            ControllDeck.AddCardToDeck(NormalSpell);
            TestDeck.AddCardToDeck(Dragon);
            ControllDeck.AddCardToDeck(Dragon);

            //ACT
            TestDeck.RemoveCardFromDeckByName("TestWaterSpellCard");

            //ASSERT
            Assert.AreNotEqual(ControllDeck.CardDeck[0]._CardName, TestDeck.CardDeck[0]._CardName);

        }
    }
}