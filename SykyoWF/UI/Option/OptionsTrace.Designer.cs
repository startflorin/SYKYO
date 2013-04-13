namespace WindowsFormsApplication1.UI
{
    partial class TraceOptions
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.LogLogicsParametersRadioButton = new System.Windows.Forms.RadioButton();
            this.bindingSourceOptionsTrace = new System.Windows.Forms.BindingSource(this.components);
            this.LogLogicsResultsRadioButton = new System.Windows.Forms.RadioButton();
            this.LogLogicsCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.LogLogicsNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LogRelationsParametersRadioButton = new System.Windows.Forms.RadioButton();
            this.LogRelationsResultsRadioButton = new System.Windows.Forms.RadioButton();
            this.LogRelationsCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.LogRelationsNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.LogObjectsParametersRadioButton = new System.Windows.Forms.RadioButton();
            this.LogObjectsResultsRadioButton = new System.Windows.Forms.RadioButton();
            this.LogObjectsCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.LogObjectsNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LogNumbersParametersRadioButton = new System.Windows.Forms.RadioButton();
            this.LogNumbersResultsRadioButton = new System.Windows.Forms.RadioButton();
            this.LogNumbersCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.LogNumbersNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HistoryTextBox = new System.Windows.Forms.RichTextBox();
            this.SaveToDefaultsTextButton = new System.Windows.Forms.Button();
            this.ResetFromDefaultsTextButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOptionsTrace)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 260);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log Levels";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.LogLogicsParametersRadioButton);
            this.groupBox7.Controls.Add(this.LogLogicsResultsRadioButton);
            this.groupBox7.Controls.Add(this.LogLogicsCodeRadioButton);
            this.groupBox7.Controls.Add(this.LogLogicsNoneRadioButton);
            this.groupBox7.Location = new System.Drawing.Point(6, 196);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(274, 53);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Logics from Relations";
            // 
            // LogLogicsParametersRadioButton
            // 
            this.LogLogicsParametersRadioButton.AutoSize = true;
            this.LogLogicsParametersRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogLogicsParameters", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogLogicsParametersRadioButton.Location = new System.Drawing.Point(129, 19);
            this.LogLogicsParametersRadioButton.Name = "LogLogicsParametersRadioButton";
            this.LogLogicsParametersRadioButton.Size = new System.Drawing.Size(78, 17);
            this.LogLogicsParametersRadioButton.TabIndex = 1;
            this.LogLogicsParametersRadioButton.TabStop = true;
            this.LogLogicsParametersRadioButton.Text = "Parameters";
            this.LogLogicsParametersRadioButton.UseVisualStyleBackColor = true;
            // 
            // bindingSourceOptionsTrace
            // 
            this.bindingSourceOptionsTrace.DataSource = typeof(WindowsFormsApplication1.DataAccess.Options.LoggingSystemOptions);
            // 
            // LogLogicsResultsRadioButton
            // 
            this.LogLogicsResultsRadioButton.AutoSize = true;
            this.LogLogicsResultsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogLogicsResults", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogLogicsResultsRadioButton.Location = new System.Drawing.Point(63, 19);
            this.LogLogicsResultsRadioButton.Name = "LogLogicsResultsRadioButton";
            this.LogLogicsResultsRadioButton.Size = new System.Drawing.Size(60, 17);
            this.LogLogicsResultsRadioButton.TabIndex = 0;
            this.LogLogicsResultsRadioButton.TabStop = true;
            this.LogLogicsResultsRadioButton.Text = "Results";
            this.LogLogicsResultsRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogLogicsCodeRadioButton
            // 
            this.LogLogicsCodeRadioButton.AutoSize = true;
            this.LogLogicsCodeRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogLogicsCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogLogicsCodeRadioButton.Location = new System.Drawing.Point(213, 21);
            this.LogLogicsCodeRadioButton.Name = "LogLogicsCodeRadioButton";
            this.LogLogicsCodeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.LogLogicsCodeRadioButton.TabIndex = 2;
            this.LogLogicsCodeRadioButton.TabStop = true;
            this.LogLogicsCodeRadioButton.Text = "Code";
            this.LogLogicsCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogLogicsNoneRadioButton
            // 
            this.LogLogicsNoneRadioButton.AutoSize = true;
            this.LogLogicsNoneRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogLogicsNone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogLogicsNoneRadioButton.Location = new System.Drawing.Point(6, 19);
            this.LogLogicsNoneRadioButton.Name = "LogLogicsNoneRadioButton";
            this.LogLogicsNoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.LogLogicsNoneRadioButton.TabIndex = 3;
            this.LogLogicsNoneRadioButton.TabStop = true;
            this.LogLogicsNoneRadioButton.Text = "None";
            this.LogLogicsNoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LogRelationsParametersRadioButton);
            this.groupBox6.Controls.Add(this.LogRelationsResultsRadioButton);
            this.groupBox6.Controls.Add(this.LogRelationsCodeRadioButton);
            this.groupBox6.Controls.Add(this.LogRelationsNoneRadioButton);
            this.groupBox6.Location = new System.Drawing.Point(6, 137);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(274, 53);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Relations from Objects";
            // 
            // LogRelationsParametersRadioButton
            // 
            this.LogRelationsParametersRadioButton.AutoSize = true;
            this.LogRelationsParametersRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogRelationsParameters", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogRelationsParametersRadioButton.Location = new System.Drawing.Point(129, 19);
            this.LogRelationsParametersRadioButton.Name = "LogRelationsParametersRadioButton";
            this.LogRelationsParametersRadioButton.Size = new System.Drawing.Size(78, 17);
            this.LogRelationsParametersRadioButton.TabIndex = 1;
            this.LogRelationsParametersRadioButton.TabStop = true;
            this.LogRelationsParametersRadioButton.Text = "Parameters";
            this.LogRelationsParametersRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogRelationsResultsRadioButton
            // 
            this.LogRelationsResultsRadioButton.AutoSize = true;
            this.LogRelationsResultsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogRelationsResults", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogRelationsResultsRadioButton.Location = new System.Drawing.Point(63, 19);
            this.LogRelationsResultsRadioButton.Name = "LogRelationsResultsRadioButton";
            this.LogRelationsResultsRadioButton.Size = new System.Drawing.Size(60, 17);
            this.LogRelationsResultsRadioButton.TabIndex = 0;
            this.LogRelationsResultsRadioButton.TabStop = true;
            this.LogRelationsResultsRadioButton.Text = "Results";
            this.LogRelationsResultsRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogRelationsCodeRadioButton
            // 
            this.LogRelationsCodeRadioButton.AutoSize = true;
            this.LogRelationsCodeRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogObjectsCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogRelationsCodeRadioButton.Location = new System.Drawing.Point(213, 21);
            this.LogRelationsCodeRadioButton.Name = "LogRelationsCodeRadioButton";
            this.LogRelationsCodeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.LogRelationsCodeRadioButton.TabIndex = 2;
            this.LogRelationsCodeRadioButton.TabStop = true;
            this.LogRelationsCodeRadioButton.Text = "Code";
            this.LogRelationsCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogRelationsNoneRadioButton
            // 
            this.LogRelationsNoneRadioButton.AutoSize = true;
            this.LogRelationsNoneRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogRelationsNone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogRelationsNoneRadioButton.Location = new System.Drawing.Point(6, 19);
            this.LogRelationsNoneRadioButton.Name = "LogRelationsNoneRadioButton";
            this.LogRelationsNoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.LogRelationsNoneRadioButton.TabIndex = 3;
            this.LogRelationsNoneRadioButton.TabStop = true;
            this.LogRelationsNoneRadioButton.Text = "None";
            this.LogRelationsNoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.LogObjectsParametersRadioButton);
            this.groupBox5.Controls.Add(this.LogObjectsResultsRadioButton);
            this.groupBox5.Controls.Add(this.LogObjectsCodeRadioButton);
            this.groupBox5.Controls.Add(this.LogObjectsNoneRadioButton);
            this.groupBox5.Location = new System.Drawing.Point(6, 78);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(274, 53);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Objects from Numbers";
            // 
            // LogObjectsParametersRadioButton
            // 
            this.LogObjectsParametersRadioButton.AutoSize = true;
            this.LogObjectsParametersRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogObjectsParameters", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogObjectsParametersRadioButton.Location = new System.Drawing.Point(129, 19);
            this.LogObjectsParametersRadioButton.Name = "LogObjectsParametersRadioButton";
            this.LogObjectsParametersRadioButton.Size = new System.Drawing.Size(78, 17);
            this.LogObjectsParametersRadioButton.TabIndex = 1;
            this.LogObjectsParametersRadioButton.TabStop = true;
            this.LogObjectsParametersRadioButton.Text = "Parameters";
            this.LogObjectsParametersRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogObjectsResultsRadioButton
            // 
            this.LogObjectsResultsRadioButton.AutoSize = true;
            this.LogObjectsResultsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogObjectsResults", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogObjectsResultsRadioButton.Location = new System.Drawing.Point(63, 19);
            this.LogObjectsResultsRadioButton.Name = "LogObjectsResultsRadioButton";
            this.LogObjectsResultsRadioButton.Size = new System.Drawing.Size(60, 17);
            this.LogObjectsResultsRadioButton.TabIndex = 0;
            this.LogObjectsResultsRadioButton.TabStop = true;
            this.LogObjectsResultsRadioButton.Text = "Results";
            this.LogObjectsResultsRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogObjectsCodeRadioButton
            // 
            this.LogObjectsCodeRadioButton.AutoSize = true;
            this.LogObjectsCodeRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogObjectsCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogObjectsCodeRadioButton.Location = new System.Drawing.Point(213, 21);
            this.LogObjectsCodeRadioButton.Name = "LogObjectsCodeRadioButton";
            this.LogObjectsCodeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.LogObjectsCodeRadioButton.TabIndex = 2;
            this.LogObjectsCodeRadioButton.TabStop = true;
            this.LogObjectsCodeRadioButton.Text = "Code";
            this.LogObjectsCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogObjectsNoneRadioButton
            // 
            this.LogObjectsNoneRadioButton.AutoSize = true;
            this.LogObjectsNoneRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogObjectsNone", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.LogObjectsNoneRadioButton.Location = new System.Drawing.Point(6, 19);
            this.LogObjectsNoneRadioButton.Name = "LogObjectsNoneRadioButton";
            this.LogObjectsNoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.LogObjectsNoneRadioButton.TabIndex = 3;
            this.LogObjectsNoneRadioButton.TabStop = true;
            this.LogObjectsNoneRadioButton.Text = "None";
            this.LogObjectsNoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LogNumbersParametersRadioButton);
            this.groupBox4.Controls.Add(this.LogNumbersResultsRadioButton);
            this.groupBox4.Controls.Add(this.LogNumbersCodeRadioButton);
            this.groupBox4.Controls.Add(this.LogNumbersNoneRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(274, 53);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Numbers from SQL";
            // 
            // LogNumbersParametersRadioButton
            // 
            this.LogNumbersParametersRadioButton.AutoSize = true;
            this.LogNumbersParametersRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogNumbersParameters", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogNumbersParametersRadioButton.Location = new System.Drawing.Point(129, 19);
            this.LogNumbersParametersRadioButton.Name = "LogNumbersParametersRadioButton";
            this.LogNumbersParametersRadioButton.Size = new System.Drawing.Size(78, 17);
            this.LogNumbersParametersRadioButton.TabIndex = 1;
            this.LogNumbersParametersRadioButton.TabStop = true;
            this.LogNumbersParametersRadioButton.Text = "Parameters";
            this.LogNumbersParametersRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogNumbersResultsRadioButton
            // 
            this.LogNumbersResultsRadioButton.AutoSize = true;
            this.LogNumbersResultsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogNumbersResults", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogNumbersResultsRadioButton.Location = new System.Drawing.Point(63, 19);
            this.LogNumbersResultsRadioButton.Name = "LogNumbersResultsRadioButton";
            this.LogNumbersResultsRadioButton.Size = new System.Drawing.Size(60, 17);
            this.LogNumbersResultsRadioButton.TabIndex = 0;
            this.LogNumbersResultsRadioButton.TabStop = true;
            this.LogNumbersResultsRadioButton.Text = "Results";
            this.LogNumbersResultsRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogNumbersCodeRadioButton
            // 
            this.LogNumbersCodeRadioButton.AutoSize = true;
            this.LogNumbersCodeRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogNumbersCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogNumbersCodeRadioButton.Location = new System.Drawing.Point(213, 21);
            this.LogNumbersCodeRadioButton.Name = "LogNumbersCodeRadioButton";
            this.LogNumbersCodeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.LogNumbersCodeRadioButton.TabIndex = 2;
            this.LogNumbersCodeRadioButton.TabStop = true;
            this.LogNumbersCodeRadioButton.Text = "Code";
            this.LogNumbersCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogNumbersNoneRadioButton
            // 
            this.LogNumbersNoneRadioButton.AutoSize = true;
            this.LogNumbersNoneRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceOptionsTrace, "LogNumbersNone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogNumbersNoneRadioButton.Location = new System.Drawing.Point(6, 19);
            this.LogNumbersNoneRadioButton.Name = "LogNumbersNoneRadioButton";
            this.LogNumbersNoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.LogNumbersNoneRadioButton.TabIndex = 3;
            this.LogNumbersNoneRadioButton.TabStop = true;
            this.LogNumbersNoneRadioButton.Text = "None";
            this.LogNumbersNoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.HistoryTextBox);
            this.groupBox3.Location = new System.Drawing.Point(308, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 202);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection History:";
            // 
            // HistoryTextBox
            // 
            this.HistoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryTextBox.Location = new System.Drawing.Point(6, 32);
            this.HistoryTextBox.Name = "HistoryTextBox";
            this.HistoryTextBox.ReadOnly = true;
            this.HistoryTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.HistoryTextBox.Size = new System.Drawing.Size(180, 159);
            this.HistoryTextBox.TabIndex = 0;
            this.HistoryTextBox.Text = "";
            // 
            // SaveToDefaultsTextButton
            // 
            this.SaveToDefaultsTextButton.Location = new System.Drawing.Point(308, 220);
            this.SaveToDefaultsTextButton.Name = "SaveToDefaultsTextButton";
            this.SaveToDefaultsTextButton.Size = new System.Drawing.Size(192, 23);
            this.SaveToDefaultsTextButton.TabIndex = 2;
            this.SaveToDefaultsTextButton.Text = "Save to Defaults";
            this.SaveToDefaultsTextButton.UseVisualStyleBackColor = true;
            this.SaveToDefaultsTextButton.Click += new System.EventHandler(this.SaveToDefaultsTextButton_Click);
            // 
            // ResetFromDefaultsTextButton
            // 
            this.ResetFromDefaultsTextButton.Location = new System.Drawing.Point(308, 249);
            this.ResetFromDefaultsTextButton.Name = "ResetFromDefaultsTextButton";
            this.ResetFromDefaultsTextButton.Size = new System.Drawing.Size(192, 23);
            this.ResetFromDefaultsTextButton.TabIndex = 4;
            this.ResetFromDefaultsTextButton.Text = "Reset from Defaults";
            this.ResetFromDefaultsTextButton.UseVisualStyleBackColor = true;
            this.ResetFromDefaultsTextButton.Click += new System.EventHandler(this.ResetFromDefaultsTextButton_Click);
            // 
            // TraceOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 284);
            this.Controls.Add(this.ResetFromDefaultsTextButton);
            this.Controls.Add(this.SaveToDefaultsTextButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "TraceOptions";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Trace Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOptionsTrace)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox HistoryTextBox;
        private System.Windows.Forms.RadioButton LogNumbersNoneRadioButton;
        private System.Windows.Forms.RadioButton LogNumbersCodeRadioButton;
        private System.Windows.Forms.RadioButton LogNumbersParametersRadioButton;
        private System.Windows.Forms.RadioButton LogNumbersResultsRadioButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton LogLogicsParametersRadioButton;
        private System.Windows.Forms.RadioButton LogLogicsResultsRadioButton;
        private System.Windows.Forms.RadioButton LogLogicsCodeRadioButton;
        private System.Windows.Forms.RadioButton LogLogicsNoneRadioButton;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton LogRelationsParametersRadioButton;
        private System.Windows.Forms.RadioButton LogRelationsResultsRadioButton;
        private System.Windows.Forms.RadioButton LogRelationsCodeRadioButton;
        private System.Windows.Forms.RadioButton LogRelationsNoneRadioButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton LogObjectsParametersRadioButton;
        private System.Windows.Forms.RadioButton LogObjectsResultsRadioButton;
        private System.Windows.Forms.RadioButton LogObjectsCodeRadioButton;
        private System.Windows.Forms.RadioButton LogObjectsNoneRadioButton;
        private System.Windows.Forms.BindingSource bindingSourceOptionsTrace;
        private System.Windows.Forms.Button SaveToDefaultsTextButton;
        private System.Windows.Forms.Button ResetFromDefaultsTextButton;
    }
}