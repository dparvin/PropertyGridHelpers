namespace SampleForms
{
    partial class Form1
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.testControl1 = new SampleControls.TestControl();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(8, 185);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(716, 184);
            this.propertyGrid1.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(8, 182);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(716, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // testControl1
            // 
            this.testControl1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.testControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.testControl1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.testControl1.Location = new System.Drawing.Point(8, 8);
            this.testControl1.Margin = new System.Windows.Forms.Padding(2);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(716, 174);
            this.testControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 377);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.testControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private SampleControls.TestControl testControl1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Splitter splitter1;
    }
}

