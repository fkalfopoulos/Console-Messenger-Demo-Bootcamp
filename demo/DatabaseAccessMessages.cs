using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class DatabaseAccessMessages
    {

        //public void ViewUserMessages()
        //{
        //    bool userExists;
        //    string nameForViewMessages;
        //    do
        //    {
        //        Console.Clear();

        //        Console.WriteLine("======= Welcome Admin =======");

        //        Console.WriteLine("======= Here you can view the users messages =======");
        //        Console.WriteLine();
        //        Console.WriteLine("Choose the username of the user you would like to view the messages:");

        //        if (nameForViewMessages is null) // is ESC is pressed return to admin menu
        //        {
        //            return;
        //        }
        //        userExists = DatabaseAccess.DoesUsernameExists(nameForViewMessages); // Check if username already exists in database
        //        if (!userExists)
        //        {

        //            Console.WriteLine("The username you entered does not exist.\nPlease choose another user.");

        //            Console.ReadKey();
        //        }
        //        else
        //        {
        //            string header = ($"\nWould you like to view the user's sent or received messages?\n");
        //            string[] mailboxes = new string[] { "Inbox", "Sent Messages", "Go Back" };
        //            do
        //            {
        //                int option = messageMenuManager.ScrollMenu(header, mailboxes);

        //                switch (option)
        //                {
        //                    case 0:
        //                        ShowInbox(nameForViewMessages);
        //                        break;
        //                    case 1:
        //                        ShowSentMessages(nameForViewMessages);
        //                        break;
        //                    case 2:
        //                        return;
        //                }
        //            } while (true);
        //        }
        //    } while (!userExists);
        //}


        public void ShowInbox(string username)
        {
            List<Message> inbox = new List<Message>(); // list to hold the received messages of user

            using (var context = new IMEntities())
            {

                inbox = context.Messages.Include("Reciever").Include("Sender")
               .Where(x => x.Receiver.Username == username).ToList();
            }

            if (inbox.Count <= 0) // check if inbox is empty and exit method
            {
                Console.Clear();
                Console.WriteLine($"\n\n\nThe Inbox of '{username}' is empty!");

                Console.WriteLine("\n\n\n\nPress any key to go back.");

                Console.ReadKey();

                return;
            }

            // Print the received messages to console
            // messageMenuManager.ScrollInboxMenu(username, inbox);

        }

        public static void UpdateMessage(Message updatedMessage)
        {
            using (var context = new IMEntities())
            {
                Message originalMessage = context.Messages.Find(updatedMessage.MessageId);
                originalMessage.Data = updatedMessage.Data;
                context.SaveChanges();
            }
        }

        //// private bool DeleteAllMessages(User UserToDelete)
        // {
        //     using (var context = new IMEntities())
        //     {
        //         User user = context.Users.SingleOrDefault(u => u.Id == UserToDelete.Id);
        //         List<Message> pmslist = context.Messages.Where(pm => pm.SenderId == user.Id || pm.RecieverId == user.Id).ToList();
        //         foreach (Message pm in pmslist)
        //         {
        //             if (pm.SenderId == user.Id)
        //             {
        //                 pm.IsMessageShownToSender = false;
        //                //δημιουργία personal message
        //             }
        //             else
        //             {
        //                 pm.IsMessageShownToReciever = false;
        //                //δημιουργία personal message
        //             }
        //         }
        //          context.SaveChanges();
        // }


    }
}




