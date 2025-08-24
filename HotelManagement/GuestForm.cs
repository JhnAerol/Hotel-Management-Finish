using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class GuestForm : Form
    {
        private List<Panel> activePanels = new List<Panel>();
        
        public string roomName;
        //public static int currentIndex;
        AdminDataForm admin = new AdminDataForm();
        public int[] roomNumbers;
        public Dictionary<string, int> roomTypeIndices = new Dictionary<string, int>();
        int Roomprice;
        int valAduInc = 0;
        int valChiInc = 0;
        Rooms rooms = new Rooms();
        AddOns addons = new AddOns();
        bool isFormOpen = false;
        int zero = 0;
        string C1 = "";
        string C2 = "";
        string C3 = "";
        int DaysDiff = 0;
        int totalprice = 0;
        public int currentRoomNumber = 0;
        Receipt receipt;

        public GuestForm()
        {
            InitializeComponent();
        }

        private void LblPriceRoom_TextChanged(object sender, EventArgs e)
        {

            int addOnsPrice = 0;

            foreach (var panel in activePanels)
            {
                if (panel.Visible)
                {
                    addOnsPrice += GetPanelPrice(panel);
                }
            }

            int totalPrice = totalprice + addOnsPrice;
            lblTotal.Text = "₱ " + totalPrice.ToString();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {

            if (panelBooking.Visible == true)
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                panelAddons.Visible = false;
                panelInfo.Visible = true;
            }
        }


        private void btnConfrim_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtGuestFName.Text) || string.IsNullOrEmpty(txtGuestLName.Text) || string.IsNullOrEmpty(txtGuestAdd.Text) || string.IsNullOrEmpty(txtGuestEmail.Text) || string.IsNullOrEmpty(txtGuestCNumber.Text) || string.IsNullOrEmpty(cboMofPayment.Text))
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(cboMofPayment.Text))
                {
                    if (cboMofPayment.SelectedIndex == 2 || cboMofPayment.SelectedIndex == 1)
                    {
                        if (string.IsNullOrEmpty(txtGuestFName.Text) || string.IsNullOrEmpty(txtGuestLName.Text) || string.IsNullOrEmpty(txtGuestAdd.Text) || string.IsNullOrEmpty(txtGuestEmail.Text) || string.IsNullOrEmpty(txtGuestCNumber.Text) || string.IsNullOrEmpty(txtCVV.Text) || string.IsNullOrEmpty(txtCHName.Text) || string.IsNullOrEmpty(txtExpiry.Text) || string.IsNullOrEmpty(txtPCode.Text) || string.IsNullOrEmpty(txtCardNum.Text) || txtCardNum.TextLength < 12)
                        {
                            MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (lblAdds1.Text == addons.C1Name)
                            {
                                C1 = "C1";
                            }
                            if (lblAdds2.Text == addons.C2Name)
                            {
                                C2 = "C2";
                            }
                            if (lblAdds3.Text == addons.C3Name)
                            {
                                C3 = "C3";
                            }

                            Guest guest = new Guest();
                    
                            guest.firstname = txtGuestFName.Text;
                            guest.lastname = txtGuestLName.Text;
                            guest.address = txtGuestAdd.Text;
                            guest.email = txtGuestEmail.Text;
                            guest.contactnumber = txtGuestCNumber.Text;
                            string Checkin = checkinpicker.Value.ToString("MM-dd-yyyy");
                            string Checkout = checkoutpicker.Value.ToString("MM-dd-yyyy");
                            string AddOns = $"{C1} {C2} {C3}";
                            double Total = Convert.ToDouble(lblTotal.Text.Replace("₱ ", ""));
                            int NofAdult = Convert.ToInt32(lblNumAdult.Text);
                            int NofChildren = Convert.ToInt32(lblNumChildren.Text);

                            if (roomNumbers == null || !roomTypeIndices.ContainsKey(roomName) || roomTypeIndices[roomName] >= roomNumbers.Length)
                            {
                                MessageBox.Show("No more rooms to display!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            currentRoomNumber = roomNumbers[roomTypeIndices[roomName]];

                            DialogResult addbooking = MessageBox.Show("do you want to add room?", "Add Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (addbooking == DialogResult.No)
                            {
                                receipt = new Receipt(currentRoomNumber, roomName, Checkin, Checkout, DaysDiff, Total);
                                receipt.Show();

                                panelBooking.Visible = true;
                                panelInfo.Visible = false;
                                panelAddons.Visible = false;
                                panelImpostor.Visible = true;
                                panelReceipt.Visible = false;
                                panelC1.Visible = false;
                                panelC2.Visible = false;
                                panelC3.Visible = false;
                                lblAdds1.Text = string.Empty;
                                lblAdds2.Text = string.Empty;
                                lblAdds3.Text = string.Empty;
                                lblAddsPrice1.Text = string.Empty;
                                lblAddsPrice2.Text = string.Empty;
                                lblAddsPrice3.Text = string.Empty;
                                txtGuestAdd.Text = string.Empty;
                                txtGuestCNumber.Text = string.Empty;
                                txtGuestEmail.Text = string.Empty;
                                txtGuestFName.Text = string.Empty;
                                txtGuestLName.Text = string.Empty;
                                cboMofPayment.SelectedItem = null;
                                lblPriceRoom.Text = zero.ToString() + " ₱";
                                lblNumAdult.Text = "0";
                                lblNumChildren.Text = "0";
                                lblCheckIn.Text = "-";
                                lblCheckOut.Text = "-";
                                valAduInc -= valAduInc;
                                valChiInc -= valChiInc;
                                valAdult.Text = valAduInc.ToString();
                                valChildren.Text = valChiInc.ToString();
                                lblTotal.Text = "0" + " ₱";
                                RemoveAddOnPanels();
                                
                            }
                            else
                            {
                                receipt = new Receipt(currentRoomNumber, roomName, Checkin, Checkout, DaysDiff, Total);
                                receipt.Show();
                                    
                                    panelBooking.Visible = true;
                                    panelInfo.Visible = false;
                                    panelAddons.Visible = false;
                                    panelImpostor.Visible = true;
                                    panelReceipt.Visible = false;
                                    panelC1.Visible = false;
                                    panelC2.Visible = false;
                                    panelC3.Visible = false;
                                    lblAdds1.Text = string.Empty;
                                    lblAdds2.Text = string.Empty;
                                    lblAdds3.Text = string.Empty;
                                    lblAddsPrice1.Text = string.Empty;
                                    lblAddsPrice2.Text = string.Empty;
                                    lblAddsPrice3.Text = string.Empty;
                                    lblPriceRoom.Text = zero.ToString() + " ₱";
                                    lblNumAdult.Text = "0";
                                    lblNumChildren.Text = "0";
                                    lblCheckIn.Text = "-";
                                    lblCheckOut.Text = "-";
                                    valAduInc -= valAduInc;
                                    valChiInc -= valChiInc;
                                    valAdult.Text = valAduInc.ToString();
                                    valChildren.Text = valChiInc.ToString();
                                    RemoveAddOnPanels();
                                    
                            }

                            
                            SharedData.data.Rows.Add(currentRoomNumber, guest.firstname, guest.lastname, guest.address, guest.email, guest.contactnumber, NofAdult, NofChildren, roomName, AddOns, Checkin, Checkout, DaysDiff, Total);
                            roomTypeIndices[roomName]++;
                            
                        }                   
                    }
                    else
                    {
                        if (lblAdds1.Text == addons.C1Name)
                        {
                            C1 = "C1";
                        }
                        if (lblAdds2.Text == addons.C2Name)
                        {
                            C2 = "C2";
                        }
                        if (lblAdds3.Text == addons.C3Name)
                        {
                            C3 = "C3";
                        }

                        Guest guest = new Guest();

                        guest.firstname = txtGuestFName.Text;
                        guest.lastname = txtGuestLName.Text;
                        guest.address = txtGuestAdd.Text;
                        guest.email = txtGuestEmail.Text;
                        guest.contactnumber = txtGuestCNumber.Text;
                        string Checkin = checkinpicker.Value.ToString("MM-dd-yyyy");
                        string Checkout = checkoutpicker.Value.ToString("MM-dd-yyyy");
                        string AddOns = $"{C1} {C2} {C3}";
                        double Total = Convert.ToDouble(lblTotal.Text.Replace("₱ ", ""));
                        int NofAdult = Convert.ToInt32(lblNumAdult.Text);
                        int NofChildren = Convert.ToInt32(lblNumChildren.Text);

                        if (roomNumbers == null || !roomTypeIndices.ContainsKey(roomName) || roomTypeIndices[roomName] >= roomNumbers.Length)
                        {
                            MessageBox.Show("No more rooms to display!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        currentRoomNumber = roomNumbers[roomTypeIndices[roomName]];

                        DialogResult addbooking = MessageBox.Show("Do you want to add room?", "Add Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (addbooking == DialogResult.No)
                        {
                            receipt = new Receipt(currentRoomNumber, roomName, Checkin, Checkout, DaysDiff, Total);
                            receipt.Show();

                            panelBooking.Visible = true;
                            panelInfo.Visible = false;
                            panelAddons.Visible = false;
                            panelImpostor.Visible = true;
                            panelReceipt.Visible = false;
                            panelC1.Visible = false;
                            panelC2.Visible = false;
                            panelC3.Visible = false;
                            lblAdds1.Text = string.Empty;
                            lblAdds2.Text = string.Empty;
                            lblAdds3.Text = string.Empty;
                            lblAddsPrice1.Text = string.Empty;
                            lblAddsPrice2.Text = string.Empty;
                            lblAddsPrice3.Text = string.Empty;
                            txtGuestAdd.Text = string.Empty;
                            txtGuestCNumber.Text = string.Empty;
                            txtGuestEmail.Text = string.Empty;
                            txtGuestFName.Text = string.Empty;
                            txtGuestLName.Text = string.Empty;
                            cboMofPayment.SelectedItem = null;
                            lblPriceRoom.Text = zero.ToString() + " ₱";
                            lblNumAdult.Text = "0";
                            lblNumChildren.Text = "0";
                            lblCheckIn.Text = "-";
                            lblCheckOut.Text = "-";
                            valAduInc -= valAduInc;
                            valChiInc -= valChiInc;
                            valAdult.Text = valAduInc.ToString();
                            valChildren.Text = valChiInc.ToString();
                            lblTotal.Text = "0" + " ₱";
                            RemoveAddOnPanels();
                            
                        }
                        else
                        {
                            receipt = new Receipt(currentRoomNumber, roomName, Checkin, Checkout, DaysDiff, Total);
                            receipt.Show();
                            panelBooking.Visible = true;
                            panelInfo.Visible = false;
                            panelAddons.Visible = false;
                            panelImpostor.Visible = true;
                            panelReceipt.Visible = false;
                            panelC1.Visible = false;
                            panelC2.Visible = false;
                            panelC3.Visible = false;
                            lblAdds1.Text = string.Empty;
                            lblAdds2.Text = string.Empty;
                            lblAdds3.Text = string.Empty;
                            lblAddsPrice1.Text = string.Empty;
                            lblAddsPrice2.Text = string.Empty;
                            lblAddsPrice3.Text = string.Empty;
                            lblPriceRoom.Text = zero.ToString() + " ₱";
                            lblNumAdult.Text = "0";
                            lblNumChildren.Text = "0";
                            lblCheckIn.Text = "-";
                            lblCheckOut.Text = "-";
                            valAduInc -= valAduInc;
                            valChiInc -= valChiInc;
                            valAdult.Text = valAduInc.ToString();
                            valChildren.Text = valChiInc.ToString();
                            RemoveAddOnPanels();

                        }
                        SharedData.data.Rows.Add(currentRoomNumber, guest.firstname, guest.lastname, guest.address, guest.email, guest.contactnumber, NofAdult, NofChildren, roomName, AddOns, Checkin, Checkout, DaysDiff, Total);
                        roomTypeIndices[roomName]++;
                        
                    }
                }
            }
        }



        public int[] GenerateRoomSequence(int[] floorStarts, int roomsPerFloor)
        {
            var roomSequence = new List<int>();

            foreach (int start in floorStarts)
            {
                for (int i = 0; i < roomsPerFloor; i++)
                {
                    roomSequence.Add(start + i);
                }
            }

            return roomSequence.ToArray();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panelGuest.Visible = true;
            panelCheckIn.Visible = false;
            panelCheckOut.Visible = false;
        }

        private void btnShowCheckInPanel_Click(object sender, EventArgs e)
        {
            panelGuest.Visible = false;
            panelCheckIn.Visible = true;
            panelCheckOut.Visible = false;
        }

        private void btnShowCheckOutPanel_Click(object sender, EventArgs e)
        {
            panelGuest.Visible = false;
            panelCheckIn.Visible = false;
            panelCheckOut.Visible = true;
        }

        private void checkinpicker_ValueChanged(object sender, EventArgs e)
        {
            if (checkinpicker.Value < DateTime.Today)
            {
                MessageBox.Show("Cannot proceed double check your reservation date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string checkindate = checkinpicker.Value.ToString("MMM dd, yyyy");
                lblCheckIn.Text = checkindate;
                panelCheckIn.Visible = false;
            }
        }

        private void checkoutpicker_ValueChanged(object sender, EventArgs e)
        {
            if (checkoutpicker.Value < checkinpicker.Value)
            {
                MessageBox.Show("Cannot proceed double check your reservation date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string checkoutdate = checkoutpicker.Value.ToString("MMM dd, yyyy");
                lblCheckOut.Text = checkoutdate;
                panelCheckOut.Visible = false;
            }
        }



        public void CalculateDays()
        {
            DateTime checkin = checkinpicker.Value;
            DateTime checkout = checkoutpicker.Value;

            DaysDiff = (checkout - checkin).Days;


            lblNightStay.Text = $"{Math.Abs(DaysDiff)} Night stay";
        }

        private void incAdult_Click(object sender, EventArgs e)
        {

            while (valAduInc < 20)
            {
                valAduInc++;
                break;
            }

            valAdult.Text = valAduInc.ToString();

        }

        private void decAdult_Click(object sender, EventArgs e)
        {
            if (valAduInc != 0)
            {
                valAduInc--;
                valAdult.Text = valAduInc.ToString();
            }
        }

        private void incChildren_Click(object sender, EventArgs e)
        {
            while (valChiInc < 20)
            {
                valChiInc++;
                break;
            }

            valChildren.Text = valChiInc.ToString();
        }

        private void decChildren_Click(object sender, EventArgs e)
        {
            if (valChiInc != 0)
            {
                valChiInc--;
                valChildren.Text = valChiInc.ToString();
            }
        }

        private void btnConfirmNumGuest_Click(object sender, EventArgs e)
        {
            lblNumAdult.Text = valAduInc.ToString();
            lblNumChildren.Text = valChiInc.ToString();
            panelGuest.Visible = false;
        }

        public void GetGuest()
        {
            if (lblNumAdult.Text != "0")
            {
                if (lblNumChildren.Text != "0")
                {
                    lblGuests.Text = $"{lblNumAdult.Text} Adults & {lblNumChildren.Text} Children";
                }
                else
                {
                    lblGuests.Text = $"{lblNumAdult.Text} Adults";
                }
            }
        }


        private void btnDeluxeKing_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {
                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;


                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblDeluxeKing.Text;
                GetGuest();
                CalculateDays();
                roomName = lblNameRoom.Text;
                totalprice = DaysDiff * Rooms.DeluxeKing;

                if (roomName == "Deluxe King")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.DeluxeKing;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }
                }

                roomNumbers = GenerateRoomSequence(new[] { 201, 301, 401, 501 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }

                if (Rooms.DeluxeKingRoom > 0 && roomName == "Deluxe King")
                {
                    Rooms.DeluxeKingRoom--;
                    Rooms.SelectedRoom = "Deluxe King";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeluxeTwin_Click(object sender, EventArgs e)
        {

            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblDeluxeTwin.Text;
                GetGuest();
                CalculateDays();
                roomName = lblNameRoom.Text;
                totalprice = DaysDiff * Rooms.DeluxeTwin;
                if (roomName == "Deluxe Twin")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.DeluxeTwin;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 206, 306, 406, 506 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.DeluxeTwinRoom > 0 && roomName == "Deluxe Twin")
                {
                    
                    Rooms.DeluxeTwinRoom--;
                    Rooms.SelectedRoom = "Deluxe Twin";
                    admin.UpdateLabels();
                }

            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGrandDeluxeKing_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")}  -  {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblGrandDeluxeKing.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.GrandDeluxeKing;
                if (roomName == "Grand Deluxe King")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.GrandDeluxeKing;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 211, 311, 411, 511 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.GrandDeluxeKingRoom > 0 && roomName == "Grand Deluxe King")
                {
                    
                    Rooms.GrandDeluxeKingRoom--;
                    Rooms.SelectedRoom = "Grand Deluxe King";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGradnDeluxeDouble_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblGrandDeluxeDouble.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.GrandDeluxeDouble;
                if (roomName == "Grand Deluxe Double")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.GrandDeluxeDouble;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 216, 316, 416, 516 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.GrandDeluxeTwinRoom > 0 && roomName == "Grand Deluxe Double")
                {
                    

                    Rooms.GrandDeluxeTwinRoom--;
                    Rooms.SelectedRoom = "Grand Deluxe Double";
                    admin.UpdateLabels();
                }

            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPremiumSuiteKing_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblPremiumSuiteKing.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.PremiumSuiteKing;
                if (roomName == "Premium Suite King")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.PremiumSuiteKing;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 221, 321, 421, 521 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.PremiumSuiteKingRoom > 0 && roomName == "Premium Suite King")
                {
                    
                    Rooms.PremiumSuiteKingRoom--;
                    Rooms.SelectedRoom = "Premium Suite King";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPremuimSuiteDouble_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblPremiumSuiteDouble.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.PremiumSuiteDouble;
                if (roomName == "Premium Suite Double")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.PremiumSuiteDouble;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 226, 326, 426, 526 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.PremiumSuiteDoubleRoom > 0 && roomName == "Premium Suite Double")
                {
                    
                    Rooms.PremiumSuiteDoubleRoom--;
                    Rooms.SelectedRoom = "Premium Suite Double";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecutiveSuiteKing_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblExecutiveSuiteKing.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.ExecutiveSuiteKing;
                if (roomName == "Executive Suite King")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.ExecutiveSuiteKing;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 231, 331, 431, 531 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.ExecutiveSuiteKingRoom > 0 && roomName == "Executive Suite King")
                {
                    
                    Rooms.ExecutiveSuiteKingRoom--;
                    Rooms.SelectedRoom = "Executive Suite King";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExecutiveSuiteDouble_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblExecutiveSuiteDouble.Text;
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.ExecutiveSuiteDouble;
                if (roomName == "Executive Suite Double")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.ExecutiveSuiteDouble;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 236, 336, 436, 536 }, 5);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.ExecutiveSuiteDoubleRoom > 0 && roomName == "Executive Suite Double")
                {
                    
                    Rooms.ExecutiveSuiteDoubleRoom--;
                    Rooms.SelectedRoom = "Executive Suite Double";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPresidentialSuite_Click(object sender, EventArgs e)
        {
            if (lblNumAdult.Text != "0" && lblCheckIn.Text != "-" && lblCheckOut.Text != "-")
            {

                panelBooking.Visible = false;
                panelAddons.Visible = true;
                panelReceipt.Visible = true;
                panelImpostor.Visible = false;

                lblDateStay.Text = $"{checkinpicker.Value.ToString("MMM dd, yyyy")} - {checkoutpicker.Value.ToString("MMM dd, yyyy")}";
                lblNameRoom.Text = lblPresidentialSuite.Text;
                lblPriceRoom.Text = $"{Rooms.PresidentalSuite}";
                GetGuest();
                CalculateDays();

                roomName = lblNameRoom.Text;

                totalprice = DaysDiff * Rooms.PresidentalSuite;
                if (roomName == "Presidential Suite")
                {
                    if (totalprice != 0)
                    {

                        lblPriceRoom.Text = totalprice.ToString() + " ₱";

                    }
                    else
                    {
                        totalprice = Rooms.PresidentalSuite;
                        lblPriceRoom.Text = totalprice.ToString() + " ₱";
                    }

                }

                roomNumbers = GenerateRoomSequence(new[] { 601 }, 10);

                if (!roomTypeIndices.ContainsKey(roomName))
                {
                    roomTypeIndices[roomName] = 0;
                }
                if (Rooms.PresidentialSuiteRoom > 0 && roomName == "Presidential Suite")
                {
                    Rooms.PresidentialSuiteRoom--;
                    Rooms.SelectedRoom = "Presidential Suite";
                    admin.UpdateLabels();
                }
            }
            else
            {
                MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCode1_Click(object sender, EventArgs e)
        {
            panelBorder.Visible = true;


            lblAddsPrice1.Text = addons.C1.ToString() + " ₱";
            lblAdds1.Text = lblCode1.Text;

            ShowPanel(panelC1);
        }

        private void btnCode2_Click(object sender, EventArgs e)
        {
            panelBorder.Visible = true;

            lblAddsPrice2.Text = $"{addons.C2} ₱";
            lblAdds2.Text = lblCode2.Text;

            ShowPanel(panelC2);
        }

        private void btnCode3_Click(object sender, EventArgs e)
        {
            panelBorder.Visible = true;

            lblAddsPrice3.Text = $"{addons.C3} ₱";
            lblAdds3.Text = lblCode3.Text;

            ShowPanel(panelC3);
        }

        private void RemoveAddOnPanels()
        {
            foreach (var panel in activePanels.ToList())
            {
                if (panel == panelC1 || panel == panelC2 || panel == panelC3)
                {
                    panel.Visible = false;
                    lblAdds1.Text = string.Empty;
                    lblAdds2.Text = string.Empty;
                    lblAdds3.Text = string.Empty;
                    activePanels.Remove(panel);
                }
            }

            int roomPrice = Convert.ToInt32(lblPriceRoom.Text.Replace(" ₱", ""));
            lblTotal.Text = "₱ " + roomPrice.ToString();
            UpdatePanelLayout();
        }

        private void ShowPanel(Panel panelToShow)
        {
            string total = lblTotal.Text;

            string numericText = new string(total.Where(char.IsDigit).ToArray());

            if (int.TryParse(numericText, out int price))
            {
                int panelPrice = GetPanelPrice(panelToShow);

                if (!activePanels.Contains(panelToShow))
                {
                    activePanels.Add(panelToShow);
                    price += panelPrice;
                }
                else
                {
                    activePanels.Remove(panelToShow);
                    activePanels.Add(panelToShow);
                }

                lblTotal.Text = "₱ " + price.ToString();
                UpdatePanelLayout();
            }


        }

        private void UpdatePanelLayout()
        {
            int yPosition = 0;
            foreach (var panel in activePanels)
            {
                panel.Visible = true;
                panel.Top = yPosition;
                yPosition += panel.Height + 10;
            }

            if (!activePanels.Contains(panelC1))
            {
                panelC1.Visible = false;
                lblAdds1.Text = string.Empty;
            }
            if (!activePanels.Contains(panelC2))
            {
                panelC2.Visible = false;
                lblAdds2.Text = string.Empty;
            }
            if (!activePanels.Contains(panelC3))
            {
                panelC3.Visible = false;
                lblAdds3.Text = string.Empty;
            }
        }

        private int GetPanelPrice(Panel panel)
        {
            if (panel == panelC1)
                return 1599;
            else if (panel == panelC2)
                return 2099;
            else if (panel == panelC3)
                return 2599;
            else
                return 0;
        }

        private void btnRemoveAddOns_Click(object sender, EventArgs e)
        {
            if (activePanels.Count > 0)
            {
                string total = lblTotal.Text;

                string numericText = new string(total.Where(char.IsDigit).ToArray());

                if (int.TryParse(numericText, out int price))
                {
                    Panel lastPanel = activePanels[activePanels.Count - 1];
                    int panelPrice = GetPanelPrice(lastPanel);

                    price -= panelPrice;
                    lblTotal.Text = "₱ " + price.ToString();

                    activePanels.RemoveAt(activePanels.Count - 1);
                    lastPanel.Visible = false;

                    UpdatePanelLayout();
                }


            }
        }

        private void btnRemoveRoom_Click(object sender, EventArgs e)
        {
            panelBooking.Visible = true;
            panelAddons.Visible = false;
            panelInfo.Visible = false;

            lblNameRoom.Text = string.Empty;
            lblPriceRoom.Text = "0";

            int total = 0;
            foreach (var panel in activePanels)
            {
                if (panel.Visible)
                {
                    total += GetPanelPrice(panel);
                }
            }

            lblTotal.Text = "₱ " + total.ToString();

            if (Rooms.SelectedRoom == "Deluxe King")
            {
                Rooms.DeluxeKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Deluxe Twin")
            {
                Rooms.DeluxeTwinRoom++;
            }
            else if (Rooms.SelectedRoom == "Grand Deluxe King")
            {
                Rooms.GrandDeluxeKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Grand Deluxe Double")
            {
                Rooms.GrandDeluxeTwinRoom++;
            }
            else if (Rooms.SelectedRoom == "Premium Suite King")
            {
                Rooms.PremiumSuiteKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Premium Suite Double")
            {
                Rooms.PremiumSuiteDoubleRoom++;
            }
            else if (Rooms.SelectedRoom == "Executive Suite King")
            {
                Rooms.ExecutiveSuiteKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Executive Suite Double")
            {
                Rooms.ExecutiveSuiteDoubleRoom++;
            }
            else if (Rooms.SelectedRoom == "Presidential Suite")
            {
                Rooms.PresidentialSuiteRoom++;
            }

            Rooms.SelectedRoom = ""; // Reset last room picked
            admin.UpdateLabels();
            MessageBox.Show("Last room selection undone.");
        }

        private void Aback_Click(object sender, EventArgs e)
        {
            panelBooking.Visible = true;
            panelAddons.Visible = false;
            panelInfo.Visible = false;
            RemoveAddOnPanels();

            if (Rooms.SelectedRoom == "Deluxe King")
            {
                Rooms.DeluxeKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Deluxe Twin")
            {
                Rooms.DeluxeTwinRoom++;
            }
            else if (Rooms.SelectedRoom == "Grand Deluxe King")
            {
                Rooms.GrandDeluxeKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Grand Deluxe Double")
            {
                Rooms.GrandDeluxeTwinRoom++;
            }
            else if (Rooms.SelectedRoom == "Premium Suite King")
            {
                Rooms.PremiumSuiteKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Premium Suite Double")
            {
                Rooms.PremiumSuiteDoubleRoom++;
            }
            else if (Rooms.SelectedRoom == "Executive Suite King")
            {
                Rooms.ExecutiveSuiteKingRoom++;
            }
            else if (Rooms.SelectedRoom == "Executive Suite Double")
            {
                Rooms.ExecutiveSuiteDoubleRoom++;
            }
            else if (Rooms.SelectedRoom == "Presidential Suite")
            {
                Rooms.PresidentialSuiteRoom++;
            }

            Rooms.SelectedRoom = ""; // Reset last room picked
            admin.UpdateLabels();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            panelBooking.Visible = false;
            panelAddons.Visible = true;
            panelInfo.Visible = false;
        }

        private void Dback_Click(object sender, EventArgs e)
        {
            foreach (Form openformDashboard in Application.OpenForms)
            {
                if (openformDashboard.Name == "Dashboard") // Replace with the actual form name
                {
                    isFormOpen = true;
                    openformDashboard.BringToFront();
                    openformDashboard.Activate();
                    break;
                }
            }

            if (!isFormOpen)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
        }

        private void txtGuestEmail_Leave(object sender, EventArgs e)
        {
            string email = txtGuestEmail.Text;
            string gmailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

            if (!Regex.IsMatch(email, gmailPattern))
            {
                MessageBox.Show("Please enter a valid Email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGuestEmail.Clear();
                txtGuestEmail.Focus();
            }
        }

        private void txtGuestCNumber_Leave(object sender, EventArgs e)
        {
            string contactnumber = txtGuestCNumber.Text;

            if (!contactnumber.StartsWith("09") || contactnumber.Length != 11)
            {
                MessageBox.Show("Please enter a valid Contact Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGuestCNumber.Clear();
                txtGuestCNumber.Focus();
            }
        }

        private void cboMofPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMofPayment.SelectedIndex == 2 || cboMofPayment.SelectedIndex == 1)
            {
                panelCard.Visible = true;
            }
            else
            {
                panelCard.Visible = false;
            }
        }

        private void panelCard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCardNum_Leave(object sender, EventArgs e)
        {
            if (txtCardNum.Text == "")
            {
                txtCardNum.Text = "0000 0000 0000 0000";
                txtCardNum.ForeColor = Color.DarkGray;
            }
        }

        private void txtCardNum_Enter(object sender, EventArgs e)
        {
            if (txtCardNum.Text == "0000 0000 0000 0000")
            {
                txtCardNum.Text = "";
                txtCardNum.ForeColor = Color.Black;
            }
            string input = txtCardNum.Text.Replace(" ", "");  // Remove any existing spaces
            string formatted = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    formatted += " ";  // Add space after every 4 characters
                }
                formatted += input[i];
            }

            // Set the formatted text back to the TextBox and maintain the cursor position
            int cursorPosition = txtCardNum.SelectionStart;
            txtCardNum.Text = formatted;
            txtCardNum.SelectionStart = cursorPosition + (formatted.Length - input.Length);
        }

        private void txtExpiry_Leave(object sender, EventArgs e)
        {
            if (txtExpiry.Text == "")
            {
                txtExpiry.Text = "MM/YYYY";
                txtExpiry.ForeColor = Color.DarkGray;
            }

            string input = txtExpiry.Text;

            if (!string.IsNullOrEmpty(input) && input.Length >= 2) // Ensure the text is not empty and has at least 2 characters
            {
                string firstTwoDigits = input.Substring(0, 2);
                string lastTwoDigit = input.Substring(3, 4);
                string getfirstnumber = input.Substring(0, 1);
                int ConvertFTD = Convert.ToInt32(firstTwoDigits);
                int ConvertLTD = Convert.ToInt32(lastTwoDigit);
                int ConvertGFN = Convert.ToInt32(getfirstnumber);

                if (ConvertFTD > 12 || ConvertFTD <= 0 || ConvertLTD <= 0)
                {
                    MessageBox.Show("Invalid Input Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExpiry.Clear();
                    txtExpiry.Focus();
                }
                else if (ConvertFTD < 12 || ConvertLTD < 2024)
                {
                    MessageBox.Show("Sorry, Your card is Expired", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExpiry.Clear();
                    txtExpiry.Focus();
                }
            }
        }

        private void txtExpiry_Enter(object sender, EventArgs e)
        {
            if (txtExpiry.Text == "MM/YYYY")
            {
                txtExpiry.Text = "";
                txtExpiry.ForeColor = Color.Black;
            }
        }

        private void txtCVV_Leave(object sender, EventArgs e)
        {
            if (txtCVV.Text == "")
            {
                txtCVV.Text = "000";
                txtCVV.ForeColor = Color.DarkGray;
            }
        }

        private void txtCVV_Enter(object sender, EventArgs e)
        {
            if (txtCVV.Text == "000")
            {
                txtCVV.Text = "";
                txtCVV.ForeColor = Color.Black;
                txtCVV.MaxLength = 3;
            }
        }

        private void txtCardNum_TextChanged(object sender, EventArgs e)
        {
            if (txtCardNum.Text != "0000 0000 0000 0000")
            {
                string input = txtCardNum.Text.Replace(" ", "");  // Remove any existing spaces
                string formatted = "";

                for (int i = 0; i < input.Length; i++)
                {
                    if (i > 0 && i % 4 == 0)
                    {
                        formatted += " ";  // Add space after every 4 characters
                    }
                    formatted += input[i];
                }

                // Set the formatted text back to the TextBox and maintain the cursor position
                int cursorPosition = txtCardNum.SelectionStart;
                txtCardNum.Text = formatted;
                txtCardNum.SelectionStart = cursorPosition + (formatted.Length - input.Length);
                txtCardNum.MaxLength = 19;
            }
            
        }

        private void txtCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCVV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtExpiry_TextChanged(object sender, EventArgs e)
        {
            if(txtExpiry.Text != "MM/YYYY")
            {
                string input = txtExpiry.Text.Replace("/", "");  // Remove any existing spaces
                string formatted = "";

                for (int i = 0; i < input.Length; i++)
                {
                    if (i <= 2)
                    {
                        if (i > 0 && i % 2 == 0)
                        {
                            formatted += "/";
                        }
                    }
                    formatted += input[i];
                }

                // Set the formatted text back to the TextBox and maintain the cursor position
                int cursorPosition = txtExpiry.SelectionStart;
                txtExpiry.Text = formatted;
                txtExpiry.SelectionStart = cursorPosition + (formatted.Length - input.Length);
                txtExpiry.MaxLength = 7;
            }   
        }

        private void txtExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtPCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;

            }
        }
    }
}
