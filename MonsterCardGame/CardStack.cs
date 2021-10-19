using System.Collections;
using System.Collections.Generic;
using MonsterCardGame.Classes;
using MonsterCardGame.Classes;

namespace MonsterCardGame.Interfaces
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
