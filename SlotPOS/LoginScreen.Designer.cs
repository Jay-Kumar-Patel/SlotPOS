namespace SlotPOS
{
    partial class LoginScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginScreen));
            PanelTitle = new Panel();
            LabelTitle = new Label();
            ButtonMinimize = new Button();
            ButtonClose = new Button();
            PanelLeft = new Panel();
            pictureBox1 = new PictureBox();
            LabelLogo = new Label();
            PanelRight = new Panel();
            PanelLogin = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Button_7 = new Button();
            Button_8 = new Button();
            Button_9 = new Button();
            Button_4 = new Button();
            Button_5 = new Button();
            Button_6 = new Button();
            Button_1 = new Button();
            Button_2 = new Button();
            Button_3 = new Button();
            Button_DEL = new Button();
            Button_0 = new Button();
            Button_Cnfrm = new Button();
            PanelTop = new Panel();
            LabelPassword = new Label();
            PanelBottom = new Panel();
            PanelTitle.SuspendLayout();
            PanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            PanelRight.SuspendLayout();
            PanelLogin.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            PanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // PanelTitle
            // 
            PanelTitle.BackColor = Color.FromArgb(41, 39, 40);
            PanelTitle.Controls.Add(LabelTitle);
            PanelTitle.Controls.Add(ButtonMinimize);
            PanelTitle.Controls.Add(ButtonClose);
            PanelTitle.Dock = DockStyle.Top;
            PanelTitle.Location = new Point(0, 0);
            PanelTitle.Name = "PanelTitle";
            PanelTitle.Size = new Size(882, 60);
            PanelTitle.TabIndex = 0;
            PanelTitle.MouseDown += PanelTitle_MouseDown;
            // 
            // LabelTitle
            // 
            LabelTitle.Dock = DockStyle.Left;
            LabelTitle.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTitle.ForeColor = Color.Snow;
            LabelTitle.Location = new Point(0, 0);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Padding = new Padding(10, 0, 0, 0);
            LabelTitle.Size = new Size(400, 60);
            LabelTitle.TabIndex = 7;
            LabelTitle.Text = "Slot Machines Management System";
            LabelTitle.TextAlign = ContentAlignment.MiddleLeft;
            LabelTitle.MouseDown += LabelTitle_MouseDown;
            // 
            // ButtonMinimize
            // 
            ButtonMinimize.BackgroundImageLayout = ImageLayout.Stretch;
            ButtonMinimize.Dock = DockStyle.Right;
            ButtonMinimize.FlatAppearance.BorderSize = 0;
            ButtonMinimize.FlatStyle = FlatStyle.Flat;
            ButtonMinimize.Image = (Image)resources.GetObject("ButtonMinimize.Image");
            ButtonMinimize.Location = new Point(802, 0);
            ButtonMinimize.Margin = new Padding(3, 10, 3, 10);
            ButtonMinimize.Name = "ButtonMinimize";
            ButtonMinimize.Padding = new Padding(10);
            ButtonMinimize.Size = new Size(40, 60);
            ButtonMinimize.TabIndex = 6;
            ButtonMinimize.TabStop = false;
            ButtonMinimize.UseVisualStyleBackColor = true;
            ButtonMinimize.Click += ButtonMinimize_Click;
            // 
            // ButtonClose
            // 
            ButtonClose.BackgroundImageLayout = ImageLayout.Stretch;
            ButtonClose.Dock = DockStyle.Right;
            ButtonClose.FlatAppearance.BorderSize = 0;
            ButtonClose.FlatStyle = FlatStyle.Flat;
            ButtonClose.Image = (Image)resources.GetObject("ButtonClose.Image");
            ButtonClose.Location = new Point(842, 0);
            ButtonClose.Margin = new Padding(0);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Padding = new Padding(5, 10, 10, 10);
            ButtonClose.Size = new Size(40, 60);
            ButtonClose.TabIndex = 4;
            ButtonClose.TabStop = false;
            ButtonClose.UseVisualStyleBackColor = true;
            ButtonClose.Click += ButtonClose_Click;
            // 
            // PanelLeft
            // 
            PanelLeft.BackColor = Color.White;
            PanelLeft.Controls.Add(pictureBox1);
            PanelLeft.Controls.Add(LabelLogo);
            PanelLeft.Dock = DockStyle.Left;
            PanelLeft.Location = new Point(0, 60);
            PanelLeft.Name = "PanelLeft";
            PanelLeft.Size = new Size(404, 493);
            PanelLeft.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(92, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(280, 308);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // LabelLogo
            // 
            LabelLogo.AutoSize = true;
            LabelLogo.Font = new Font("Bookman Old Style", 22.2F, FontStyle.Italic, GraphicsUnit.Point);
            LabelLogo.ForeColor = Color.LightCoral;
            LabelLogo.Location = new Point(132, 349);
            LabelLogo.Name = "LabelLogo";
            LabelLogo.Size = new Size(203, 82);
            LabelLogo.TabIndex = 0;
            LabelLogo.Text = "SlotPayout\r\n(POS)";
            LabelLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelRight
            // 
            PanelRight.BackColor = Color.White;
            PanelRight.Controls.Add(PanelLogin);
            PanelRight.Controls.Add(PanelTop);
            PanelRight.Controls.Add(PanelBottom);
            PanelRight.Dock = DockStyle.Fill;
            PanelRight.Location = new Point(404, 60);
            PanelRight.Name = "PanelRight";
            PanelRight.Size = new Size(478, 493);
            PanelRight.TabIndex = 2;
            // 
            // PanelLogin
            // 
            PanelLogin.Controls.Add(tableLayoutPanel1);
            PanelLogin.Dock = DockStyle.Fill;
            PanelLogin.Location = new Point(0, 96);
            PanelLogin.Name = "PanelLogin";
            PanelLogin.Size = new Size(478, 307);
            PanelLogin.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(Button_7, 1, 0);
            tableLayoutPanel1.Controls.Add(Button_8, 2, 0);
            tableLayoutPanel1.Controls.Add(Button_9, 3, 0);
            tableLayoutPanel1.Controls.Add(Button_4, 1, 1);
            tableLayoutPanel1.Controls.Add(Button_5, 2, 1);
            tableLayoutPanel1.Controls.Add(Button_6, 3, 1);
            tableLayoutPanel1.Controls.Add(Button_1, 1, 2);
            tableLayoutPanel1.Controls.Add(Button_2, 2, 2);
            tableLayoutPanel1.Controls.Add(Button_3, 3, 2);
            tableLayoutPanel1.Controls.Add(Button_DEL, 1, 3);
            tableLayoutPanel1.Controls.Add(Button_0, 2, 3);
            tableLayoutPanel1.Controls.Add(Button_Cnfrm, 3, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(478, 307);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // Button_7
            // 
            Button_7.BackColor = Color.Snow;
            Button_7.Dock = DockStyle.Fill;
            Button_7.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_7.ForeColor = Color.FromArgb(41, 39, 40);
            Button_7.Location = new Point(100, 5);
            Button_7.Margin = new Padding(5);
            Button_7.Name = "Button_7";
            Button_7.Size = new Size(85, 66);
            Button_7.TabIndex = 0;
            Button_7.TabStop = false;
            Button_7.Text = "7";
            Button_7.UseVisualStyleBackColor = false;
            Button_7.Click += Button_7_Click;
            // 
            // Button_8
            // 
            Button_8.BackColor = Color.Snow;
            Button_8.Dock = DockStyle.Fill;
            Button_8.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_8.ForeColor = Color.FromArgb(41, 39, 40);
            Button_8.Location = new Point(195, 5);
            Button_8.Margin = new Padding(5);
            Button_8.Name = "Button_8";
            Button_8.Size = new Size(85, 66);
            Button_8.TabIndex = 1;
            Button_8.TabStop = false;
            Button_8.Text = "8";
            Button_8.UseVisualStyleBackColor = false;
            Button_8.Click += Button_8_Click;
            // 
            // Button_9
            // 
            Button_9.BackColor = Color.Snow;
            Button_9.Dock = DockStyle.Fill;
            Button_9.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_9.ForeColor = Color.FromArgb(41, 39, 40);
            Button_9.Location = new Point(290, 5);
            Button_9.Margin = new Padding(5);
            Button_9.Name = "Button_9";
            Button_9.Size = new Size(85, 66);
            Button_9.TabIndex = 2;
            Button_9.TabStop = false;
            Button_9.Text = "9";
            Button_9.UseVisualStyleBackColor = false;
            Button_9.Click += Button_9_Click;
            // 
            // Button_4
            // 
            Button_4.BackColor = Color.Snow;
            Button_4.Dock = DockStyle.Fill;
            Button_4.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_4.ForeColor = Color.FromArgb(41, 39, 40);
            Button_4.Location = new Point(100, 81);
            Button_4.Margin = new Padding(5);
            Button_4.Name = "Button_4";
            Button_4.Size = new Size(85, 66);
            Button_4.TabIndex = 3;
            Button_4.TabStop = false;
            Button_4.Text = "4";
            Button_4.UseVisualStyleBackColor = false;
            Button_4.Click += Button_4_Click;
            // 
            // Button_5
            // 
            Button_5.BackColor = Color.Snow;
            Button_5.Dock = DockStyle.Fill;
            Button_5.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_5.ForeColor = Color.FromArgb(41, 39, 40);
            Button_5.Location = new Point(195, 81);
            Button_5.Margin = new Padding(5);
            Button_5.Name = "Button_5";
            Button_5.Size = new Size(85, 66);
            Button_5.TabIndex = 4;
            Button_5.TabStop = false;
            Button_5.Text = "5";
            Button_5.UseVisualStyleBackColor = false;
            Button_5.Click += Button_5_Click;
            // 
            // Button_6
            // 
            Button_6.BackColor = Color.Snow;
            Button_6.Dock = DockStyle.Fill;
            Button_6.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_6.ForeColor = Color.FromArgb(41, 39, 40);
            Button_6.Location = new Point(290, 81);
            Button_6.Margin = new Padding(5);
            Button_6.Name = "Button_6";
            Button_6.Size = new Size(85, 66);
            Button_6.TabIndex = 5;
            Button_6.TabStop = false;
            Button_6.Text = "6";
            Button_6.UseVisualStyleBackColor = false;
            Button_6.Click += Button_6_Click;
            // 
            // Button_1
            // 
            Button_1.BackColor = Color.Snow;
            Button_1.Dock = DockStyle.Fill;
            Button_1.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_1.ForeColor = Color.FromArgb(41, 39, 40);
            Button_1.Location = new Point(100, 157);
            Button_1.Margin = new Padding(5);
            Button_1.Name = "Button_1";
            Button_1.Size = new Size(85, 66);
            Button_1.TabIndex = 6;
            Button_1.TabStop = false;
            Button_1.Text = "1";
            Button_1.UseVisualStyleBackColor = false;
            Button_1.Click += Button_1_Click;
            // 
            // Button_2
            // 
            Button_2.BackColor = Color.Snow;
            Button_2.Dock = DockStyle.Fill;
            Button_2.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_2.ForeColor = Color.FromArgb(41, 39, 40);
            Button_2.Location = new Point(195, 157);
            Button_2.Margin = new Padding(5);
            Button_2.Name = "Button_2";
            Button_2.Size = new Size(85, 66);
            Button_2.TabIndex = 7;
            Button_2.TabStop = false;
            Button_2.Text = "2";
            Button_2.UseVisualStyleBackColor = false;
            Button_2.Click += Button_2_Click;
            // 
            // Button_3
            // 
            Button_3.BackColor = Color.Snow;
            Button_3.Dock = DockStyle.Fill;
            Button_3.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_3.ForeColor = Color.FromArgb(41, 39, 40);
            Button_3.Location = new Point(290, 157);
            Button_3.Margin = new Padding(5);
            Button_3.Name = "Button_3";
            Button_3.Size = new Size(85, 66);
            Button_3.TabIndex = 8;
            Button_3.TabStop = false;
            Button_3.Text = "3";
            Button_3.UseVisualStyleBackColor = false;
            Button_3.Click += Button_3_Click;
            // 
            // Button_DEL
            // 
            Button_DEL.BackColor = Color.Snow;
            Button_DEL.Dock = DockStyle.Fill;
            Button_DEL.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            Button_DEL.ForeColor = Color.FromArgb(41, 39, 40);
            Button_DEL.Location = new Point(100, 233);
            Button_DEL.Margin = new Padding(5);
            Button_DEL.Name = "Button_DEL";
            Button_DEL.Size = new Size(85, 69);
            Button_DEL.TabIndex = 9;
            Button_DEL.TabStop = false;
            Button_DEL.Text = "DEL";
            Button_DEL.UseVisualStyleBackColor = false;
            Button_DEL.Click += Button_DEL_Click;
            // 
            // Button_0
            // 
            Button_0.BackColor = Color.Snow;
            Button_0.Dock = DockStyle.Fill;
            Button_0.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            Button_0.ForeColor = Color.FromArgb(41, 39, 40);
            Button_0.Location = new Point(195, 233);
            Button_0.Margin = new Padding(5);
            Button_0.Name = "Button_0";
            Button_0.Size = new Size(85, 69);
            Button_0.TabIndex = 10;
            Button_0.TabStop = false;
            Button_0.Text = "0";
            Button_0.UseVisualStyleBackColor = false;
            Button_0.Click += Button_0_Click;
            // 
            // Button_Cnfrm
            // 
            Button_Cnfrm.BackColor = Color.Snow;
            Button_Cnfrm.BackgroundImageLayout = ImageLayout.Center;
            Button_Cnfrm.Dock = DockStyle.Fill;
            Button_Cnfrm.Image = (Image)resources.GetObject("Button_Cnfrm.Image");
            Button_Cnfrm.Location = new Point(290, 233);
            Button_Cnfrm.Margin = new Padding(5);
            Button_Cnfrm.Name = "Button_Cnfrm";
            Button_Cnfrm.Size = new Size(85, 69);
            Button_Cnfrm.TabIndex = 11;
            Button_Cnfrm.TabStop = false;
            Button_Cnfrm.UseVisualStyleBackColor = false;
            Button_Cnfrm.Click += Button_Cnfrm_Click;
            // 
            // PanelTop
            // 
            PanelTop.Controls.Add(LabelPassword);
            PanelTop.Dock = DockStyle.Top;
            PanelTop.Location = new Point(0, 0);
            PanelTop.Name = "PanelTop";
            PanelTop.Size = new Size(478, 96);
            PanelTop.TabIndex = 1;
            // 
            // LabelPassword
            // 
            LabelPassword.Dock = DockStyle.Fill;
            LabelPassword.Font = new Font("Bookman Old Style", 33F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPassword.ForeColor = Color.Black;
            LabelPassword.Location = new Point(0, 0);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new Size(478, 96);
            LabelPassword.TabIndex = 0;
            LabelPassword.TextAlign = ContentAlignment.BottomCenter;
            // 
            // PanelBottom
            // 
            PanelBottom.Dock = DockStyle.Bottom;
            PanelBottom.Location = new Point(0, 403);
            PanelBottom.Name = "PanelBottom";
            PanelBottom.Size = new Size(478, 90);
            PanelBottom.TabIndex = 0;
            // 
            // LoginScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(PanelRight);
            Controls.Add(PanelLeft);
            Controls.Add(PanelTitle);
            KeyPreview = true;
            Name = "LoginScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginScreen";
            Load += LoginScreen_Load;
            KeyDown += LoginScreen_KeyDown;
            PanelTitle.ResumeLayout(false);
            PanelLeft.ResumeLayout(false);
            PanelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            PanelRight.ResumeLayout(false);
            PanelLogin.ResumeLayout(false);
            PanelLogin.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            PanelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelTitle;
        private Button ButtonClose;
        private Button ButtonMinimize;
        private Panel PanelLeft;
        private Label LabelLogo;
        private Panel PanelRight;
        private Panel PanelLogin;
        private TableLayoutPanel tableLayoutPanel1;
        private Button Button_7;
        private Panel PanelTop;
        private Panel PanelBottom;
        private Button Button_8;
        private Button Button_9;
        private Button Button_4;
        private Button Button_5;
        private Button Button_6;
        private Button Button_1;
        private Button Button_2;
        private Button Button_3;
        private Button Button_DEL;
        private Button Button_0;
        private Button Button_Cnfrm;
        private Label LabelPassword;
        private Label LabelTitle;
        private PictureBox pictureBox1;
    }
}