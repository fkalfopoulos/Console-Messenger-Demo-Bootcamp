using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    class UserMenu
    {

        public User LoggedIn;
        public DatabaseAccess DB;
        public DatabaseAccessMessages DM;
        public MenuManager MM;

        public UserMenu(User LoggedInUser)
        {
            LoggedIn = LoggedInUser;
            DB = new DatabaseAccess();
            DM = new DatabaseAccessMessages();
            MM = new MenuManager(LoggedIn);
        }

        public void ManageUserMenu()
        {
            while (true)
            {
                List<string> UserMenuOptions = new List<string>
                {
                "View my Profile",
                "Edit  my username and password",
                "Send Messages",
                "View Messages",
                "Delete Messages",
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
                         UpdatemyInfo();
                        break;
                    case 2:
                        Console.Clear();
                        MessageSend(LoggedIn);
                        break;
                    case 3:
                        MM.ChooseSentOrReceived();
                        break;
                    case 4:
                        DM.DeleteMessage();
                        break;
                    case 5:
                        return;
                    case 6:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public void UpdatemyInfo()
        {

            List<string> UpdatePersonalMenuOptions = new List<string>
            {
                "Edit Username",
                "Edit Password"
            };
            int option = ConsoleMenu.GetUserChoice(UpdatePersonalMenuOptions, DesignedStrings.UpdateUserMenu).IndexOfChoice;

            using (var context = new IMEntities())
            {


              //  User LoggedIn = context.Users.Single(u => u.Username == newname && u.Password == newpass);


                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("\n\tnew username: ");
                        string newusername = Console.ReadLine();
                        LoggedIn.Username = newusername;
                        context.SaveChanges();
                        break;
                    case 1:
                        Console.WriteLine("\n\tnew Password: ");
                        string newPassword = Console.ReadLine();
                        LoggedIn.Password = newPassword;
                        context.SaveChanges();
                        break;
                }
            }
        }









        private void MessageSend(User loggedIn)
        {

        }

        private void ChooseSentOrReceived()
        {

        }



    }
}
