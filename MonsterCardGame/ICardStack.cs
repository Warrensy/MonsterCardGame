using MonsterCardGame.Classes;

namespace MonsterCradGame.Interfaces
{
    interface ICardStack
    {
        void AddMonsterCardToStack(MonsterCard NewCard);
        void RemoveCardFromStackByName(string CardName);
    }
}
