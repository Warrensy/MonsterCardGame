using System;

namespace MonsterCardGame
{
    public class MonsterCard : Card
    {
        public MonsterCard(int dmg, string Name, MonsterType Type, ElementType Element, MonsterType Weakness, ElementType ElementWeakness, int ID)
        {
            _dmg = dmg;
            _CardName = Name;
            _Element = Element;
            _Type = Type;
            _Weakness = Weakness;
            _ElementWeakness = ElementWeakness;
            _Nerf = false;
            _Boost = false;
            _CardID = ID;
        }
        //variable used to double or half the dmg output of a card
        private int factor = 2;
        public int Attack()
        {
            //No need to reset _Boost and _Nerf. Will be evaluated and set every time befor a card attacks by Logic.CheckElements function
            if (_Boost)
            {
                return _dmg * factor;
            }
            if(_Nerf)
            {
                return _dmg / factor;
            }
            return _dmg;
        }

        public override void PrintStats()
        {
            Console.WriteLine($"Name: {_CardName}\nDamage: {_dmg}\nCard Type: {_Type}\nElement: {_Element}");
        }
    }
}
