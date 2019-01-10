using IMModel;
using System;
using System.Collections.Generic;

namespace demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //IMEntities context = new IMEntities();
            UserReception UF = new UserReception();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Choose an option");

                List<string> RegisterMenuOptions = new List<string>
                {
                    "Register",
                    "Login",
                    "Terminate the Program"
                };

                int option = ConsoleMenu.GetUserChoice(RegisterMenuOptions, DesignedStrings.DemoMessenger).IndexOfChoice;

                switch (option)
                {
                    case 0:
                        UF.RegisterUser();
                        break;
                    case 1:
                        UF.LoginUser();
                        break;
                    case 2:
                        Console.WriteLine("Thank you for choosing us for your communication needs.");
                        Console.WriteLine("The program will now terminate.");
                        return;
                }
            }
        }
    }
}