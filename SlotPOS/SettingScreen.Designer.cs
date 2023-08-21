namespace SlotPOS
{
    partial class SettingScreen
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
            tableLayoutPanel1 = new TableLayoutPanel();
            ButtonStoreInfo = new Button();
            ButtonUserManagement = new Button();
            ButtonReporting = new Button();
            ButtonPromoSetup = new Button();
            ButtonShiftManagement = new Button();
            ButtonTruncate = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.Controls.Add(ButtonStoreInfo, 1, 1);
            tableLayoutPanel1.Controls.Add(ButtonUserManagement, 1, 2);
            tableLayoutPanel1.Controls.Add(ButtonReporting, 1, 3);
            tableLayoutPanel1.Controls.Add(ButtonPromoSetup, 1, 4);
            tableLayoutPanel1.Controls.Add(ButtonShiftManagement, 1, 5);
            tableLayoutPanel1.Controls.Add(ButtonTruncate, 1, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.Size = new Size(1902, 918);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // ButtonStoreInfo
            // 
            ButtonStoreInfo.BackColor = Color.MidnightBlue;
            ButtonStoreInfo.Dock = DockStyle.Fill;
            ButtonStoreInfo.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonStoreInfo.ForeColor = Color.White;
            ButtonStoreInfo.Location = new Point(687, 115);
            ButtonStoreInfo.Margin = new Padding(60, 15, 60, 15);
            ButtonStoreInfo.Name = "ButtonStoreInfo";
            ButtonStoreInfo.Size = new Size(507, 70);
            ButtonStoreInfo.TabIndex = 0;
            ButtonStoreInfo.Text = "Store Information";
            ButtonStoreInfo.UseVisualStyleBackColor = false;
            ButtonStoreInfo.Click += ButtonStoreInfo_Click;
            // 
            // ButtonUserManagement
            // 
            ButtonUserManagement.BackColor = Color.MidnightBlue;
            ButtonUserManagement.Dock = DockStyle.Fill;
            ButtonUserManagement.FlatAppearance.BorderSize = 0;
            ButtonUserManagement.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonUserManagement.ForeColor = Color.White;
            ButtonUserManagement.Location = new Point(687, 215);
            ButtonUserManagement.Margin = new Padding(60, 15, 60, 15);
            ButtonUserManagement.Name = "ButtonUserManagement";
            ButtonUserManagement.Size = new Size(507, 70);
            ButtonUserManagement.TabIndex = 1;
            ButtonUserManagement.Text = "User Management";
            ButtonUserManagement.UseVisualStyleBackColor = false;
            ButtonUserManagement.Click += ButtonUserManagement_Click;
            // 
            // ButtonReporting
            // 
            ButtonReporting.BackColor = Color.MidnightBlue;
            ButtonReporting.Dock = DockStyle.Fill;
            ButtonReporting.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonReporting.ForeColor = Color.White;
            ButtonReporting.Location = new Point(687, 315);
            ButtonReporting.Margin = new Padding(60, 15, 60, 15);
            ButtonReporting.Name = "ButtonReporting";
            ButtonReporting.Size = new Size(507, 70);
            ButtonReporting.TabIndex = 2;
            ButtonReporting.Text = "Reporting";
            ButtonReporting.UseVisualStyleBackColor = false;
            ButtonReporting.Click += ButtonReporting_Click;
            // 
            // ButtonPromoSetup
            // 
            ButtonPromoSetup.BackColor = Color.MidnightBlue;
            ButtonPromoSetup.Dock = DockStyle.Fill;
            ButtonPromoSetup.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonPromoSetup.ForeColor = Color.White;
            ButtonPromoSetup.Location = new Point(687, 415);
            ButtonPromoSetup.Margin = new Padding(60, 15, 60, 15);
            ButtonPromoSetup.Name = "ButtonPromoSetup";
            ButtonPromoSetup.Size = new Size(507, 70);
            ButtonPromoSetup.TabIndex = 3;
            ButtonPromoSetup.Text = "Promo Setup";
            ButtonPromoSetup.UseVisualStyleBackColor = false;
            ButtonPromoSetup.Click += ButtonPromoSetup_Click;
            // 
            // ButtonShiftManagement
            // 
            ButtonShiftManagement.BackColor = Color.MidnightBlue;
            ButtonShiftManagement.Dock = DockStyle.Fill;
            ButtonShiftManagement.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonShiftManagement.ForeColor = Color.White;
            ButtonShiftManagement.Location = new Point(687, 515);
            ButtonShiftManagement.Margin = new Padding(60, 15, 60, 15);
            ButtonShiftManagement.Name = "ButtonShiftManagement";
            ButtonShiftManagement.Size = new Size(507, 70);
            ButtonShiftManagement.TabIndex = 4;
            ButtonShiftManagement.Text = "Shift Management";
            ButtonShiftManagement.UseVisualStyleBackColor = false;
            ButtonShiftManagement.Click += ButtonShiftManagement_Click;
            // 
            // ButtonTruncate
            // 
            ButtonTruncate.BackColor = Color.Snow;
            ButtonTruncate.Dock = DockStyle.Fill;
            ButtonTruncate.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonTruncate.FlatStyle = FlatStyle.Flat;
            ButtonTruncate.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            ButtonTruncate.ForeColor = Color.MidnightBlue;
            ButtonTruncate.Location = new Point(687, 615);
            ButtonTruncate.Margin = new Padding(60, 15, 60, 15);
            ButtonTruncate.Name = "ButtonTruncate";
            ButtonTruncate.Size = new Size(507, 70);
            ButtonTruncate.TabIndex = 5;
            ButtonTruncate.Text = "Truncate the database";
            ButtonTruncate.UseVisualStyleBackColor = false;
            ButtonTruncate.Click += ButtonTruncate_Click;
            // 
            // SettingScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1902, 918);
            Controls.Add(tableLayoutPanel1);
            Name = "SettingScreen";
            Text = "SettingScreen";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button ButtonStoreInfo;
        private Button ButtonUserManagement;
        private Button ButtonReporting;
        private Button ButtonPromoSetup;
        private Button ButtonShiftManagement;
        private Button ButtonTruncate;
    }
}