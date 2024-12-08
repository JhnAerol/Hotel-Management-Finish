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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Focus();
        }
        private string ID = "23-2024";
        private string pass = "123456";
        bool isFormOpen = false;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            

            if (txtAdminID.Text == ID || txtAdminPassword.Text == pass)
            {
                if (txtAdminPassword.Text == pass && txtAdminID.Text == ID)
                {
                    txtAdminID.Clear();
                    txtAdminPassword.Clear();
                    AdminDataForm adminForm = new AdminDataForm();
                    adminForm.Show();
                    this.Hide();
                }
                else if (txtAdminPassword.Text == pass && txtAdminID.Text != ID)
                {
                    txtAdminID.ForeColor = Color.Red;
                    txtAdminID.Text = "Incorrect ID";
                }
                else
                {
                    txtAdminPassword.ForeColor = Color.Red;
                    txtAdminPassword.Text = "Incorrect Password";
                    if (txtAdminPassword.Text == "Incorrect Password")
                    {
                        txtAdminPassword.PasswordChar = '\0';
                    }
                }
            }
            else
            {

                if (txtAdminID.Text == "Enter ID here..." || txtAdminPassword.Text == "Enter Password here...")
                {
                    if (txtAdminPassword.Text == "Enter Password here..." && txtAdminID.Text == "Enter ID here...")
                    {
                        txtAdminPassword.ForeColor = Color.Red;
                        txtAdminPassword.Text = "Input Password";
                        txtAdminID.ForeColor = Color.Red;
                        txtAdminID.Text = "Input ID";
                    }
                    else if (txtAdminPassword.Text == "Enter Password here..." && txtAdminID.Text != "Enter ID here...")
                    {
                        txtAdminID.ForeColor = Color.Red;
                        txtAdminID.Text = "Input ID";
                    }
                    else if (txtAdminID.Text == "Enter ID here..." && txtAdminPassword.Text != pass)
                    {
                        txtAdminID.ForeColor = Color.Red;
                        txtAdminID.Text = "Input ID";
                        txtAdminPassword.ForeColor = Color.Red;
                        txtAdminPassword.Text = "Incorrect Password";
                        if (txtAdminPassword.Text == "Incorrect Password")
                        {
                            txtAdminPassword.PasswordChar = '\0';
                        }
                    }
                    else
                    {
                        txtAdminPassword.ForeColor = Color.Red;
                        txtAdminPassword.Text = "Input Password";
                        if (txtAdminPassword.Text == "Input Password")
                        {
                            txtAdminPassword.PasswordChar = '\0';
                        }
                    }
                }
                else
                {
                    if (txtAdminID.Text == "Input ID")
                    {
                        txtAdminID.Text = "Input ID";
                        txtAdminID.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtAdminID.ForeColor = Color.Red;
                        txtAdminID.Text = "Incorrect ID";
                    }

                    if (txtAdminPassword.Text == "Input Password")
                    {
                        txtAdminPassword.Text = "Input Password";
                        txtAdminPassword.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtAdminPassword.ForeColor = Color.Red;
                        txtAdminPassword.Text = "Incorrect Password";
                    }

                    if (txtAdminPassword.Text == "Incorrect Password")
                    {
                        txtAdminPassword.PasswordChar = '\0';
                    }
                }
            }
        }

        private void ShowPassword_Click(object sender, EventArgs e)
        {
            
            HidePassword.Visible = true;
            ShowPassword.Visible = false;

            if (HidePassword.Visible == true)
            {
                if (HidePassword.Visible == true && txtAdminID.Text != "Enter ID here...")
                {
                    txtAdminID.ForeColor = Color.Black;
                }
                else if (txtAdminID.Text == "Incorrect ID")
                {
                    txtAdminID.ForeColor = Color.Red;
                }
                else
                {
                    txtAdminID.ForeColor = Color.DarkGray;
                }
                txtAdminPassword.PasswordChar = '\0';
            }

            

            if (txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == "Incorrect ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }
            if (txtAdminPassword.Text == "Input Password" && txtAdminID.Text == "Input ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == "Input Password" && txtAdminID.Text == ID)
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Black;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }
            if (txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == ID)
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Black;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == pass && txtAdminID.Text == "Incorrect ID")
            {
                txtAdminPassword.ForeColor = Color.Black;
                txtAdminID.ForeColor = Color.Red;
            }
            if (txtAdminPassword.Text == pass && txtAdminID.Text == "Input ID")
            {
                txtAdminPassword.ForeColor = Color.Black;
                txtAdminID.ForeColor = Color.Red;
            }

            if (txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == "Input ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == "Input Password" && txtAdminID.Text == "Incorrect ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }
        }
        //password
        private void HidePassword_Click(object sender, EventArgs e)
        {
            HidePassword.Visible = false;
            ShowPassword.Visible = true;

            txtAdminPassword.PasswordChar = '•';
            txtAdminPassword.ForeColor = Color.Black;

            if (txtAdminPassword.Text == "Enter Password here...")
            {
                txtAdminPassword.Text = "Enter Password here...";
                txtAdminPassword.PasswordChar = '\0';
                txtAdminPassword.ForeColor = Color.DarkGray;
            }

            if(txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == "Incorrect ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if(txtAdminPassword.Text == "Input Password" && txtAdminID.Text == ID)
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Black;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }
            if (txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == ID)
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Black;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == "Input Password" && txtAdminID.Text == "Input ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == "Incorrect Password" && txtAdminID.Text == "Input ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Incorrect Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }

            if (txtAdminPassword.Text == "Input Password" && txtAdminID.Text == "Incorrect ID")
            {
                txtAdminPassword.ForeColor = Color.Red;
                txtAdminID.ForeColor = Color.Red;
                if (txtAdminPassword.Text == "Input Password")
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
            }
        }

        private void txtAdminID_Leave(object sender, EventArgs e)
        {
            if (txtAdminID.Text == "")
            {
                txtAdminID.Text = "Enter ID here...";
                txtAdminID.ForeColor = Color.DarkGray;
            }
            if (txtAdminID.Text == "Incorrect ID")
            {
                txtAdminID.ForeColor = Color.Red;
            }
            if (txtAdminID.Text == "Input ID")
            {
                txtAdminID.ForeColor = Color.Red;
            }
        }

        private void txtAdminID_Enter(object sender, EventArgs e)
        {
            if (txtAdminID.Text == "Enter ID here...")
            {
                txtAdminID.Text = "";
                txtAdminID.ForeColor = Color.Black;
            }
            if (txtAdminID.Text == "Incorrect ID")
            {
                txtAdminID.Text = "";
                txtAdminID.ForeColor = Color.Black;
            }
            if (txtAdminID.Text == "Input ID")
            {
                txtAdminID.Text = "";
                txtAdminID.ForeColor = Color.Black;
            }
        }

        private void txtAdminPassword_Enter(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "Enter Password here...")
            {
                txtAdminPassword.Text = "";
                if (HidePassword.Visible == true)
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
                else
                {
                    txtAdminPassword.PasswordChar = '•';
                }
                txtAdminPassword.ForeColor = Color.Black;
            }
            if (txtAdminPassword.Text == "Incorrect Password")
            {
               txtAdminPassword.Text = "";
                if (HidePassword.Visible == true)
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
                else
                {
                    txtAdminPassword.PasswordChar = '•';
                }
                txtAdminPassword.ForeColor = Color.Black;
            }
            if (txtAdminPassword.Text == "Input Password")
            {
                txtAdminPassword.Text = "";
                if (HidePassword.Visible == true)
                {
                    txtAdminPassword.PasswordChar = '\0';
                }
                else
                {
                    txtAdminPassword.PasswordChar = '•';
                }
                txtAdminPassword.ForeColor = Color.Black;
            }
        }

        private void txtAdminPassword_Leave(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "")
            {
                txtAdminPassword.Text = "Enter Password here...";
                txtAdminPassword.PasswordChar = '\0';
                txtAdminPassword.ForeColor = Color.DarkGray;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
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
