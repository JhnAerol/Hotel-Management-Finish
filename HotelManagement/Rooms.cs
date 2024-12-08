using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public class Rooms
    {
        public static int DeluxeKing { get; set; }
        public static int DeluxeTwin { get; set; }
        public static int GrandDeluxeKing { get; set; }
        public static int GrandDeluxeDouble { get; set; }
        public static int PremiumSuiteKing { get; set; }
        public static int PremiumSuiteDouble { get; set; }
        public static int ExecutiveSuiteKing { get; set; }
        public static int ExecutiveSuiteDouble { get; set; }
        public static int PresidentalSuite { get; set; }


        public Rooms()
        {
            DeluxeKing = 11600;
            DeluxeTwin = 12500;
            GrandDeluxeKing = 13400;
            GrandDeluxeDouble = 14300;
            PremiumSuiteKing = 10700;
            PremiumSuiteDouble = 21900;
            ExecutiveSuiteKing = 28588;
            ExecutiveSuiteDouble = 31088;
            PresidentalSuite = 191588;
        }

        public static int DeluxeKingRoom = 20;
        public static int DeluxeTwinRoom = 20;
        public static int GrandDeluxeKingRoom = 20;
        public static int GrandDeluxeTwinRoom = 20;
        public static int PremiumSuiteKingRoom = 20;
        public static int PremiumSuiteDoubleRoom = 20;
        public static int ExecutiveSuiteKingRoom = 20;
        public static int ExecutiveSuiteDoubleRoom = 20;
        public static int PresidentialSuiteRoom = 20;

        public static string SelectedRoom = "";

        public const string DeluxeKingRoomName = "Deluxe King";
        public const string DeluxeTwinRoomName = "Deluxe Twin";
        public const string GrandDeluxeKingRoomName = "Grand Deluxe King";
        public const string GrandDeluxeDoubleRoomName = "Grand Deluxe Double";
        public const string PremiumSuiteKingRoomName = "Premium Suite King";
        public const string PremiumSuiteDoubleRoomName = "Premium Suite Double";
        public const string ExecutiveSuiteKingRoomName = "Executive Suite King";
        public const string ExecutiveSuiteDoubleRoomName = "Executive Suite Double";
        public const string PresidentialSuiteRoomName = "Presidential Suite";
    }
}
