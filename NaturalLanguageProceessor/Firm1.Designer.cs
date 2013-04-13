namespace NaturalLanguageProcessor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naturalFromInternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internalFromNaturalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naturalWriterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.naturalFromInternalToolStripMenuItem,
            this.internalFromNaturalToolStripMenuItem,
            this.naturalWriterToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // naturalFromInternalToolStripMenuItem
            // 
            this.naturalFromInternalToolStripMenuItem.Name = "naturalFromInternalToolStripMenuItem";
            this.naturalFromInternalToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.naturalFromInternalToolStripMenuItem.Text = "Natural from Internal";
            this.naturalFromInternalToolStripMenuItem.Click += new System.EventHandler(this.naturalFromInternalToolStripMenuItem_Click);
            // 
            // internalFromNaturalToolStripMenuItem
            // 
            this.internalFromNaturalToolStripMenuItem.Name = "internalFromNaturalToolStripMenuItem";
            this.internalFromNaturalToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.internalFromNaturalToolStripMenuItem.Text = "Internal from Natural";
            this.internalFromNaturalToolStripMenuItem.Click += new System.EventHandler(this.internalFromNaturalToolStripMenuItem_Click);
            // 
            // naturalWriterToolStripMenuItem
            // 
            this.naturalWriterToolStripMenuItem.Name = "naturalWriterToolStripMenuItem";
            this.naturalWriterToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.naturalWriterToolStripMenuItem.Text = "NaturalWriter";
            this.naturalWriterToolStripMenuItem.Click += new System.EventHandler(this.naturalWriterToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create Test Program";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naturalFromInternalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internalFromNaturalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naturalWriterToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

