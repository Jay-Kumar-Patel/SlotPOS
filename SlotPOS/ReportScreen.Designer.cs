namespace SlotPOS
{
    partial class ReportScreen
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
            PanelUpper = new Panel();
            panel2 = new Panel();
            ComboBoxReport = new ComboBox();
            PanelFrom = new Panel();
            label1 = new Label();
            DTPFrom = new DateTimePicker();
            PanelTo = new Panel();
            LabelTo = new Label();
            DTPto = new DateTimePicker();
            panel1 = new Panel();
            ButtonSubmit = new Button();
            PanelPrint = new Panel();
            ButtonPrint = new Button();
            PanelMain = new Panel();
            PanelPDF = new Panel();
            PanelUpper.SuspendLayout();
            panel2.SuspendLayout();
            PanelFrom.SuspendLayout();
            PanelTo.SuspendLayout();
            panel1.SuspendLayout();
            PanelPrint.SuspendLayout();
            PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // PanelUpper
            // 
            PanelUpper.Controls.Add(panel2);
            PanelUpper.Controls.Add(PanelFrom);
            PanelUpper.Controls.Add(PanelTo);
            PanelUpper.Controls.Add(panel1);
            PanelUpper.Controls.Add(PanelPrint);
            PanelUpper.Dock = DockStyle.Top;
            PanelUpper.Location = new Point(0, 0);
            PanelUpper.Name = "PanelUpper";
            PanelUpper.Size = new Size(1200, 70);
            PanelUpper.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(ComboBoxReport);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(160, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 22, 10, 20);
            panel2.Size = new Size(220, 70);
            panel2.TabIndex = 4;
            // 
            // ComboBoxReport
            // 
            ComboBoxReport.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxReport.FormattingEnabled = true;
            ComboBoxReport.Location = new Point(64, 22);
            ComboBoxReport.Name = "ComboBoxReport";
            ComboBoxReport.Size = new Size(151, 28);
            ComboBoxReport.TabIndex = 0;
            // 
            // PanelFrom
            // 
            PanelFrom.Controls.Add(label1);
            PanelFrom.Controls.Add(DTPFrom);
            PanelFrom.Dock = DockStyle.Right;
            PanelFrom.Location = new Point(380, 0);
            PanelFrom.Name = "PanelFrom";
            PanelFrom.Padding = new Padding(0, 23, 25, 20);
            PanelFrom.Size = new Size(250, 70);
            PanelFrom.TabIndex = 3;
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
            PanelTo.Location = new Point(630, 0);
            PanelTo.Name = "PanelTo";
            PanelTo.Padding = new Padding(0, 23, 5, 20);
            PanelTo.Size = new Size(190, 70);
            PanelTo.TabIndex = 2;
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
            panel1.Location = new Point(820, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(30, 15, 10, 15);
            panel1.Size = new Size(180, 70);
            panel1.TabIndex = 1;
            // 
            // ButtonSubmit
            // 
            ButtonSubmit.BackColor = Color.Snow;
            ButtonSubmit.Dock = DockStyle.Fill;
            ButtonSubmit.FlatStyle = FlatStyle.Flat;
            ButtonSubmit.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonSubmit.ForeColor = Color.MidnightBlue;
            ButtonSubmit.Location = new Point(30, 15);
            ButtonSubmit.Name = "ButtonSubmit";
            ButtonSubmit.Size = new Size(140, 40);
            ButtonSubmit.TabIndex = 1;
            ButtonSubmit.Text = "Submit";
            ButtonSubmit.UseVisualStyleBackColor = false;
            ButtonSubmit.Click += ButtonSubmit_Click;
            // 
            // PanelPrint
            // 
            PanelPrint.Controls.Add(ButtonPrint);
            PanelPrint.Dock = DockStyle.Right;
            PanelPrint.Location = new Point(1000, 0);
            PanelPrint.Name = "PanelPrint";
            PanelPrint.Padding = new Padding(30, 15, 30, 15);
            PanelPrint.Size = new Size(200, 70);
            PanelPrint.TabIndex = 0;
            // 
            // ButtonPrint
            // 
            ButtonPrint.BackColor = Color.Snow;
            ButtonPrint.Dock = DockStyle.Fill;
            ButtonPrint.FlatStyle = FlatStyle.Flat;
            ButtonPrint.Font = new Font("Bookman Old Style", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonPrint.ForeColor = Color.MidnightBlue;
            ButtonPrint.Location = new Point(30, 15);
            ButtonPrint.Name = "ButtonPrint";
            ButtonPrint.Size = new Size(140, 40);
            ButtonPrint.TabIndex = 1;
            ButtonPrint.Text = "Print";
            ButtonPrint.UseVisualStyleBackColor = false;
            ButtonPrint.Click += ButtonPrint_Click;
            // 
            // PanelMain
            // 
            PanelMain.Controls.Add(PanelPDF);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.Location = new Point(0, 70);
            PanelMain.Name = "PanelMain";
            PanelMain.Padding = new Padding(30, 15, 30, 30);
            PanelMain.Size = new Size(1200, 380);
            PanelMain.TabIndex = 1;
            // 
            // PanelPDF
            // 
            PanelPDF.AutoScroll = true;
            PanelPDF.BackColor = Color.AliceBlue;
            PanelPDF.Dock = DockStyle.Fill;
            PanelPDF.Location = new Point(30, 15);
            PanelPDF.Name = "PanelPDF";
            PanelPDF.Padding = new Padding(20);
            PanelPDF.Size = new Size(1140, 335);
            PanelPDF.TabIndex = 0;
            // 
            // ReportScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 450);
            Controls.Add(PanelMain);
            Controls.Add(PanelUpper);
            Name = "ReportScreen";
            Text = "ReportScreen";
            PanelUpper.ResumeLayout(false);
            panel2.ResumeLayout(false);
            PanelFrom.ResumeLayout(false);
            PanelTo.ResumeLayout(false);
            panel1.ResumeLayout(false);
            PanelPrint.ResumeLayout(false);
            PanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelUpper;
        private Panel PanelPrint;
        private Button ButtonPrint;
        private Panel panel1;
        private Button ButtonSubmit;
        private Panel PanelTo;
        private Label LabelTo;
        private DateTimePicker DTPto;
        private Panel PanelFrom;
        private Label label1;
        private DateTimePicker DTPFrom;
        private Panel panel2;
        private ComboBox ComboBoxReport;
        private Panel PanelMain;
        private Panel PanelPDF;
    }
}