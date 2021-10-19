using MonsterCardGame.Interfaces;
using MonsterCradGame.Interfaces;
using MonsterCradGame.Classes;
using MonsterCardGame.Classes;
using MonsterCardGame;

namespace MonsterCradGame.Classes
{
    class User : IUser
    {
        const int CardPackPrice = 5;
        const int StartCoins = 20;
        public User()
        {
            _Coins = StartCoins;
            PlayerCardCollection = new CardStack();
            PlayerDeck = new Deck();
        }
        public uint _Coins { get; set; }
        public Deck PlayerDeck;
        public CardStack PlayerCardCollection;
        public void BuyPackage()
        {
            if (_Coins >= CardPackPrice)
            {
                //open Pack, Add 5 Cards to Stack
            }
        }

        /*public MonsterCard SelectCard()
        {
            MonsterCard newCard = RandomMonsterCard();
            return newCard;
        }*/

        public void TradeCard()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
    }
}
