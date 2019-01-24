using IMModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace demo
{

    public class DatabaseAccess
    {
        public User LoggedIn;

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
                    Role = Role,
                    RegisterDate = DateTime.Now,
                    IsUserActive = true
                });

                context.SaveChanges();
                Console.WriteLine($"\nNew user profile with username '{username}' has been created!");
                Console.WriteLine($"\n Press any key to continue");
                Console.ReadKey();

                Console.ResetColor();
            }
        }

        public void DeleteUser(int id)
        {

            Console.WriteLine(DesignedStrings.DeleteUsr);

            ViewAllUsers();

            string usernameForDelete;

            Console.WriteLine("Choose the username of the user you would like to delete:");
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
                bool userActive = IsUserActive(usernameForDelete); // check if user is active

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


        public void Update()
        {
            using (var context = new IMEntities())
            {
                List<User> AllUsers = context.Users.ToList();
                UserChoice Choice = ConsoleMenu.GetUserChoice(AllUsers.Select(user => user.Username).ToList(), "Choose the user you wanna edit");

                User newuser = AllUsers[Choice.IndexOfChoice];

                List<string> UpdateMenuOptions = new List<string>
                {
                    "Edit Role",
                    "Edit Username",
                    "Edit Password"
                };

                int option = ConsoleMenu.GetUserChoice(UpdateMenuOptions, DesignedStrings.UpdateUserMenu).IndexOfChoice;
                switch (option)
                {
                    case 0:
                        Console.WriteLine("\n\n\n\n\tNew Role: ");
                        newuser.Role = (UserAccess)ConsoleMenu.GetUserChoice(new List<string> { "Super Admin", "Moderator", "User" }).IndexOfChoice;
                        Console.WriteLine($"New User Role is: {newuser.Role}");
                        break;
                    case 1:
                        Console.WriteLine("\n\n\n\n\tnew Username: ");
                        newuser.Username = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("\n\tnew Password: ");
                        newuser.Username = Console.ReadLine();
                        break;
                }
                context.SaveChanges();
                Console.ReadKey();
            }
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

        // Method to fetch information about user from database
        public void GetUserInfo(string username)
        {
            using (var context = new IMEntities())
            {
                Console.Clear();
                var user = context.Users.Where(x => x.Username == username).First();

                Console.WriteLine($"======= Information about {username}'s profile =======\n\n");

                if (user.IsUserActive)
                {
                    Console.WriteLine($"Status   : Active");
                }
                else
                {
                    Console.WriteLine($"Status   : Deleted");
                }
            }
        }

        public static List<User> GetUsers()
        {
            using (var context = new IMEntities())
            {
                return context.Users.ToList();
            }
        }

        public void ViewAllUsers()
        {
            List<User> users = GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User[{user.Id} + {user.Username}");
            }
        }





        public string InputUserName()
        {
            string username;

            ConsoleKeyInfo keyPressed;
            username = "";
            do
            {
                keyPressed = Console.ReadKey(true); // Using false the pressed key is displayed in the console window
                while (keyPressed.Key == ConsoleKey.Enter && username.Length == 0) // Prevent from typing zero length username
                {

                    Console.WriteLine("\nWrong username! Please try again.");

                    keyPressed = Console.ReadKey(true);
                }
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    return null;
                }
                if ((!char.IsControl(keyPressed.KeyChar))) // To remove control characters
                {
                    username += keyPressed.KeyChar;
                    Console.Write(keyPressed.KeyChar);
                }
                else
                {
                    if (keyPressed.Key == ConsoleKey.Backspace && username.Length > 0)
                    {
                        username = username.Substring(0, (username.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (keyPressed.Key != ConsoleKey.Enter);
            Console.WriteLine();

            return username;
        }






        public List<Message> GetUserMessages(User LoggedIn, bool IsUserSender)
        {
            using (IMEntities DB = new IMEntities())
            {
                if (IsUserSender)
                {
                    return DB.Messages
                        .Include(msg => msg.Sender)
                        .Include(msg => msg.Receiver)
                        .Where(msg => msg.SenderId == LoggedIn.Id && msg.IsMessageShownToSender)
                        .ToList();
                }
                else
                {
                    return DB.Messages
                        .Include(msg => msg.Sender)
                        .Include(msg => msg.Receiver)
                        .Where(msg => msg.RecieverId == LoggedIn.Id && msg.IsMessageShownToReciever)
                        .ToList();
                }
            }
        }
    }
}



