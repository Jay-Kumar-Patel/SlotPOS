namespace SlotPOS
{
    partial class EditPassword
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
            panel2 = new Panel();
            TextBoxPassword = new TextBox();
            LabelPassword = new Label();
            panel3 = new Panel();
            ButtonEditPassword = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(LabelPassword);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(504, 125);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(TextBoxPassword);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 62);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(130, 10, 130, 10);
            panel2.Size = new Size(504, 63);
            panel2.TabIndex = 1;
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.Dock = DockStyle.Fill;
            TextBoxPassword.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxPassword.Location = new Point(130, 10);
            TextBoxPassword.MaxLength = 6;
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.Size = new Size(244, 31);
            TextBoxPassword.TabIndex = 1;
            TextBoxPassword.TabStop = false;
            TextBoxPassword.TextAlign = HorizontalAlignment.Center;
            TextBoxPassword.UseSystemPasswordChar = true;
            // 
            // LabelPassword
            // 
            LabelPassword.Dock = DockStyle.Top;
            LabelPassword.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelPassword.Location = new Point(0, 0);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new Size(504, 62);
            LabelPassword.TabIndex = 0;
            LabelPassword.Text = "Enter New Password:";
            LabelPassword.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(ButtonEditPassword);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 125);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(160, 30, 160, 60);
            panel3.Size = new Size(504, 139);
            panel3.TabIndex = 1;
            // 
            // ButtonEditPassword
            // 
            ButtonEditPassword.BackColor = Color.Snow;
            ButtonEditPassword.Dock = DockStyle.Fill;
            ButtonEditPassword.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonEditPassword.FlatStyle = FlatStyle.Flat;
            ButtonEditPassword.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonEditPassword.Location = new Point(160, 30);
            ButtonEditPassword.Name = "ButtonEditPassword";
            ButtonEditPassword.Size = new Size(184, 49);
            ButtonEditPassword.TabIndex = 1;
            ButtonEditPassword.Text = "Update";
            ButtonEditPassword.UseVisualStyleBackColor = false;
            ButtonEditPassword.Click += ButtonEditPassword_Click;
            // 
            // EditPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(504, 264);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Name = "EditPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EditPassword";
            TopMost = true;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label LabelPassword;
        private Panel panel2;
        private TextBox TextBoxPassword;
        private Panel panel3;
        private Button ButtonEditPassword;
    }
}