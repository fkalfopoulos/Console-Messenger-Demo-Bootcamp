using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class UserFunctions
    {
        public User LoggedIn;
        public DatabaseAccess DB;

        public UserFunctions(User ActiveUser)
        {
            LoggedIn = ActiveUser;
            DB = new DatabaseAccess(LoggedIn);
        }

        public void AddUser(string username, string password, UserAccess Role = UserAccess.User)
        {
            using (var context = new IMEntities())
            {
                Console.Clear();
                Console.WriteLine(DesignedStrings.CreateUsr);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Creating new user profile.....Please wait...");

                context.Users.Add(new User
                {
                    Username = username,
                    Password = password,
                    Role = UserAccess.User,
                    RegisterDate = DateTime.Now,
                    IsUserActive = true
                });

                context.SaveChanges();

                Console.ReadLine();
                Console.WriteLine($"\nNew user profile with username '{username}' has been created!");
                Console.WriteLine($"\n Press any key to continue");
                Console.ReadKey();

                Console.ResetColor();
            }
        }

        public void DeleteUser(int id)
        {

            Console.WriteLine(DesignedStrings.DeleteUsr);
            List<User> UsersList = DB.GetAllUsers();
            UserChoice Choice = ConsoleMenu.GetUserChoice(UsersList.Select(usr => usr.Username).ToList(), "Choose the right User for you ");



            string usernameForDelete;

            //Console.WriteLine("Choose the username of the user you would like to delete:");
            usernameForDelete = Console.ReadLine();

            if (usernameForDelete is null)
            {
                return;
            }

            if (!DoesUsernameExist(usernameForDelete)) // Check if username already exists in database
            {
                Console.WriteLine("We are so sorry  but the username you entered does not exist.\nPlease choose another user to delete.");

                Console.ReadKey();
            }
            else
            {
                User ToBeDeleted = FindUserViaUsername(usernameForDelete);

                if (ToBeDeleted.Role == UserAccess.SuperAdministrator)
                {
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ReadKey();
                    return;
                }
                bool userActive = IsUserActive(usernameForDelete); 

                if (!userActive)
                {
                    Console.WriteLine($"\nThe user with username '{usernameForDelete}' is no longer active.");

                    Console.WriteLine("\n\nPress any key to go back to Menu.");

                    Console.ReadKey();
                }
                else
                {
                    using (var context = new IMEntities())
                    {
                        User ToDelete = context.Users.FirstOrDefault(usr => usr.Id == ToBeDeleted.Id);
                        context.Users.Remove(ToDelete);
                        context.SaveChanges();
                    }

                    Console.WriteLine($"\nUser '{usernameForDelete}' is no longer active .");
                    Console.WriteLine("\n\nPress any key to go back to  Menu.");

                    Console.ReadKey();
                }
            }
        }

        private User SelectUser()
        {
            throw new NotImplementedException();
        }

        public void ViewProfile()
        {
            Console.Clear();
            Console.WriteLine(DesignedStrings.Viewmyprofile);
            Console.WriteLine("\n this is your profile");

            Console.WriteLine($"User { LoggedIn.Id} & \n  Password : { LoggedIn.Password} & \n {LoggedIn.RegisterDate}");
            Console.ReadKey();
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
            // MOVE TO class ViewUser.cs and break to more methods
            List<string> ViewMenuOptions = new List<string>
           {
                "View Usernames",
                "View Usernames & Date of Registration",
                "View Usernames & UserIDs",
                "View Usernames + Passwords",
                "View Usernames & Roles"
            };

            int option = ConsoleMenu.GetUserChoice(ViewMenuOptions, DesignedStrings.ViewUsr).IndexOfChoice;

            switch (option)
            {
                case 0:
                    ShowUsernames();
                    break;

                case 1:
                    ShowUsernameDate();
                    break;

                case 2:
                    ShowUserIds();
                    break;

                case 3:
                    ShowUserPass();
                    break;
                case 4:
                    ShowUserRole();
                    break;
            }
        }

        public void ShowUsernames()
        {
            Console.Clear();
            Console.WriteLine("These are the users in the database ");

            List<User> users = GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User : {user.Username} ");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
        }

        public void ShowUsernameDate()
        {
            Console.Clear();
            Console.WriteLine("These are the users in the database and their Date of Registration");

            List<User> usersdate = GetUsers();
            foreach (User user in usersdate)
            {
                Console.WriteLine($"User :  {user.Username} + \n Date : {user.RegisterDate}");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
        }

        public  void ShowUserIds()
        {
            Console.Clear();
            Console.WriteLine("These are the usernames + user IDs in the database ");

            List<User> usersnames = GetUsers();
            foreach (User user in usersnames)
            {
                Console.WriteLine($"User : {user.Username} +  \n\n ID : {user.Id}");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
        }

        public  void ShowUserPass()
        {
            Console.Clear();
            Console.WriteLine("These are the usernames + user Passwords in the database ");

            List<User> usersnames = GetUsers();
            foreach (User user in usersnames)
            {
                Console.WriteLine($"User : {user.Username} +  \n\n Password : {user.Password}");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();

        }

        public  void ShowUserRole()
        {
            Console.Clear();
            Console.WriteLine("These are the usernames + user Roles in the database ");

            List<User> usersnames = GetUsers();
            foreach (User user in usersnames)
            {
                Console.WriteLine($"User : {user.Username} +  \n\n Password : {user.Role}");
            }
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();

        }

        private bool DoesMessageExist(int messageid)
        {
            using (var context = new IMEntities())
            {
                return context.Messages.Any(m => m.MessageId == messageid);
            }

        }

        private void ChooseSentOrReceived()
        {

        }
        
        public static User FindUserViaUsername(string Username)
        {
            using (var context = new IMEntities())
            {
                return context.Users.SingleOrDefault(us => us.Username == Username);
            }
        }

        public static User FindUserViaUserId(int id)
        {
            using (var context = new IMEntities())
            {
                return context.Users.Find(id);

            }
        }

        public bool DoesUsernameExist(string username)
        {
            using (var context = new IMEntities())
            {
                return context.Users.Any(x => x.Username == username);
            }
        }

        public bool IsPasswordCorrect(string username, string password)
        {
            using (var context = new IMEntities())
            {
                return context.Users.Any(x => x.Username == username && x.Password == password);
            }
        }

        public bool IsUserActive(string username)
        {
            using (var context = new IMEntities())
            {
                return context.Users.SingleOrDefault(x => x.Username == username).IsUserActive;
            }
        }


    }
}
