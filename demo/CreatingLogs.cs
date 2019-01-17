using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace demo
{
    class CreatingLogs
    {

        static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\";
        const string UserLog = "Users.txt", MessageLog = "Logmessages.txt";

        public static void LogMessage(Message LM)
        {
            Debug.Write(FilePath);
            File.AppendAllText(FilePath + MessageLog, $"Date : {LM.Date}, Message ID : {LM.SenderId}, Message Content : {LM.Data}, Receiver ID : {LM.RecieverId}");
        }

        public static void LogUser(User LU)
        {
            Debug.Write(FilePath);
            File.AppendAllText(FilePath + UserLog, $"Username : {LU.Username},Time of Registration : {LU.RegisterDate}, User role : {LU.Role}");
        }

        public static List<User> ReadUserLog()
        {
            List<User> UserList = new List<User>();
            foreach (string line in File.ReadLines(FilePath + UserLog))
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
            foreach (string line in File.ReadLines(FilePath + UserLog))
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
