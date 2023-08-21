namespace SlotPOS
{
    partial class AddNewUser
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
            panel1 = new Panel();
            ButtonAddUser = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            LabelUserName = new Label();
            LabelType = new Label();
            LabelPassword = new Label();
            LabelCnfrmPswd = new Label();
            TextBoxUsername = new TextBox();
            TextBoxPassword = new TextBox();
            TextBoxCnfrmPswd = new TextBox();
            ComboBoxType = new ComboBox();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(ButtonAddUser);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 265);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(180, 30, 180, 55);
            panel1.Size = new Size(519, 125);
            panel1.TabIndex = 0;
            // 
            // ButtonAddUser
            // 
            ButtonAddUser.BackColor = Color.Snow;
            ButtonAddUser.Dock = DockStyle.Fill;
            ButtonAddUser.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonAddUser.FlatStyle = FlatStyle.Flat;
            ButtonAddUser.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonAddUser.Location = new Point(180, 30);
            ButtonAddUser.Name = "ButtonAddUser";
            ButtonAddUser.Size = new Size(159, 40);
            ButtonAddUser.TabIndex = 0;
            ButtonAddUser.Text = "Add User";
            ButtonAddUser.UseVisualStyleBackColor = false;
            ButtonAddUser.Click += ButtonAddUser_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(LabelUserName, 0, 1);
            tableLayoutPanel1.Controls.Add(LabelType, 0, 2);
            tableLayoutPanel1.Controls.Add(LabelPassword, 0, 3);
            tableLayoutPanel1.Controls.Add(LabelCnfrmPswd, 0, 4);
            tableLayoutPanel1.Controls.Add(TextBoxUsername, 1, 1);
            tableLayoutPanel1.Controls.Add(TextBoxPassword, 1, 3);
            tableLayoutPanel1.Controls.Add(TextBoxCnfrmPswd, 1, 4);
            tableLayoutPanel1.Controls.Add(ComboBoxType, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(519, 265);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // LabelUserName
            // 
            LabelUserName.Dock = DockStyle.Fill;
            LabelUserName.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelUserName.Location = new Point(3, 53);
            LabelUserName.Name = "LabelUserName";
            LabelUserName.Size = new Size(253, 53);
            LabelUserName.TabIndex = 0;
            LabelUserName.Text = "Username:";
            LabelUserName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelType
            // 
            LabelType.Dock = DockStyle.Fill;
            LabelType.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelType.Location = new Point(3, 106);
            LabelType.Name = "LabelType";
            LabelType.Size = new Size(253, 53);
            LabelType.TabIndex = 1;
            LabelType.Text = "Type:";
            LabelType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelPassword
            // 
            LabelPassword.Dock = DockStyle.Fill;
            LabelPassword.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelPassword.Location = new Point(3, 159);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new Size(253, 53);
            LabelPassword.TabIndex = 2;
            LabelPassword.Text = "Password:";
            LabelPassword.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelCnfrmPswd
            // 
            LabelCnfrmPswd.Dock = DockStyle.Fill;
            LabelCnfrmPswd.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelCnfrmPswd.Location = new Point(3, 212);
            LabelCnfrmPswd.Name = "LabelCnfrmPswd";
            LabelCnfrmPswd.Size = new Size(253, 53);
            LabelCnfrmPswd.TabIndex = 3;
            LabelCnfrmPswd.Text = "Confirm Password:";
            LabelCnfrmPswd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TextBoxUsername
            // 
            TextBoxUsername.Dock = DockStyle.Fill;
            TextBoxUsername.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxUsername.Location = new Point(274, 63);
            TextBoxUsername.Margin = new Padding(15, 10, 25, 12);
            TextBoxUsername.Name = "TextBoxUsername";
            TextBoxUsername.Size = new Size(220, 27);
            TextBoxUsername.TabIndex = 4;
            TextBoxUsername.KeyPress += TextBoxUsername_KeyPress;
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.Dock = DockStyle.Fill;
            TextBoxPassword.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxPassword.Location = new Point(274, 169);
            TextBoxPassword.Margin = new Padding(15, 10, 25, 12);
            TextBoxPassword.MaxLength = 6;
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.Size = new Size(220, 27);
            TextBoxPassword.TabIndex = 5;
            TextBoxPassword.UseSystemPasswordChar = true;
            TextBoxPassword.KeyPress += TextBoxPassword_KeyPress;
            // 
            // TextBoxCnfrmPswd
            // 
            TextBoxCnfrmPswd.Dock = DockStyle.Fill;
            TextBoxCnfrmPswd.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxCnfrmPswd.Location = new Point(274, 222);
            TextBoxCnfrmPswd.Margin = new Padding(15, 10, 25, 12);
            TextBoxCnfrmPswd.MaxLength = 6;
            TextBoxCnfrmPswd.Name = "TextBoxCnfrmPswd";
            TextBoxCnfrmPswd.Size = new Size(220, 27);
            TextBoxCnfrmPswd.TabIndex = 6;
            TextBoxCnfrmPswd.UseSystemPasswordChar = true;
            TextBoxCnfrmPswd.KeyPress += TextBoxCnfrmPswd_KeyPress;
            // 
            // ComboBoxType
            // 
            ComboBoxType.Dock = DockStyle.Fill;
            ComboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxType.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxType.Items.AddRange(new object[] { "Cashier", "Admin", "Manager" });
            ComboBoxType.Location = new Point(274, 116);
            ComboBoxType.Margin = new Padding(15, 10, 25, 12);
            ComboBoxType.Name = "ComboBoxType";
            ComboBoxType.Size = new Size(220, 29);
            ComboBoxType.TabIndex = 7;
            ComboBoxType.KeyPress += ComboBoxType_KeyPress;
            // 
            // AddNewUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(519, 390);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "AddNewUser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddNewUser";
            TopMost = true;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label LabelUserName;
        private Label LabelType;
        private Label LabelPassword;
        private Label LabelCnfrmPswd;
        private TextBox TextBoxUsername;
        private TextBox TextBoxPassword;
        private TextBox TextBoxCnfrmPswd;
        private ComboBox ComboBoxType;
        private Button ButtonAddUser;
    }
}