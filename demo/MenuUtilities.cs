using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    class menuutilities
    {
        public string InputPassword()
        {
            while (true)
            {
                string Password = HidePassword();
                if (!IsCorrectLength(Password))
                {
                    Console.WriteLine("Input 5-20 characters");
                }
                else if (!PasswordHasNumbs(Password))
                {
                    Console.WriteLine("Password must contain Numbs");
                }
                else
                {
                    return Password;
                }
            }
        }

        public string HidePassword()
        {
            string pass = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, (pass.Length - 1));
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            return pass;
        }


        private bool IsCorrectLength(string pass)
        {
            return pass.Length > 5 && pass.Length < 20;
        }

        private bool IsUsernameCorrectLength(string input)
        {
            return input.Length > 5 && input.Length < 20;
        }

        private bool PasswordHasNumbs(string password)
        {
            foreach (var character in password)
            {
                if (char.IsNumber(character))
                {
                    return true;
                }

            }
            return false;
        }
    }



    

}
