using MonsterCardGame.Classes;

namespace MonsterCardGame.Interfaces
{
    interface IPlayerDeck
    {
        void AddCardToDeck(MonsterCard NewCard);
        void RemoveCardFromDeckByName(string CardName);
    }
}
