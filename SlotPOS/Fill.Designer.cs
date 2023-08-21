namespace SlotPOS
{
    partial class Fill
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
            PanelAmountButton = new Panel();
            TextBoxAmount = new TextBox();
            LabelAmount = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ButtonMachineFill = new Button();
            ButtonRegularFill = new Button();
            panel1.SuspendLayout();
            PanelAmountButton.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(PanelAmountButton);
            panel1.Controls.Add(LabelAmount);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 125);
            panel1.TabIndex = 0;
            // 
            // PanelAmountButton
            // 
            PanelAmountButton.Controls.Add(TextBoxAmount);
            PanelAmountButton.Dock = DockStyle.Fill;
            PanelAmountButton.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            PanelAmountButton.Location = new Point(0, 63);
            PanelAmountButton.Name = "PanelAmountButton";
            PanelAmountButton.Padding = new Padding(140, 10, 140, 0);
            PanelAmountButton.Size = new Size(500, 62);
            PanelAmountButton.TabIndex = 4;
            // 
            // TextBoxAmount
            // 
            TextBoxAmount.Dock = DockStyle.Fill;
            TextBoxAmount.Location = new Point(140, 10);
            TextBoxAmount.Name = "TextBoxAmount";
            TextBoxAmount.Size = new Size(220, 39);
            TextBoxAmount.TabIndex = 0;
            TextBoxAmount.TabStop = false;
            TextBoxAmount.TextAlign = HorizontalAlignment.Center;
            TextBoxAmount.KeyDown += TextBoxAmount_KeyDown;
            TextBoxAmount.KeyPress += TextBoxAmount_KeyPress;
            // 
            // LabelAmount
            // 
            LabelAmount.Dock = DockStyle.Top;
            LabelAmount.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelAmount.Location = new Point(0, 0);
            LabelAmount.Name = "LabelAmount";
            LabelAmount.Size = new Size(500, 63);
            LabelAmount.TabIndex = 3;
            LabelAmount.Text = "Amount (In $):";
            LabelAmount.TextAlign = ContentAlignment.BottomCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(ButtonMachineFill, 1, 0);
            tableLayoutPanel1.Controls.Add(ButtonRegularFill, 0, 0);
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
            // ButtonMachineFill
            // 
            ButtonMachineFill.BackColor = SystemColors.ActiveCaptionText;
            ButtonMachineFill.Dock = DockStyle.Fill;
            ButtonMachineFill.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonMachineFill.ForeColor = Color.Snow;
            ButtonMachineFill.Location = new Point(270, 25);
            ButtonMachineFill.Margin = new Padding(20, 25, 3, 25);
            ButtonMachineFill.Name = "ButtonMachineFill";
            ButtonMachineFill.Size = new Size(207, 75);
            ButtonMachineFill.TabIndex = 2;
            ButtonMachineFill.TabStop = false;
            ButtonMachineFill.Text = "Machine Fill";
            ButtonMachineFill.UseVisualStyleBackColor = false;
            ButtonMachineFill.Click += ButtonMachineFill_Click;
            // 
            // ButtonRegularFill
            // 
            ButtonRegularFill.BackColor = SystemColors.ActiveCaptionText;
            ButtonRegularFill.Dock = DockStyle.Fill;
            ButtonRegularFill.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonRegularFill.ForeColor = Color.Snow;
            ButtonRegularFill.Location = new Point(23, 25);
            ButtonRegularFill.Margin = new Padding(3, 25, 20, 25);
            ButtonRegularFill.Name = "ButtonRegularFill";
            ButtonRegularFill.Size = new Size(207, 75);
            ButtonRegularFill.TabIndex = 1;
            ButtonRegularFill.TabStop = false;
            ButtonRegularFill.Text = "Regular Fill";
            ButtonRegularFill.UseVisualStyleBackColor = false;
            ButtonRegularFill.Click += ButtonRegularFill_Click;
            // 
            // Fill
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 260);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Fill";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fill";
            TopMost = true;
            KeyDown += Fill_KeyDown;
            panel1.ResumeLayout(false);
            PanelAmountButton.ResumeLayout(false);
            PanelAmountButton.PerformLayout();
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
        private Button ButtonRegularFill;
    }
}