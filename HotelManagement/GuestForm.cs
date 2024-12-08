using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class GuestForm : Form
    {
        private List<Panel> activePanels = new List<Panel>();
        public static string roomName;     
        public int currentIndex;     
        public int[] roomNumbers;
        public Dictionary<string, int> roomTypeIndices = new Dictionary<string, int>();
        AdminDataForm admin = new AdminDataForm();
        int Roomprice;
        int valAduInc = 0;
        int valChiInc = 0;
        Rooms rooms = new Rooms();
        AddOns addons = new AddOns();
        bool isFormOpen = false;
        int zero = 0;

        public GuestForm()
        {
            InitializeComponent();
        }

        private void LblPriceRoom_TextChanged(object sender, EventArgs e)
        {
            Roomprice = Convert.ToInt32(lblPriceRoom.Text);
            lblTotal.Text = $"{Roomprice.ToString()}";
            if (lblPriceRoom.Text == "0")
            {
                Roomprice = Convert.ToInt32(lblPriceRoom.Text);
                lblTotal.Text = $"{Roomprice.ToString()}";
            }           
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            panelAddons.Visible = false;
            panelInfo.Visible = true;
        }

        private void btnConfrim_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGuestFName.Text) && string.IsNullOrEmpty(txtGuestLName.Text) && string.IsNullOrEmpty(txtGuestAdd.Text) && string.IsNullOrEmpty(txtGuestEmail.Text) && string.IsNullOrEmpty(txtGuestCNumber.Text) && string.IsNullOrEmpty(cboMofPayment.Text))
                {
                    MessageBox.Show("You can't proceed. Check if you have missed something to fill up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string GFName = txtGuestFName.Text;
                    string GLName = txtGuestLName.Text;
                    string GAdd = txtGuestAdd.Text;
                    string GEmail = txtGuestEmail.Text;
                    int GCNumber = Convert.ToInt32(txtGuestCNumber.Text);
                    string Checkin = checkinpicker.Value.ToString("MM-dd-yyyy");
                    string Checkout = checkoutpicker.Value.ToString("MM-dd-yyyy");
                    string AddOns = $"{lblAdds1.Text} {lblAdds2.Text} {lblAdds3.Text}";
                    double Total = Convert.ToDouble(lblTotal.Text);
                    int NofAdult = Convert.ToInt32(lblNumAdult.Text);
                    int NofChildren = Convert.ToInt32(lblNumChildren.Text);

                    
                    

                    if (roomNumbers == null || !roomTypeIndices.ContainsKey(roomName) || roomTypeIndices[roomName] >= roomNumbers.Length)
                    {
                        MessageBox.Show("No more rooms to display!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int currentRoomNumber = roomNumbers[roomTypeIndices[roomName]];

                    DialogResult addbooking = MessageBox.Show("do you want to add room?", "Add Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (addbooking == DialogResult.No)
                    {
                        MessageBox.Show($"You Booked {lblNameRoom.Text}\nYour room number is {currentRoomNumber}\n\nYou Choose/s this add ons \n{lblAdds1.Text}\n{lblAdds2.Text}\n{lblAdds3.Text}\n\nWith a total price of {lblTotal.Text}");

                        panelBooking.Visible = true;
                        panelInfo.Visible = false;
                        panelAddons.Visible = false;
                        panelImpostor.Visible = true;
                        panelReceipt.Visible = false;
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
                        lblPriceRoom.Text = zero.ToString();
                        lblNumAdult.Text = "0";
                        lblNumChildren.Text = "0";
                        lblCheckIn.Text = "-";
                        lblCheckOut.Text = "-";
                        valAduInc -= valAduInc;
                        valChiInc -= valChiInc;
                        valAdult.Text = valAduInc.ToString();
                        valChildren.Text = valChiInc.ToString();
                        lblTotal.Text = "0";
                    }
                    else
                    {
                        DialogResult ok = MessageBox.Show($"You Booked {lblNameRoom.Text}\nYour room number is {currentRoomNumber}\n\nYou Choose/s this add ons \n{lblAdds1.Text}\n{lblAdds2.Text}\n{lblAdds3.Text}\n\nWith a total price of {lblTotal.Text}", "Receipt", MessageBoxButtons.OK);
                        if (ok == DialogResult.OK)
                        {

                            panelBooking.Visible = true;
                            panelInfo.Visible = false;
                            panelAddons.Visible = false;
                            panelImpostor.Visible = true;
                            panelReceipt.Visible = false;
                            lblAdds1.Text = string.Empty;
                            lblAdds2.Text = string.Empty;
                            lblAdds3.Text = string.Empty;
                            lblAddsPrice1.Text = string.Empty;
                            lblAddsPrice2.Text = string.Empty;
                            lblAddsPrice3.Text = string.Empty;
                            lblPriceRoom.Text = zero.ToString();
                            lblNumAdult.Text = "0";
                            lblNumChildren.Text = "0";
                            lblCheckIn.Text = "-";
                            lblCheckOut.Text = "-";
                            valAduInc -= valAduInc;
                            valChiInc -= valChiInc;
                            valAdult.Text = valAduInc.ToString();
                            valChildren.Text = valChiInc.ToString();
                        }
                    }

                    RemoveAddOnPanels();
                    SharedData.data.Rows.Add(currentRoomNumber, GFName, GLName, GAdd, GEmail, GCNumber, NofAdult, NofChildren, roomName, AddOns, Checkin, Checkout, Total);
                    roomTypeIndices[roomName]++;

                }
            }
            catch
            {
                MessageBox.Show("You Entered Incorrect Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public int[] GenerateRoomSequence(int[] floorStarts, int roomsPerFloor)
        {
            var roomSequence = new System.Collections.Generic.List<int>();

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

        int DaysDiff = 0;

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
            if(valAduInc != 0)
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

        int totalprice = 0;
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
                        
                        lblPriceRoom.Text = totalprice.ToString();
                        
                    }
                    else
                    {
                        totalprice = Rooms.DeluxeKing;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.DeluxeTwin;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.GrandDeluxeKing;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.GrandDeluxeDouble;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.PremiumSuiteKing;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.PremiumSuiteDouble;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.ExecutiveSuiteKing;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.ExecutiveSuiteDouble;
                        lblPriceRoom.Text = totalprice.ToString();
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

                        lblPriceRoom.Text = totalprice.ToString();

                    }
                    else
                    {
                        totalprice = Rooms.PresidentalSuite;
                        lblPriceRoom.Text = totalprice.ToString();
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

            
            lblAddsPrice1.Text = addons.C1.ToString();
            lblAdds1.Text = lblCode1.Text;

            ShowPanel(panelC1);
        }

        private void btnCode2_Click(object sender, EventArgs e)
        {
            panelBorder.Visible = true;

            lblAddsPrice2.Text = $"{addons.C2}";
            lblAdds2.Text = lblCode2.Text;

            ShowPanel(panelC2);
        }

        private void btnCode3_Click(object sender, EventArgs e)
        {
            panelBorder.Visible = true;

            lblAddsPrice3.Text = $"{addons.C3}";
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

            int roomPrice = Convert.ToInt32(lblPriceRoom.Text);
            lblTotal.Text = roomPrice.ToString();
            UpdatePanelLayout();
        }

        private void ShowPanel(Panel panelToShow)
        {
            int total = Convert.ToInt32(lblTotal.Text);
            int panelPrice = GetPanelPrice(panelToShow);

            if (!activePanels.Contains(panelToShow))
            {
                activePanels.Add(panelToShow);
                total += panelPrice; // Add panel price when a panel is added
            }
            else
            {
                activePanels.Remove(panelToShow);
                activePanels.Add(panelToShow); // Keep panel at the end of the list
            }

            lblTotal.Text = total.ToString(); // Update the total price label
            UpdatePanelLayout();
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
                int total = Convert.ToInt32(lblTotal.Text);
                Panel lastPanel = activePanels[activePanels.Count - 1];
                int panelPrice = GetPanelPrice(lastPanel);

                total -= panelPrice; // Subtract the panel's price
                lblTotal.Text = total.ToString(); // Update the label

                activePanels.RemoveAt(activePanels.Count - 1);
                lastPanel.Visible = false;

                UpdatePanelLayout();
            }
        }

        private void btnRemoveRoom_Click(object sender, EventArgs e)
        {
            panelBooking.Visible = true;
            panelAddons.Visible = false;
            panelInfo.Visible = false;
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
    }
}
