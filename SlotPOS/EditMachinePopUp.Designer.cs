namespace SlotPOS
{
    partial class EditMachinePopUp
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
            ButtonEdit = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            TextBoxStaticIP = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            LabelMachineNumber = new Label();
            TextBoxGameName = new TextBox();
            ComboBoxGroup = new ComboBox();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(ButtonEdit);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 325);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(230, 30, 230, 50);
            panel1.Size = new Size(600, 125);
            panel1.TabIndex = 0;
            // 
            // ButtonEdit
            // 
            ButtonEdit.BackColor = Color.Snow;
            ButtonEdit.Dock = DockStyle.Fill;
            ButtonEdit.FlatStyle = FlatStyle.Flat;
            ButtonEdit.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonEdit.ForeColor = Color.MidnightBlue;
            ButtonEdit.Location = new Point(230, 30);
            ButtonEdit.Name = "ButtonEdit";
            ButtonEdit.Size = new Size(140, 45);
            ButtonEdit.TabIndex = 0;
            ButtonEdit.Text = "Edit";
            ButtonEdit.UseVisualStyleBackColor = false;
            ButtonEdit.Click += ButtonEdit_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(TextBoxStaticIP, 1, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(LabelMachineNumber, 1, 0);
            tableLayoutPanel1.Controls.Add(TextBoxGameName, 1, 1);
            tableLayoutPanel1.Controls.Add(ComboBoxGroup, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(600, 325);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // TextBoxStaticIP
            // 
            TextBoxStaticIP.Dock = DockStyle.Left;
            TextBoxStaticIP.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxStaticIP.Location = new Point(320, 182);
            TextBoxStaticIP.Margin = new Padding(20, 20, 3, 20);
            TextBoxStaticIP.Name = "TextBoxStaticIP";
            TextBoxStaticIP.Size = new Size(200, 27);
            TextBoxStaticIP.TabIndex = 7;
            TextBoxStaticIP.TextAlign = HorizontalAlignment.Center;
            TextBoxStaticIP.TextChanged += TextBoxStaticIP_TextChanged;
            TextBoxStaticIP.KeyPress += TextBoxStaticIP_KeyPress;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(294, 81);
            label1.TabIndex = 0;
            label1.Text = "Machine Number :";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(3, 81);
            label2.Name = "label2";
            label2.Size = new Size(294, 81);
            label2.TabIndex = 1;
            label2.Text = "Game Name :";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(3, 162);
            label3.Name = "label3";
            label3.Size = new Size(294, 81);
            label3.TabIndex = 2;
            label3.Text = "Mac Address:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label4.Location = new Point(3, 243);
            label4.Name = "label4";
            label4.Size = new Size(294, 82);
            label4.TabIndex = 3;
            label4.Text = "Group Name :";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelMachineNumber
            // 
            LabelMachineNumber.Dock = DockStyle.Fill;
            LabelMachineNumber.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelMachineNumber.ForeColor = SystemColors.ActiveCaptionText;
            LabelMachineNumber.Location = new Point(320, 0);
            LabelMachineNumber.Margin = new Padding(20, 0, 3, 0);
            LabelMachineNumber.Name = "LabelMachineNumber";
            LabelMachineNumber.Size = new Size(277, 81);
            LabelMachineNumber.TabIndex = 4;
            LabelMachineNumber.Text = "N/A";
            LabelMachineNumber.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBoxGameName
            // 
            TextBoxGameName.Dock = DockStyle.Left;
            TextBoxGameName.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxGameName.Location = new Point(320, 101);
            TextBoxGameName.Margin = new Padding(20, 20, 3, 20);
            TextBoxGameName.Name = "TextBoxGameName";
            TextBoxGameName.Size = new Size(200, 27);
            TextBoxGameName.TabIndex = 5;
            TextBoxGameName.TextAlign = HorizontalAlignment.Center;
            TextBoxGameName.KeyPress += TextBoxGameName_KeyPress;
            // 
            // ComboBoxGroup
            // 
            ComboBoxGroup.Dock = DockStyle.Left;
            ComboBoxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxGroup.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxGroup.FormattingEnabled = true;
            ComboBoxGroup.Location = new Point(320, 271);
            ComboBoxGroup.Margin = new Padding(20, 28, 3, 3);
            ComboBoxGroup.Name = "ComboBoxGroup";
            ComboBoxGroup.Size = new Size(200, 29);
            ComboBoxGroup.TabIndex = 6;
            // 
            // EditMachinePopUp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(600, 450);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "EditMachinePopUp";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EditMachinePopUp";
            TopMost = true;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button ButtonEdit;
        private TextBox TextBoxStaticIP;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label LabelMachineNumber;
        private TextBox TextBoxGameName;
        private ComboBox ComboBoxGroup;
    }
}