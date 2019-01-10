using IMModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace demo
{
    public class DatabaseAccess
    {
        User LoggedIn;

        public DatabaseAccess(User ActiveUser)
        {
            LoggedIn = ActiveUser;
        }

        public List<User> GetAllUsers()
        {
            using (IMEntities DB = new IMEntities())
            {
                return DB.Users.Where(usr => usr.Id != LoggedIn.Id).ToList();
            }
        }

        public void Update()
        {
            User EditedUser;

            using (var context = new IMEntities())
            {
                List<User> AllUsers = context.Users.ToList();
                UserChoice Choice = ConsoleMenu.GetUserChoice(AllUsers.Select(user => user.Username).ToList(), "Choose the user you wanna edit");

                EditedUser = AllUsers[Choice.IndexOfChoice];
            }

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
                    UpdateRole(EditedUser);
                    break;
                case 1:
                    UpdateUsername(EditedUser);
                    break;
                case 2:
                    UpdatePassword(EditedUser);
                    break;
            }

            Console.ReadKey();
        }

        public void UpdateUsername(User ToChange)
        {
            using (IMEntities context = new IMEntities())
            {
                Console.Clear();

                Console.Write("\n\n\n\n\tNew Username: ");
                string NewUserName = Console.ReadLine();
                User WithNewName = context.Users.Single(usr => usr.Id == ToChange.Id);
                WithNewName.Username = NewUserName;
                context.Entry(WithNewName).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdatePassword(User ToChange)
        {
            using (var context = new IMEntities())
            {
                Console.Clear();

                Console.Write("\n\n\n\n\tNew Password: ");
                string NewPass = Console.ReadLine();
                User WithNewName = context.Users.Single(ThisUser => ThisUser.Id == ToChange.Id);
                WithNewName.Password = NewPass;
                context.Entry(WithNewName).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateRole(User ToChange)
        {
            using (var context = new IMEntities())
            {
                Console.WriteLine("\n\n\n\n\tNew Role: ");

                User WithnewRole = context.Users.Single(ThisUser => ThisUser.Id == ToChange.Id);
                WithnewRole.Role = (UserAccess)ConsoleMenu.GetUserChoice(new List<string> { "Super Admin", "Moderator", "User" }).IndexOfChoice;
                context.Entry(WithnewRole).State = EntityState.Modified;
                context.SaveChanges();
                Console.WriteLine($"New User Role is: {WithnewRole.Role}");
            }
        }

        public void DeleteMessage(MenuManager menu)
        {
            List<string> DeleteMenuOptions = new List<string>
            {
                "Delete All",
                "Delete From Recieved",
                "Delete From Sent"
            };
           
            int option = ConsoleMenu.GetUserChoice(DeleteMenuOptions, DesignedStrings.DeleteMsg).IndexOfChoice;
            using (var context = new IMEntities())
            {
                if (option > 0)
                {
                    Message ToDelete = menu.ShowMessages(option > 1);

                    context.Messages.Attach(ToDelete);
                    context.Messages.Remove(ToDelete);
                    context.SaveChanges();
                }
                else 
                {
                    Console.WriteLine("\n\n\tWARNING!!!\n\n\tAre you sure you would like to delete EVERYTHING???");
                    Console.ReadKey();
                    context.Messages.RemoveRange(context.Messages
                        .Where(msg => msg.SenderId == LoggedIn.Id || msg.RecieverId == LoggedIn.Id));
                    context.SaveChanges();
                }

                else
                {

                }
            }
        }

        public void UpdateMessage(Message Updatedmessage)
        {
            Console.Write("\n\n\n\n\tNew Subject: ");
            string newMessageSubject = Console.ReadLine();

            Console.Write("\n\tNew Body: ");
            string newMessageData = Console.ReadLine();

            using (var context = new IMEntities())
            {
                {
                    Message newMessage = context.Messages.SingleOrDefault(msg => msg.MessageId == Updatedmessage.MessageId);
                    newMessage.Subject = newMessageSubject;
                    newMessage.Data = newMessageData;
                    context.Entry(newMessage).State = EntityState.Modified;
                    // x = (y > 0) ? 5 : -1;
                    // x = (expression) ? true : false;
                    Console.Write(context.SaveChanges() > 0 ? $"\n\n Message updated successfully\n\n\tOK" : "DUUUUde FUCK!");
                }

                Console.ReadKey(true);
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




