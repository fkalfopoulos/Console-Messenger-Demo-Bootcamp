using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace demo
{
    public class ConsoleMenu
    {
        public static UserChoice GetUserChoice(List<string> Options, string Header = "\n")
        {
            if (Options.Count == 0)
            {
                Console.WriteLine("No Items to show");
                return new UserChoice() { IndexOfChoice = -1 };
            }

            int currentOption = 0; // Holds current option in the menu
            ConsoleKeyInfo keyPressed; // Struct ConsoleKeyInfo
            do
            {
                Console.Clear();
                Console.WriteLine(Header);

                for (int c = 0; c < Options.Count; c++)
                {
                    if (currentOption == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{c}: {Options[c]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{c}: {Options[c]}");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.ResetColor();
                keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    currentOption++;
                    if (currentOption > Options.Count - 1) //Go back to top
                    {
                        currentOption = 0;
                    }
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    currentOption--;
                    if (currentOption < 0) // Go back to bottom
                    {
                        currentOption = (Options.Count - 1);
                    }
                }
            } while (keyPressed.Key != ConsoleKey.Enter);
            Debug.WriteLine(currentOption);
            return new UserChoice
            {
                IndexOfChoice = currentOption,
                NameOfChoice = Options[currentOption]
            };
        }

        internal static object GetUserChoice(List<User> usersList, string deleteUsr)
        {
            throw new NotImplementedException();
        }
    }

    public struct UserChoice
    {
        public int IndexOfChoice;
        public string NameOfChoice;
    }
}