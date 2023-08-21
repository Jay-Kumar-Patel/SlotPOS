namespace SlotPOS
{
    partial class MatchPlay
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
            PanelMachine = new Panel();
            PanelMachineButton = new Panel();
            TextBoxMachine = new TextBox();
            label1 = new Label();
            panel1 = new Panel();
            PanelAmountButton = new Panel();
            TextBoxAmount = new TextBox();
            LabelAmount = new Label();
            panel2 = new Panel();
            ButtonConfirm = new Button();
            PanelMachine.SuspendLayout();
            PanelMachineButton.SuspendLayout();
            panel1.SuspendLayout();
            PanelAmountButton.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMachine
            // 
            PanelMachine.Controls.Add(PanelMachineButton);
            PanelMachine.Controls.Add(label1);
            PanelMachine.Dock = DockStyle.Top;
            PanelMachine.Location = new Point(0, 0);
            PanelMachine.Name = "PanelMachine";
            PanelMachine.Size = new Size(500, 125);
            PanelMachine.TabIndex = 0;
            // 
            // PanelMachineButton
            // 
            PanelMachineButton.Controls.Add(TextBoxMachine);
            PanelMachineButton.Dock = DockStyle.Fill;
            PanelMachineButton.Location = new Point(0, 63);
            PanelMachineButton.Name = "PanelMachineButton";
            PanelMachineButton.Padding = new Padding(140, 10, 140, 0);
            PanelMachineButton.Size = new Size(500, 62);
            PanelMachineButton.TabIndex = 1;
            // 
            // TextBoxMachine
            // 
            TextBoxMachine.Dock = DockStyle.Fill;
            TextBoxMachine.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxMachine.Location = new Point(140, 10);
            TextBoxMachine.MaxLength = 10;
            TextBoxMachine.Name = "TextBoxMachine";
            TextBoxMachine.Size = new Size(220, 39);
            TextBoxMachine.TabIndex = 0;
            TextBoxMachine.TabStop = false;
            TextBoxMachine.TextAlign = HorizontalAlignment.Center;
            TextBoxMachine.KeyDown += TextBoxMachine_KeyDown;
            TextBoxMachine.KeyPress += TextBoxMachine_KeyPress;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(500, 63);
            label1.TabIndex = 0;
            label1.Text = "Enter Machine No:";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(PanelAmountButton);
            panel1.Controls.Add(LabelAmount);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 125);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 125);
            panel1.TabIndex = 1;
            // 
            // PanelAmountButton
            // 
            PanelAmountButton.Controls.Add(TextBoxAmount);
            PanelAmountButton.Dock = DockStyle.Fill;
            PanelAmountButton.Location = new Point(0, 63);
            PanelAmountButton.Name = "PanelAmountButton";
            PanelAmountButton.Padding = new Padding(140, 10, 140, 0);
            PanelAmountButton.Size = new Size(500, 62);
            PanelAmountButton.TabIndex = 2;
            // 
            // TextBoxAmount
            // 
            TextBoxAmount.Dock = DockStyle.Fill;
            TextBoxAmount.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxAmount.Location = new Point(140, 10);
            TextBoxAmount.MaxLength = 15;
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
            LabelAmount.TabIndex = 1;
            LabelAmount.Text = "Amount (In $):";
            LabelAmount.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(ButtonConfirm);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 250);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(125, 30, 125, 10);
            panel2.Size = new Size(500, 105);
            panel2.TabIndex = 2;
            // 
            // ButtonConfirm
            // 
            ButtonConfirm.BackColor = SystemColors.ActiveCaptionText;
            ButtonConfirm.Dock = DockStyle.Fill;
            ButtonConfirm.FlatAppearance.BorderSize = 0;
            ButtonConfirm.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonConfirm.ForeColor = Color.Snow;
            ButtonConfirm.Location = new Point(125, 30);
            ButtonConfirm.Name = "ButtonConfirm";
            ButtonConfirm.Size = new Size(250, 65);
            ButtonConfirm.TabIndex = 0;
            ButtonConfirm.TabStop = false;
            ButtonConfirm.Text = "Confirm";
            ButtonConfirm.UseVisualStyleBackColor = false;
            ButtonConfirm.Click += ButtonConfirm_Click;
            // 
            // MatchPlay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 360);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(PanelMachine);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MatchPlay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MatchPlay";
            TopMost = true;
            KeyDown += MatchPlay_KeyDown;
            PanelMachine.ResumeLayout(false);
            PanelMachineButton.ResumeLayout(false);
            PanelMachineButton.PerformLayout();
            panel1.ResumeLayout(false);
            PanelAmountButton.ResumeLayout(false);
            PanelAmountButton.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMachine;
        private Panel PanelMachineButton;
        private TextBox TextBoxMachine;
        private Label label1;
        private Panel panel1;
        private Panel PanelAmountButton;
        private TextBox TextBoxAmount;
        private Label LabelAmount;
        private Panel panel2;
        private Button ButtonConfirm;
    }
}