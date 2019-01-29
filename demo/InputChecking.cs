using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace demo
{
   public  class InputChecking
                   
    {
        public string InputSubject()
        {
            while(true)
            {
                string Subject = Console.ReadLine();
                if(!CorrectSubjectLength(Subject))
                {
                    Console.WriteLine("Enter a proper Subject:");
                }
                else
                {
                    return Subject;
                }
            }
        }

        public string InputMessage()
        {
            while(true)
            {              
                string Data = Console.ReadLine();                          

                 if (!CorrectMessageLength(Data))
                {
                    Console.WriteLine("Your body length must be between 5 and 250 characters\n Please enter again:");
                }
                else
                {
                    return Data;
                }
            }
        }       

        public string InputPassword()
        {
            while (true)
            {
                string Password = HidePassword();
                if (!IsCorrectLength(Password))
                {
                    Console.WriteLine("Password must be between 4-20 characters");
                }
                else if (!PasswordHasNumbers(Password))
                {
                    Console.WriteLine("Password must contain Numbers");
                }
                else
                {
                    return EncryptPassword(Password);
                }
            }
        }

        internal string EncryptPassword(string Pass)
        {
            // Convert string to byte array for encryption
            byte[] crypto = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(Pass));
            // Convert encrypted bytes of Hash back to string
            return Encoding.Default.GetString(crypto);
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


        public string UsernameInput()
        {
            while (true)
            {
                string username = Console.ReadLine();

                if (!CorrectUsernameLength(username))
                {
                    Console.WriteLine("Username must be between 5-20 Characters \n Please enter again");
                }
                else
                {
                    return username;
                }
            }
        }

        public bool IsCorrectLength(string pass)
        {
            return pass.Length > 4 && pass.Length < 20;
        }

        public bool IsUsernameCorrect(string username)

        {
            return username.Length > 4 && username.Length < 20;
        }

        public  bool PasswordHasNumbers(string password)
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
       
        public bool CorrectUsernameLength(string username)
        {
            if (username.Length < 4 || username.Length > 12)
            {
                return false;
            }
            return true;
        }

        public bool CorrectSubjectLength(string subject)
        {
            return subject.Length > 2;
        }

        public bool CorrectMessageLength(string body)
        {
            return body.Length > 5 && body.Length < 250;
        }

    }   
}
