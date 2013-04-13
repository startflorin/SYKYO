namespace WindowsFormsApplication1.UI
{
    partial class TestServer
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
            this.DuplicateCheckBox = new System.Windows.Forms.CheckBox();
            this.EvaluateButton = new System.Windows.Forms.Button();
            this.CreateCheckBox = new System.Windows.Forms.CheckBox();
            this.ResultRichTextBox = new System.Windows.Forms.RichTextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DuplicateCheckBox
            // 
            this.DuplicateCheckBox.AutoSize = true;
            this.DuplicateCheckBox.Location = new System.Drawing.Point(12, 90);
            this.DuplicateCheckBox.Name = "DuplicateCheckBox";
            this.DuplicateCheckBox.Size = new System.Drawing.Size(158, 17);
            this.DuplicateCheckBox.TabIndex = 0;
            this.DuplicateCheckBox.Text = "Force Creation As Duplicate";
            this.DuplicateCheckBox.UseVisualStyleBackColor = true;
            this.DuplicateCheckBox.CheckedChanged += new System.EventHandler(this.DuplicateCheckBox_CheckedChanged);
            // 
            // EvaluateButton
            // 
            this.EvaluateButton.Location = new System.Drawing.Point(177, 113);
            this.EvaluateButton.Name = "EvaluateButton";
            this.EvaluateButton.Size = new System.Drawing.Size(75, 23);
            this.EvaluateButton.TabIndex = 1;
            this.EvaluateButton.Text = "GO !";
            this.EvaluateButton.UseVisualStyleBackColor = true;
            this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButton_Click);
            // 
            // CreateCheckBox
            // 
            this.CreateCheckBox.AutoSize = true;
            this.CreateCheckBox.Location = new System.Drawing.Point(12, 113);
            this.CreateCheckBox.Name = "CreateCheckBox";
            this.CreateCheckBox.Size = new System.Drawing.Size(116, 17);
            this.CreateCheckBox.TabIndex = 2;
            this.CreateCheckBox.Text = "Create If Not Exists";
            this.CreateCheckBox.UseVisualStyleBackColor = true;
            this.CreateCheckBox.CheckedChanged += new System.EventHandler(this.CreateCheckBox_CheckedChanged);
            // 
            // ResultRichTextBox
            // 
            this.ResultRichTextBox.Location = new System.Drawing.Point(12, 142);
            this.ResultRichTextBox.Name = "ResultRichTextBox";
            this.ResultRichTextBox.Size = new System.Drawing.Size(240, 122);
            this.ResultRichTextBox.TabIndex = 3;
            this.ResultRichTextBox.Text = "Find the symbol name by ID;\nFind the symbol ID by name;\nCreate new symbol if not " +
    "exist;\nCreate a duplicate is symbol esysts;";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(90, 38);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(162, 20);
            this.NameTextBox.TabIndex = 4;
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(90, 12);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(162, 20);
            this.IDTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Symbol Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Symbol ID";
            // 
            // CountTextBox
            // 
            this.CountTextBox.Location = new System.Drawing.Point(90, 64);
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.ReadOnly = true;
            this.CountTextBox.Size = new System.Drawing.Size(162, 20);
            this.CountTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Count";
            // 
            // TestServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 276);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CountTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.ResultRichTextBox);
            this.Controls.Add(this.CreateCheckBox);
            this.Controls.Add(this.EvaluateButton);
            this.Controls.Add(this.DuplicateCheckBox);
            this.Name = "TestServer";
            this.Text = "TestServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DuplicateCheckBox;
        private System.Windows.Forms.Button EvaluateButton;
        private System.Windows.Forms.CheckBox CreateCheckBox;
        private System.Windows.Forms.RichTextBox ResultRichTextBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CountTextBox;
        private System.Windows.Forms.Label label3;
    }
}