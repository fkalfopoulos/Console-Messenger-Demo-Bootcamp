using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class MenuManager
    {
        public User LoggedIn;
        public DatabaseAccess DB;
        public DatabaseAccessMessages DM;
        public MenuManager MM;

        public MenuManager(User LoggedInUser)
        {
            LoggedIn = LoggedInUser;
            DB = new DatabaseAccess();
            DM = new DatabaseAccessMessages();

            ManagerMenu();
        }


        public void ManagerMenu()
        {
            while (true)
            {
                List<string> MainMenuOptions = new List<string>
                {
                    "View user",
                    "Add  User",
                    "Delete User",
                    "Update user",
                    "Send Message",
                    "Delete Message",
                    "View Message",
                    "Assign Role",
                    "Log Out",
                    "Terminate the program"
                };

                int option = ConsoleMenu.GetUserChoice(MainMenuOptions, DesignedStrings.Welcome).IndexOfChoice;

                switch (option)
                {
                    case 0:
                        ViewAllUsers();
                        break;
                    case 1:
                        Console.WriteLine("\n\nGive Username");
                        string Username = Console.ReadLine();

                        Console.WriteLine("\n\nGive Password");
                        string Password = Console.ReadLine();

                        DB.AddUser(Username, Password);
                        break;
                    case 2:
                        Console.Clear();
                        DB.DeleteUser(LoggedIn.Id);
                        break;
                    case 3:
                        DB.Update();
                        break;
                    case 4:
                        MessageSend(LoggedIn);
                        break;
                    case 5:
                        DM.DeleteMessage();
                        break;
                    case 6:
                        ChooseSentOrReceived();
                        break;
                    case 7:
                        UserMenu M = new UserMenu(LoggedIn);
                        M.ManageUserMenu();
                        Console.WriteLine("SKATA");
                        Console.ReadKey();
                        break;
                    case 8:
                        return;
                    case 9:
                        Console.WriteLine("Thank you for choosing us for your communication needs.");
                        Console.WriteLine("The program will now terminate.");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void FindUserViaUserId()
        {
        }

        private static void GetAllUsers()
        {

        }

        public static List<User> GetUsers()
        {
            using (var context = new IMEntities())
            {
                var users = context.Users.ToList();
                return users;
            }
        }

        public void ViewAllUsers()
        {

            List<string> ViewMenuOptions = new List<string>
           {

                "View Usernames",
                "View Usernames & Date of Registration",
                "View Usernames & UserIDs",
                "View Usernames + Passwords"
            };

            int option = ConsoleMenu.GetUserChoice(ViewMenuOptions, DesignedStrings.ViewUsr).IndexOfChoice;

            switch (option)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("These are the users in the database ");

                    List<User> users = GetUsers();
                    foreach (User user in users)
                    {
                        Console.WriteLine($"User : {user.Username} ");
                    }
                    Console.WriteLine("Press any key to return to the main menu");
                    Console.ReadKey();
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine("These are the users in the database ");

                    List<User> usersdate = GetUsers();
                    foreach (User user in usersdate)
                    {
                        Console.WriteLine($"User :  {user.Username} + \n\n Date : {user.RegisterDate}");
                    }
                    Console.WriteLine("Press any key to return to the main menu");
                    Console.ReadKey();

                    break;

                case 2:

                    Console.Clear();
                    Console.WriteLine("These are the usernames + user IDs in the database ");

                    List<User> usersnames = GetUsers();
                    foreach (User user in usersnames)
                    {
                        Console.WriteLine($"User : {user.Username} +  \n\n ID : {user.Id}");
                    }
                    Console.WriteLine("Press any key to return to the main menu");
                    Console.ReadKey();

                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("These are the usernames + user passwords in the database ");

                    List<User> userspass = GetUsers();
                    foreach (User user in userspass)
                    {
                        Console.WriteLine($"User : {user.Username} + \n\n Password :  {user.Password}");
                    }
                    Console.WriteLine("Press any key to return to the main menu");
                    Console.ReadKey();
                    break;

            }
        }




        public static void MessageSend(User Sender)
        {
            User Reciever;
            do
            {
                Console.Clear();
                Console.WriteLine("Select a user to write your message :");
                string ReceiverName = Console.ReadLine();
                Reciever = DatabaseAccess.FindUserViaUsername(ReceiverName);
            } while (Reciever is null);

            Console.WriteLine(" Please write the subject of your Message :");
            string Subject = Console.ReadLine();

            using (var context = new IMEntities())
            {
                Console.WriteLine("What you wanna send");

                context.Messages.Add(new Message
                {
                    SenderId = Sender.Id,
                    RecieverId = Reciever.Id,
                    Subject = Subject,
                    Data = Console.ReadLine(),
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }

            Console.WriteLine("Message sent! Press any key to continue");
            Console.ReadKey();
        }

        public static User FindUserViaUserId(int id)
        {
            User result = null;

            using (var context = new IMEntities())
            {
                result = context.Users.Find(id);
            }

            return result;
        }

        public static User FindUserViaUsername(string username)
        {
            User result = null;

            using (var context = new IMEntities())
            {
                result = context.Users.Where(x => x.Username == username).SingleOrDefault();
            }

            return result;
        }

        public bool DoesUsernameExist(string username)
        {
            bool itExists = false;
            using (var context = new IMEntities())
            {
                if (context.Users.Any(x => x.Username == username))
                {
                    itExists = true;
                }
            }
            return itExists;
        }

        public void ShowMessages(bool Sent)
        {
            List<Message> Messages = DB.GetUserMessages(LoggedIn, IsUserSender: Sent).ToList();
            UserChoice Choice = ConsoleMenu.GetUserChoice(Messages.Select(msg => msg.Subject).ToList(), "Choose the message you wanna see");

            if (Choice.IndexOfChoice == -1)
            {
                return;
            }
            Message SelectedMessage = Messages[Choice.IndexOfChoice];

            Console.WriteLine(SelectedMessage);


            Console.WriteLine("You want to edit the message ");

            List<string> YesorNoOptions = new List<string>
           {

                "Yes",
                "No"
            };

            int option = ConsoleMenu.GetUserChoice(YesorNoOptions, DesignedStrings.DeleteMsg).IndexOfChoice;

            switch (option)
            {
                case 0:
                    DM.UpdateMessage(Updatedmessage: SelectedMessage);
                    break;

                case 1:
                    return;


            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void ChooseSentOrReceived()
        {
            ShowMessages(ConsoleMenu.GetUserChoice(new List<string> { "Sent", "Received" }).IndexOfChoice == 0);
        }


        public void AssignRole()
        {


        }


    }

}
