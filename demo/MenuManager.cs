using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class MenuManager
    {
        const string ASSIGN_ROLE = "Assign Role", VIEW_USERS = "View users", ADD_USER = "Add  User", DELETE_USER = "Delete User",
                 UPDATE_USER = "Update User", EDIT_MESSAGE = "Edit Message", VIEW_PROFILE = "View My Profile",
                 EDIT_USERNAME = "Edit My Username", EDIT_PASSWORD = "Edit My Password", SEND_MESSAGE = "Send Messages",
                 VIEW_MESSAGES = "View Messages", DELETE_MESSAGE = "Delete Messages", LOG_OUT = "Log Out", EXIT = "Terminate the program";

        public User LoggedIn;
        public DatabaseAccess DB;
        public InputChecking IC;
        public UserFunctions UF;


        public MenuManager(User LoggedInUser)
        {
            LoggedIn = LoggedInUser;
            DB = new DatabaseAccess(LoggedIn);
            UF = new UserFunctions(LoggedIn);
            IC = new InputChecking();
        }


        public void ManagerMenu()
        {
            while (true)
            {
                List<string> MainMenuOptions = new List<string>
                {
                    // "View users"
                    // "Add  User"
                    // "Delete User"
                    // "Update user"
                    // "Edit Message"
                    VIEW_PROFILE,//case 5
                    EDIT_USERNAME,
                    EDIT_PASSWORD,
                    SEND_MESSAGE,
                    VIEW_MESSAGES,//Case 9
                    DELETE_MESSAGE,
                    LOG_OUT,
                    EXIT
                };

                if (LoggedIn.Role == UserAccess.Moderator)
                {
                    MainMenuOptions.Insert(0, ASSIGN_ROLE);
                    // TODO
                }
                else if (LoggedIn.Role == UserAccess.SuperAdministrator)
                {
                    MainMenuOptions.Insert(0, VIEW_USERS);
                    MainMenuOptions.Insert(1, ADD_USER);
                    MainMenuOptions.Insert(2, DELETE_USER);
                    MainMenuOptions.Insert(3, UPDATE_USER);
                    MainMenuOptions.Insert(4, EDIT_MESSAGE);
                }

                string option = ConsoleMenu.GetUserChoice(MainMenuOptions, DesignedStrings.DemoMessenger).NameOfChoice;

                switch (option)
                {
                    case VIEW_USERS:
                        UF.ViewAllUsers();
                        break;
                    case ADD_USER:
                        Console.WriteLine("\n\nGive Username");
                        string Username = Console.ReadLine();

                        Console.WriteLine("\n\nGive Password");
                        string Password = Console.ReadLine();

                        UF.AddUser(Username, Password);
                        break;
                    case DELETE_USER:
                        Console.Clear();
                        UF.DeleteUser(LoggedIn.Id);
                        break;
                    case UPDATE_USER:
                        DB.Update();
                        break;
                    case EDIT_MESSAGE:
                        EditMessage();
                        break;
                    case VIEW_PROFILE:
                        UF.ViewProfile();
                        break;
                    case EDIT_USERNAME:
                        DB.UpdateUsername(LoggedIn);
                        break;
                    case EDIT_PASSWORD:
                        DB.UpdatePassword(LoggedIn);
                        break;
                    case VIEW_MESSAGES:
                        ChooseSentOrReceived();
                        break;
                    case SEND_MESSAGE:
                        MessageSend(LoggedIn);
                        break;
                    case DELETE_MESSAGE:
                        DB.DeleteMessage(this);
                        break;
                    case LOG_OUT:
                        return;
                    case EXIT:
                        Console.WriteLine("Thank you for choosing us for your communication needs.");
                        Console.WriteLine("The program will now terminate.");
                        Environment.Exit(0);
                        break;
                }
            }
        }


        public void MessageSend(User Sender)
        {
            User Receiver ;
            do
            {
                Receiver = SelectUser();                
            }
            while (Receiver is null);

            Console.WriteLine(DesignedStrings.Messagestring + "\nPlease write the subject of your Message :");
            string Subject = IC.InputSubject();

            using (var context = new IMEntities())
            {
                Console.WriteLine("What you wanna send");

                context.Messages.Add(new Message
                {
                    SenderId = Sender.Id,
                    RecieverId = Receiver.Id,
                    Subject = Subject,
                    Data = IC.InputMessage(),
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }

            Console.WriteLine("Message sent! Press any key to continue");
            Console.ReadLine();
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


        public  User FindUserViaUsername(string username)
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

        public User SelectUser()
        {
            List<User> UsersList = DB.GetAllUsers();
            UserChoice Choice = ConsoleMenu.GetUserChoice(UsersList.Select(usr => usr.Username).ToList(), "Choose the right User for you ");

            if (Choice.IndexOfChoice == -1)
            {
                return null;
            }
            return UsersList[Choice.IndexOfChoice]; ;
        }

        public Message ShowMessages(bool Sent)
        {
            List<Message> Messages = DB.GetUserMessages(LoggedIn, IsUserSender: Sent).ToList();
            UserChoice Choice = ConsoleMenu.GetUserChoice(Messages.Select(msg => msg.Subject).ToList(), "Choose the message you wanna see");

            if (Choice.IndexOfChoice == -1)
            {
                return null;
            }
            Message SelectedMessage = Messages[Choice.IndexOfChoice];

            Console.WriteLine(SelectedMessage);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return SelectedMessage;
        }

        public Message ChooseSentOrReceived()
        {
            return ShowMessages(ConsoleMenu.GetUserChoice(new List<string> { "Sent", "Received" }).IndexOfChoice == 0);
        }

        public void EditMessage()
        {
            Message SelectedMessage = ChooseSentOrReceived();

            if(SelectedMessage is null)
            {
                Console.WriteLine("There are NO MESSAGES HERE!\nOK");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(SelectedMessage);
            Console.WriteLine("You want to edit the message?");

            List<string> YesorNoOptions = new List<string>
           {
                "Yes",
                "No"
            };

            int option = ConsoleMenu.GetUserChoice(YesorNoOptions, DesignedStrings.Messagestring).IndexOfChoice;

            switch (option)
            {
                case 0:
                    DB.UpdateMessage(Updatedmessage: SelectedMessage);
                    break;
                case 1:
                    return;
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
