namespace WindowsFormsApplication1.UI
{
    partial class DatabaseOptions
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
            this.OracleRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SqLiteRadioButton = new System.Windows.Forms.RadioButton();
            this.PostgreeRadioButton = new System.Windows.Forms.RadioButton();
            this.SqlServerRadioButton = new System.Windows.Forms.RadioButton();
            this.MySqlRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HistoryTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.databaseOptionsConditionModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.databaseOptionsConditionModelBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOptionsConditionModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOptionsConditionModelBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // OracleRadioButton
            // 
            this.OracleRadioButton.AutoSize = true;
            this.OracleRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.databaseOptionsConditionModelBindingSource, "Oracle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.OracleRadioButton.Location = new System.Drawing.Point(6, 42);
            this.OracleRadioButton.Name = "OracleRadioButton";
            this.OracleRadioButton.Size = new System.Drawing.Size(56, 17);
            this.OracleRadioButton.TabIndex = 1;
            this.OracleRadioButton.TabStop = true;
            this.OracleRadioButton.Text = "Oracle";
            this.OracleRadioButton.UseVisualStyleBackColor = true;
            this.OracleRadioButton.CheckedChanged += new System.EventHandler(this.OracleRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SqLiteRadioButton);
            this.groupBox1.Controls.Add(this.PostgreeRadioButton);
            this.groupBox1.Controls.Add(this.SqlServerRadioButton);
            this.groupBox1.Controls.Add(this.MySqlRadioButton);
            this.groupBox1.Controls.Add(this.OracleRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Type";
            // 
            // SqLiteRadioButton
            // 
            this.SqLiteRadioButton.AutoSize = true;
            this.SqLiteRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.databaseOptionsConditionModelBindingSource, "SqLite", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SqLiteRadioButton.Location = new System.Drawing.Point(6, 111);
            this.SqLiteRadioButton.Name = "SqLiteRadioButton";
            this.SqLiteRadioButton.Size = new System.Drawing.Size(57, 17);
            this.SqLiteRadioButton.TabIndex = 4;
            this.SqLiteRadioButton.TabStop = true;
            this.SqLiteRadioButton.Text = "SQLite";
            this.SqLiteRadioButton.UseVisualStyleBackColor = true;
            this.SqLiteRadioButton.CheckedChanged += new System.EventHandler(this.SqLiteRadioButton_CheckedChanged);
            // 
            // PostgreeRadioButton
            // 
            this.PostgreeRadioButton.AutoSize = true;
            this.PostgreeRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.databaseOptionsConditionModelBindingSource, "Postgree", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PostgreeRadioButton.Location = new System.Drawing.Point(6, 88);
            this.PostgreeRadioButton.Name = "PostgreeRadioButton";
            this.PostgreeRadioButton.Size = new System.Drawing.Size(67, 17);
            this.PostgreeRadioButton.TabIndex = 3;
            this.PostgreeRadioButton.TabStop = true;
            this.PostgreeRadioButton.Text = "Postgree";
            this.PostgreeRadioButton.UseVisualStyleBackColor = true;
            this.PostgreeRadioButton.CheckedChanged += new System.EventHandler(this.PostgreeRadioButton_CheckedChanged);
            // 
            // SqlServerRadioButton
            // 
            this.SqlServerRadioButton.AutoSize = true;
            this.SqlServerRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.databaseOptionsConditionModelBindingSource, "SqlServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SqlServerRadioButton.Location = new System.Drawing.Point(6, 65);
            this.SqlServerRadioButton.Name = "SqlServerRadioButton";
            this.SqlServerRadioButton.Size = new System.Drawing.Size(74, 17);
            this.SqlServerRadioButton.TabIndex = 2;
            this.SqlServerRadioButton.TabStop = true;
            this.SqlServerRadioButton.Text = "Sql-Server";
            this.SqlServerRadioButton.UseVisualStyleBackColor = true;
            this.SqlServerRadioButton.CheckedChanged += new System.EventHandler(this.SqlServerRadioButton_CheckedChanged);
            // 
            // MySqlRadioButton
            // 
            this.MySqlRadioButton.AutoSize = true;
            this.MySqlRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.databaseOptionsConditionModelBindingSource, "MySql", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MySqlRadioButton.Location = new System.Drawing.Point(6, 19);
            this.MySqlRadioButton.Name = "MySqlRadioButton";
            this.MySqlRadioButton.Size = new System.Drawing.Size(54, 17);
            this.MySqlRadioButton.TabIndex = 0;
            this.MySqlRadioButton.TabStop = true;
            this.MySqlRadioButton.Text = "MySql";
            this.MySqlRadioButton.UseVisualStyleBackColor = true;
            this.MySqlRadioButton.CheckedChanged += new System.EventHandler(this.MySqlRadioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 46);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Business Type";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(136, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Use Stored Procedures";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.HistoryTextBox);
            this.groupBox3.Location = new System.Drawing.Point(169, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 194);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // HistoryTextBox
            // 
            this.HistoryTextBox.Location = new System.Drawing.Point(6, 32);
            this.HistoryTextBox.Name = "HistoryTextBox";
            this.HistoryTextBox.ReadOnly = true;
            this.HistoryTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.HistoryTextBox.Size = new System.Drawing.Size(180, 156);
            this.HistoryTextBox.TabIndex = 0;
            this.HistoryTextBox.Text = "";
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
            // databaseOptionsConditionModelBindingSource
            // 
            this.databaseOptionsConditionModelBindingSource.DataSource = typeof(WindowsFormsApplication1.BL.DatabaseOptionsConditionModel);
            // 
            // databaseOptionsConditionModelBindingSource1
            // 
            this.databaseOptionsConditionModelBindingSource1.DataSource = typeof(WindowsFormsApplication1.BL.DatabaseOptionsConditionModel);
            // 
            // DatabaseOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 222);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DatabaseOptions";
            this.Text = "DatabaseOptions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOptionsConditionModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOptionsConditionModelBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton OracleRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SqLiteRadioButton;
        private System.Windows.Forms.RadioButton PostgreeRadioButton;
        private System.Windows.Forms.RadioButton SqlServerRadioButton;
        private System.Windows.Forms.RadioButton MySqlRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.BindingSource databaseOptionsConditionModelBindingSource;
        private System.Windows.Forms.BindingSource databaseOptionsConditionModelBindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox HistoryTextBox;
    }
}