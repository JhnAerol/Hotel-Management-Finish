using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    public class Admin
    {
        public string AdminID { get; set; }
        public string AdminPassword { get; set; }

        public Admin (string adminID, string adminPassword)
        {
            AdminID = adminID;
            AdminPassword = adminPassword;
        }
    }
}
