using System;
using System.Collections;
using System.Collections.Generic;

namespace MonsterCardGame
{
    public class CardStack : ICardStack
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
        public void PrintStack(int selected)
        {
            int count = 0;
            Console.WriteLine("  -Cards in Stack-\n\n ");
            foreach (var MonsterCard in CardsInStack)
            {
                if (count == selected)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                count++;
                Console.WriteLine($"                                                [{count}]");
                Console.WriteLine($" Name: {MonsterCard._CardName}\n DMG: {MonsterCard._dmg} Type: {MonsterCard._Type}\n Element: {MonsterCard._Element} Weakness: {MonsterCard._Weakness}");
                Console.WriteLine("---------------------------------------------------------");
                Console.ResetColor();
            }
        }

        public void RemoveCardFromStackByName(string CardName)
        {
            for (int i = CardsInStack.Count - 1; i >= 0; i--)
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
