
namespace MonsterCardGame
{
    interface ICardStack
    {
        void AddMonsterCardToStack(MonsterCard NewCard);
        void RemoveCardFromStackByName(string CardName);
    }
}
