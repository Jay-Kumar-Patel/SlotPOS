﻿namespace SlotPOS
{
    partial class InitialAmount
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
            panel2 = new Panel();
            ButtonConfirm = new Button();
            panel1.SuspendLayout();
            PanelAmountButton.SuspendLayout();
            panel2.SuspendLayout();
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
            PanelAmountButton.TabIndex = 3;
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
            LabelAmount.TabIndex = 2;
            LabelAmount.Text = "Initial Amount (In $):";
            LabelAmount.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(ButtonConfirm);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 125);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(125, 30, 125, 10);
            panel2.Size = new Size(500, 105);
            panel2.TabIndex = 3;
            // 
            // ButtonConfirm
            // 
            ButtonConfirm.BackColor = SystemColors.ActiveCaptionText;
            ButtonConfirm.Dock = DockStyle.Fill;
            ButtonConfirm.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonConfirm.ForeColor = Color.Snow;
            ButtonConfirm.Location = new Point(125, 30);
            ButtonConfirm.Name = "ButtonConfirm";
            ButtonConfirm.Size = new Size(250, 65);
            ButtonConfirm.TabIndex = 0;
            ButtonConfirm.TabStop = false;
            ButtonConfirm.Text = "Continue";
            ButtonConfirm.UseVisualStyleBackColor = false;
            ButtonConfirm.Click += ButtonConfirm_Click;
            ButtonConfirm.KeyDown += ButtonConfirm_KeyDown;
            // 
            // InitialAmount
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 260);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InitialAmount";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Starting Drawer";
            TopMost = true;
            KeyDown += InitialAmount_KeyDown;
            panel1.ResumeLayout(false);
            PanelAmountButton.ResumeLayout(false);
            PanelAmountButton.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label LabelAmount;
        private Panel PanelAmountButton;
        private TextBox TextBoxAmount;
        private Panel panel2;
        private Button ButtonConfirm;
    }
}