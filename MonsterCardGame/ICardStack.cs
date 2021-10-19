using MonsterCardGame.Classes;

namespace MonsterCardGame.Interfaces
{
    interface ICardStack
    {
        void AddMonsterCardToStack(MonsterCard NewCard);
        void RemoveCardFromStackByName(string CardName);
    }
}
