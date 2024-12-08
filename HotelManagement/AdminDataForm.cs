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


        public AdminDataForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = SharedData.data;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = true;
            dataGridView1.Columns[12].ReadOnly = true;
            UpdateLabels();
        }

        private void btnGData_Click(object sender, EventArgs e)
        {
            panelImp.Visible = false;
            panelData.Visible = true;
            panelAvailableRooms.Visible = false;
        }

        private void btnAvRoom_Click(object sender, EventArgs e)
        {
            panelImp.Visible = false;
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

            AdminDataForm admin = new AdminDataForm();
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
                admin.UpdateLabels();
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

            DialogResult result = MessageBox.Show("Are you sure you want to Update this row ?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var valueRoom = dataGridView1.Rows[r].Cells[8].Value;
                var valueAddons = dataGridView1.Rows[r].Cells[9].Value;
                string cellValueRoom = valueRoom?.ToString() ?? string.Empty;
                string cellValueAddons = valueAddons?.ToString() ?? string.Empty;

                int roomprice = 0;
                int addsprice = 0;


                if (cellValueRoom == "Deluxe King")
                {
                    roomprice = Rooms.DeluxeKing;
                }
                else if (cellValueRoom == "Deluxe Twin")
                {
                    roomprice = Rooms.DeluxeTwin;
                }
                else if (cellValueRoom == "Grand Deluxe King")
                {
                    roomprice = Rooms.GrandDeluxeKing;
                }
                else if (cellValueRoom == "Grand Deluxe Double")
                {
                    roomprice = Rooms.GrandDeluxeDouble;
                }
                else if (cellValueRoom == "Premium Suite King")
                {
                    roomprice = Rooms.PremiumSuiteKing;
                }
                else if (cellValueRoom == "Premium Suite Double")
                {
                    roomprice = Rooms.PremiumSuiteDouble;
                }
                else if (cellValueRoom == "Executive Suite King")
                {
                    roomprice = Rooms.ExecutiveSuiteKing;
                }
                else if (cellValueRoom == "Executive Suite Double")
                {
                    roomprice = Rooms.ExecutiveSuiteDouble;
                }
                else if (cellValueRoom == "Presidential Suite")
                {
                    roomprice = Rooms.PresidentalSuite;
                }
                else
                {
                    MessageBox.Show("No room available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (cellValueAddons == "C1")
                {
                    addsprice = addOns.C1;
                }
                else if (cellValueAddons == "C1 C2")
                {
                    addsprice = addOns.C1 + addOns.C2;
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
                else if (cellValueAddons == "C2")
                {
                    addsprice = addOns.C2;
                }
                else if (cellValueAddons == "C2 C1")
                {
                    addsprice = addOns.C2 + addOns.C1;
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

                SharedData.data.Rows.Add(dataGridView1.Rows[r].Cells[0].Value.ToString(), dataGridView1.Rows[r].Cells[1].Value.ToString(), dataGridView1.Rows[r].Cells[2].Value.ToString(), dataGridView1.Rows[r].Cells[3].Value.ToString(), dataGridView1.Rows[r].Cells[4].Value.ToString(), dataGridView1.Rows[r].Cells[5].Value.ToString(), dataGridView1.Rows[r].Cells[6].Value.ToString(), dataGridView1.Rows[r].Cells[7].Value.ToString(), dataGridView1.Rows[r].Cells[8].Value.ToString(), dataGridView1.Rows[r].Cells[9].Value.ToString(), dataGridView1.Rows[r].Cells[10].Value.ToString(), dataGridView1.Rows[r].Cells[11].Value.ToString(), total);

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
    }
}
