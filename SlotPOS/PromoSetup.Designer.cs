namespace SlotPOS
{
    partial class PromoSetup
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
            tableLayoutPanel1 = new TableLayoutPanel();
            LabelPromo = new Label();
            LabelPromoAmount = new Label();
            LabelMatchPlay = new Label();
            LabelMatchPlayAmount = new Label();
            TextBoxPromo = new TextBox();
            TextBoxMatchPlay = new TextBox();
            ButtonPromo = new Button();
            ButtonMatchPlay = new Button();
            CheckBoxPromo = new CheckBox();
            CheckBoxMatchPlay = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Controls.Add(LabelPromo, 0, 1);
            tableLayoutPanel1.Controls.Add(LabelPromoAmount, 0, 2);
            tableLayoutPanel1.Controls.Add(LabelMatchPlay, 0, 4);
            tableLayoutPanel1.Controls.Add(LabelMatchPlayAmount, 0, 5);
            tableLayoutPanel1.Controls.Add(TextBoxPromo, 1, 2);
            tableLayoutPanel1.Controls.Add(TextBoxMatchPlay, 1, 5);
            tableLayoutPanel1.Controls.Add(ButtonPromo, 2, 2);
            tableLayoutPanel1.Controls.Add(ButtonMatchPlay, 2, 5);
            tableLayoutPanel1.Controls.Add(CheckBoxPromo, 1, 1);
            tableLayoutPanel1.Controls.Add(CheckBoxMatchPlay, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.Size = new Size(490, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // LabelPromo
            // 
            LabelPromo.Dock = DockStyle.Fill;
            LabelPromo.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelPromo.Location = new Point(3, 63);
            LabelPromo.Name = "LabelPromo";
            LabelPromo.Size = new Size(141, 63);
            LabelPromo.TabIndex = 0;
            LabelPromo.Text = "Promo:";
            LabelPromo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelPromoAmount
            // 
            LabelPromoAmount.Dock = DockStyle.Fill;
            LabelPromoAmount.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelPromoAmount.Location = new Point(3, 126);
            LabelPromoAmount.Name = "LabelPromoAmount";
            LabelPromoAmount.Size = new Size(141, 63);
            LabelPromoAmount.TabIndex = 1;
            LabelPromoAmount.Text = "Amount:";
            LabelPromoAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelMatchPlay
            // 
            LabelMatchPlay.Dock = DockStyle.Fill;
            LabelMatchPlay.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelMatchPlay.Location = new Point(3, 261);
            LabelMatchPlay.Name = "LabelMatchPlay";
            LabelMatchPlay.Size = new Size(141, 63);
            LabelMatchPlay.TabIndex = 2;
            LabelMatchPlay.Text = "Match Play:";
            LabelMatchPlay.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelMatchPlayAmount
            // 
            LabelMatchPlayAmount.Dock = DockStyle.Fill;
            LabelMatchPlayAmount.Font = new Font("Bookman Old Style", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelMatchPlayAmount.Location = new Point(3, 324);
            LabelMatchPlayAmount.Name = "LabelMatchPlayAmount";
            LabelMatchPlayAmount.Size = new Size(141, 63);
            LabelMatchPlayAmount.TabIndex = 3;
            LabelMatchPlayAmount.Text = "Amount:";
            LabelMatchPlayAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TextBoxPromo
            // 
            TextBoxPromo.Dock = DockStyle.Fill;
            TextBoxPromo.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxPromo.Location = new Point(154, 144);
            TextBoxPromo.Margin = new Padding(7, 18, 7, 12);
            TextBoxPromo.MaxLength = 7;
            TextBoxPromo.Name = "TextBoxPromo";
            TextBoxPromo.Size = new Size(182, 27);
            TextBoxPromo.TabIndex = 4;
            TextBoxPromo.TabStop = false;
            TextBoxPromo.TextAlign = HorizontalAlignment.Right;
            TextBoxPromo.KeyPress += TextBoxPromo_KeyPress;
            // 
            // TextBoxMatchPlay
            // 
            TextBoxMatchPlay.Dock = DockStyle.Fill;
            TextBoxMatchPlay.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxMatchPlay.Location = new Point(154, 342);
            TextBoxMatchPlay.Margin = new Padding(7, 18, 7, 12);
            TextBoxMatchPlay.MaxLength = 7;
            TextBoxMatchPlay.Name = "TextBoxMatchPlay";
            TextBoxMatchPlay.Size = new Size(182, 27);
            TextBoxMatchPlay.TabIndex = 5;
            TextBoxMatchPlay.TabStop = false;
            TextBoxMatchPlay.TextAlign = HorizontalAlignment.Right;
            TextBoxMatchPlay.KeyPress += TextBoxMatchPlay_KeyPress;
            // 
            // ButtonPromo
            // 
            ButtonPromo.BackColor = Color.Snow;
            ButtonPromo.Dock = DockStyle.Fill;
            ButtonPromo.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonPromo.FlatStyle = FlatStyle.Flat;
            ButtonPromo.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonPromo.ForeColor = Color.Black;
            ButtonPromo.Location = new Point(373, 138);
            ButtonPromo.Margin = new Padding(30, 12, 30, 13);
            ButtonPromo.Name = "ButtonPromo";
            ButtonPromo.Size = new Size(87, 38);
            ButtonPromo.TabIndex = 6;
            ButtonPromo.TabStop = false;
            ButtonPromo.Text = "Set";
            ButtonPromo.UseVisualStyleBackColor = false;
            ButtonPromo.Click += ButtonPromo_Click;
            // 
            // ButtonMatchPlay
            // 
            ButtonMatchPlay.BackColor = Color.Snow;
            ButtonMatchPlay.Dock = DockStyle.Fill;
            ButtonMatchPlay.FlatAppearance.BorderColor = Color.MidnightBlue;
            ButtonMatchPlay.FlatStyle = FlatStyle.Flat;
            ButtonMatchPlay.Font = new Font("Bookman Old Style", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonMatchPlay.Location = new Point(373, 336);
            ButtonMatchPlay.Margin = new Padding(30, 12, 30, 13);
            ButtonMatchPlay.Name = "ButtonMatchPlay";
            ButtonMatchPlay.Size = new Size(87, 38);
            ButtonMatchPlay.TabIndex = 7;
            ButtonMatchPlay.TabStop = false;
            ButtonMatchPlay.Text = "Set";
            ButtonMatchPlay.UseVisualStyleBackColor = false;
            ButtonMatchPlay.Click += ButtonMatchPlay_Click;
            // 
            // CheckBoxPromo
            // 
            CheckBoxPromo.Appearance = Appearance.Button;
            CheckBoxPromo.AutoSize = true;
            CheckBoxPromo.BackColor = Color.Snow;
            CheckBoxPromo.Dock = DockStyle.Fill;
            CheckBoxPromo.FlatAppearance.BorderColor = Color.MidnightBlue;
            CheckBoxPromo.FlatStyle = FlatStyle.Flat;
            CheckBoxPromo.Location = new Point(162, 75);
            CheckBoxPromo.Margin = new Padding(15, 12, 75, 13);
            CheckBoxPromo.Name = "CheckBoxPromo";
            CheckBoxPromo.Size = new Size(106, 38);
            CheckBoxPromo.TabIndex = 8;
            CheckBoxPromo.TabStop = false;
            CheckBoxPromo.Text = "Enabled";
            CheckBoxPromo.TextAlign = ContentAlignment.MiddleCenter;
            CheckBoxPromo.UseVisualStyleBackColor = false;
            CheckBoxPromo.CheckedChanged += CheckBoxPromo_CheckedChanged;
            // 
            // CheckBoxMatchPlay
            // 
            CheckBoxMatchPlay.Appearance = Appearance.Button;
            CheckBoxMatchPlay.AutoSize = true;
            CheckBoxMatchPlay.BackColor = Color.Snow;
            CheckBoxMatchPlay.Dock = DockStyle.Fill;
            CheckBoxMatchPlay.FlatAppearance.BorderColor = Color.MidnightBlue;
            CheckBoxMatchPlay.FlatStyle = FlatStyle.Flat;
            CheckBoxMatchPlay.Location = new Point(162, 273);
            CheckBoxMatchPlay.Margin = new Padding(15, 12, 75, 13);
            CheckBoxMatchPlay.Name = "CheckBoxMatchPlay";
            CheckBoxMatchPlay.Size = new Size(106, 38);
            CheckBoxMatchPlay.TabIndex = 9;
            CheckBoxMatchPlay.TabStop = false;
            CheckBoxMatchPlay.Text = "Enabled";
            CheckBoxMatchPlay.TextAlign = ContentAlignment.MiddleCenter;
            CheckBoxMatchPlay.UseVisualStyleBackColor = false;
            CheckBoxMatchPlay.CheckedChanged += CheckBoxMatchPlay_CheckedChanged;
            // 
            // PromoSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(490, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "PromoSetup";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PromoSetup";
            TopMost = true;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label LabelPromo;
        private Label LabelPromoAmount;
        private Label LabelMatchPlay;
        private Label LabelMatchPlayAmount;
        private TextBox TextBoxPromo;
        private TextBox TextBoxMatchPlay;
        private Button ButtonPromo;
        private Button ButtonMatchPlay;
        private CheckBox CheckBoxPromo;
        private CheckBox CheckBoxMatchPlay;
    }
}