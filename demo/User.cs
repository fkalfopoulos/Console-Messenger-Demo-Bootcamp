using System;
using System.Collections.Generic;

namespace demo
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserAccess Role { get; set; }
        public DateTime RegisterDate { get; set; }

        public bool IsUserActive { get; set; }

        public virtual IList<Message> SentMessages { get; set; }
        public virtual IList<Message> ReceivedMessages { get; set; }

        public User()
        {
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
        }
    }

    public enum UserAccess
    {
        SuperAdministrator,
        Moderator,
        User
    }
}
