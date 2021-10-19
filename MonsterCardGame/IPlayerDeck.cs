using MonsterCardGame.Classes;

namespace MonsterCradGame.Interfaces
{
    interface IPlayerDeck
    {
        void AddCardToDeck(MonsterCard NewCard);
        void RemoveCardFromDeckByName(string CardName);
    }
}
