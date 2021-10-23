
namespace MonsterCardGame
{
    interface IPlayerDeck
    {
        void AddCardToDeck(MonsterCard NewCard);
        void RemoveCardFromDeckByName(string CardName);
    }
}
