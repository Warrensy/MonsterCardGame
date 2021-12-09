using System;
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
            connection.Open();
            string sql = $"SELECT username, password, userid, coins, elo FROM users WHERE username='{Username}'";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            string DBUser = "";
            string DBPW = "";
            while (dataReader.Read())
            {
                DBUser = (string)dataReader.GetValue(0);
                DBPW = (string)dataReader.GetValue(1);
                User.UserID = (int)dataReader.GetValue(2);
                User.Coins = (int)dataReader.GetValue(3);
                User.ELO = (int)dataReader.GetValue(4);
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
            User.Coins = -1;
            User.ELO = -1;
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
                Console.WriteLine("Enter Password: ");
                Password = GetPassword();
                Console.WriteLine("Repeat Password: ");
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
                connection.Open();
                sql = $"INSERT INTO users (password,username) values ('{Password}','{Username}');";
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
    }
}
