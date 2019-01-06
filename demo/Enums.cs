using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    class Enums
    {

        public enum MainMenuOptions
        {
            Login = 1,
            Register = 2,
            Info = 3,
            Exit = 4
        }

        public enum UserMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            Info = 4,
            ExitToMain = 5,
            Quit = 6
        }

        public enum SuperAdminMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            

            CreateNewUser = 4,
            DeleteUser = 5,
            EditUserType = 6,
            ViewUserInfo = 7,

            ViewUserMessages = 8,
            ViewAllMessages = 9,
            DeleteMessages = 10,
            EditMessages = 11,

            ExitToMain = 12,
            Quit = 13
        }
        public enum UserTypes
        {
            
            SuperAdmin = 1,
            Moderator = 2,
            SimpleUser = 3,
        }
      
    }
}
