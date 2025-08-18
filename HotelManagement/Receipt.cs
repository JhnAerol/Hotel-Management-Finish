using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class Receipt : Form
    {
        public Receipt(int currentRoomNumber, string roomName, string Checkin, string Checkout, int DaysDiff, double Total)
        {
            InitializeComponent();
            lblRoomNumber.Text = currentRoomNumber.ToString();
            lblRoomType.Text = roomName;
            lblCheckin.Text = Checkin;
            lblCheckout.Text = Checkout;
            lblTNS.Text = DaysDiff.ToString();
            lblTAmount.Text = Total.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
