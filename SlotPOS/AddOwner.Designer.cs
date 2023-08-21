namespace SlotPOS
{
    partial class AddOwner
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
            PanelMain = new Panel();
            PanelTextBox = new Panel();
            TextBoxEmail = new TextBox();
            LabelEmail = new Label();
            PanelConfirm = new Panel();
            ButtonAdd = new Button();
            PanelMain.SuspendLayout();
            PanelTextBox.SuspendLayout();
            PanelConfirm.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.Controls.Add(PanelTextBox);
            PanelMain.Controls.Add(LabelEmail);
            PanelMain.Dock = DockStyle.Top;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Name = "PanelMain";
            PanelMain.Size = new Size(500, 125);
            PanelMain.TabIndex = 0;
            // 
            // PanelTextBox
            // 
            PanelTextBox.Controls.Add(TextBoxEmail);
            PanelTextBox.Dock = DockStyle.Fill;
            PanelTextBox.Location = new Point(0, 63);
            PanelTextBox.Name = "PanelTextBox";
            PanelTextBox.Padding = new Padding(50, 10, 50, 10);
            PanelTextBox.Size = new Size(500, 62);
            PanelTextBox.TabIndex = 1;
            // 
            // TextBoxEmail
            // 
            TextBoxEmail.Dock = DockStyle.Top;
            TextBoxEmail.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            TextBoxEmail.Location = new Point(50, 10);
            TextBoxEmail.MaxLength = 50;
            TextBoxEmail.Name = "TextBoxEmail";
            TextBoxEmail.Size = new Size(400, 27);
            TextBoxEmail.TabIndex = 0;
            TextBoxEmail.TextAlign = HorizontalAlignment.Center;
            TextBoxEmail.KeyPress += TextBoxEmail_KeyPress;
            // 
            // LabelEmail
            // 
            LabelEmail.Dock = DockStyle.Top;
            LabelEmail.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelEmail.Location = new Point(0, 0);
            LabelEmail.Name = "LabelEmail";
            LabelEmail.Size = new Size(500, 63);
            LabelEmail.TabIndex = 0;
            LabelEmail.Text = "Enter E-mail:";
            LabelEmail.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelConfirm
            // 
            PanelConfirm.Controls.Add(ButtonAdd);
            PanelConfirm.Dock = DockStyle.Fill;
            PanelConfirm.Location = new Point(0, 125);
            PanelConfirm.Name = "PanelConfirm";
            PanelConfirm.Padding = new Padding(175, 30, 175, 60);
            PanelConfirm.Size = new Size(500, 135);
            PanelConfirm.TabIndex = 1;
            // 
            // ButtonAdd
            // 
            ButtonAdd.BackColor = Color.Snow;
            ButtonAdd.Dock = DockStyle.Fill;
            ButtonAdd.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonAdd.FlatStyle = FlatStyle.Flat;
            ButtonAdd.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonAdd.Location = new Point(175, 30);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(150, 45);
            ButtonAdd.TabIndex = 0;
            ButtonAdd.Text = "Add";
            ButtonAdd.UseVisualStyleBackColor = false;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // AddOwner
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 260);
            Controls.Add(PanelConfirm);
            Controls.Add(PanelMain);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddOwner";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddOwner";
            TopMost = true;
            PanelMain.ResumeLayout(false);
            PanelTextBox.ResumeLayout(false);
            PanelTextBox.PerformLayout();
            PanelConfirm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMain;
        private Panel PanelTextBox;
        private Label LabelEmail;
        private TextBox TextBoxEmail;
        private Panel PanelConfirm;
        private Button ButtonAdd;
    }
}