using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    internal class AddOns : Rooms
    {
        public int C1 { get; set; }
        public int C2 { get; set; }
        public int C3 { get; set; }

        public string C1Name { get; set; }
        public string C2Name { get; set; }
        public string C3Name { get; set; }
        
        public AddOns() 
        {
            C1 = 1599;
            C2 = 2099;
            C3 = 2599;

            C1Name = "Elysian Delight";
            C2Name = "Zaroda Serenity Set";
            C3Name = "Pure Radiance Collection";
        }


    }
}
