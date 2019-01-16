using IMModel;
using System;
using System.Linq;

namespace demo
{
    class UserReception
    {
        public InputChecking IC;
        public UserReception()
        {
            IC = new InputChecking();
        }


        public void LoginUser()
        {
            Console.Clear();
            Console.WriteLine(DesignedStrings.LoginString);
            Console.WriteLine("Login user");
            Console.WriteLine("-------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter username:");
            var usernameLogin = IC.UsernameInput();


            Console.Write("Enter password:");
            string password = IC.InputPassword();


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
                Console.Clear();
                Console.WriteLine(DesignedStrings.Welcome);                 
                Console.WriteLine("\nWelcome {0}", checkUser.Username);
                Console.WriteLine("\npress any key to procceed to the main menu");
                Console.ReadKey();
                var MainMenu = new MenuManager(checkUser);
                MainMenu.ManagerMenu();
            }
        }

        public void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine(DesignedStrings.CreateUsr);
            Console.WriteLine("\nRegister user");
            Console.WriteLine("-------------");

            Console.Write("\nEnter username:");
            var username = IC.UsernameInput();

            Console.Write("Enter password:");
            var password = IC.InputPassword();

            using (var context = new IMEntities())
            {
                if (username == "admin" && password == "admin1")
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


    

