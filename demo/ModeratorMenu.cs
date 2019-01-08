using System;
using System.Collections.Generic;

namespace demo
{
    class ModeratorMenu
    {
        public User LoggedIn;
        public DatabaseAccess DB;
        public DatabaseAccessMessages DM;

        public ModeratorMenu()
        {
            DB = new DatabaseAccess();
            DM = new DatabaseAccessMessages();
        }

        public void MenuForModerator()
        {
            while (true)
            {
                List<string> UserMenuOptions = new List<string>
            {
                "View my Profile",
                "Edit  my username and password",
                "Send Messages",
                "View and delete Messages",
                "Log Out",
                "Terminate the program"
            };

                int option = ConsoleMenu.GetUserChoice(UserMenuOptions, DesignedStrings.UsrMenu).IndexOfChoice;

                switch (option)
                {
                    case 0:
                        Console.WriteLine($"User { LoggedIn.Id} & \n  Password : { LoggedIn.Password} & \n {LoggedIn.RegisterDate}");
                        break;
                    case 1:
                        // UpdatemyInfo(LoggedIn);
                        break;
                    case 2:
                        Console.Clear();
                        //DB.MessageSend(LoggedIn);
                        break;
                    case 3:
                        //DB.ChooseSentOrReceived();
                        DM.DeleteMessage();
                        break;
                    case 4:

                        break;
                    case 5:
                        return;
                    case 6:
                        Environment.Exit(0);
                        break;
                }

            }



        }








    }
}
