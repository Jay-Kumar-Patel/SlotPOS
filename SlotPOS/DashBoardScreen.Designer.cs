namespace SlotPOS
{
    partial class DashBoardScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashBoardScreen));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            PanelWelcome = new Panel();
            ButtonMinimize = new Button();
            ButtonClose = new Button();
            LabelTitle = new Label();
            TableLayoutPanelMenu = new TableLayoutPanel();
            ButtonSettings = new Guna.UI2.WinForms.Guna2Button();
            ButtonUnknown = new Guna.UI2.WinForms.Guna2Button();
            ButtonReports = new Guna.UI2.WinForms.Guna2Button();
            ButtonHome = new Guna.UI2.WinForms.Guna2Button();
            ButtonHistory = new Guna.UI2.WinForms.Guna2Button();
            PanelMain = new Panel();
            PanelWelcome.SuspendLayout();
            TableLayoutPanelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // PanelWelcome
            // 
            PanelWelcome.BackColor = Color.FromArgb(41, 39, 40);
            PanelWelcome.Controls.Add(ButtonMinimize);
            PanelWelcome.Controls.Add(ButtonClose);
            PanelWelcome.Controls.Add(LabelTitle);
            PanelWelcome.Dock = DockStyle.Top;
            PanelWelcome.Location = new Point(0, 0);
            PanelWelcome.Name = "PanelWelcome";
            PanelWelcome.Size = new Size(1920, 60);
            PanelWelcome.TabIndex = 0;
            // 
            // ButtonMinimize
            // 
            ButtonMinimize.BackgroundImageLayout = ImageLayout.Stretch;
            ButtonMinimize.Dock = DockStyle.Right;
            ButtonMinimize.FlatAppearance.BorderSize = 0;
            ButtonMinimize.FlatStyle = FlatStyle.Flat;
            ButtonMinimize.Image = (Image)resources.GetObject("ButtonMinimize.Image");
            ButtonMinimize.Location = new Point(1820, 0);
            ButtonMinimize.Margin = new Padding(3, 10, 3, 10);
            ButtonMinimize.Name = "ButtonMinimize";
            ButtonMinimize.Padding = new Padding(10);
            ButtonMinimize.Size = new Size(40, 60);
            ButtonMinimize.TabIndex = 5;
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
            ButtonClose.Location = new Point(1860, 0);
            ButtonClose.Margin = new Padding(0);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Padding = new Padding(5, 10, 30, 10);
            ButtonClose.Size = new Size(60, 60);
            ButtonClose.TabIndex = 3;
            ButtonClose.UseVisualStyleBackColor = true;
            ButtonClose.Click += ButtonClose_Click;
            // 
            // LabelTitle
            // 
            LabelTitle.Dock = DockStyle.Left;
            LabelTitle.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            LabelTitle.ForeColor = Color.Snow;
            LabelTitle.Location = new Point(0, 0);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Padding = new Padding(20, 0, 0, 0);
            LabelTitle.Size = new Size(200, 60);
            LabelTitle.TabIndex = 0;
            LabelTitle.Text = "SlotPayout";
            LabelTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TableLayoutPanelMenu
            // 
            TableLayoutPanelMenu.ColumnCount = 5;
            TableLayoutPanelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TableLayoutPanelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TableLayoutPanelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TableLayoutPanelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TableLayoutPanelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TableLayoutPanelMenu.Controls.Add(ButtonSettings, 4, 0);
            TableLayoutPanelMenu.Controls.Add(ButtonUnknown, 3, 0);
            TableLayoutPanelMenu.Controls.Add(ButtonReports, 2, 0);
            TableLayoutPanelMenu.Controls.Add(ButtonHome, 0, 0);
            TableLayoutPanelMenu.Controls.Add(ButtonHistory, 1, 0);
            TableLayoutPanelMenu.Dock = DockStyle.Top;
            TableLayoutPanelMenu.Location = new Point(0, 60);
            TableLayoutPanelMenu.Name = "TableLayoutPanelMenu";
            TableLayoutPanelMenu.RowCount = 1;
            TableLayoutPanelMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelMenu.Size = new Size(1920, 55);
            TableLayoutPanelMenu.TabIndex = 1;
            // 
            // ButtonSettings
            // 
            ButtonSettings.BackColor = Color.RoyalBlue;
            ButtonSettings.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            ButtonSettings.CheckedState.FillColor = Color.Snow;
            ButtonSettings.Cursor = Cursors.Hand;
            ButtonSettings.CustomBorderColor = Color.Transparent;
            ButtonSettings.CustomBorderThickness = new Padding(0, 0, 0, 3);
            ButtonSettings.CustomizableEdges = customizableEdges1;
            ButtonSettings.DisabledState.BorderColor = Color.DarkGray;
            ButtonSettings.DisabledState.CustomBorderColor = Color.DarkGray;
            ButtonSettings.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ButtonSettings.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ButtonSettings.Dock = DockStyle.Fill;
            ButtonSettings.FillColor = Color.Snow;
            ButtonSettings.FocusedColor = Color.Snow;
            ButtonSettings.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonSettings.ForeColor = Color.Black;
            ButtonSettings.Location = new Point(1539, 3);
            ButtonSettings.Name = "ButtonSettings";
            ButtonSettings.ShadowDecoration.CustomizableEdges = customizableEdges2;
            ButtonSettings.Size = new Size(378, 49);
            ButtonSettings.TabIndex = 4;
            ButtonSettings.Text = "Settings";
            ButtonSettings.Click += ButtonSettings_Click;
            // 
            // ButtonUnknown
            // 
            ButtonUnknown.BackColor = Color.RoyalBlue;
            ButtonUnknown.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            ButtonUnknown.CheckedState.FillColor = Color.Snow;
            ButtonUnknown.Cursor = Cursors.Hand;
            ButtonUnknown.CustomBorderColor = Color.Transparent;
            ButtonUnknown.CustomBorderThickness = new Padding(0, 0, 0, 3);
            ButtonUnknown.CustomizableEdges = customizableEdges3;
            ButtonUnknown.DisabledState.BorderColor = Color.DarkGray;
            ButtonUnknown.DisabledState.CustomBorderColor = Color.DarkGray;
            ButtonUnknown.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ButtonUnknown.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ButtonUnknown.Dock = DockStyle.Fill;
            ButtonUnknown.FillColor = Color.Snow;
            ButtonUnknown.FocusedColor = Color.Snow;
            ButtonUnknown.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonUnknown.ForeColor = Color.Black;
            ButtonUnknown.Location = new Point(1155, 3);
            ButtonUnknown.Name = "ButtonUnknown";
            ButtonUnknown.ShadowDecoration.CustomizableEdges = customizableEdges4;
            ButtonUnknown.Size = new Size(378, 49);
            ButtonUnknown.TabIndex = 3;
            ButtonUnknown.Text = "Slot Setup";
            ButtonUnknown.Click += ButtonUnknown_Click;
            // 
            // ButtonReports
            // 
            ButtonReports.BackColor = Color.RoyalBlue;
            ButtonReports.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            ButtonReports.CheckedState.FillColor = Color.Snow;
            ButtonReports.Cursor = Cursors.Hand;
            ButtonReports.CustomBorderColor = Color.Transparent;
            ButtonReports.CustomBorderThickness = new Padding(0, 0, 0, 3);
            ButtonReports.CustomizableEdges = customizableEdges5;
            ButtonReports.DisabledState.BorderColor = Color.DarkGray;
            ButtonReports.DisabledState.CustomBorderColor = Color.DarkGray;
            ButtonReports.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ButtonReports.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ButtonReports.Dock = DockStyle.Fill;
            ButtonReports.FillColor = Color.Snow;
            ButtonReports.FocusedColor = Color.Snow;
            ButtonReports.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonReports.ForeColor = Color.Black;
            ButtonReports.Location = new Point(771, 3);
            ButtonReports.Name = "ButtonReports";
            ButtonReports.ShadowDecoration.CustomizableEdges = customizableEdges6;
            ButtonReports.Size = new Size(378, 49);
            ButtonReports.TabIndex = 2;
            ButtonReports.Text = "Reports";
            ButtonReports.Click += ButtonReports_Click;
            // 
            // ButtonHome
            // 
            ButtonHome.BackColor = Color.RoyalBlue;
            ButtonHome.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            ButtonHome.CheckedState.FillColor = Color.Snow;
            ButtonHome.Cursor = Cursors.Hand;
            ButtonHome.CustomBorderColor = Color.FromArgb(178, 8, 55);
            ButtonHome.CustomBorderThickness = new Padding(0, 0, 0, 3);
            ButtonHome.CustomizableEdges = customizableEdges7;
            ButtonHome.DisabledState.BorderColor = Color.DarkGray;
            ButtonHome.DisabledState.CustomBorderColor = Color.DarkGray;
            ButtonHome.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ButtonHome.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ButtonHome.Dock = DockStyle.Fill;
            ButtonHome.FillColor = Color.Snow;
            ButtonHome.FocusedColor = Color.Snow;
            ButtonHome.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonHome.ForeColor = Color.Black;
            ButtonHome.Location = new Point(3, 3);
            ButtonHome.Name = "ButtonHome";
            ButtonHome.ShadowDecoration.CustomizableEdges = customizableEdges8;
            ButtonHome.Size = new Size(378, 49);
            ButtonHome.TabIndex = 1;
            ButtonHome.Text = "Home";
            ButtonHome.Click += ButtonHome_Click;
            // 
            // ButtonHistory
            // 
            ButtonHistory.BackColor = Color.RoyalBlue;
            ButtonHistory.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            ButtonHistory.CheckedState.FillColor = Color.Snow;
            ButtonHistory.Cursor = Cursors.Hand;
            ButtonHistory.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderThickness = new Padding(0, 0, 0, 3);
            ButtonHistory.CustomizableEdges = customizableEdges9;
            ButtonHistory.DisabledState.BorderColor = Color.DarkGray;
            ButtonHistory.DisabledState.CustomBorderColor = Color.DarkGray;
            ButtonHistory.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ButtonHistory.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ButtonHistory.Dock = DockStyle.Fill;
            ButtonHistory.FillColor = Color.Snow;
            ButtonHistory.FocusedColor = Color.Snow;
            ButtonHistory.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonHistory.ForeColor = Color.Black;
            ButtonHistory.Location = new Point(387, 3);
            ButtonHistory.Name = "ButtonHistory";
            ButtonHistory.ShadowDecoration.CustomizableEdges = customizableEdges10;
            ButtonHistory.Size = new Size(378, 49);
            ButtonHistory.TabIndex = 0;
            ButtonHistory.Text = "History";
            ButtonHistory.Click += ButtonHistory_Click;
            // 
            // PanelMain
            // 
            PanelMain.BackColor = Color.White;
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 115);
            PanelMain.Name = "PanelMain";
            PanelMain.Size = new Size(1920, 965);
            PanelMain.TabIndex = 2;
            // 
            // DashBoardScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1080);
            Controls.Add(PanelMain);
            Controls.Add(TableLayoutPanelMenu);
            Controls.Add(PanelWelcome);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DashBoardScreen";
            Text = "DashBoardScreen";
            WindowState = FormWindowState.Maximized;
            Load += DashBoardScreen_Load;
            PanelWelcome.ResumeLayout(false);
            TableLayoutPanelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelWelcome;
        private Label LabelTitle;
        private TableLayoutPanel TableLayoutPanelMenu;
        private Guna.UI2.WinForms.Guna2Button ButtonHistory;
        private Guna.UI2.WinForms.Guna2Button ButtonHome;
        private Guna.UI2.WinForms.Guna2Button ButtonSettings;
        private Guna.UI2.WinForms.Guna2Button ButtonUnknown;
        private Guna.UI2.WinForms.Guna2Button ButtonReports;
        private Panel PanelMain;
        private Button ButtonClose;
        private Button ButtonMinimize;
    }
}