namespace GeneratedWF
{
    partial class DataWraper
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WrapperComponentSelection = new System.Windows.Forms.Button();
            this.WrapperID = new System.Windows.Forms.TextBox();
            this.WrapperLabel = new System.Windows.Forms.Label();
            this.WrapperCheckBox = new System.Windows.Forms.CheckBox();
            this.WrapperComboBox = new System.Windows.Forms.ComboBox();
            this.WrapperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WrapperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // WrapperComponentSelection
            // 
            this.WrapperComponentSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WrapperComponentSelection.Location = new System.Drawing.Point(3, 3);
            this.WrapperComponentSelection.Name = "WrapperComponentSelection";
            this.WrapperComponentSelection.Size = new System.Drawing.Size(33, 23);
            this.WrapperComponentSelection.TabIndex = 0;
            this.WrapperComponentSelection.Text = ">>";
            this.WrapperComponentSelection.UseVisualStyleBackColor = true;
            // 
            // WrapperID
            // 
            this.WrapperID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WrapperID.Location = new System.Drawing.Point(311, 6);
            this.WrapperID.Name = "WrapperID";
            this.WrapperID.ReadOnly = true;
            this.WrapperID.Size = new System.Drawing.Size(39, 20);
            this.WrapperID.TabIndex = 1;
            // 
            // WrapperLabel
            // 
            this.WrapperLabel.AutoSize = true;
            this.WrapperLabel.Location = new System.Drawing.Point(171, 8);
            this.WrapperLabel.Name = "WrapperLabel";
            this.WrapperLabel.Size = new System.Drawing.Size(61, 13);
            this.WrapperLabel.TabIndex = 3;
            this.WrapperLabel.Text = "Component";
            // 
            // WrapperCheckBox
            // 
            this.WrapperCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WrapperCheckBox.AutoSize = true;
            this.WrapperCheckBox.Location = new System.Drawing.Point(356, 9);
            this.WrapperCheckBox.Name = "WrapperCheckBox";
            this.WrapperCheckBox.Size = new System.Drawing.Size(15, 14);
            this.WrapperCheckBox.TabIndex = 4;
            this.WrapperCheckBox.UseVisualStyleBackColor = true;
            // 
            // WrapperComboBox
            // 
            this.WrapperComboBox.FormattingEnabled = true;
            this.WrapperComboBox.Location = new System.Drawing.Point(42, 5);
            this.WrapperComboBox.Name = "WrapperComboBox";
            this.WrapperComboBox.Size = new System.Drawing.Size(123, 21);
            this.WrapperComboBox.TabIndex = 5;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DataWraper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WrapperComboBox);
            this.Controls.Add(this.WrapperCheckBox);
            this.Controls.Add(this.WrapperLabel);
            this.Controls.Add(this.WrapperID);
            this.Controls.Add(this.WrapperComponentSelection);
            this.Name = "DataWraper";
            this.Size = new System.Drawing.Size(374, 31);
            ((System.ComponentModel.ISupportInitialize)(this.WrapperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button WrapperComponentSelection;
        private System.Windows.Forms.TextBox WrapperID;
        private System.Windows.Forms.Label WrapperLabel;
        private System.Windows.Forms.CheckBox WrapperCheckBox;
        private System.Windows.Forms.ComboBox WrapperComboBox;
        private System.Windows.Forms.BindingSource WrapperBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
