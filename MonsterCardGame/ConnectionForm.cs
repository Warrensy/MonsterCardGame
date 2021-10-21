using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class ConnectionForm
    {
        public bool LoginForm()
        {
            Console.Clear();
            Console.WriteLine("Username: ");
            string Username = Console.ReadLine();
            Console.WriteLine("Password: ");
            SecureString PW = GetPassword();
            if(Username == "Warrensy")
            {
                Console.WriteLine($"{Username} successfully logged-in");
                return true;
            }
            Console.WriteLine("Login failed. Check login credentials and try agian.");
            Console.ReadKey();
            return false;
        }
        public void RegistrationForm()
        {
            Console.WriteLine("Coming soon");
            Console.ReadKey();
        }
        private static SecureString GetPassword()
        {
            SecureString pwd = new SecureString();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if(pwd.Length > 0)
                    {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd;
        }
    }
}
