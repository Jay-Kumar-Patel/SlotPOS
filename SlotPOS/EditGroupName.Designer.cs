namespace SlotPOS
{
    partial class EditGroupName
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
            PanelAmountButton = new Panel();
            ComboBoxOldGroup = new ComboBox();
            LabelOldGroup = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            TextBoxNewGroup = new TextBox();
            label1 = new Label();
            panel5 = new Panel();
            panel6 = new Panel();
            ButtonConfirm = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            PanelAmountButton.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(LabelOldGroup);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 125);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(PanelAmountButton);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 63);
            panel2.Name = "panel2";
            panel2.Size = new Size(500, 62);
            panel2.TabIndex = 4;
            // 
            // PanelAmountButton
            // 
            PanelAmountButton.Controls.Add(ComboBoxOldGroup);
            PanelAmountButton.Dock = DockStyle.Fill;
            PanelAmountButton.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            PanelAmountButton.Location = new Point(0, 0);
            PanelAmountButton.Name = "PanelAmountButton";
            PanelAmountButton.Padding = new Padding(140, 10, 140, 0);
            PanelAmountButton.Size = new Size(500, 62);
            PanelAmountButton.TabIndex = 5;
            // 
            // ComboBoxOldGroup
            // 
            ComboBoxOldGroup.Dock = DockStyle.Fill;
            ComboBoxOldGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxOldGroup.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ComboBoxOldGroup.Location = new Point(140, 10);
            ComboBoxOldGroup.Name = "ComboBoxOldGroup";
            ComboBoxOldGroup.Size = new Size(220, 31);
            ComboBoxOldGroup.TabIndex = 0;
            ComboBoxOldGroup.KeyDown += ComboBoxOldGroup_KeyDown;
            // 
            // LabelOldGroup
            // 
            LabelOldGroup.Dock = DockStyle.Top;
            LabelOldGroup.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LabelOldGroup.Location = new Point(0, 0);
            LabelOldGroup.Name = "LabelOldGroup";
            LabelOldGroup.Size = new Size(500, 63);
            LabelOldGroup.TabIndex = 3;
            LabelOldGroup.Text = "Select Group Name:";
            LabelOldGroup.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 125);
            panel3.Name = "panel3";
            panel3.Size = new Size(500, 125);
            panel3.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Controls.Add(TextBoxNewGroup);
            panel4.Dock = DockStyle.Fill;
            panel4.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            panel4.Location = new Point(0, 63);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(140, 10, 140, 0);
            panel4.Size = new Size(500, 62);
            panel4.TabIndex = 6;
            // 
            // TextBoxNewGroup
            // 
            TextBoxNewGroup.Dock = DockStyle.Fill;
            TextBoxNewGroup.Location = new Point(140, 10);
            TextBoxNewGroup.Name = "TextBoxNewGroup";
            TextBoxNewGroup.Size = new Size(220, 39);
            TextBoxNewGroup.TabIndex = 0;
            TextBoxNewGroup.TabStop = false;
            TextBoxNewGroup.TextAlign = HorizontalAlignment.Center;
            TextBoxNewGroup.KeyDown += TextBoxNewGroup_KeyDown;
            TextBoxNewGroup.KeyPress += TextBoxNewGroup_KeyPress;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(500, 63);
            label1.TabIndex = 4;
            label1.Text = "Enter New Group Name:";
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel5
            // 
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 250);
            panel5.Name = "panel5";
            panel5.Size = new Size(500, 100);
            panel5.TabIndex = 2;
            // 
            // panel6
            // 
            panel6.Controls.Add(ButtonConfirm);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(125, 25, 125, 15);
            panel6.Size = new Size(500, 105);
            panel6.TabIndex = 3;
            // 
            // ButtonConfirm
            // 
            ButtonConfirm.BackColor = SystemColors.ActiveCaptionText;
            ButtonConfirm.Dock = DockStyle.Fill;
            ButtonConfirm.FlatAppearance.BorderSize = 0;
            ButtonConfirm.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            ButtonConfirm.ForeColor = Color.Snow;
            ButtonConfirm.Location = new Point(125, 25);
            ButtonConfirm.Name = "ButtonConfirm";
            ButtonConfirm.Size = new Size(250, 65);
            ButtonConfirm.TabIndex = 0;
            ButtonConfirm.TabStop = false;
            ButtonConfirm.Text = "Confirm";
            ButtonConfirm.UseVisualStyleBackColor = false;
            ButtonConfirm.Click += ButtonConfirm_Click;
            ButtonConfirm.KeyDown += ButtonConfirm_KeyDown;
            // 
            // EditGroupName
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(500, 350);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditGroupName";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EditGroupName";
            TopMost = true;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            PanelAmountButton.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label LabelOldGroup;
        private Panel panel2;
        private Panel PanelAmountButton;
        private Panel panel3;
        private Panel panel4;
        private TextBox TextBoxNewGroup;
        private Label label1;
        private Panel panel5;
        private Panel panel6;
        private Button ButtonConfirm;
        private ComboBox ComboBoxOldGroup;
    }
}