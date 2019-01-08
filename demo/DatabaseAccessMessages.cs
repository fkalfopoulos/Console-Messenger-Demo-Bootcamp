using IMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class DatabaseAccessMessages
    {
        public User LoggedIn;
        public DatabaseAccess DB;
        public DatabaseAccessMessages()

        {
            DB = new DatabaseAccess();
        }


        public void DeleteMessage()
        {

            List<string> DeleteMenuOptions = new List<string>
           {

                "Delete All",
                "Delete Recieved",
                "Delete Sent"
            };


            int option = ConsoleMenu.GetUserChoice(DeleteMenuOptions, DesignedStrings.DeleteMsg).IndexOfChoice;



            ChooseSentOrReceived();



            Console.WriteLine("Choose the username of the user you would like to delete:");

            bool checkinput = int.TryParse(Console.ReadLine(), out int deletemsg);

            if (checkinput)


                using (var context = new IMEntities())
                {

                    var msg = context.Messages.Where(x => x.MessageId == deletemsg).SingleOrDefault();
                    if (msg != null)
                    {
                        context.Messages.Remove(msg);
                        context.SaveChanges();
                        Console.WriteLine("Message Deleted");
                    }
                    else
                        Console.WriteLine("The message does not exist. Press any key to go back to the Main Menu");
                }
            Console.ReadKey();

            Console.WriteLine($"\n Message '{deletemsg}' is no longer active .");
            Console.WriteLine("\n\nPress any key to go back to  Menu.");

            Console.ReadKey();
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

                    Message newMessage = context.Messages.Find(Updatedmessage.MessageId);
                    newMessage.Subject = newMessageSubject;
                    newMessage.Data = newMessageData;
                    newMessage.IsMessageShownToReciever = false;
                    context.SaveChanges();

                    Console.Write($"\n\n Message updated successfully\n\n\tOK");
                }

                Console.ReadKey(true);
            }
        }


        private void ChooseSentOrReceived()

        {
        }
    }
}




