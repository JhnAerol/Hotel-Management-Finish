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
    public partial class AdminDataForm : Form
    {
        private string RoomBeforeUpdate;


        public AdminDataForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = SharedData.data;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = true;
            dataGridView1.Columns[13].ReadOnly = true;
            UpdateLabels();
        }

        private void btnGData_Click(object sender, EventArgs e)
        {
            panelData.Visible = true;
            panelAvailableRooms.Visible = false;
        }

        private void btnAvRoom_Click(object sender, EventArgs e)
        {
            panelData.Visible = false;
            panelAvailableRooms.Visible = true;
        }

        public void UpdateLabels()
        {
            lblAvDK.Text = Rooms.DeluxeKingRoom.ToString();
            lblAvDT.Text = Rooms.DeluxeTwinRoom.ToString();
            lblAvGDK.Text = Rooms.GrandDeluxeKingRoom.ToString();
            lblAvGDD.Text = Rooms.GrandDeluxeTwinRoom.ToString();
            lblAvPSK.Text = Rooms.PremiumSuiteKingRoom.ToString();
            lblAvPSD.Text = Rooms.PremiumSuiteDoubleRoom.ToString();
            lblAvESK.Text = Rooms.ExecutiveSuiteKingRoom.ToString();
            lblAvESD.Text = Rooms.ExecutiveSuiteDoubleRoom.ToString();
            lblAvPS.Text = Rooms.PresidentialSuiteRoom.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            
            DialogResult result = MessageBox.Show("Are you sure you want to Remove this row ?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int r = dataGridView1.CurrentCell.RowIndex;
                int c = dataGridView1.CurrentCell.ColumnIndex;
                var value = dataGridView1.Rows[r].Cells[8].Value;
                string cellValue = value?.ToString() ?? string.Empty;

                if (cellValue == "Deluxe King")
                {
                    Rooms.DeluxeKingRoom++;
                }
                else if (cellValue == "Deluxe Twin")
                {
                    Rooms.DeluxeTwinRoom++;
                }
                else if (cellValue == "Grand Deluxe King")
                {
                    Rooms.GrandDeluxeKingRoom++;
                }
                else if (cellValue == "Grand Deluxe Double")
                {
                    Rooms.GrandDeluxeTwinRoom++;
                }
                else if (cellValue == "Premium Suite King")
                {
                    Rooms.PremiumSuiteKingRoom++;
                }
                else if (cellValue == "Premium Suite Double")
                {
                    Rooms.PremiumSuiteDoubleRoom++;
                }
                else if (cellValue == "Executive Suite King")
                {
                    Rooms.ExecutiveSuiteKingRoom++;
                }
                else if (cellValue == "Executive Suite Double")
                {
                    Rooms.ExecutiveSuiteDoubleRoom++;
                }
                else if (cellValue == "Presidential Suite")
                {
                    Rooms.PresidentialSuiteRoom++;
                }

                dataGridView1.Rows.RemoveAt(r);
                UpdateLabels();
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }



        private void Update_Click(object sender, EventArgs e)
        {
            AddOns addOns = new AddOns();
            int r = dataGridView1.CurrentCell.RowIndex;
            int c = dataGridView1.CurrentCell.ColumnIndex;

            GuestForm guestForm = new GuestForm();

            DialogResult result = MessageBox.Show("Are you sure you want to Update this row ?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var valueRoom = dataGridView1.Rows[r].Cells[8].Value;
                var valueAddons = dataGridView1.Rows[r].Cells[9].Value;
                var valueNS = dataGridView1.Rows[r].Cells[12].Value;
                string cellValueRoom = valueRoom?.ToString() ?? string.Empty;
                string cellValueAddons = valueAddons?.ToString() ?? string.Empty;
                string cellValueNS = valueNS?.ToString() ?? string.Empty;

                int roomprice = 0;
                int addsprice = 0;


                if (cellValueRoom == "Deluxe King")
                {
                    roomprice = (int)(Rooms.DeluxeKing * Convert.ToUInt32(cellValueNS));
                    Rooms.DeluxeKingRoom--;
                }
                else if (cellValueRoom == "Deluxe Twin")
                {
                    roomprice = Rooms.DeluxeTwin * Convert.ToInt32(cellValueNS); 
                    Rooms.DeluxeTwinRoom--;
                }
                else if (cellValueRoom == "Grand Deluxe King")
                {
                    roomprice = Rooms.GrandDeluxeKing * Convert.ToInt32(cellValueNS);
                    Rooms.GrandDeluxeKingRoom--;
                }
                else if (cellValueRoom == "Grand Deluxe Double")
                {
                    roomprice = Rooms.GrandDeluxeDouble * Convert.ToInt32(cellValueNS);
                    Rooms.GrandDeluxeTwinRoom--;
                }
                else if (cellValueRoom == "Premium Suite King")
                {
                    roomprice = Rooms.PremiumSuiteKing * Convert.ToInt32(cellValueNS);
                    Rooms.PremiumSuiteKingRoom--;
                }
                else if (cellValueRoom == "Premium Suite Double")
                {
                    roomprice = Rooms.PremiumSuiteDouble * Convert.ToInt32(cellValueNS);
                    Rooms.PremiumSuiteDoubleRoom--;
                }
                else if (cellValueRoom == "Executive Suite King")
                {
                    roomprice = Rooms.ExecutiveSuiteKing * Convert.ToInt32(cellValueNS);
                    Rooms.ExecutiveSuiteKing--;
                }
                else if (cellValueRoom == "Executive Suite Double")
                {
                    roomprice = Rooms.ExecutiveSuiteDouble * Convert.ToInt32(cellValueNS);
                    Rooms.ExecutiveSuiteDoubleRoom--;
                }
                else if (cellValueRoom == "Presidential Suite")
                {
                    roomprice = Rooms.PresidentalSuite * Convert.ToInt32(cellValueNS);
                    Rooms.PresidentialSuiteRoom--;
                }
                else
                {
                    MessageBox.Show("No room available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (cellValueAddons == "C1  ")
                {
                    addsprice = addOns.C1;
                }
                else if (cellValueAddons == " ")
                {
                    addsprice = 0;
                }
                else if (cellValueAddons == "")
                {
                    addsprice = 0;
                }
                else if (cellValueAddons == "  ")
                {
                    addsprice = 0;
                }
                else if (cellValueAddons == "   ")
                {
                    addsprice = 0;
                }
                else if (cellValueAddons == "C1")
                {
                    addsprice = addOns.C1;
                }
                else if (cellValueAddons == "C1 C2 ")
                {
                    addsprice = addOns.C1 + addOns.C2;
                }
                else if (cellValueAddons == "C1 C2")
                {
                    addsprice = addOns.C1 + addOns.C2;
                }
                else if (cellValueAddons == "C1  C3")
                {
                    addsprice = addOns.C1 + addOns.C3;
                }
                else if (cellValueAddons == "C1 C3")
                {
                    addsprice = addOns.C1 + addOns.C3;
                }
                else if (cellValueAddons == "C1 C2 C3")
                {
                    addsprice = addOns.C1 + addOns.C2 + addOns.C3;
                }
                else if (cellValueAddons == "C1 C3 C2")
                {
                    addsprice = addOns.C1 + addOns.C3 + addOns.C2;
                }
                else if (cellValueAddons == " C2 ")
                {
                    addsprice = addOns.C2;
                }
                else if (cellValueAddons == "C2")
                {
                    addsprice = addOns.C2;
                }
                else if (cellValueAddons == "C2 C1")
                {
                    addsprice = addOns.C2 + addOns.C1;
                }
                else if (cellValueAddons == " C2 C3")
                {
                    addsprice = addOns.C2 + addOns.C3;
                }
                else if (cellValueAddons == "C2 C3")
                {
                    addsprice = addOns.C2 + addOns.C3;
                }
                else if (cellValueAddons == "C2 C1 C3")
                {
                    addsprice = addOns.C2 + addOns.C1 + addOns.C3;
                }
                else if (cellValueAddons == "C2 C3 C1")
                {
                    addsprice = addOns.C2 + addOns.C3 + addOns.C1;
                }
                else if (cellValueAddons == "  C3")
                {
                    addsprice = addOns.C3;
                }
                else if (cellValueAddons == "C3")
                {
                    addsprice = addOns.C3;
                }
                else if (cellValueAddons == "C3 C1")
                {
                    addsprice = addOns.C3 + addOns.C1;
                }
                else if (cellValueAddons == "C3 C2")
                {
                    addsprice = addOns.C2 + addOns.C3;
                }
                else if (cellValueAddons == "C3 C1 C2")
                {
                    addsprice = addOns.C2 + addOns.C1 + addOns.C3;
                }
                else if (cellValueAddons == "C3 C2 C1")
                {
                    addsprice = addOns.C2 + addOns.C3 + addOns.C1;
                }
                else
                {
                    MessageBox.Show("No addons available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                double total = roomprice + addsprice;

                SharedData.data.Rows.Add(dataGridView1.Rows[r].Cells[0].Value.ToString(), dataGridView1.Rows[r].Cells[1].Value.ToString(), dataGridView1.Rows[r].Cells[2].Value.ToString(), dataGridView1.Rows[r].Cells[3].Value.ToString(), dataGridView1.Rows[r].Cells[4].Value.ToString(), dataGridView1.Rows[r].Cells[5].Value.ToString(), dataGridView1.Rows[r].Cells[6].Value.ToString(), dataGridView1.Rows[r].Cells[7].Value.ToString(), dataGridView1.Rows[r].Cells[8].Value.ToString(), dataGridView1.Rows[r].Cells[9].Value.ToString(), dataGridView1.Rows[r].Cells[10].Value.ToString(), dataGridView1.Rows[r].Cells[11].Value.ToString(), dataGridView1.Rows[r].Cells[12].Value.ToString(), total);
                
                dataGridView1.Rows.RemoveAt(r);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text;

            if (int.TryParse(filterText, out int roomFilter))
            {
                DataView dataView = SharedData.data.DefaultView;
                dataView.RowFilter = $"CONVERT([Room no#], 'System.String') LIKE '{roomFilter}%'";
                
            }
            else
            {
                DataView dataView = SharedData.data.DefaultView;
                dataView.RowFilter = $"[Guest LName] LIKE '{filterText}%'";
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            RoomBeforeUpdate = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString();
            if (RoomBeforeUpdate == "Deluxe King")
            {
                Rooms.DeluxeKingRoom++;
            }
            if (RoomBeforeUpdate == "Deluxe Twin")
            {
                Rooms.DeluxeTwinRoom++;
            }
            if (RoomBeforeUpdate == "Grand Deluxe King")
            {
                Rooms.GrandDeluxeKingRoom++;
            }
            if (RoomBeforeUpdate == "Grand Deluxe Double")
            {
                Rooms.GrandDeluxeTwinRoom++;
            }
            if (RoomBeforeUpdate == "Executive Suite King")
            {
                Rooms.ExecutiveSuiteKingRoom++;
            }
            if (RoomBeforeUpdate == "Premium Suite King")
            {
                Rooms.PremiumSuiteKingRoom++;
            }
            if (RoomBeforeUpdate == "Premium Suite Double")
            {
                Rooms.PremiumSuiteDoubleRoom++;
            }
            if (RoomBeforeUpdate == "Executive Suite Double")
            {
                Rooms.ExecutiveSuiteDoubleRoom++;
            }
            if (RoomBeforeUpdate == "Presidential Suite")
            { 
                Rooms.PresidentialSuiteRoom++;
            }
        }
    }
}
