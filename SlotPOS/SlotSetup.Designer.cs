namespace SlotPOS
{
    partial class SlotSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlotSetup));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            PanelAddMachine = new Panel();
            TableLayoutPanelAdd = new TableLayoutPanel();
            LabelMachineNo = new Label();
            LabelGameName = new Label();
            LabelIPAddress = new Label();
            ComboBoxGroup = new ComboBox();
            ButtonSubmit = new Button();
            TextBoxMachineNo = new TextBox();
            TextBoxGameName = new TextBox();
            TextBoxIPAddress = new TextBox();
            Status = new DataGridViewTextBoxColumn();
            PanelEditGroup = new Panel();
            EditGroup = new Button();
            PanelMain = new Panel();
            DataGridViewMachines = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            Machine_No = new DataGridViewTextBoxColumn();
            Game_Name = new DataGridViewTextBoxColumn();
            Static_IP = new DataGridViewTextBoxColumn();
            Group = new DataGridViewTextBoxColumn();
            Last_Cleared_On = new DataGridViewTextBoxColumn();
            Edit = new DataGridViewImageColumn();
            Clear = new DataGridViewImageColumn();
            Delete = new DataGridViewImageColumn();
            PanelAddMachine.SuspendLayout();
            TableLayoutPanelAdd.SuspendLayout();
            PanelEditGroup.SuspendLayout();
            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMachines).BeginInit();
            SuspendLayout();
            // 
            // PanelAddMachine
            // 
            PanelAddMachine.Controls.Add(TableLayoutPanelAdd);
            PanelAddMachine.Dock = DockStyle.Top;
            PanelAddMachine.Location = new Point(0, 0);
            PanelAddMachine.Name = "PanelAddMachine";
            PanelAddMachine.Size = new Size(1902, 65);
            PanelAddMachine.TabIndex = 0;
            // 
            // TableLayoutPanelAdd
            // 
            TableLayoutPanelAdd.BackColor = Color.White;
            TableLayoutPanelAdd.ColumnCount = 10;
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            TableLayoutPanelAdd.Controls.Add(LabelMachineNo, 1, 0);
            TableLayoutPanelAdd.Controls.Add(LabelGameName, 3, 0);
            TableLayoutPanelAdd.Controls.Add(LabelIPAddress, 5, 0);
            TableLayoutPanelAdd.Controls.Add(ComboBoxGroup, 7, 0);
            TableLayoutPanelAdd.Controls.Add(ButtonSubmit, 8, 0);
            TableLayoutPanelAdd.Controls.Add(TextBoxMachineNo, 2, 0);
            TableLayoutPanelAdd.Controls.Add(TextBoxGameName, 4, 0);
            TableLayoutPanelAdd.Controls.Add(TextBoxIPAddress, 6, 0);
            TableLayoutPanelAdd.Dock = DockStyle.Fill;
            TableLayoutPanelAdd.Location = new Point(0, 0);
            TableLayoutPanelAdd.Name = "TableLayoutPanelAdd";
            TableLayoutPanelAdd.RowCount = 1;
            TableLayoutPanelAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelAdd.Size = new Size(1902, 65);
            TableLayoutPanelAdd.TabIndex = 0;
            // 
            // LabelMachineNo
            // 
            LabelMachineNo.BackColor = Color.White;
            LabelMachineNo.Dock = DockStyle.Fill;
            LabelMachineNo.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelMachineNo.ForeColor = Color.MidnightBlue;
            LabelMachineNo.Location = new Point(193, 0);
            LabelMachineNo.Name = "LabelMachineNo";
            LabelMachineNo.Size = new Size(184, 65);
            LabelMachineNo.TabIndex = 0;
            LabelMachineNo.Text = "Machine Number:";
            LabelMachineNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelGameName
            // 
            LabelGameName.Dock = DockStyle.Fill;
            LabelGameName.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelGameName.ForeColor = Color.MidnightBlue;
            LabelGameName.Location = new Point(573, 0);
            LabelGameName.Name = "LabelGameName";
            LabelGameName.Size = new Size(184, 65);
            LabelGameName.TabIndex = 1;
            LabelGameName.Text = "Game Name:";
            LabelGameName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelIPAddress
            // 
            LabelIPAddress.Dock = DockStyle.Fill;
            LabelIPAddress.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            LabelIPAddress.ForeColor = Color.MidnightBlue;
            LabelIPAddress.ImageAlign = ContentAlignment.MiddleRight;
            LabelIPAddress.Location = new Point(953, 0);
            LabelIPAddress.Name = "LabelIPAddress";
            LabelIPAddress.Size = new Size(184, 65);
            LabelIPAddress.TabIndex = 2;
            LabelIPAddress.Text = "Mac Address:";
            LabelIPAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ComboBoxGroup
            // 
            ComboBoxGroup.Anchor = AnchorStyles.None;
            ComboBoxGroup.BackColor = SystemColors.Window;
            ComboBoxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxGroup.Location = new Point(1349, 18);
            ComboBoxGroup.Name = "ComboBoxGroup";
            ComboBoxGroup.Size = new Size(151, 28);
            ComboBoxGroup.TabIndex = 3;
            // 
            // ButtonSubmit
            // 
            ButtonSubmit.BackColor = Color.MidnightBlue;
            ButtonSubmit.Dock = DockStyle.Fill;
            ButtonSubmit.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonSubmit.ForeColor = Color.White;
            ButtonSubmit.Location = new Point(1550, 10);
            ButtonSubmit.Margin = new Padding(30, 10, 30, 10);
            ButtonSubmit.Name = "ButtonSubmit";
            ButtonSubmit.Size = new Size(130, 45);
            ButtonSubmit.TabIndex = 4;
            ButtonSubmit.Text = "Add";
            ButtonSubmit.UseVisualStyleBackColor = false;
            ButtonSubmit.Click += ButtonSubmit_Click;
            // 
            // TextBoxMachineNo
            // 
            TextBoxMachineNo.Anchor = AnchorStyles.None;
            TextBoxMachineNo.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxMachineNo.ForeColor = Color.Black;
            TextBoxMachineNo.Location = new Point(397, 18);
            TextBoxMachineNo.Name = "TextBoxMachineNo";
            TextBoxMachineNo.Size = new Size(155, 29);
            TextBoxMachineNo.TabIndex = 5;
            TextBoxMachineNo.WordWrap = false;
            TextBoxMachineNo.KeyPress += TextBoxMachineNo_KeyPress;
            // 
            // TextBoxGameName
            // 
            TextBoxGameName.Anchor = AnchorStyles.None;
            TextBoxGameName.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxGameName.Location = new Point(777, 18);
            TextBoxGameName.Name = "TextBoxGameName";
            TextBoxGameName.Size = new Size(155, 29);
            TextBoxGameName.TabIndex = 6;
            TextBoxGameName.KeyPress += TextBoxGameName_KeyPress;
            // 
            // TextBoxIPAddress
            // 
            TextBoxIPAddress.Anchor = AnchorStyles.None;
            TextBoxIPAddress.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxIPAddress.Location = new Point(1157, 18);
            TextBoxIPAddress.Name = "TextBoxIPAddress";
            TextBoxIPAddress.Size = new Size(155, 29);
            TextBoxIPAddress.TabIndex = 7;
            TextBoxIPAddress.TextChanged += TextBoxIPAddress_TextChanged;
            TextBoxIPAddress.KeyPress += TextBoxIPAddress_KeyPress;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.MaxInputLength = 100000000;
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Resizable = DataGridViewTriState.False;
            Status.SortMode = DataGridViewColumnSortMode.NotSortable;
            Status.Width = 125;
            // 
            // PanelEditGroup
            // 
            PanelEditGroup.BackColor = SystemColors.Control;
            PanelEditGroup.Controls.Add(EditGroup);
            PanelEditGroup.Dock = DockStyle.Top;
            PanelEditGroup.Location = new Point(0, 65);
            PanelEditGroup.Name = "PanelEditGroup";
            PanelEditGroup.Padding = new Padding(0, 0, 20, 0);
            PanelEditGroup.Size = new Size(1902, 50);
            PanelEditGroup.TabIndex = 2;
            // 
            // EditGroup
            // 
            EditGroup.BackgroundImageLayout = ImageLayout.Center;
            EditGroup.Cursor = Cursors.Hand;
            EditGroup.Dock = DockStyle.Right;
            EditGroup.FlatAppearance.BorderSize = 0;
            EditGroup.FlatAppearance.MouseOverBackColor = Color.Transparent;
            EditGroup.FlatStyle = FlatStyle.Flat;
            EditGroup.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold, GraphicsUnit.Point);
            EditGroup.ForeColor = Color.MidnightBlue;
            EditGroup.Image = (Image)resources.GetObject("EditGroup.Image");
            EditGroup.ImageAlign = ContentAlignment.MiddleLeft;
            EditGroup.Location = new Point(1702, 0);
            EditGroup.Name = "EditGroup";
            EditGroup.Size = new Size(180, 50);
            EditGroup.TabIndex = 0;
            EditGroup.Text = "Edit Group Name";
            EditGroup.TextAlign = ContentAlignment.MiddleRight;
            EditGroup.UseVisualStyleBackColor = false;
            EditGroup.Click += EditGroup_Click;
            // 
            // PanelMain
            // 
            PanelMain.Controls.Add(DataGridViewMachines);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 115);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(30, 20, 30, 30);
            PanelMain.Size = new Size(1902, 803);
            PanelMain.TabIndex = 4;
            // 
            // DataGridViewMachines
            // 
            DataGridViewMachines.AllowUserToAddRows = false;
            DataGridViewMachines.AllowUserToDeleteRows = false;
            DataGridViewMachines.AllowUserToResizeColumns = false;
            DataGridViewMachines.AllowUserToResizeRows = false;
            DataGridViewMachines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewMachines.BackgroundColor = Color.AliceBlue;
            DataGridViewMachines.BorderStyle = BorderStyle.None;
            DataGridViewMachines.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataGridViewMachines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewMachines.ColumnHeadersHeight = 50;
            DataGridViewMachines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewMachines.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, Machine_No, Game_Name, Static_IP, Group, Last_Cleared_On, Edit, Clear, Delete });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridViewMachines.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewMachines.Dock = DockStyle.Fill;
            DataGridViewMachines.EnableHeadersVisualStyles = false;
            DataGridViewMachines.GridColor = Color.LightGray;
            DataGridViewMachines.Location = new Point(30, 20);
            DataGridViewMachines.MultiSelect = false;
            DataGridViewMachines.Name = "DataGridViewMachines";
            DataGridViewMachines.ReadOnly = true;
            DataGridViewMachines.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewMachines.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewMachines.RowHeadersVisible = false;
            DataGridViewMachines.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DataGridViewMachines.RowsDefaultCellStyle = dataGridViewCellStyle4;
            DataGridViewMachines.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DataGridViewMachines.RowTemplate.Height = 50;
            DataGridViewMachines.RowTemplate.Resizable = DataGridViewTriState.False;
            DataGridViewMachines.ScrollBars = ScrollBars.Vertical;
            DataGridViewMachines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewMachines.ShowEditingIcon = false;
            DataGridViewMachines.Size = new Size(1842, 753);
            DataGridViewMachines.TabIndex = 0;
            DataGridViewMachines.TabStop = false;
            DataGridViewMachines.CellClick += DataGridViewMachines_CellClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Status";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Machine_No
            // 
            Machine_No.HeaderText = "Machine Number";
            Machine_No.MinimumWidth = 6;
            Machine_No.Name = "Machine_No";
            Machine_No.ReadOnly = true;
            // 
            // Game_Name
            // 
            Game_Name.HeaderText = "Game";
            Game_Name.MinimumWidth = 6;
            Game_Name.Name = "Game_Name";
            Game_Name.ReadOnly = true;
            // 
            // Static_IP
            // 
            Static_IP.HeaderText = "Mac Address";
            Static_IP.MinimumWidth = 6;
            Static_IP.Name = "Static_IP";
            Static_IP.ReadOnly = true;
            // 
            // Group
            // 
            Group.HeaderText = "Group";
            Group.MinimumWidth = 6;
            Group.Name = "Group";
            Group.ReadOnly = true;
            // 
            // Last_Cleared_On
            // 
            Last_Cleared_On.HeaderText = "Last Cleared On";
            Last_Cleared_On.MinimumWidth = 6;
            Last_Cleared_On.Name = "Last_Cleared_On";
            Last_Cleared_On.ReadOnly = true;
            // 
            // Edit
            // 
            Edit.HeaderText = "Edit";
            Edit.Image = (Image)resources.GetObject("Edit.Image");
            Edit.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Edit.MinimumWidth = 6;
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            // 
            // Clear
            // 
            Clear.HeaderText = "Clear";
            Clear.Image = (Image)resources.GetObject("Clear.Image");
            Clear.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Clear.MinimumWidth = 6;
            Clear.Name = "Clear";
            Clear.ReadOnly = true;
            // 
            // Delete
            // 
            Delete.HeaderText = "Delete";
            Delete.Image = (Image)resources.GetObject("Delete.Image");
            Delete.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Delete.MinimumWidth = 6;
            Delete.Name = "Delete";
            Delete.ReadOnly = true;
            // 
            // SlotSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 918);
            Controls.Add(PanelMain);
            Controls.Add(PanelEditGroup);
            Controls.Add(PanelAddMachine);
            Name = "SlotSetup";
            Text = "SlotSetup";
            PanelAddMachine.ResumeLayout(false);
            TableLayoutPanelAdd.ResumeLayout(false);
            TableLayoutPanelAdd.PerformLayout();
            PanelEditGroup.ResumeLayout(false);
            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewMachines).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelAddMachine;
        private TableLayoutPanel TableLayoutPanelAdd;
        private Label LabelMachineNo;
        private Label LabelGameName;
        private Label LabelIPAddress;
        private ComboBox ComboBoxGroup;
        private Button ButtonSubmit;
        private TextBox TextBoxMachineNo;
        private TextBox TextBoxGameName;
        private TextBox TextBoxIPAddress;
        private DataGridViewTextBoxColumn Status;
        private Panel PanelEditGroup;
        private Panel PanelMain;
        private DataGridView DataGridViewMachines;
        private Button EditGroup;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn Machine_No;
        private DataGridViewTextBoxColumn Game_Name;
        private DataGridViewTextBoxColumn Static_IP;
        private DataGridViewTextBoxColumn Group;
        private DataGridViewTextBoxColumn Last_Cleared_On;
        private DataGridViewImageColumn Edit;
        private DataGridViewImageColumn Clear;
        private DataGridViewImageColumn Delete;
    }
}