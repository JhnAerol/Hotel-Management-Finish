namespace HotelManagement
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBookNow = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picb = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnLoginAdmin = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBookNow
            // 
            this.btnBookNow.Animated = true;
            this.btnBookNow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBookNow.BorderColor = System.Drawing.Color.Transparent;
            this.btnBookNow.BorderRadius = 9;
            this.btnBookNow.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBookNow.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBookNow.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBookNow.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBookNow.FillColor = System.Drawing.Color.Black;
            this.btnBookNow.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnBookNow.ForeColor = System.Drawing.Color.White;
            this.btnBookNow.Location = new System.Drawing.Point(1344, 28);
            this.btnBookNow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBookNow.Name = "btnBookNow";
            this.btnBookNow.Size = new System.Drawing.Size(179, 48);
            this.btnBookNow.TabIndex = 3;
            this.btnBookNow.Text = "BOOK NOW";
            this.btnBookNow.Click += new System.EventHandler(this.btnBookNow_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.picb);
            this.panel1.Location = new System.Drawing.Point(0, 137);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1549, 761);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 189);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(558, 294);
            this.label1.TabIndex = 2;
            this.label1.Text = "You\'re in \r\nSafe hands.";
            // 
            // picb
            // 
            this.picb.BackColor = System.Drawing.Color.Transparent;
            this.picb.FillColor = System.Drawing.Color.Transparent;
            this.picb.Image = global::HotelManagement.Properties.Resources.Deluxe_Twin;
            this.picb.ImageRotate = 0F;
            this.picb.Location = new System.Drawing.Point(4, 4);
            this.picb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picb.Name = "picb";
            this.picb.Size = new System.Drawing.Size(1543, 757);
            this.picb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb.TabIndex = 1;
            this.picb.TabStop = false;
            this.picb.UseTransparentBackground = true;
            // 
            // btnLoginAdmin
            // 
            this.btnLoginAdmin.BackColor = System.Drawing.Color.White;
            this.btnLoginAdmin.BorderColor = System.Drawing.Color.Transparent;
            this.btnLoginAdmin.BorderRadius = 9;
            this.btnLoginAdmin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoginAdmin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoginAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoginAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoginAdmin.FillColor = System.Drawing.Color.Transparent;
            this.btnLoginAdmin.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginAdmin.ForeColor = System.Drawing.Color.Black;
            this.btnLoginAdmin.Location = new System.Drawing.Point(1344, 87);
            this.btnLoginAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoginAdmin.Name = "btnLoginAdmin";
            this.btnLoginAdmin.Size = new System.Drawing.Size(179, 32);
            this.btnLoginAdmin.TabIndex = 5;
            this.btnLoginAdmin.Text = "LOGIN AS ADMIN";
            this.btnLoginAdmin.Click += new System.EventHandler(this.btnLoginAdmin_Click);
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = global::HotelManagement.Properties.Resources.image_removebg_preview;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(745, 25);
            this.guna2PictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(136, 89);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox2.TabIndex = 6;
            this.guna2PictureBox2.TabStop = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1551, 898);
            this.Controls.Add(this.guna2PictureBox2);
            this.Controls.Add(this.btnLoginAdmin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBookNow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnBookNow;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2PictureBox picb;
        private Guna.UI2.WinForms.Guna2Button btnLoginAdmin;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label1;
    }
}