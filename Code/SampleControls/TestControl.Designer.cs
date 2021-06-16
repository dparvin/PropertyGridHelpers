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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vs = new System.Windows.Forms.VScrollBar();
            this.hs = new System.Windows.Forms.HScrollBar();
            this.vshsPanel = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // vs
            // 
            this.vs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vs.Location = new System.Drawing.Point(598, 0);
            this.vs.Name = "vs";
            this.vs.Size = new System.Drawing.Size(18, 331);
            this.vs.TabIndex = 0;
            this.vs.Visible = false;
            // 
            // hs
            // 
            this.hs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hs.Location = new System.Drawing.Point(0, 332);
            this.hs.Name = "hs";
            this.hs.Size = new System.Drawing.Size(598, 18);
            this.hs.TabIndex = 1;
            this.hs.Visible = false;
            // 
            // vshsPanel
            // 
            this.vshsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vshsPanel.Location = new System.Drawing.Point(598, 332);
            this.vshsPanel.Name = "vshsPanel";
            this.vshsPanel.Size = new System.Drawing.Size(18, 18);
            this.vshsPanel.TabIndex = 2;
            this.vshsPanel.Visible = false;
            // 
            // lbl
            // 
            this.lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl.BackColor = System.Drawing.SystemColors.Control;
            this.lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl.Location = new System.Drawing.Point(3, 3);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(595, 328);
            this.lbl.TabIndex = 3;
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.vs);
            this.Controls.Add(this.hs);
            this.Controls.Add(this.vshsPanel);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(616, 350);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vs;
        private System.Windows.Forms.HScrollBar hs;
        private System.Windows.Forms.Panel vshsPanel;
        private System.Windows.Forms.Label lbl;
    }
}
