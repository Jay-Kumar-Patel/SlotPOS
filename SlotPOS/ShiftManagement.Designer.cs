namespace SlotPOS
{
    partial class ShiftManagement
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            DataGridViewShift = new DataGridView();
            txt_shift_Id = new DataGridViewTextBoxColumn();
            txt_loginId = new DataGridViewTextBoxColumn();
            txt_username = new DataGridViewTextBoxColumn();
            txt_startTime = new DataGridViewTextBoxColumn();
            txt_btn_endShift = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewShift).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.AliceBlue;
            panel1.Controls.Add(DataGridViewShift);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(20);
            panel1.Size = new Size(859, 453);
            panel1.TabIndex = 0;
            // 
            // DataGridViewShift
            // 
            DataGridViewShift.AllowUserToAddRows = false;
            DataGridViewShift.AllowUserToDeleteRows = false;
            DataGridViewShift.AllowUserToResizeColumns = false;
            DataGridViewShift.AllowUserToResizeRows = false;
            DataGridViewShift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewShift.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataGridViewShift.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewShift.ColumnHeadersHeight = 50;
            DataGridViewShift.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewShift.Columns.AddRange(new DataGridViewColumn[] { txt_shift_Id, txt_loginId, txt_username, txt_startTime, txt_btn_endShift });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewShift.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewShift.Dock = DockStyle.Fill;
            DataGridViewShift.EnableHeadersVisualStyles = false;
            DataGridViewShift.Location = new Point(20, 20);
            DataGridViewShift.Name = "DataGridViewShift";
            DataGridViewShift.ReadOnly = true;
            DataGridViewShift.RowHeadersVisible = false;
            DataGridViewShift.RowHeadersWidth = 51;
            DataGridViewShift.RowTemplate.Height = 29;
            DataGridViewShift.Size = new Size(819, 413);
            DataGridViewShift.TabIndex = 0;
            DataGridViewShift.CellContentClick += DataGridViewShift_CellContentClick;
            DataGridViewShift.CellFormatting += DataGridViewShift_CellFormatting;
            // 
            // txt_shift_Id
            // 
            txt_shift_Id.HeaderText = "Shift_ID";
            txt_shift_Id.MinimumWidth = 6;
            txt_shift_Id.Name = "txt_shift_Id";
            txt_shift_Id.ReadOnly = true;
            // 
            // txt_loginId
            // 
            txt_loginId.HeaderText = "Login_ID";
            txt_loginId.MinimumWidth = 6;
            txt_loginId.Name = "txt_loginId";
            txt_loginId.ReadOnly = true;
            txt_loginId.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_username
            // 
            txt_username.HeaderText = "Username";
            txt_username.MinimumWidth = 6;
            txt_username.Name = "txt_username";
            txt_username.ReadOnly = true;
            txt_username.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_startTime
            // 
            txt_startTime.HeaderText = "Starting Time";
            txt_startTime.MinimumWidth = 6;
            txt_startTime.Name = "txt_startTime";
            txt_startTime.ReadOnly = true;
            txt_startTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_btn_endShift
            // 
            txt_btn_endShift.HeaderText = "Click Here";
            txt_btn_endShift.MinimumWidth = 6;
            txt_btn_endShift.Name = "txt_btn_endShift";
            txt_btn_endShift.ReadOnly = true;
            // 
            // ShiftManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 453);
            Controls.Add(panel1);
            Name = "ShiftManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShiftManagement";
            TopMost = true;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewShift).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView DataGridViewShift;
        private DataGridViewTextBoxColumn txt_loginId;
        private DataGridViewTextBoxColumn txt_username;
        private DataGridViewTextBoxColumn txt_startTime;
        private DataGridViewButtonColumn txt_btn_endShift;
        private DataGridViewTextBoxColumn txt_shift_Id;
    }
}