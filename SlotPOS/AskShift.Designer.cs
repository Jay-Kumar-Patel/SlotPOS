namespace SlotPOS
{
    partial class AskShift
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
            LabelAmount = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ButtonContinue = new Button();
            ButtonEndShift = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(LabelAmount);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 125);
            panel1.TabIndex = 0;
            // 
            // LabelAmount
            // 
            LabelAmount.Dock = DockStyle.Fill;
            LabelAmount.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            LabelAmount.Location = new Point(0, 0);
            LabelAmount.Name = "LabelAmount";
            LabelAmount.Size = new Size(500, 125);
            LabelAmount.TabIndex = 3;
            LabelAmount.Text = "Active Shift";
            LabelAmount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(ButtonContinue, 1, 0);
            tableLayoutPanel1.Controls.Add(ButtonEndShift, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 125);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(20, 0, 20, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(500, 125);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // ButtonContinue
            // 
            ButtonContinue.BackColor = Color.MidnightBlue;
            ButtonContinue.Dock = DockStyle.Fill;
            ButtonContinue.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonContinue.ForeColor = Color.Snow;
            ButtonContinue.Location = new Point(275, 30);
            ButtonContinue.Margin = new Padding(25, 30, 13, 30);
            ButtonContinue.Name = "ButtonContinue";
            ButtonContinue.Size = new Size(192, 65);
            ButtonContinue.TabIndex = 2;
            ButtonContinue.Text = "Continue";
            ButtonContinue.UseVisualStyleBackColor = false;
            ButtonContinue.Click += ButtonContinue_Click;
            // 
            // ButtonEndShift
            // 
            ButtonEndShift.BackColor = Color.ForestGreen;
            ButtonEndShift.Dock = DockStyle.Fill;
            ButtonEndShift.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonEndShift.ForeColor = Color.Snow;
            ButtonEndShift.Location = new Point(33, 30);
            ButtonEndShift.Margin = new Padding(13, 30, 25, 30);
            ButtonEndShift.Name = "ButtonEndShift";
            ButtonEndShift.Size = new Size(192, 65);
            ButtonEndShift.TabIndex = 1;
            ButtonEndShift.Text = "End Shift";
            ButtonEndShift.UseVisualStyleBackColor = false;
            ButtonEndShift.Click += ButtonEndShift_Click;
            // 
            // AskShift
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            ClientSize = new Size(500, 260);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AskShift";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Active Shift";
            TopMost = true;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label LabelAmount;
        private Panel PanelAmountButton;
        private TextBox TextBoxAmount;
        private TableLayoutPanel tableLayoutPanel1;
        private Button ButtonMachineFill;
        private Button ButtonContinue;
        private Button ButtonEndShift;
    }
}