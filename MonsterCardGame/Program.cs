using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Npgsql;


namespace MonsterCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection.Close();
            //HttpServer newServer = new HttpServer(10001);
            //newServer.Run();
            Menu MainMenu = new Menu();
            MainMenu.Start();
            Console.WriteLine("Press any key to close application");
            Console.ReadKey();
        }
    }
}

/* Load Cards into DB.
 
   User Player1 = new User();
   CardDB db = new CardDB(Player1);
   NpgsqlCommand command;
   NpgsqlDataReader dataReader;
   string sql = "";
   int Element = 0;
   int ElementWeakness = 0;
   int Type = 0;
   int Weakness = 0;
   foreach (MonsterCard item in Player1.PlayerCardCollection.CardsInStack)
   {

       NpgsqlConnection connection = Connector.EstablishCon();
       connection.Open();
       if (item._Element == Card.ElementType.Fire)
       {
           Element = 1;
           ElementWeakness = 0;
       }
       else if (item._Element == Card.ElementType.Water)
       {
           Element = 0;
           ElementWeakness = 2;
       }
       else if (item._Element == Card.ElementType.Normal)
       {
           Element = 2;
           ElementWeakness = 1;
       }
       if (item._Type == Card.MonsterType.Goblin)
       { Type = 0; }
       if (item._Type == Card.MonsterType.Wizzard)
       { Type = 1; }
       if (item._Type == Card.MonsterType.Spell)
       { Type = 2; }
       if (item._Type == Card.MonsterType.Dragon)
       { Type = 3; }
       if (item._Type == Card.MonsterType.Ork)
       { Type = 4; }
       if (item._Type == Card.MonsterType.Knight)
       { Type = 5; }
       if (item._Type == Card.MonsterType.Kraken)
       { Type = 6; }
       if (item._Type == Card.MonsterType.FireElves)
       { Type = 7; }
       if (item._Type == Card.MonsterType.FireSpell)
       { Type = 8; }
       if (item._Type == Card.MonsterType.WaterSpell)
       { Type = 9; }
       if (item._Type == Card.MonsterType.None)
       { Type = 10; }
       if (item._Type == Card.MonsterType.Spirit)
       { Type = 11; }
       if (item._Type == Card.MonsterType.Machine)
       { Type = 12; }

       if (item._Weakness == Card.MonsterType.Goblin)
       { Weakness = 0; }
       if (item._Weakness == Card.MonsterType.Wizzard)
       { Weakness = 1; }
       if (item._Weakness == Card.MonsterType.Dragon)
       { Weakness = 2; }
       if (item._Weakness == Card.MonsterType.Ork)
       { Weakness = 3; }
       if (item._Weakness == Card.MonsterType.Knight)
       { Weakness = 4; }
       if (item._Weakness == Card.MonsterType.Kraken)
       { Weakness = 5; }
       if (item._Weakness == Card.MonsterType.FireElves)
       { Weakness = 6; }
       if (item._Weakness == Card.MonsterType.Spell)
       { Weakness = 7; }
       if (item._Weakness == Card.MonsterType.FireSpell)
       { Weakness = 8; }
       if (item._Weakness == Card.MonsterType.WaterSpell)
       { Weakness = 9; }
       if (item._Weakness == Card.MonsterType.None)
       { Weakness = 10; }
       if (item._Weakness == Card.MonsterType.Spirit)
       { Type = 11; }
       if (item._Weakness == Card.MonsterType.Machine)
       { Type = 12; }
       if (item._Weakness == Card.MonsterType.NormalSpell)
       { Type = 13; }

       sql = $"INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('{item._CardName}','{Element}','{ElementWeakness}','{Type}','{Weakness}','{item._dmg}');";
       command = new NpgsqlCommand(sql, connection);
       dataReader = command.ExecuteReader();
       connection.Close();
   }*/