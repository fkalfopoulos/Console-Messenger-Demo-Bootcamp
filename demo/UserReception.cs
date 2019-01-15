using IMModel;
using System;
using System.Linq;

namespace demo
{
    class UserReception
    {
        public void LoginUser()
        {
            Console.Clear();
            Console.WriteLine(DesignedStrings.LoginString);
            Console.WriteLine("Login user");
            Console.WriteLine("-------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter username:");
            var usernameLogin = Console.ReadLine();
            Console.Write("Enter password:");
            string password = Console.ReadLine();                           //MenuUtilities.MaskPassword();

            User checkUser;
            using (var context = new IMEntities())
            {
                checkUser = context.Users.Where(c => c.Username == usernameLogin).SingleOrDefault();
            }

            if (checkUser == null)
            {
                Console.WriteLine("User does not exist. Press enter to leave");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Welcome {0}", checkUser.Username);
                Console.WriteLine("press any key to procceed to the main menu");
                Console.ReadKey();
                var MainMenu = new MenuManager(checkUser);
                MainMenu.ManagerMenu();
            }
        }

        public void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("\nRegister user");
            Console.WriteLine("-------------");

            Console.Write("\nEnter username:");
            var username = Console.ReadLine();

            Console.Write("Enter password:");



            var password = Console.ReadLine();



            using (var context = new IMEntities())
            {
                if (username == "admin3" && password == "admin3")
                {
                    var checkUser = context.Users.SingleOrDefault(c => c.Username == username);

                    if (checkUser == null)
                    {
                        context.Users.Add(new User()
                        {
                            Username = username,
                            Password = password,
                            Role = UserAccess.SuperAdministrator,
                            RegisterDate = DateTime.Now,
                            IsUserActive = true
                        });

                        context.SaveChanges();
                    }
                }
                else
                {
                    context.Users.Add(new User()
                    {
                        Username = username,
                        Password = password,
                        Role = UserAccess.User,
                        RegisterDate = DateTime.Now,
                        IsUserActive = true
                    });

                    context.SaveChanges();
                    Console.WriteLine("Registration Successful!");
                    Console.ReadLine();
                }
            }
        }
    }
}


    

