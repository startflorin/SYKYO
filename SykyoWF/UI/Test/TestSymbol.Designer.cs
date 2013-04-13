namespace WindowsFormsApplication1.UI
{
    partial class TestSymbol
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SynonimesTextBox = new System.Windows.Forms.RichTextBox();
            this.EvaluateButton = new System.Windows.Forms.Button();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ParonymesListBox = new System.Windows.Forms.ListBox();
            this.ReferenceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.AcceptNewCheckBox = new System.Windows.Forms.CheckBox();
            this.AcceptDuplicatesCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(12, 25);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Symbol Name";
            // 
            // SynonimesTextBox
            // 
            this.SynonimesTextBox.Location = new System.Drawing.Point(12, 154);
            this.SynonimesTextBox.Name = "SynonimesTextBox";
            this.SynonimesTextBox.Size = new System.Drawing.Size(154, 96);
            this.SynonimesTextBox.TabIndex = 2;
            this.SynonimesTextBox.Text = "";
            // 
            // EvaluateButton
            // 
            this.EvaluateButton.Location = new System.Drawing.Point(118, 23);
            this.EvaluateButton.Name = "EvaluateButton";
            this.EvaluateButton.Size = new System.Drawing.Size(75, 23);
            this.EvaluateButton.TabIndex = 4;
            this.EvaluateButton.Text = "Evaluate";
            this.EvaluateButton.UseVisualStyleBackColor = true;
            this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButton_Click);
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(199, 25);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.ReadOnly = true;
            this.IDTextBox.Size = new System.Drawing.Size(73, 20);
            this.IDTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID";
            // 
            // ParonymesListBox
            // 
            this.ParonymesListBox.FormattingEnabled = true;
            this.ParonymesListBox.Location = new System.Drawing.Point(172, 155);
            this.ParonymesListBox.Name = "ParonymesListBox";
            this.ParonymesListBox.Size = new System.Drawing.Size(100, 95);
            this.ParonymesListBox.TabIndex = 7;
            this.ParonymesListBox.SelectedIndexChanged += new System.EventHandler(this.ParonymesListBox_SelectedIndexChanged);
            // 
            // ReferenceTextBox
            // 
            this.ReferenceTextBox.Location = new System.Drawing.Point(222, 69);
            this.ReferenceTextBox.Name = "ReferenceTextBox";
            this.ReferenceTextBox.Size = new System.Drawing.Size(50, 20);
            this.ReferenceTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Reference Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Create Alias";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 11;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 95);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "RadioButton";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // AcceptNewCheckBox
            // 
            this.AcceptNewCheckBox.AutoSize = true;
            this.AcceptNewCheckBox.Location = new System.Drawing.Point(141, 96);
            this.AcceptNewCheckBox.Name = "AcceptNewCheckBox";
            this.AcceptNewCheckBox.Size = new System.Drawing.Size(85, 17);
            this.AcceptNewCheckBox.TabIndex = 13;
            this.AcceptNewCheckBox.Text = "Accept New";
            this.AcceptNewCheckBox.UseVisualStyleBackColor = true;
            this.AcceptNewCheckBox.CheckedChanged += new System.EventHandler(this.AcceptNewCheckBox_CheckedChanged);
            // 
            // AcceptDuplicatesCheckBox
            // 
            this.AcceptDuplicatesCheckBox.AutoSize = true;
            this.AcceptDuplicatesCheckBox.Location = new System.Drawing.Point(141, 119);
            this.AcceptDuplicatesCheckBox.Name = "AcceptDuplicatesCheckBox";
            this.AcceptDuplicatesCheckBox.Size = new System.Drawing.Size(113, 17);
            this.AcceptDuplicatesCheckBox.TabIndex = 14;
            this.AcceptDuplicatesCheckBox.Text = "Accept Duplicates";
            this.AcceptDuplicatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // TestSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.AcceptDuplicatesCheckBox);
            this.Controls.Add(this.AcceptNewCheckBox);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReferenceTextBox);
            this.Controls.Add(this.ParonymesListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IDTextBox);
            this.Controls.Add(this.EvaluateButton);
            this.Controls.Add(this.SynonimesTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Name = "TestSymbol";
            this.Text = "TestSymbol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox SynonimesTextBox;
        private System.Windows.Forms.Button EvaluateButton;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ParonymesListBox;
        private System.Windows.Forms.TextBox ReferenceTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.CheckBox AcceptNewCheckBox;
        private System.Windows.Forms.CheckBox AcceptDuplicatesCheckBox;
    }
}