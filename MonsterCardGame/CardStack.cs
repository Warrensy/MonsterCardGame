using System;
using System.Collections;
using System.Collections.Generic;

namespace MonsterCardGame
{
    class CardStack : ICardStack
    {
        public CardStack()
        {

        }
        public List<MonsterCard> CardsInStack = new List<MonsterCard>();
        public void AddMonsterCardToStack(MonsterCard NewCard)
        {
            CardsInStack.Add(NewCard);
        }
        public void RemoveCardByIndex(int index)
        {
           CardsInStack.RemoveAt(index);
        }
        public void PrintStack()
        {
            int count = 0;
            Console.WriteLine("  -Cards in Stack-\n\n ");
            foreach (var MonsterCard in CardsInStack)
            {
                count++;
                Console.WriteLine($"                                                [{count}]");
                Console.WriteLine($" Name: {MonsterCard._CardName}\n DMG: {MonsterCard._dmg} Type: {MonsterCard._Type}\n Element: {MonsterCard._Element} Weakness: {MonsterCard._Weakness}");
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        public void RemoveCardFromStackByName(string CardName)
        {
            for (int i = CardsInStack.Capacity - 1; i >= 0; i--)
            {
                if (CardsInStack[i]._CardName == CardName)
                {
                    CardsInStack.Remove(CardsInStack[i]);
                    break;
                }
            }
        }
    }
}
