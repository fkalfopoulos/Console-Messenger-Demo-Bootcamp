using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace demo
{
    class InputManager
    {



        public bool CorrectPasswordLength(string password)
        {
            if (password.Length < 5 || password.Length > 12)
            {
                return false;
            }
            return true;
        }




        public  bool IsUsernameCorrectLength(string username)
        {
            return username.Length > 5 && username.Length < 20;
        }
    }
}




        
