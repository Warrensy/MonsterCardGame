using System;
using System.Data;
using Npgsql;

namespace MonsterCardGame
{
    class Connector
    {
        public static NpgsqlConnection EstablishCon()
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=9080;Database=monstercardgame";
            var connection = new NpgsqlConnection(connectionString);
            return connection;
        }
    }
}
