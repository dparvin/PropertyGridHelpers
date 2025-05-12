using System.Windows.Forms;

namespace SampleControls
{
    partial class CalculatorControl
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
            if (disposing)
            {
                _display?.Dispose();
                components?.Dispose();
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
            this._display = new System.Windows.Forms.TextBox();
            this._buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btnDiv = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btnAsk = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnPer = new System.Windows.Forms.Button();
            this.btnEql = new System.Windows.Forms.Button();
            this.btnPls = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLParn = new System.Windows.Forms.Button();
            this.btnRParn = new System.Windows.Forms.Button();
            this.btnClr = new System.Windows.Forms.Button();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _display
            // 
            this._display.AccessibleDescription = "Formula to calculate you value";
            this._display.AccessibleName = "Display";
            this._display.Dock = System.Windows.Forms.DockStyle.Top;
            this._display.Location = new System.Drawing.Point(0, 0);
            this._display.Name = "_display";
            this._display.ReadOnly = true;
            this._display.Size = new System.Drawing.Size(171, 20);
            this._display.TabIndex = 0;
            this._display.KeyDown += CalculatorControl_KeyDown;
            this._display.PreviewKeyDown += CalculatorControl_PreviewKeyDown;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.ColumnCount = 7;
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.Controls.Add(this.btn1, 1, 1);
            this._buttonsPanel.Controls.Add(this.btn2, 2, 1);
            this._buttonsPanel.Controls.Add(this.btn3, 3, 1);
            this._buttonsPanel.Controls.Add(this.btnDiv, 4, 1);
            this._buttonsPanel.Controls.Add(this.btn4, 1, 2);
            this._buttonsPanel.Controls.Add(this.btn5, 2, 2);
            this._buttonsPanel.Controls.Add(this.btn6, 3, 2);
            this._buttonsPanel.Controls.Add(this.btnAsk, 4, 2);
            this._buttonsPanel.Controls.Add(this.btn7, 1, 3);
            this._buttonsPanel.Controls.Add(this.btn8, 2, 3);
            this._buttonsPanel.Controls.Add(this.btn9, 3, 3);
            this._buttonsPanel.Controls.Add(this.btnSub, 4, 3);
            this._buttonsPanel.Controls.Add(this.btn0, 1, 4);
            this._buttonsPanel.Controls.Add(this.btnPer, 2, 4);
            this._buttonsPanel.Controls.Add(this.btnEql, 3, 4);
            this._buttonsPanel.Controls.Add(this.btnPls, 4, 4);
            this._buttonsPanel.Controls.Add(this.btnBack, 5, 1);
            this._buttonsPanel.Controls.Add(this.btnLParn, 5, 2);
            this._buttonsPanel.Controls.Add(this.btnRParn, 5, 3);
            this._buttonsPanel.Controls.Add(this.btnClr, 5, 4);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 20);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.RowCount = 6;
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.Size = new System.Drawing.Size(171, 140);
            this._buttonsPanel.TabIndex = 1;
            // 
            // btn1
            // 
            this.btn1.AccessibleDescription = "Inserts the number 1";
            this.btn1.AccessibleName = "One";
            this.btn1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn1.AutoSize = true;
            this.btn1.FlatAppearance.BorderSize = 0;
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1.Location = new System.Drawing.Point(13, 11);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(24, 25);
            this.btn1.TabIndex = 0;
            this.btn1.Tag = "1";
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn2
            // 
            this.btn2.AccessibleDescription = "Inserts the number 2";
            this.btn2.AccessibleName = "Two";
            this.btn2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn2.AutoSize = true;
            this.btn2.FlatAppearance.BorderSize = 0;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Location = new System.Drawing.Point(43, 11);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(24, 25);
            this.btn2.TabIndex = 1;
            this.btn2.Tag = "2";
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn3
            // 
            this.btn3.AccessibleDescription = "Inserts the number 3";
            this.btn3.AccessibleName = "Three";
            this.btn3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn3.AutoSize = true;
            this.btn3.FlatAppearance.BorderSize = 0;
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3.Location = new System.Drawing.Point(73, 11);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(24, 25);
            this.btn3.TabIndex = 2;
            this.btn3.Tag = "3";
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnDiv
            // 
            this.btnDiv.AccessibleDescription = "Divides the current number";
            this.btnDiv.AccessibleName = "Divide";
            this.btnDiv.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDiv.AutoSize = true;
            this.btnDiv.FlatAppearance.BorderSize = 0;
            this.btnDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiv.Location = new System.Drawing.Point(103, 11);
            this.btnDiv.Name = "btnDiv";
            this.btnDiv.Size = new System.Drawing.Size(24, 25);
            this.btnDiv.TabIndex = 11;
            this.btnDiv.Tag = "/";
            this.btnDiv.Text = "/";
            this.btnDiv.UseVisualStyleBackColor = true;
            this.btnDiv.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn4
            // 
            this.btn4.AccessibleDescription = "Inserts the number 4";
            this.btn4.AccessibleName = "Four";
            this.btn4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn4.AutoSize = true;
            this.btn4.FlatAppearance.BorderSize = 0;
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4.Location = new System.Drawing.Point(13, 42);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(24, 25);
            this.btn4.TabIndex = 3;
            this.btn4.Tag = "4";
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn5
            // 
            this.btn5.AccessibleDescription = "Inserts the number 5";
            this.btn5.AccessibleName = "Five";
            this.btn5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn5.AutoSize = true;
            this.btn5.FlatAppearance.BorderSize = 0;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Location = new System.Drawing.Point(43, 42);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(24, 25);
            this.btn5.TabIndex = 4;
            this.btn5.Tag = "5";
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn6
            // 
            this.btn6.AccessibleDescription = "Inserts the number 6";
            this.btn6.AccessibleName = "Six";
            this.btn6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn6.AutoSize = true;
            this.btn6.FlatAppearance.BorderSize = 0;
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6.Location = new System.Drawing.Point(73, 42);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(24, 25);
            this.btn6.TabIndex = 5;
            this.btn6.Tag = "6";
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnAsk
            // 
            this.btnAsk.AccessibleDescription = "Multiplies the current number";
            this.btnAsk.AccessibleName = "Multiply";
            this.btnAsk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAsk.AutoSize = true;
            this.btnAsk.FlatAppearance.BorderSize = 0;
            this.btnAsk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsk.Location = new System.Drawing.Point(103, 42);
            this.btnAsk.Name = "btnAsk";
            this.btnAsk.Size = new System.Drawing.Size(24, 25);
            this.btnAsk.TabIndex = 12;
            this.btnAsk.Tag = "*";
            this.btnAsk.Text = "*";
            this.btnAsk.UseVisualStyleBackColor = true;
            this.btnAsk.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn7
            // 
            this.btn7.AccessibleDescription = "Inserts the number 7";
            this.btn7.AccessibleName = "Seven";
            this.btn7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn7.AutoSize = true;
            this.btn7.FlatAppearance.BorderSize = 0;
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7.Location = new System.Drawing.Point(13, 73);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(24, 25);
            this.btn7.TabIndex = 6;
            this.btn7.Tag = "7";
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn8
            // 
            this.btn8.AccessibleDescription = "Inserts the number 8";
            this.btn8.AccessibleName = "Eight";
            this.btn8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn8.AutoSize = true;
            this.btn8.FlatAppearance.BorderSize = 0;
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8.Location = new System.Drawing.Point(43, 73);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(24, 25);
            this.btn8.TabIndex = 7;
            this.btn8.Tag = "8";
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn9
            // 
            this.btn9.AccessibleDescription = "Inserts the number 9";
            this.btn9.AccessibleName = "Nine";
            this.btn9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn9.AutoSize = true;
            this.btn9.FlatAppearance.BorderSize = 0;
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn9.Location = new System.Drawing.Point(73, 73);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(24, 25);
            this.btn9.TabIndex = 8;
            this.btn9.Tag = "9";
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnSub
            // 
            this.btnSub.AccessibleDescription = "Subtracts the current number";
            this.btnSub.AccessibleName = "Subtract";
            this.btnSub.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSub.AutoSize = true;
            this.btnSub.FlatAppearance.BorderSize = 0;
            this.btnSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSub.Location = new System.Drawing.Point(103, 73);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(24, 25);
            this.btnSub.TabIndex = 13;
            this.btnSub.Tag = "-";
            this.btnSub.Text = "-";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btn0
            // 
            this.btn0.AccessibleDescription = "Inserts the number 0";
            this.btn0.AccessibleName = "Zero";
            this.btn0.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn0.AutoSize = true;
            this.btn0.FlatAppearance.BorderSize = 0;
            this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn0.Location = new System.Drawing.Point(13, 104);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(24, 25);
            this.btn0.TabIndex = 9;
            this.btn0.Tag = "0";
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnPer
            // 
            this.btnPer.AccessibleDescription = "Inserts a decimal point";
            this.btnPer.AccessibleName = "Decimal";
            this.btnPer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPer.AutoSize = true;
            this.btnPer.FlatAppearance.BorderSize = 0;
            this.btnPer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPer.Location = new System.Drawing.Point(43, 104);
            this.btnPer.Name = "btnPer";
            this.btnPer.Size = new System.Drawing.Size(24, 25);
            this.btnPer.TabIndex = 10;
            this.btnPer.Tag = ".";
            this.btnPer.Text = ".";
            this.btnPer.UseVisualStyleBackColor = true;
            this.btnPer.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnEql
            // 
            this.btnEql.AccessibleDescription = "Calculates the result";
            this.btnEql.AccessibleName = "Equals";
            this.btnEql.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEql.AutoSize = true;
            this.btnEql.FlatAppearance.BorderSize = 0;
            this.btnEql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEql.Location = new System.Drawing.Point(73, 104);
            this.btnEql.Name = "btnEql";
            this.btnEql.Size = new System.Drawing.Size(24, 25);
            this.btnEql.TabIndex = 19;
            this.btnEql.Tag = "=";
            this.btnEql.Text = "=";
            this.btnEql.UseVisualStyleBackColor = true;
            this.btnEql.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnPls
            // 
            this.btnPls.AccessibleDescription = "Adds the current number";
            this.btnPls.AccessibleName = "Add";
            this.btnPls.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPls.AutoSize = true;
            this.btnPls.FlatAppearance.BorderSize = 0;
            this.btnPls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPls.Location = new System.Drawing.Point(103, 104);
            this.btnPls.Name = "btnPls";
            this.btnPls.Size = new System.Drawing.Size(24, 25);
            this.btnPls.TabIndex = 14;
            this.btnPls.Tag = "+";
            this.btnPls.Text = "+";
            this.btnPls.UseVisualStyleBackColor = true;
            this.btnPls.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleDescription = "Backspace to delete prior input";
            this.btnBack.AccessibleName = "Backspace";
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(133, 11);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(24, 23);
            this.btnBack.TabIndex = 15;
            this.btnBack.Tag = "<";
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnLParn
            // 
            this.btnLParn.AccessibleDescription = "Inserts an opening parenthesis";
            this.btnLParn.AccessibleName = "Left Parenthesis";
            this.btnLParn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLParn.FlatAppearance.BorderSize = 0;
            this.btnLParn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLParn.Location = new System.Drawing.Point(133, 42);
            this.btnLParn.Name = "btnLParn";
            this.btnLParn.Size = new System.Drawing.Size(24, 23);
            this.btnLParn.TabIndex = 16;
            this.btnLParn.Tag = "(";
            this.btnLParn.Text = "(";
            this.btnLParn.UseVisualStyleBackColor = true;
            this.btnLParn.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnRParn
            // 
            this.btnRParn.AccessibleDescription = "Inserts a closing parenthesis";
            this.btnRParn.AccessibleName = "Right Parenthesis";
            this.btnRParn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRParn.FlatAppearance.BorderSize = 0;
            this.btnRParn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRParn.Location = new System.Drawing.Point(133, 73);
            this.btnRParn.Name = "btnRParn";
            this.btnRParn.Size = new System.Drawing.Size(24, 23);
            this.btnRParn.TabIndex = 17;
            this.btnRParn.Tag = ")";
            this.btnRParn.Text = "}";
            this.btnRParn.UseVisualStyleBackColor = true;
            this.btnRParn.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnClr
            // 
            this.btnClr.AccessibleDescription = "Clears the current input";
            this.btnClr.AccessibleName = "Clear";
            this.btnClr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClr.FlatAppearance.BorderSize = 0;
            this.btnClr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClr.Location = new System.Drawing.Point(133, 104);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(24, 23);
            this.btnClr.TabIndex = 18;
            this.btnClr.Tag = "C";
            this.btnClr.Text = "C";
            this.btnClr.UseVisualStyleBackColor = true;
            this.btnClr.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // CalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buttonsPanel);
            this.Controls.Add(this._display);
            this.Name = "CalculatorControl";
            this.Size = new System.Drawing.Size(171, 160);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalculatorControl_KeyDown);
            this._buttonsPanel.ResumeLayout(false);
            this._buttonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private TextBox _display;
        private TableLayoutPanel _buttonsPanel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btnDiv;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btnAsk;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btnSub;
        private Button btn0;
        private Button btnPer;
        private Button btnEql;
        private Button btnPls;
        private Button btnBack;
        private Button btnLParn;
        private Button btnRParn;
        private Button btnClr;

        private readonly ToolTip _tooltip = new ToolTip();
        private decimal _value = 0;
        private string _expression = string.Empty;
    }
}
