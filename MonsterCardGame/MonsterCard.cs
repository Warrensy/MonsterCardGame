using System;

namespace MonsterCardGame
{
    class MonsterCard : Card
    {
        public MonsterCard(int dmg, string Name, MonsterType Type, ElementType Element, int ID, MonsterType Weakness, ElementType ElementWeakness)
        {
            _dmg = dmg;
            _CardName = Name;
            _Element = Element;
            _Type = Type;
            _CardID = ID;
            _Weakness = Weakness;
            _ElementWeakness = ElementWeakness;
            _Nerf = false;
            _Boost = false;
        }
        private int factor = 2;
        public int Attack()
        {
            if(_Boost)
            {
                _Boost = false;
                return _dmg * factor;
            }
            if(_Nerf)
            {
                _Nerf = false;
                return _dmg / factor;
            }
            return _dmg;
        }
        public override void CardEffect()
        {
            throw new System.NotImplementedException();
        }

        public override void PrintStats()
        {
            Console.WriteLine($"Name: {_CardName}\nDamage: {_dmg}\nCard Type: {_Type}\nElement: {_Element}");
        }
    }
}
