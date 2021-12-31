﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MonsterCardGame
{
    class ConnectionForm
    {
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;

        private NpgsqlConnection connection = Connector.EstablishCon();
        public bool LoginForm()
        {
            Console.Clear();
            Console.WriteLine("Username: ");
            string Username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string PW = GetPassword();
            //curl - X POST http://localhost:10001/users --header "Content-Type: application/json" -d "{\"Username\":\"kienboec\", \"Password\":\"daniel\"}"
            connection.Open();
            string sql = $"SELECT username, password, userid, coins, elo FROM users WHERE username='{Username}'";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            string DBUser = null;
            string DBPW = null;
            while (dataReader.Read())
            {
                DBUser = (string)dataReader.GetValue(0);
                DBPW = (string)dataReader.GetValue(1);
                User.UserID = (int)dataReader.GetValue(2);
            }
            connection.Close();
            if (Username == DBUser && PW == DBPW)
            {
                Console.WriteLine($"{Username} successfully logged-in");
                
                return true;
            }

            Console.WriteLine("Login failed. Check login credentials and try agian.");
            Console.ReadKey();
            User.UserID = -1;
            return false;
        }
        public void RegistrationForm()
        {
            string UserInput = "";
            string Password = "";
            string PasswordRepeat;
            string Username = "";
            string sql;
            bool register = false;

            while(UserInput != "quit")
            {
                Console.Clear();
                Console.WriteLine("Username: ");
                Username = Console.ReadLine();
                Console.WriteLine($"\nIs {Username} correct, press Enter? \nCancel with any other Key!");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    UserInput = "quit";
                }
                else
                {
                    UserInput = "";
                }
            }

            while (UserInput != "exit")
            {
                Console.Clear();
                Console.Write("Enter Password: ");
                Password = GetPassword();
                Console.Write("\nRepeat Password: ");
                PasswordRepeat = GetPassword();
                if (PasswordRepeat == Password)
                {
                    Console.WriteLine($"You will be registered as {Username}.\n Accept [Enter]\n Cancel [any Key]");
                    if(Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        register = true;
                        UserInput = "exit";
                    }
                }
                else
                {
                    Console.WriteLine("Password didn't match the repeated Password. Try again.");
                    UserInput = "";
                }
            }
            if(register)
            {
                string token = createRandToken();
                connection.Open();
                sql = $"INSERT INTO users (userid,password,username) values ('{token}','{Password}','{Username}');";
                command = new NpgsqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                Console.WriteLine("Regisration request send.");
                if (dataReader.RecordsAffected > 0)
                {
                    Console.WriteLine("Regisration successfull.");
                }
            }
            else
            {
                Console.WriteLine("Registration has been canceled!");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            connection.Close();
        }
        private static string GetPassword()
        {
            string pwd = "";
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if(pwd.Length-1 > 0)
                    {
                        pwd.Remove(pwd.Length - 2, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    pwd += i.KeyChar;
                    Console.Write("*");
                }
            }
            return pwd;
        }
        private string createRandToken()
        {
            Random rnd = new Random();
            string newToken = "";
            for (int i = 0; i < 5; i++)
            {
                newToken += rnd.Next(10).ToString();
            }
            return newToken;
        }
    }
}
