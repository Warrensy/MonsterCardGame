using System;
using System.Data;
using Npgsql;

namespace MonsterCardGame
{
    class Connector
    {
        public  static NpgsqlConnection EstablishCon()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=9080;Database=monstercardgame";
            NpgsqlConnection connection;
            connection = new NpgsqlConnection(connectionString);
            Console.WriteLine("\nConnection prepared.");
            return connection;
        }
    }
}
//NpgsqlCommand command;
//NpgsqlDataReader dataReader;
//string sql = "SELECT cardid FROM stack";
//string Output = "";
//command = new NpgsqlCommand(sql, connection);
//dataReader = command.ExecuteReader();
//while (dataReader.Read())
//{
//    Output += Output + dataReader.GetValue(0) + "\n";
//}
//Console.WriteLine(Output);