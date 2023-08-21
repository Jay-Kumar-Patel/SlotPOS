namespace SlotPOS
{
    partial class HistoryScreen
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
            PanelPicker = new Panel();
            PanelFrom = new Panel();
            label1 = new Label();
            DTPFrom = new DateTimePicker();
            PanelTo = new Panel();
            LabelTo = new Label();
            DTPto = new DateTimePicker();
            panel1 = new Panel();
            ButtonSubmit = new Button();
            PanelHistory = new Panel();
            PanelHistory2 = new Panel();
            DataGridViewHistory = new DataGridView();
            PanelPicker.SuspendLayout();
            PanelFrom.SuspendLayout();
            PanelTo.SuspendLayout();
            panel1.SuspendLayout();
            PanelHistory.SuspendLayout();
            PanelHistory2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewHistory).BeginInit();
            SuspendLayout();
            // 
            // PanelPicker
            // 
            PanelPicker.Controls.Add(PanelFrom);
            PanelPicker.Controls.Add(PanelTo);
            PanelPicker.Controls.Add(panel1);
            PanelPicker.Dock = DockStyle.Top;
            PanelPicker.Location = new Point(0, 0);
            PanelPicker.Name = "PanelPicker";
            PanelPicker.Size = new Size(800, 70);
            PanelPicker.TabIndex = 1;
            // 
            // PanelFrom
            // 
            PanelFrom.Controls.Add(label1);
            PanelFrom.Controls.Add(DTPFrom);
            PanelFrom.Dock = DockStyle.Right;
            PanelFrom.Location = new Point(160, 0);
            PanelFrom.Name = "PanelFrom";
            PanelFrom.Padding = new Padding(0, 23, 25, 20);
            PanelFrom.Size = new Size(250, 70);
            PanelFrom.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(0, 23);
            label1.Name = "label1";
            label1.Size = new Size(80, 27);
            label1.TabIndex = 2;
            label1.Text = "From:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DTPFrom
            // 
            DTPFrom.CalendarFont = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            DTPFrom.CalendarForeColor = SystemColors.ActiveCaptionText;
            DTPFrom.CalendarMonthBackground = SystemColors.HighlightText;
            DTPFrom.Dock = DockStyle.Right;
            DTPFrom.Location = new Point(85, 23);
            DTPFrom.Name = "DTPFrom";
            DTPFrom.Size = new Size(140, 27);
            DTPFrom.TabIndex = 1;
            // 
            // PanelTo
            // 
            PanelTo.Controls.Add(LabelTo);
            PanelTo.Controls.Add(DTPto);
            PanelTo.Dock = DockStyle.Right;
            PanelTo.Location = new Point(410, 0);
            PanelTo.Name = "PanelTo";
            PanelTo.Padding = new Padding(0, 23, 5, 20);
            PanelTo.Size = new Size(190, 70);
            PanelTo.TabIndex = 1;
            // 
            // LabelTo
            // 
            LabelTo.Dock = DockStyle.Left;
            LabelTo.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            LabelTo.ForeColor = Color.MidnightBlue;
            LabelTo.Location = new Point(0, 23);
            LabelTo.Name = "LabelTo";
            LabelTo.Size = new Size(40, 27);
            LabelTo.TabIndex = 1;
            LabelTo.Text = "To:";
            LabelTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DTPto
            // 
            DTPto.CalendarFont = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            DTPto.CalendarForeColor = SystemColors.ActiveCaptionText;
            DTPto.CalendarMonthBackground = SystemColors.HighlightText;
            DTPto.Dock = DockStyle.Right;
            DTPto.Location = new Point(45, 23);
            DTPto.Name = "DTPto";
            DTPto.Size = new Size(140, 27);
            DTPto.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(ButtonSubmit);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(600, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(30, 15, 30, 15);
            panel1.Size = new Size(200, 70);
            panel1.TabIndex = 0;
            // 
            // ButtonSubmit
            // 
            ButtonSubmit.BackColor = Color.Snow;
            ButtonSubmit.Dock = DockStyle.Fill;
            ButtonSubmit.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonSubmit.FlatStyle = FlatStyle.Flat;
            ButtonSubmit.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonSubmit.ForeColor = Color.MidnightBlue;
            ButtonSubmit.Location = new Point(30, 15);
            ButtonSubmit.Name = "ButtonSubmit";
            ButtonSubmit.Size = new Size(140, 40);
            ButtonSubmit.TabIndex = 0;
            ButtonSubmit.Text = "Submit";
            ButtonSubmit.UseVisualStyleBackColor = false;
            ButtonSubmit.Click += ButtonSubmit_Click;
            // 
            // PanelHistory
            // 
            PanelHistory.Controls.Add(PanelHistory2);
            PanelHistory.Dock = DockStyle.Fill;
            PanelHistory.Location = new Point(0, 70);
            PanelHistory.Name = "PanelHistory";
            PanelHistory.Padding = new Padding(20);
            PanelHistory.Size = new Size(800, 380);
            PanelHistory.TabIndex = 2;
            // 
            // PanelHistory2
            // 
            PanelHistory2.AutoScroll = true;
            PanelHistory2.BackColor = Color.White;
            PanelHistory2.BorderStyle = BorderStyle.FixedSingle;
            PanelHistory2.Controls.Add(DataGridViewHistory);
            PanelHistory2.Dock = DockStyle.Fill;
            PanelHistory2.Location = new Point(20, 20);
            PanelHistory2.Name = "PanelHistory2";
            PanelHistory2.Size = new Size(760, 340);
            PanelHistory2.TabIndex = 0;
            // 
            // DataGridViewHistory
            // 
            DataGridViewHistory.AllowUserToAddRows = false;
            DataGridViewHistory.AllowUserToDeleteRows = false;
            DataGridViewHistory.AllowUserToResizeColumns = false;
            DataGridViewHistory.AllowUserToResizeRows = false;
            DataGridViewHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewHistory.BackgroundColor = Color.White;
            DataGridViewHistory.BorderStyle = BorderStyle.None;
            DataGridViewHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataGridViewHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewHistory.ColumnHeadersHeight = 50;
            DataGridViewHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridViewHistory.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewHistory.Dock = DockStyle.Fill;
            DataGridViewHistory.EnableHeadersVisualStyles = false;
            DataGridViewHistory.GridColor = Color.LightGray;
            DataGridViewHistory.Location = new Point(0, 0);
            DataGridViewHistory.MultiSelect = false;
            DataGridViewHistory.Name = "DataGridViewHistory";
            DataGridViewHistory.ReadOnly = true;
            DataGridViewHistory.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DataGridViewHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewHistory.RowHeadersVisible = false;
            DataGridViewHistory.RowHeadersWidth = 51;
            DataGridViewHistory.RowTemplate.Height = 50;
            DataGridViewHistory.ScrollBars = ScrollBars.Vertical;
            DataGridViewHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewHistory.ShowEditingIcon = false;
            DataGridViewHistory.Size = new Size(758, 338);
            DataGridViewHistory.TabIndex = 1;
            DataGridViewHistory.TabStop = false;
            DataGridViewHistory.CellFormatting += DataGridViewHistory_CellFormatting;
            // 
            // HistoryScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelHistory);
            Controls.Add(PanelPicker);
            Name = "HistoryScreen";
            Text = "HistoryScreen";
            Load += HistoryScreen_Load;
            Shown += HistoryScreen_Shown;
            PanelPicker.ResumeLayout(false);
            PanelFrom.ResumeLayout(false);
            PanelTo.ResumeLayout(false);
            panel1.ResumeLayout(false);
            PanelHistory.ResumeLayout(false);
            PanelHistory2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelPicker;
        private Panel PanelHistory;
        private Panel PanelTo;
        private DateTimePicker DTPto;
        private Panel panel1;
        private Button ButtonSubmit;
        private Label LabelTo;
        private Panel PanelFrom;
        private Label label1;
        private DateTimePicker DTPFrom;
        private Panel PanelHistory2;
        private DataGridView DataGridViewHistory;
    }
}