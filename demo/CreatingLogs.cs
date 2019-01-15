using System;
using System.Collections.Generic;
using System.IO;

namespace demo
{
    class CreatingLogs
    {
        public static class LogFile
        {
            const string Path = @"C:\Users\MMNew\Desktop\Txts";
            const string UserLog = "Users.txt", MessageLog = "Logmessages.txt";

            public static void LogMessage(Message LM)
            {
                File.AppendAllText(Path + MessageLog, $"{LM.Date},{LM.SenderId},{LM.Data},{LM.RecieverId}");
            }

            public static void LogUser(User LU)
            {
                File.AppendAllText(Path + UserLog, $"{LU.Username},{LU.RegisterDate},{LU.Role}");
            }

            public static void UserList( User UL)

            {
                
            }


            public static List<User> ReadUserLog()
            {
                List<User> UserList = new List<User>();
                foreach (string line in File.ReadLines(Path + UserLog))
                {
                    string[] UserElements = line.Split(',');
                    UserList.Add(new User()
                    {
                        Username = UserElements[0],
                        RegisterDate = DateTime.Parse(UserElements[1]),
                        Role = (UserAccess)int.Parse(UserElements[2])
                    });
                }
                return UserList;
            }

            public static List<Message> ReadMessageLog()
            {
                List<Message> MessageList = new List<Message>();
                foreach (string line in File.ReadLines(Path + UserLog))
                {
                    string[] MessageElements = line.Split(',');
                    MessageList.Add(new Message()
                    {
                        Date = DateTime.Parse(MessageElements[1]),
                        SenderId = int.Parse(MessageElements[1]),
                        Data = MessageElements[2],
                        RecieverId = int.Parse(MessageElements[3])
                    });
                }
                return MessageList;
            }
        }
    }
}