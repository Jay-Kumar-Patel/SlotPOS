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
            panel1 = new Panel();
            PanelAmountButton = new Panel();
            panel2 = new Panel();
            ButtonConfirm = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            PanelAmountButton.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(PanelAmountButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 125);
            panel1.TabIndex = 1;
            // 
            // PanelAmountButton
            // 
            PanelAmountButton.Controls.Add(label1);
            PanelAmountButton.Dock = DockStyle.Fill;
            PanelAmountButton.Location = new Point(0, 0);
            PanelAmountButton.Name = "PanelAmountButton";
            PanelAmountButton.Padding = new Padding(140, 10, 140, 0);
            PanelAmountButton.Size = new Size(500, 125);
            PanelAmountButton.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(ButtonConfirm);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 125);
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
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(140, 10);
            label1.Name = "label1";
            label1.Size = new Size(220, 115);
            label1.TabIndex = 0;
            label1.Text = "Amount : $20 on $20";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MatchPlay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 231);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MatchPlay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MatchPlay";
            TopMost = true;
            KeyDown += MatchPlay_KeyDown;
            panel1.ResumeLayout(false);
            PanelAmountButton.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel PanelAmountButton;
        private Panel panel2;
        private Button ButtonConfirm;
        private Label label1;
    }
}