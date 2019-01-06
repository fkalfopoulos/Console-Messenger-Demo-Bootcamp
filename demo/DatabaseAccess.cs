using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace demo
{
    public class DatabaseAccess
    {
        public void AddUser(string username, string password, int type = 1)
        {
            using (var context = new IMEntities())
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Creating new user profile.....Please wait...");
                context.Users.Add(new User());
                context.SaveChanges();
                Console.WriteLine($"\nNew user profile with username '{username}' has been created!");
                Console.ResetColor();
            }
        }

        public void DeleteUser(int id)
        {
            string usernameForDelete;

            Console.WriteLine("Choose the username of the user you would like to delete:");
            ViewAllUsers();
            usernameForDelete = Console.ReadLine();

            if (usernameForDelete is null) // is ESC is pressed return to admin menu
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

                if (ToBeDeleted.Role == (int) RoleEnum.SuperAdmin)
                {
                    Console.WriteLine("Master, you cannot give away your powers!");
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ReadKey();
                    return;
                }
                bool userActive = IsUserActive(usernameForDelete); // check if user is active

                if (!userActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nThe user with username '{usernameForDelete}' is no longer active.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    using (var context = new IMEntities())
                    {
                        User ToDelete = context.Users.Find(ToBeDeleted.Id);
                        context.Users.Remove(ToDelete);
                        context.SaveChanges();
                    }

                    Console.WriteLine($"\nUser '{usernameForDelete}' is no longer active Master.");
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");

                    Console.ReadKey();
                }
            }
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


        //public void EditUserType()
        //{
        //    bool userExists;
        //    string usernameToEdit;
        //    do
        //    {
        //        Console.Clear();
               
        //        Console.WriteLine("======= Welcome Admin =======");
               
        //        Console.WriteLine("/n======= You have the power to edit the type of a user! =======");
        //        Console.WriteLine();
        //        Console.WriteLine("Choose the username of the user you would like to edit:");
        //        usernameToEdit = InputUserName(); // username is Null if ESC is pressed during input
        //        if (usernameToEdit is null) // is ESC is pressed return to admin menu
        //        {
        //            return;
        //        }
        //        if (usernameToEdit == "admin")
        //        {
                                    
        //            Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    
        //            Console.ReadKey();
        //            return;
        //        }
        //        userExists = DoesUsernameExist(usernameToEdit); // Check if username already exists in database
        //        if (!userExists)
        //        {
                    
        //            Console.WriteLine("We are so sorry  but the username you entered does not exist.\nPlease choose another user to edit.");
                  
        //            Console.ReadKey();
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine($"Please choose a type for user '{usernameToEdit}'.");
        //            Console.WriteLine("Press 1 for admin, 2 for Moderetor, 3 for simple user:");

                    
        //            int userType;
        //            bool successType = false;
        //            do
        //            {
        //                string input = Console.ReadLine();
        //                if (int.TryParse(input, out userType))
        //                {
        //                    if (userType != 1 && userType != 2 && userType != 3)
        //                    {
        //                        Console.WriteLine("That type does not exist. Please try again.");
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine($"\nYou chose type {userType}.");
        //                        successType = true;
        //                    }
        //                }
        //                else // Parse failed
        //                {
        //                    Console.WriteLine("Invalid Input. Please try again.");

        //                }
        //            } while (!successType);

                   

        //    //        using (var context = new IMEntities())
        //    //        {
        //    //         context.Users.Where(x => x.Username == usernameToEdit).FirstOrDefault().Role = user.RoleEnum;
        //    //            context.SaveChanges();
        //    //        }
        //    //        Console.WriteLine();                    
        //    //        Console.WriteLine($"User '{usernameToEdit}' is now user of type {userType}: {(UserTypes)userType}.");
                   
        //    //        Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
        //    //        Console.ResetColor();
        //    //        Console.ReadKey();
        //    //    }
        //    //} while (!userExists);
        //}

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

