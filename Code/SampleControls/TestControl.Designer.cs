namespace SampleControls
{
    partial class TestControl
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

        #region Component Designer generated code ^^^^^^^^^

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vs = new System.Windows.Forms.VScrollBar();
            this.hs = new System.Windows.Forms.HScrollBar();
            this.lbl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vshsPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // vs
            // 
            this.vs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vs.Location = new System.Drawing.Point(707, 1);
            this.vs.Margin = new System.Windows.Forms.Padding(1);
            this.vs.Name = "vs";
            this.tableLayoutPanel1.SetRowSpan(this.vs, 3);
            this.vs.Size = new System.Drawing.Size(16, 330);
            this.vs.TabIndex = 0;
            this.vs.Visible = false;
            // 
            // hs
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.hs, 2);
            this.hs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hs.Location = new System.Drawing.Point(1, 333);
            this.hs.Margin = new System.Windows.Forms.Padding(1);
            this.hs.Name = "hs";
            this.hs.Size = new System.Drawing.Size(704, 16);
            this.hs.TabIndex = 1;
            this.hs.Visible = false;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.SystemColors.Control;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl.Location = new System.Drawing.Point(129, 0);
            this.lbl.Name = "lbl";
            this.tableLayoutPanel1.SetRowSpan(this.lbl, 3);
            this.lbl.Size = new System.Drawing.Size(574, 332);
            this.lbl.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Controls.Add(this.vs, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.vshsPanel, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.hs, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(724, 350);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // vshsPanel
            // 
            this.vshsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vshsPanel.Location = new System.Drawing.Point(706, 332);
            this.vshsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.vshsPanel.Name = "vshsPanel";
            this.vshsPanel.Size = new System.Drawing.Size(18, 18);
            this.vshsPanel.TabIndex = 2;
            this.vshsPanel.Visible = false;
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(724, 350);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vs;
        private System.Windows.Forms.HScrollBar hs;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel vshsPanel;
    }
}
