using IMModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace demo
{
    class Program
    {

        public DatabaseAccess DB;
        public DatabaseAccessMessages DM;


        static void Main(string[] args)
        {
            IMEntities context = new IMEntities();
            DatabaseAccess DB = new DatabaseAccess();



            while (true)
            {
                Console.Clear();



                Console.WriteLine("Choose an option");


                List<string> RegisterMenuOptions = new List<string>
                {
                "Register",
                "Login",
                "Terminate the Program"
                };

                int option = ConsoleMenu.GetUserChoice(RegisterMenuOptions, DesignedStrings.DemoMessenger).IndexOfChoice;

                switch (option)
                {
                    case 0:
                        DB.AddUser();
                        break;
                    case 1:

                        break;
                    case 2:
                        Console.WriteLine("Thank you for choosing us for your communication needs.");
                        Console.WriteLine("The program will now terminate.");
                        Environment.Exit(0);
                        break;
                }

                //Register
                if (option == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Register user");
                    Console.WriteLine("-------------");
                    Console.WriteLine();
                    Console.Write("Enter username:");
                    var username = Console.ReadLine();
                    Console.Write("Enter password:");
                    var password = Console.ReadLine();

                    //Αν το input του χρήστη είναι το παρακάτω τότε θα προχωρήσει στην αποθήκευση
                    if (username == "admin" && password == "admin")
                    {

                        var checkUser = context.Users.SingleOrDefault(c => c.Username == username);

                        //Αν δεν υπάρχει κάνουμε καταχώρηση του χρήστη Admin..
                        if (checkUser == null)
                        {
                            //..δημιουγώντας ένα αντικείμενο της κλάσης User της βάσης μας
                            User newUser = new User()
                            {
                                Username = username,
                                Password = password,
                                Role = UserAccess.User
                            };
                            //Καταχώρηση
                            context.Users.Add(newUser);
                            //Αποθήκευση αλλαγών
                            context.SaveChanges();


                            Console.WriteLine("Registration Successful!");
                            Console.ReadLine();


                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Admin already registered. Press enter to leave");
                            Console.ReadLine();
                        }
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Only admin can register. Press enter to leave");
                        Console.ReadLine();
                    }
                }

                //Login
                if (option == "2")
                {
                    Console.Clear();
                    Console.WriteLine(DesignedStrings.LoginString);
                    Console.WriteLine("Login user");
                    Console.WriteLine("-------------");
                    Console.WriteLine();

                    Console.Write("Enter username:");
                    var username = Console.ReadLine();
                    Console.Write("Enter password:");
                    string password = Console.ReadLine();                           //MenuUtilities.MaskPassword();

                    //Ελέγχει να δει αν ο χρήστης όντως είναι καταχωρημένος στην βάση
                    var checkUser = context.Users.Where(c => c.Username == username).SingleOrDefault();


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
                        new MenuManager(checkUser);
                    }

                }

            }
        }
    }


       
    

   


    
}


