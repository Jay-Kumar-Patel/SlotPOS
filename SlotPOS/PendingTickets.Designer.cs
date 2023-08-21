namespace SlotPOS
{
    partial class PendingTickets
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
            LabelGameName = new Label();
            LabelMachineNo = new Label();
            PanelTable = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(41, 39, 40);
            panel1.Controls.Add(LabelGameName);
            panel1.Controls.Add(LabelMachineNo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(810, 60);
            panel1.TabIndex = 0;
            // 
            // LabelGameName
            // 
            LabelGameName.Dock = DockStyle.Right;
            LabelGameName.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            LabelGameName.ForeColor = Color.Snow;
            LabelGameName.Location = new Point(461, 0);
            LabelGameName.Name = "LabelGameName";
            LabelGameName.Padding = new Padding(0, 0, 40, 0);
            LabelGameName.Size = new Size(349, 60);
            LabelGameName.TabIndex = 1;
            LabelGameName.Text = "Game Name :";
            LabelGameName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelMachineNo
            // 
            LabelMachineNo.Dock = DockStyle.Left;
            LabelMachineNo.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            LabelMachineNo.ForeColor = Color.Snow;
            LabelMachineNo.Location = new Point(0, 0);
            LabelMachineNo.Name = "LabelMachineNo";
            LabelMachineNo.Padding = new Padding(40, 0, 0, 0);
            LabelMachineNo.Size = new Size(455, 60);
            LabelMachineNo.TabIndex = 0;
            LabelMachineNo.Text = "Machine No. :";
            LabelMachineNo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PanelTable
            // 
            PanelTable.BackColor = Color.White;
            PanelTable.Dock = DockStyle.Fill;
            PanelTable.Location = new Point(0, 60);
            PanelTable.Name = "PanelTable";
            PanelTable.Size = new Size(810, 442);
            PanelTable.TabIndex = 1;
            // 
            // PendingTickets
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 502);
            Controls.Add(PanelTable);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PendingTickets";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "pendingTickets";
            TopMost = true;
            Load += pendingTickets_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label LabelGameName;
        private Label LabelMachineNo;
        private Panel PanelTable;

    }
}