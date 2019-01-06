using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    public class Database : IMEntities
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


      /*  public void DeleteUser(int id)
        {
            bool userExists;
            string usernameForDelete;

            Console.WriteLine("Choose the username of the user you would like to delete:");
            GetUsers();
            usernameForDelete = InputUserName();

            if (usernameForDelete is null) // is ESC is pressed return to admin menu
            {
                return;
            }
            if (usernameForDelete == "admin")
            {
                Console.WriteLine("Master, you cannot give away your powers!");
                Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                Console.ReadKey();
                return;
            }
            userExists = DoesUsernameExist(usernameForDelete); // Check if username already exists in database

            if (!userExists)
            {

                Console.WriteLine("We are so sorry  but the username you entered does not exist.\nPlease choose another user to delete.");

                Console.ReadKey();
            }
            else
            {
                bool userActive = IsUserActive(usernameForDelete); // check if user is active
                if (!userActive)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The user with username '{usernameForDelete}' is no longer active.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    using (var context = new IMEntities())
                    {
                        context.Users.Where(x => x.Username == usernameForDelete = false;
                        context.SaveChanges();
                    }
                    Console.WriteLine();

                    Console.WriteLine($"User '{usernameForDelete}' is no longer active Master.");

                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");

                    Console.ReadKey();
                }


                using (var context = new IMEntities())
                {
                    User u = context.Users.Find(id);
                    context.Users.Remove(u);
                    context.SaveChanges();
                }




            }
        }
        */

        static public void InputUserName()
        {

        }


      static public User FindUserViaUserId(int id)
        {
            User result = null;

            using (var context = new IMEntities())
            {
                result = context.Users.Find(id);
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


        public bool IsPasswordCorrect(string username, string password)
        {
            using (var context = new IMEntities())
            {
                bool passwordCorrect = false;
                if (context.Users.Any(x => x.Username == username && x.Password == password))
                {
                    passwordCorrect = true;
                }
                return passwordCorrect;
            }
        }

        public bool IsUserActive(string username)
        {
            bool isActive = false;
            using (var context = new IMEntities())
            {
                if (context.Users.Where(x => x.Username == username).FirstOrDefault().IsUserActive == true)
                {
                    isActive = true;
                }
            }
            return isActive;

        }

        // Method to fetch information about user from database
        public void GetUserInfo(string username)
        {
            using (var context = new IMEntities())
            {
                Console.Clear();
                var user = context.Users.Where(x => x.Username == username).First();
               
                  
            
                Console.WriteLine($"======= Information about {username}'s profile =======");
                Console.WriteLine();
              
                Console.WriteLine();
    


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
                var users = context.Users.ToList();
                return users;
            }
        }

        public static void ViewAllUsers()
        {
            List<User> users = GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User[{user.Id} + {user.Name}");
            }
        }
    }
}



      
  


            



    

