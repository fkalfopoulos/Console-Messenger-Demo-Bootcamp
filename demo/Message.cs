using System;

namespace demo
{
    public class Message
    {
        public int MessageId { get; set; }
        
        public string Data { get; set; }
        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public int SenderId { get; set; }
        public int RecieverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }

        public bool IsMessageShownToReciever { get; set; }
        public bool IsMessageShownToSender { get; set; }
        public bool IsMessageRead { get; set; }

        public Message()
        {
            Date = DateTime.Now;
            IsMessageShownToReciever = true;
            IsMessageShownToSender = true;
            IsMessageRead = false;
        }

        public override string ToString()
        {
            if(Sender is null)
            {
                return $"\n\t{Subject} :\n\t{Data}\n\n\t{Date}";
            }
            return $"\n\tFrom: {Sender.Username}\n\tTo:{Receiver.Username}\n\t{Subject} :\n\t{Data}\n\n\t{Date}";
        }
    }
}

