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

        public MenuManager(User LoggedInUser)
        {
            LoggedIn = LoggedInUser;
            DB = new DatabaseAccess();
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
                        Console.WriteLine("Give Username");
                        string Username = Console.ReadLine();

                        Console.WriteLine("Give Password");
                        string Password = Console.ReadLine();

                        DB.AddUser(Username, Password);
                        break;
                    case 2:
                        Console.Clear();
                        DB.DeleteUser(LoggedIn.Id);
                        break;
                    case 3:
                        Console.Clear();
                        break;
                    case 4:
                        MessageSend(LoggedIn);
                        break;
                    case 5:
                        Console.WriteLine("Delete Madafaka");
                        break;
                    case 6:
                        ScrollInboxMenu();
                        break;
                    case 7:
                        return;
                    case 8:
                        Console.WriteLine("Thank you for choosing us for your communication needs.");
                        Console.WriteLine("The program will now terminate.");
                        Environment.Exit(0);
                        break;
                }
            }
        }


        private void ScrollInboxMenu()
        {
            ShowMessages(ConsoleMenu.GetUserChoice(new List<string> { "Received", "Sent" }, "What do you want to see?").IndexOfChoice == 1);
        }

        private void ShowMessages(bool Sent)
        {
            List<Message> Messages = DB.GetUserMessages(LoggedIn, IsUserSender: Sent).ToList();
            UserChoice Choice = ConsoleMenu.GetUserChoice(Messages.Select(msg => msg.Subject).ToList(), "Choose the message you wanna see");

            if(Choice.IndexOfChoice == -1)
            {
                return;
            }
            Message SelectedMessage = Messages[Choice.IndexOfChoice];

            Console.WriteLine(SelectedMessage);
            Console.ReadKey();
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
            Console.Clear();
            Console.WriteLine("These are the users in the database ");

            List<User> users = GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User[{user.Id} + {user.Username}");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            return;
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
    }
}
