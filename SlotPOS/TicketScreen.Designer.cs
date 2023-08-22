namespace SlotPOS
{
    partial class TicketScreen
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            PanelLeft = new Panel();
            ButtonLogOut = new Button();
            ButtonTakeBreak = new Button();
            ButtonEndShift = new Button();
            PanelExtraLeft = new Panel();
            PanelDrop = new Panel();
            ButtonDrop = new Button();
            PanelFill = new Panel();
            ButtonFill = new Button();
            PanelPromo = new Panel();
            ButtonPromoCode = new Button();
            PanelMatchPlay = new Panel();
            ButtonMatchPlay = new Button();
            PanelBalance = new Panel();
            LabelCashTitle = new Label();
            LabelBalance = new Label();
            PanelFooter = new Panel();
            PanelStatus = new Panel();
            total = new Label();
            slash = new Label();
            status = new Label();
            online = new Label();
            LabelDate = new Label();
            PanelHistory = new Panel();
            PanelMain = new Panel();
            PanelMain2 = new Panel();
            DataGridViewRecent = new DataGridView();
            PanelTitle = new Panel();
            LabelTitle = new Label();
            PanelMainPart = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            PanelSearch = new Panel();
            TextBoxSearch = new TextBox();
            Search = new Button();
            PanelExtra2 = new Panel();
            PanelLeft.SuspendLayout();
            PanelExtraLeft.SuspendLayout();
            PanelDrop.SuspendLayout();
            PanelFill.SuspendLayout();
            PanelPromo.SuspendLayout();
            PanelMatchPlay.SuspendLayout();
            PanelBalance.SuspendLayout();
            PanelFooter.SuspendLayout();
            PanelStatus.SuspendLayout();
            PanelHistory.SuspendLayout();
            PanelMain.SuspendLayout();
            PanelMain2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewRecent).BeginInit();
            PanelTitle.SuspendLayout();
            PanelMainPart.SuspendLayout();
            PanelSearch.SuspendLayout();
            SuspendLayout();
            // 
            // PanelLeft
            // 
            PanelLeft.Controls.Add(ButtonLogOut);
            PanelLeft.Controls.Add(ButtonTakeBreak);
            PanelLeft.Controls.Add(ButtonEndShift);
            PanelLeft.Controls.Add(PanelExtraLeft);
            PanelLeft.Controls.Add(PanelBalance);
            PanelLeft.Dock = DockStyle.Left;
            PanelLeft.Location = new Point(0, 0);
            PanelLeft.Name = "PanelLeft";
            PanelLeft.Size = new Size(353, 965);
            PanelLeft.TabIndex = 0;
            // 
            // ButtonLogOut
            // 
            ButtonLogOut.BackColor = SystemColors.ActiveCaptionText;
            ButtonLogOut.Dock = DockStyle.Bottom;
            ButtonLogOut.FlatAppearance.BorderSize = 0;
            ButtonLogOut.FlatStyle = FlatStyle.Flat;
            ButtonLogOut.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonLogOut.ForeColor = Color.Snow;
            ButtonLogOut.Location = new Point(0, 765);
            ButtonLogOut.Name = "ButtonLogOut";
            ButtonLogOut.Size = new Size(353, 80);
            ButtonLogOut.TabIndex = 8;
            ButtonLogOut.Text = "Log Out";
            ButtonLogOut.UseVisualStyleBackColor = false;
            ButtonLogOut.Click += ButtonLogOut_Click;
            // 
            // ButtonTakeBreak
            // 
            ButtonTakeBreak.BackColor = SystemColors.ActiveCaptionText;
            ButtonTakeBreak.Dock = DockStyle.Bottom;
            ButtonTakeBreak.FlatAppearance.BorderSize = 0;
            ButtonTakeBreak.FlatStyle = FlatStyle.Flat;
            ButtonTakeBreak.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonTakeBreak.ForeColor = Color.Snow;
            ButtonTakeBreak.Location = new Point(0, 845);
            ButtonTakeBreak.Name = "ButtonTakeBreak";
            ButtonTakeBreak.Padding = new Padding(40, 0, 0, 0);
            ButtonTakeBreak.Size = new Size(353, 60);
            ButtonTakeBreak.TabIndex = 7;
            ButtonTakeBreak.Text = "Suspend";
            ButtonTakeBreak.TextAlign = ContentAlignment.MiddleLeft;
            ButtonTakeBreak.UseVisualStyleBackColor = false;
            ButtonTakeBreak.Click += ButtonTakeBreak_Click;
            // 
            // ButtonEndShift
            // 
            ButtonEndShift.BackColor = SystemColors.ActiveCaptionText;
            ButtonEndShift.Dock = DockStyle.Bottom;
            ButtonEndShift.FlatAppearance.BorderSize = 0;
            ButtonEndShift.FlatStyle = FlatStyle.Flat;
            ButtonEndShift.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonEndShift.ForeColor = Color.Snow;
            ButtonEndShift.Location = new Point(0, 905);
            ButtonEndShift.Name = "ButtonEndShift";
            ButtonEndShift.Padding = new Padding(40, 0, 0, 0);
            ButtonEndShift.Size = new Size(353, 60);
            ButtonEndShift.TabIndex = 6;
            ButtonEndShift.Text = "End Your Shift";
            ButtonEndShift.TextAlign = ContentAlignment.MiddleLeft;
            ButtonEndShift.UseVisualStyleBackColor = false;
            ButtonEndShift.Click += ButtonEndShift_Click;
            // 
            // PanelExtraLeft
            // 
            PanelExtraLeft.BackColor = Color.FromArgb(41, 39, 40);
            PanelExtraLeft.Controls.Add(PanelDrop);
            PanelExtraLeft.Controls.Add(PanelFill);
            PanelExtraLeft.Controls.Add(PanelPromo);
            PanelExtraLeft.Controls.Add(PanelMatchPlay);
            PanelExtraLeft.Dock = DockStyle.Fill;
            PanelExtraLeft.Location = new Point(0, 125);
            PanelExtraLeft.Name = "PanelExtraLeft";
            PanelExtraLeft.Size = new Size(353, 840);
            PanelExtraLeft.TabIndex = 5;
            // 
            // PanelDrop
            // 
            PanelDrop.Controls.Add(ButtonDrop);
            PanelDrop.Dock = DockStyle.Top;
            PanelDrop.Location = new Point(0, 240);
            PanelDrop.Name = "PanelDrop";
            PanelDrop.Padding = new Padding(20, 10, 15, 10);
            PanelDrop.Size = new Size(353, 80);
            PanelDrop.TabIndex = 3;
            // 
            // ButtonDrop
            // 
            ButtonDrop.BackColor = Color.White;
            ButtonDrop.Cursor = Cursors.Hand;
            ButtonDrop.Dock = DockStyle.Fill;
            ButtonDrop.FlatAppearance.BorderSize = 0;
            ButtonDrop.FlatStyle = FlatStyle.Flat;
            ButtonDrop.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonDrop.ForeColor = SystemColors.ActiveCaptionText;
            ButtonDrop.Location = new Point(20, 10);
            ButtonDrop.Margin = new Padding(0);
            ButtonDrop.Name = "ButtonDrop";
            ButtonDrop.Size = new Size(318, 60);
            ButtonDrop.TabIndex = 3;
            ButtonDrop.Text = "Drop";
            ButtonDrop.UseVisualStyleBackColor = false;
            ButtonDrop.Click += ButtonDrop_Click;
            // 
            // PanelFill
            // 
            PanelFill.Controls.Add(ButtonFill);
            PanelFill.Dock = DockStyle.Top;
            PanelFill.Location = new Point(0, 160);
            PanelFill.Name = "PanelFill";
            PanelFill.Padding = new Padding(20, 10, 15, 10);
            PanelFill.Size = new Size(353, 80);
            PanelFill.TabIndex = 2;
            // 
            // ButtonFill
            // 
            ButtonFill.BackColor = Color.White;
            ButtonFill.Cursor = Cursors.Hand;
            ButtonFill.Dock = DockStyle.Fill;
            ButtonFill.FlatAppearance.BorderSize = 0;
            ButtonFill.FlatStyle = FlatStyle.Flat;
            ButtonFill.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonFill.ForeColor = SystemColors.ActiveCaptionText;
            ButtonFill.Location = new Point(20, 10);
            ButtonFill.Margin = new Padding(0);
            ButtonFill.Name = "ButtonFill";
            ButtonFill.Size = new Size(318, 60);
            ButtonFill.TabIndex = 2;
            ButtonFill.Text = "Fill";
            ButtonFill.UseVisualStyleBackColor = false;
            ButtonFill.Click += ButtonFill_Click;
            // 
            // PanelPromo
            // 
            PanelPromo.BackColor = Color.FromArgb(41, 39, 40);
            PanelPromo.Controls.Add(ButtonPromoCode);
            PanelPromo.Dock = DockStyle.Top;
            PanelPromo.Location = new Point(0, 80);
            PanelPromo.Name = "PanelPromo";
            PanelPromo.Padding = new Padding(20, 10, 15, 10);
            PanelPromo.Size = new Size(353, 80);
            PanelPromo.TabIndex = 1;
            // 
            // ButtonPromoCode
            // 
            ButtonPromoCode.BackColor = Color.White;
            ButtonPromoCode.Cursor = Cursors.Hand;
            ButtonPromoCode.Dock = DockStyle.Fill;
            ButtonPromoCode.FlatAppearance.BorderSize = 0;
            ButtonPromoCode.FlatStyle = FlatStyle.Flat;
            ButtonPromoCode.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonPromoCode.ForeColor = SystemColors.ActiveCaptionText;
            ButtonPromoCode.Location = new Point(20, 10);
            ButtonPromoCode.Margin = new Padding(0);
            ButtonPromoCode.Name = "ButtonPromoCode";
            ButtonPromoCode.Size = new Size(318, 60);
            ButtonPromoCode.TabIndex = 0;
            ButtonPromoCode.Text = "Promo";
            ButtonPromoCode.UseVisualStyleBackColor = false;
            ButtonPromoCode.Click += ButtonPromoCode_Click;
            // 
            // PanelMatchPlay
            // 
            PanelMatchPlay.BackColor = Color.FromArgb(41, 39, 40);
            PanelMatchPlay.Controls.Add(ButtonMatchPlay);
            PanelMatchPlay.Dock = DockStyle.Top;
            PanelMatchPlay.Location = new Point(0, 0);
            PanelMatchPlay.Name = "PanelMatchPlay";
            PanelMatchPlay.Padding = new Padding(20, 10, 15, 10);
            PanelMatchPlay.Size = new Size(353, 80);
            PanelMatchPlay.TabIndex = 0;
            // 
            // ButtonMatchPlay
            // 
            ButtonMatchPlay.BackColor = Color.White;
            ButtonMatchPlay.Cursor = Cursors.Hand;
            ButtonMatchPlay.Dock = DockStyle.Fill;
            ButtonMatchPlay.FlatAppearance.BorderSize = 0;
            ButtonMatchPlay.FlatStyle = FlatStyle.Flat;
            ButtonMatchPlay.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonMatchPlay.ForeColor = SystemColors.ActiveCaptionText;
            ButtonMatchPlay.Location = new Point(20, 10);
            ButtonMatchPlay.Margin = new Padding(0);
            ButtonMatchPlay.Name = "ButtonMatchPlay";
            ButtonMatchPlay.Size = new Size(318, 60);
            ButtonMatchPlay.TabIndex = 0;
            ButtonMatchPlay.Text = "Match Play";
            ButtonMatchPlay.UseVisualStyleBackColor = false;
            ButtonMatchPlay.Click += ButtonMatchPlay_Click;
            // 
            // PanelBalance
            // 
            PanelBalance.BorderStyle = BorderStyle.FixedSingle;
            PanelBalance.Controls.Add(LabelCashTitle);
            PanelBalance.Controls.Add(LabelBalance);
            PanelBalance.Dock = DockStyle.Top;
            PanelBalance.Location = new Point(0, 0);
            PanelBalance.Name = "PanelBalance";
            PanelBalance.Size = new Size(353, 125);
            PanelBalance.TabIndex = 0;
            // 
            // LabelCashTitle
            // 
            LabelCashTitle.BackColor = Color.AliceBlue;
            LabelCashTitle.Dock = DockStyle.Top;
            LabelCashTitle.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            LabelCashTitle.ForeColor = Color.MidnightBlue;
            LabelCashTitle.Location = new Point(0, 0);
            LabelCashTitle.Name = "LabelCashTitle";
            LabelCashTitle.Size = new Size(351, 62);
            LabelCashTitle.TabIndex = 1;
            LabelCashTitle.Text = "Cash Balance:";
            LabelCashTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // LabelBalance
            // 
            LabelBalance.BackColor = Color.AliceBlue;
            LabelBalance.Dock = DockStyle.Bottom;
            LabelBalance.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LabelBalance.ForeColor = Color.MidnightBlue;
            LabelBalance.ImageAlign = ContentAlignment.BottomRight;
            LabelBalance.Location = new Point(0, 60);
            LabelBalance.Name = "LabelBalance";
            LabelBalance.Padding = new Padding(0, 5, 0, 0);
            LabelBalance.Size = new Size(351, 63);
            LabelBalance.TabIndex = 0;
            LabelBalance.Text = "$5,000.00";
            LabelBalance.TextAlign = ContentAlignment.TopCenter;
            // 
            // PanelFooter
            // 
            PanelFooter.BackColor = Color.FromArgb(41, 39, 40);
            PanelFooter.Controls.Add(PanelStatus);
            PanelFooter.Controls.Add(LabelDate);
            PanelFooter.Dock = DockStyle.Bottom;
            PanelFooter.Location = new Point(353, 905);
            PanelFooter.Name = "PanelFooter";
            PanelFooter.Size = new Size(1567, 60);
            PanelFooter.TabIndex = 1;
            // 
            // PanelStatus
            // 
            PanelStatus.Controls.Add(total);
            PanelStatus.Controls.Add(slash);
            PanelStatus.Controls.Add(status);
            PanelStatus.Controls.Add(online);
            PanelStatus.Dock = DockStyle.Fill;
            PanelStatus.Location = new Point(0, 0);
            PanelStatus.Name = "PanelStatus";
            PanelStatus.Size = new Size(1224, 60);
            PanelStatus.TabIndex = 1;
            // 
            // total
            // 
            total.AutoSize = true;
            total.Dock = DockStyle.Left;
            total.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            total.ForeColor = Color.Snow;
            total.Location = new Point(138, 0);
            total.Name = "total";
            total.Padding = new Padding(0, 20, 0, 0);
            total.Size = new Size(34, 43);
            total.TabIndex = 3;
            total.Text = "NA";
            // 
            // slash
            // 
            slash.AutoSize = true;
            slash.Dock = DockStyle.Left;
            slash.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            slash.ForeColor = Color.Snow;
            slash.Location = new Point(118, 0);
            slash.Margin = new Padding(0);
            slash.Name = "slash";
            slash.Padding = new Padding(0, 16, 0, 0);
            slash.Size = new Size(20, 44);
            slash.TabIndex = 2;
            slash.Text = "/";
            // 
            // status
            // 
            status.AutoSize = true;
            status.Dock = DockStyle.Left;
            status.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            status.ForeColor = Color.Chartreuse;
            status.Location = new Point(84, 0);
            status.Margin = new Padding(0);
            status.Name = "status";
            status.Padding = new Padding(0, 20, 0, 0);
            status.Size = new Size(34, 43);
            status.TabIndex = 1;
            status.Text = "NA";
            // 
            // online
            // 
            online.AutoSize = true;
            online.Dock = DockStyle.Left;
            online.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            online.ForeColor = Color.Snow;
            online.Location = new Point(0, 0);
            online.Name = "online";
            online.Padding = new Padding(20, 20, 0, 0);
            online.Size = new Size(84, 43);
            online.TabIndex = 0;
            online.Text = "Online:";
            // 
            // LabelDate
            // 
            LabelDate.Dock = DockStyle.Right;
            LabelDate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LabelDate.ForeColor = Color.Snow;
            LabelDate.Location = new Point(1224, 0);
            LabelDate.Name = "LabelDate";
            LabelDate.Padding = new Padding(0, 0, 20, 0);
            LabelDate.Size = new Size(343, 60);
            LabelDate.TabIndex = 0;
            LabelDate.Text = "Date: 27-02";
            LabelDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PanelHistory
            // 
            PanelHistory.BackColor = SystemColors.Control;
            PanelHistory.Controls.Add(PanelMain);
            PanelHistory.Controls.Add(PanelTitle);
            PanelHistory.Dock = DockStyle.Right;
            PanelHistory.Location = new Point(1396, 0);
            PanelHistory.Name = "PanelHistory";
            PanelHistory.Size = new Size(524, 905);
            PanelHistory.TabIndex = 2;
            // 
            // PanelMain
            // 
            PanelMain.BackColor = SystemColors.Control;
            PanelMain.Controls.Add(PanelMain2);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 96);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(10, 10, 20, 40);
            PanelMain.Size = new Size(524, 809);
            PanelMain.TabIndex = 1;
            // 
            // PanelMain2
            // 
            PanelMain2.BackColor = Color.White;
            PanelMain2.BorderStyle = BorderStyle.FixedSingle;
            PanelMain2.Controls.Add(DataGridViewRecent);
            PanelMain2.Dock = DockStyle.Fill;
            PanelMain2.Location = new Point(10, 10);
            PanelMain2.Name = "PanelMain2";
            PanelMain2.Size = new Size(494, 759);
            PanelMain2.TabIndex = 0;
            // 
            // DataGridViewRecent
            // 
            DataGridViewRecent.AllowUserToAddRows = false;
            DataGridViewRecent.AllowUserToDeleteRows = false;
            DataGridViewRecent.AllowUserToResizeColumns = false;
            DataGridViewRecent.AllowUserToResizeRows = false;
            DataGridViewRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewRecent.BackgroundColor = Color.White;
            DataGridViewRecent.BorderStyle = BorderStyle.None;
            DataGridViewRecent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(28, 58, 112);
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(28, 58, 112);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataGridViewRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewRecent.ColumnHeadersHeight = 50;
            DataGridViewRecent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridViewRecent.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewRecent.Dock = DockStyle.Fill;
            DataGridViewRecent.EnableHeadersVisualStyles = false;
            DataGridViewRecent.GridColor = Color.LightGray;
            DataGridViewRecent.Location = new Point(0, 0);
            DataGridViewRecent.MultiSelect = false;
            DataGridViewRecent.Name = "DataGridViewRecent";
            DataGridViewRecent.ReadOnly = true;
            DataGridViewRecent.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewRecent.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewRecent.RowHeadersVisible = false;
            DataGridViewRecent.RowHeadersWidth = 51;
            DataGridViewRecent.RowTemplate.Height = 50;
            DataGridViewRecent.ScrollBars = ScrollBars.None;
            DataGridViewRecent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewRecent.ShowEditingIcon = false;
            DataGridViewRecent.Size = new Size(492, 757);
            DataGridViewRecent.TabIndex = 0;
            DataGridViewRecent.TabStop = false;
            DataGridViewRecent.CellFormatting += DataGridViewRecent_CellFormatting;
            // 
            // PanelTitle
            // 
            PanelTitle.BackColor = SystemColors.Control;
            PanelTitle.Controls.Add(LabelTitle);
            PanelTitle.Dock = DockStyle.Top;
            PanelTitle.Location = new Point(0, 0);
            PanelTitle.Name = "PanelTitle";
            PanelTitle.Size = new Size(524, 96);
            PanelTitle.TabIndex = 0;
            // 
            // LabelTitle
            // 
            LabelTitle.Dock = DockStyle.Bottom;
            LabelTitle.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelTitle.Location = new Point(0, 45);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Size = new Size(524, 51);
            LabelTitle.TabIndex = 0;
            LabelTitle.Text = "Recent Transactions";
            LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelMainPart
            // 
            PanelMainPart.Controls.Add(flowLayoutPanel1);
            PanelMainPart.Controls.Add(PanelSearch);
            PanelMainPart.Controls.Add(PanelExtra2);
            PanelMainPart.Dock = DockStyle.Fill;
            PanelMainPart.Location = new Point(353, 0);
            PanelMainPart.Name = "PanelMainPart";
            PanelMainPart.Size = new Size(1043, 905);
            PanelMainPart.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.Control;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 96);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(20, 10, 10, 0);
            flowLayoutPanel1.Size = new Size(1043, 809);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // PanelSearch
            // 
            PanelSearch.Controls.Add(TextBoxSearch);
            PanelSearch.Controls.Add(Search);
            PanelSearch.Dock = DockStyle.Top;
            PanelSearch.Location = new Point(0, 42);
            PanelSearch.Name = "PanelSearch";
            PanelSearch.Padding = new Padding(20, 0, 20, 0);
            PanelSearch.Size = new Size(1043, 54);
            PanelSearch.TabIndex = 1;
            // 
            // TextBoxSearch
            // 
            TextBoxSearch.Dock = DockStyle.Fill;
            TextBoxSearch.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxSearch.Location = new Point(20, 0);
            TextBoxSearch.Name = "TextBoxSearch";
            TextBoxSearch.Size = new Size(909, 31);
            TextBoxSearch.TabIndex = 1;
            TextBoxSearch.TextAlign = HorizontalAlignment.Center;
            TextBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            TextBoxSearch.KeyPress += TextBoxSearch_KeyPress;
            // 
            // Search
            // 
            Search.BackColor = Color.FromArgb(28, 58, 112);
            Search.Cursor = Cursors.Hand;
            Search.Dock = DockStyle.Right;
            Search.FlatAppearance.BorderSize = 0;
            Search.FlatStyle = FlatStyle.Flat;
            Search.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            Search.ForeColor = Color.Snow;
            Search.Location = new Point(929, 0);
            Search.Name = "Search";
            Search.Size = new Size(94, 54);
            Search.TabIndex = 0;
            Search.Text = "Search";
            Search.UseVisualStyleBackColor = false;
            Search.Click += button1_Click;
            // 
            // PanelExtra2
            // 
            PanelExtra2.Dock = DockStyle.Top;
            PanelExtra2.Location = new Point(0, 0);
            PanelExtra2.Name = "PanelExtra2";
            PanelExtra2.Size = new Size(1043, 42);
            PanelExtra2.TabIndex = 0;
            // 
            // TicketScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1920, 965);
            Controls.Add(PanelMainPart);
            Controls.Add(PanelHistory);
            Controls.Add(PanelFooter);
            Controls.Add(PanelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TicketScreen";
            Text = "TicketScreen";
            Load += TicketScreen_Load;
            Shown += TicketScreen_Shown;
            PanelLeft.ResumeLayout(false);
            PanelExtraLeft.ResumeLayout(false);
            PanelDrop.ResumeLayout(false);
            PanelFill.ResumeLayout(false);
            PanelPromo.ResumeLayout(false);
            PanelMatchPlay.ResumeLayout(false);
            PanelBalance.ResumeLayout(false);
            PanelFooter.ResumeLayout(false);
            PanelStatus.ResumeLayout(false);
            PanelStatus.PerformLayout();
            PanelHistory.ResumeLayout(false);
            PanelMain.ResumeLayout(false);
            PanelMain2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewRecent).EndInit();
            PanelTitle.ResumeLayout(false);
            PanelMainPart.ResumeLayout(false);
            PanelSearch.ResumeLayout(false);
            PanelSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelLeft;
        private Panel PanelBalance;
        public Label LabelBalance;
        private Panel PanelSearch;
        private Label label2;
        private Panel PanelFooter;
        private Label LabelDate;
        private Panel PanelExtraLeft;
        private Panel PanelHistory;
        private Panel PanelMainPart;
        private Panel PanelExtra2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Search;
        private TextBox TextBoxSearch;
        private Button ButtonLogOut;
        private Button ButtonTakeBreak;
        private Button ButtonEndShift;
        private Panel PanelTitle;
        private Label LabelTitle;
        private Panel PanelMain;
        private Label LabelCashTitle;
        private Panel PanelMatchPlay;
        private Button ButtonMatchPlay;
        private Panel PanelPromo;
        private Button ButtonPromoCode;
        private Panel PanelDrop;
        private Button ButtonDrop;
        private Panel PanelFill;
        private Button ButtonFill;
        private Panel PanelMain2;
        private DataGridView DataGridViewRecent;
        private Panel PanelStatus;
        private Label online;
        private Label status;
        private Label total;
        private Label slash;
    }
}