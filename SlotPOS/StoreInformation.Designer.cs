namespace SlotPOS
{
    partial class StoreInformation
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
            PanelFooter = new Panel();
            btn_confirm_storeDetails = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            TextBoxName = new TextBox();
            TextBoxAddress = new TextBox();
            TextBoxCity = new TextBox();
            TextBoxState = new TextBox();
            TextBoxZipCode = new TextBox();
            DateTimePickerCounting = new DateTimePicker();
            PanelFooter.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // PanelFooter
            // 
            PanelFooter.Controls.Add(btn_confirm_storeDetails);
            PanelFooter.Dock = DockStyle.Bottom;
            PanelFooter.Location = new Point(0, 525);
            PanelFooter.Name = "PanelFooter";
            PanelFooter.Padding = new Padding(330, 35, 330, 40);
            PanelFooter.Size = new Size(890, 125);
            PanelFooter.TabIndex = 0;
            // 
            // btn_confirm_storeDetails
            // 
            btn_confirm_storeDetails.BackColor = Color.White;
            btn_confirm_storeDetails.Dock = DockStyle.Fill;
            btn_confirm_storeDetails.FlatAppearance.BorderColor = Color.MidnightBlue;
            btn_confirm_storeDetails.FlatStyle = FlatStyle.Flat;
            btn_confirm_storeDetails.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            btn_confirm_storeDetails.ForeColor = Color.Black;
            btn_confirm_storeDetails.Location = new Point(330, 35);
            btn_confirm_storeDetails.Margin = new Padding(30);
            btn_confirm_storeDetails.Name = "btn_confirm_storeDetails";
            btn_confirm_storeDetails.Size = new Size(230, 50);
            btn_confirm_storeDetails.TabIndex = 0;
            btn_confirm_storeDetails.Text = "Update Details";
            btn_confirm_storeDetails.UseVisualStyleBackColor = false;
            btn_confirm_storeDetails.Click += btn_confirm_storeDetails_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(label4, 0, 4);
            tableLayoutPanel1.Controls.Add(label5, 0, 5);
            tableLayoutPanel1.Controls.Add(label6, 0, 6);
            tableLayoutPanel1.Controls.Add(TextBoxName, 1, 1);
            tableLayoutPanel1.Controls.Add(TextBoxAddress, 1, 2);
            tableLayoutPanel1.Controls.Add(TextBoxCity, 1, 3);
            tableLayoutPanel1.Controls.Add(TextBoxState, 1, 4);
            tableLayoutPanel1.Controls.Add(TextBoxZipCode, 1, 5);
            tableLayoutPanel1.Controls.Add(DateTimePickerCounting, 1, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Size = new Size(890, 525);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(3, 75);
            label1.Name = "label1";
            label1.Size = new Size(261, 75);
            label1.TabIndex = 0;
            label1.Text = "Store Name:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(3, 170);
            label2.Margin = new Padding(3, 20, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(261, 55);
            label2.TabIndex = 1;
            label2.Text = "Street Address:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(3, 225);
            label3.Name = "label3";
            label3.Size = new Size(261, 75);
            label3.TabIndex = 2;
            label3.Text = "City:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label4.Location = new Point(3, 300);
            label4.Name = "label4";
            label4.Size = new Size(261, 75);
            label4.TabIndex = 3;
            label4.Text = "State:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label5.Location = new Point(3, 375);
            label5.Name = "label5";
            label5.Size = new Size(261, 75);
            label5.TabIndex = 4;
            label5.Text = "Zip Code:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label6.Location = new Point(3, 450);
            label6.Name = "label6";
            label6.Size = new Size(261, 75);
            label6.TabIndex = 5;
            label6.Text = "Counting:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TextBoxName
            // 
            TextBoxName.Dock = DockStyle.Fill;
            TextBoxName.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxName.Location = new Point(287, 95);
            TextBoxName.Margin = new Padding(20, 20, 120, 15);
            TextBoxName.MaxLength = 256;
            TextBoxName.Name = "TextBoxName";
            TextBoxName.Size = new Size(483, 27);
            TextBoxName.TabIndex = 6;
            TextBoxName.KeyPress += TextBoxName_KeyPress;
            // 
            // TextBoxAddress
            // 
            TextBoxAddress.Dock = DockStyle.Fill;
            TextBoxAddress.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxAddress.Location = new Point(287, 170);
            TextBoxAddress.Margin = new Padding(20, 20, 120, 15);
            TextBoxAddress.MaxLength = 256;
            TextBoxAddress.Name = "TextBoxAddress";
            TextBoxAddress.Size = new Size(483, 27);
            TextBoxAddress.TabIndex = 7;
            TextBoxAddress.KeyPress += TextBoxAddress_KeyPress;
            // 
            // TextBoxCity
            // 
            TextBoxCity.Dock = DockStyle.Fill;
            TextBoxCity.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxCity.Location = new Point(287, 245);
            TextBoxCity.Margin = new Padding(20, 20, 120, 15);
            TextBoxCity.MaxLength = 100;
            TextBoxCity.Name = "TextBoxCity";
            TextBoxCity.Size = new Size(483, 27);
            TextBoxCity.TabIndex = 8;
            TextBoxCity.KeyPress += TextBoxCity_KeyPress;
            // 
            // TextBoxState
            // 
            TextBoxState.Dock = DockStyle.Fill;
            TextBoxState.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxState.Location = new Point(287, 320);
            TextBoxState.Margin = new Padding(20, 20, 120, 15);
            TextBoxState.MaxLength = 100;
            TextBoxState.Name = "TextBoxState";
            TextBoxState.Size = new Size(483, 27);
            TextBoxState.TabIndex = 9;
            TextBoxState.KeyPress += TextBoxState_KeyPress;
            // 
            // TextBoxZipCode
            // 
            TextBoxZipCode.Dock = DockStyle.Fill;
            TextBoxZipCode.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxZipCode.Location = new Point(287, 395);
            TextBoxZipCode.Margin = new Padding(20, 20, 120, 15);
            TextBoxZipCode.MaxLength = 9;
            TextBoxZipCode.Name = "TextBoxZipCode";
            TextBoxZipCode.Size = new Size(483, 27);
            TextBoxZipCode.TabIndex = 10;
            TextBoxZipCode.KeyPress += TextBoxZipCode_KeyPress;
            // 
            // DateTimePickerCounting
            // 
            DateTimePickerCounting.Dock = DockStyle.Left;
            DateTimePickerCounting.Location = new Point(287, 470);
            DateTimePickerCounting.Margin = new Padding(20, 20, 500, 15);
            DateTimePickerCounting.Name = "DateTimePickerCounting";
            DateTimePickerCounting.Size = new Size(103, 27);
            DateTimePickerCounting.TabIndex = 11;
            // 
            // StoreInformation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(890, 650);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(PanelFooter);
            Name = "StoreInformation";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StoreInformation";
            TopMost = true;
            PanelFooter.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelFooter;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_confirm_storeDetails;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox TextBoxName;
        private TextBox TextBoxAddress;
        private TextBox TextBoxCity;
        private TextBox TextBoxState;
        private TextBox TextBoxZipCode;
        private DateTimePicker DateTimePickerCounting;
    }
}