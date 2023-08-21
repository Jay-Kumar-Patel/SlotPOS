namespace SlotPOS
{
    partial class Reporting
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
            ButtonAddEmail = new Button();
            panel2 = new Panel();
            DataGridViewReporting = new DataGridView();
            txt_user_email = new DataGridViewTextBoxColumn();
            btn_user_isDailyReport = new DataGridViewCheckBoxColumn();
            btn_user_isShiftReport = new DataGridViewCheckBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewReporting).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(ButtonAddEmail);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 390);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 10, 10, 10);
            panel1.Size = new Size(640, 60);
            panel1.TabIndex = 0;
            // 
            // ButtonAddEmail
            // 
            ButtonAddEmail.BackColor = Color.Snow;
            ButtonAddEmail.Dock = DockStyle.Right;
            ButtonAddEmail.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonAddEmail.FlatStyle = FlatStyle.Flat;
            ButtonAddEmail.Font = new Font("Bookman Old Style", 9F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonAddEmail.Location = new Point(526, 10);
            ButtonAddEmail.Name = "ButtonAddEmail";
            ButtonAddEmail.Size = new Size(104, 40);
            ButtonAddEmail.TabIndex = 0;
            ButtonAddEmail.Text = "+ E-mail";
            ButtonAddEmail.UseVisualStyleBackColor = false;
            ButtonAddEmail.Click += ButtonAddEmail_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(DataGridViewReporting);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20);
            panel2.Size = new Size(640, 390);
            panel2.TabIndex = 1;
            // 
            // DataGridViewReporting
            // 
            DataGridViewReporting.AllowUserToAddRows = false;
            DataGridViewReporting.AllowUserToDeleteRows = false;
            DataGridViewReporting.AllowUserToResizeColumns = false;
            DataGridViewReporting.AllowUserToResizeRows = false;
            DataGridViewReporting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewReporting.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 10F, FontStyle.Italic, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataGridViewReporting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewReporting.ColumnHeadersHeight = 50;
            DataGridViewReporting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewReporting.Columns.AddRange(new DataGridViewColumn[] { txt_user_email, btn_user_isDailyReport, btn_user_isShiftReport });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewReporting.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewReporting.Dock = DockStyle.Fill;
            DataGridViewReporting.EnableHeadersVisualStyles = false;
            DataGridViewReporting.Location = new Point(20, 20);
            DataGridViewReporting.Name = "DataGridViewReporting";
            DataGridViewReporting.RowHeadersVisible = false;
            DataGridViewReporting.RowHeadersWidth = 51;
            DataGridViewReporting.RowTemplate.Height = 29;
            DataGridViewReporting.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewReporting.Size = new Size(600, 350);
            DataGridViewReporting.TabIndex = 0;
            DataGridViewReporting.CellContentClick += DataGridViewReporting_CellContentClick;
            // 
            // txt_user_email
            // 
            txt_user_email.HeaderText = "Email-ID";
            txt_user_email.MinimumWidth = 6;
            txt_user_email.Name = "txt_user_email";
            txt_user_email.ReadOnly = true;
            txt_user_email.Resizable = DataGridViewTriState.False;
            // 
            // btn_user_isDailyReport
            // 
            btn_user_isDailyReport.FalseValue = "";
            btn_user_isDailyReport.HeaderText = "Daily Report";
            btn_user_isDailyReport.MinimumWidth = 6;
            btn_user_isDailyReport.Name = "btn_user_isDailyReport";
            btn_user_isDailyReport.TrueValue = "";
            // 
            // btn_user_isShiftReport
            // 
            btn_user_isShiftReport.FalseValue = "";
            btn_user_isShiftReport.HeaderText = "Shift Report";
            btn_user_isShiftReport.MinimumWidth = 6;
            btn_user_isShiftReport.Name = "btn_user_isShiftReport";
            btn_user_isShiftReport.TrueValue = "";
            // 
            // Reporting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(640, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Reporting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporting";
            TopMost = true;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewReporting).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button ButtonAddEmail;
        private Panel panel2;
        private DataGridView DataGridViewReporting;
        private DataGridViewTextBoxColumn txt_user_email;
        private DataGridViewCheckBoxColumn btn_user_isDailyReport;
        private DataGridViewCheckBoxColumn btn_user_isShiftReport;
    }
}