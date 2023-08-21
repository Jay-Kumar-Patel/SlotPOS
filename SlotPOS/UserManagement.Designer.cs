namespace SlotPOS
{
    partial class UserManagement
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            ButtonAddNewUser = new Button();
            panel2 = new Panel();
            DataGridViewUsers = new DataGridView();
            txt_user_id = new DataGridViewTextBoxColumn();
            txt_user_name = new DataGridViewTextBoxColumn();
            txt_user_type = new DataGridViewTextBoxColumn();
            btn_user_editPassword = new DataGridViewButtonColumn();
            btn_user_status = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(ButtonAddNewUser);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 438);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 10, 15, 7);
            panel1.Size = new Size(999, 60);
            panel1.TabIndex = 0;
            // 
            // ButtonAddNewUser
            // 
            ButtonAddNewUser.BackColor = Color.White;
            ButtonAddNewUser.Dock = DockStyle.Right;
            ButtonAddNewUser.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonAddNewUser.FlatStyle = FlatStyle.Flat;
            ButtonAddNewUser.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonAddNewUser.Location = new Point(810, 10);
            ButtonAddNewUser.Name = "ButtonAddNewUser";
            ButtonAddNewUser.Size = new Size(174, 43);
            ButtonAddNewUser.TabIndex = 0;
            ButtonAddNewUser.Text = "+ Create New User";
            ButtonAddNewUser.UseVisualStyleBackColor = false;
            ButtonAddNewUser.Click += ButtonAddNewUser_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(DataGridViewUsers);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20);
            panel2.Size = new Size(999, 438);
            panel2.TabIndex = 1;
            // 
            // DataGridViewUsers
            // 
            DataGridViewUsers.AllowUserToAddRows = false;
            DataGridViewUsers.AllowUserToDeleteRows = false;
            DataGridViewUsers.AllowUserToResizeColumns = false;
            DataGridViewUsers.AllowUserToResizeRows = false;
            DataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewUsers.BackgroundColor = Color.White;
            DataGridViewUsers.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewUsers.ColumnHeadersHeight = 50;
            DataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewUsers.Columns.AddRange(new DataGridViewColumn[] { txt_user_id, txt_user_name, txt_user_type, btn_user_editPassword, btn_user_status });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DataGridViewUsers.DefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewUsers.Dock = DockStyle.Fill;
            DataGridViewUsers.EnableHeadersVisualStyles = false;
            DataGridViewUsers.Location = new Point(20, 20);
            DataGridViewUsers.Name = "DataGridViewUsers";
            DataGridViewUsers.ReadOnly = true;
            DataGridViewUsers.RowHeadersVisible = false;
            DataGridViewUsers.RowHeadersWidth = 51;
            DataGridViewUsers.RowTemplate.Height = 29;
            DataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewUsers.ShowCellToolTips = false;
            DataGridViewUsers.ShowEditingIcon = false;
            DataGridViewUsers.ShowRowErrors = false;
            DataGridViewUsers.Size = new Size(959, 398);
            DataGridViewUsers.TabIndex = 0;
            DataGridViewUsers.CellClick += DataGridViewUsers_CellClick;
            DataGridViewUsers.CellFormatting += DataGridViewUsers_CellFormatting;
            // 
            // txt_user_id
            // 
            txt_user_id.HeaderText = "User Id";
            txt_user_id.MinimumWidth = 6;
            txt_user_id.Name = "txt_user_id";
            txt_user_id.ReadOnly = true;
            txt_user_id.Resizable = DataGridViewTriState.False;
            txt_user_id.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_user_name
            // 
            txt_user_name.HeaderText = "User Name";
            txt_user_name.MinimumWidth = 6;
            txt_user_name.Name = "txt_user_name";
            txt_user_name.ReadOnly = true;
            txt_user_name.Resizable = DataGridViewTriState.False;
            txt_user_name.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_user_type
            // 
            txt_user_type.HeaderText = "User Type";
            txt_user_type.MinimumWidth = 6;
            txt_user_type.Name = "txt_user_type";
            txt_user_type.ReadOnly = true;
            txt_user_type.Resizable = DataGridViewTriState.False;
            txt_user_type.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_user_editPassword
            // 
            btn_user_editPassword.HeaderText = "Edit Password";
            btn_user_editPassword.MinimumWidth = 6;
            btn_user_editPassword.Name = "btn_user_editPassword";
            btn_user_editPassword.ReadOnly = true;
            // 
            // btn_user_status
            // 
            btn_user_status.HeaderText = "Status";
            btn_user_status.MinimumWidth = 6;
            btn_user_status.Name = "btn_user_status";
            btn_user_status.ReadOnly = true;
            // 
            // UserManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(999, 498);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "UserManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UserManagement";
            TopMost = true;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button ButtonAddNewUser;
        private Panel panel2;
        private DataGridView DataGridViewUsers;
        private DataGridViewTextBoxColumn txt_user_id;
        private DataGridViewTextBoxColumn txt_user_name;
        private DataGridViewTextBoxColumn txt_user_type;
        private DataGridViewButtonColumn btn_user_editPassword;
        private DataGridViewButtonColumn btn_user_status;
    }
}