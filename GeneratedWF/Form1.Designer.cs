namespace GeneratedWF
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.actionButtonNew = new System.Windows.Forms.Button();
            this.actionButtonCopy = new System.Windows.Forms.Button();
            this.actionButtonCancel = new System.Windows.Forms.Button();
            this.actionButtonDelete = new System.Windows.Forms.Button();
            this.actionButtonSave = new System.Windows.Forms.Button();
            this.actionButtonExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(604, 195);
            this.dataGridView1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.actionButtonNew);
            this.flowLayoutPanel1.Controls.Add(this.actionButtonCopy);
            this.flowLayoutPanel1.Controls.Add(this.actionButtonCancel);
            this.flowLayoutPanel1.Controls.Add(this.actionButtonDelete);
            this.flowLayoutPanel1.Controls.Add(this.actionButtonSave);
            this.flowLayoutPanel1.Controls.Add(this.actionButtonExit);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 251);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(624, 45);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // actionButtonNew
            // 
            this.actionButtonNew.Location = new System.Drawing.Point(3, 3);
            this.actionButtonNew.Name = "actionButtonNew";
            this.actionButtonNew.Size = new System.Drawing.Size(75, 23);
            this.actionButtonNew.TabIndex = 0;
            this.actionButtonNew.Text = "New";
            this.actionButtonNew.UseVisualStyleBackColor = true;
            this.actionButtonNew.Click += new System.EventHandler(this.actionButtonNew_Click);
            // 
            // actionButtonCopy
            // 
            this.actionButtonCopy.Location = new System.Drawing.Point(84, 3);
            this.actionButtonCopy.Name = "actionButtonCopy";
            this.actionButtonCopy.Size = new System.Drawing.Size(75, 23);
            this.actionButtonCopy.TabIndex = 1;
            this.actionButtonCopy.Text = "Copy";
            this.actionButtonCopy.UseVisualStyleBackColor = true;
            this.actionButtonCopy.Click += new System.EventHandler(this.actionButtonCopy_Click);
            // 
            // actionButtonCancel
            // 
            this.actionButtonCancel.Location = new System.Drawing.Point(165, 3);
            this.actionButtonCancel.Name = "actionButtonCancel";
            this.actionButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.actionButtonCancel.TabIndex = 3;
            this.actionButtonCancel.Text = "Cancel";
            this.actionButtonCancel.UseVisualStyleBackColor = true;
            this.actionButtonCancel.Click += new System.EventHandler(this.actionButtonCancel_Click);
            // 
            // actionButtonDelete
            // 
            this.actionButtonDelete.Location = new System.Drawing.Point(246, 3);
            this.actionButtonDelete.Name = "actionButtonDelete";
            this.actionButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.actionButtonDelete.TabIndex = 5;
            this.actionButtonDelete.Text = "Delete";
            this.actionButtonDelete.UseVisualStyleBackColor = true;
            this.actionButtonDelete.Click += new System.EventHandler(this.actionButtonDelete_Click);
            // 
            // actionButtonSave
            // 
            this.actionButtonSave.Location = new System.Drawing.Point(327, 3);
            this.actionButtonSave.Name = "actionButtonSave";
            this.actionButtonSave.Size = new System.Drawing.Size(75, 23);
            this.actionButtonSave.TabIndex = 6;
            this.actionButtonSave.Text = "Save";
            this.actionButtonSave.UseVisualStyleBackColor = true;
            this.actionButtonSave.Click += new System.EventHandler(this.actionButtonSave_Click);
            // 
            // actionButtonExit
            // 
            this.actionButtonExit.Location = new System.Drawing.Point(408, 3);
            this.actionButtonExit.Name = "actionButtonExit";
            this.actionButtonExit.Size = new System.Drawing.Size(75, 23);
            this.actionButtonExit.TabIndex = 7;
            this.actionButtonExit.Text = "Exit";
            this.actionButtonExit.UseVisualStyleBackColor = true;
            this.actionButtonExit.Click += new System.EventHandler(this.actionButtonExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 233);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Overview";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 207);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(616, 207);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Simulation";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSource1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 308);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button actionButtonNew;
        private System.Windows.Forms.Button actionButtonCopy;
        private System.Windows.Forms.Button actionButtonCancel;
        private System.Windows.Forms.Button actionButtonDelete;
        private System.Windows.Forms.Button actionButtonSave;
        private System.Windows.Forms.Button actionButtonExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

