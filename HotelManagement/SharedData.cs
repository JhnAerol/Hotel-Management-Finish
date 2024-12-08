using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HotelManagement
{
    public static class SharedData
    {
        public static DataTable data = new DataTable();

        static SharedData() 
        { 
            data.Columns.Add("Room no#", typeof(int));
            data.Columns.Add("Guest FName", typeof(string));
            data.Columns.Add("Guest LName", typeof(string));
            data.Columns.Add("Guest Address", typeof(string));
            data.Columns.Add("Guest Email", typeof(string));
            data.Columns.Add("Guest CNumber", typeof(int));
            data.Columns.Add("No. of Adults", typeof(int));
            data.Columns.Add("No. of Children", typeof(int));
            data.Columns.Add("Type of Suites", typeof(string));
            data.Columns.Add("Add ons", typeof(string));
            data.Columns.Add("Check in", typeof(string));
            data.Columns.Add("Check out", typeof(string));
            data.Columns.Add("Total", typeof(double));
        }
    }
}
