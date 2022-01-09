using System;
using System.Data;
using Npgsql;

namespace MonsterCardGame
{
    class Connector
    {
        public static NpgsqlConnection EstablishCon()
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=9080;Database=test";
            var connection = new NpgsqlConnection(connectionString);
            return connection;
        }
    }
}/*
INSERT INTO cards VALUES 
(Fire Dragon,1,0,3,6,7)
(Shining Knight, 2, 1, 5, 9, 2)
(Goblin, 2, 1, 0, 2, 4)
(Kraken, 0, 2, 6, 10, 7)
(Fire Elves, 1, 0, 7, 10, 4)
(Ork, 2, 1, 4, 1, 4)
(Water Spell, 0, 2, 2, 5, 8)
(Fire Spell, 1, 0, 2, 5, 3)
(Normal Spell, 2, 1, 2, 5, 3)
(Fire Wizzard, 1, 0, 1, 10, 8)
(Tsunami, 0, 2, 2, 5, 6)
(Firestorm, 1, 0, 2, 5, 7)
(Sky Bolder, 2, 1, 2, 5, 7)
*/