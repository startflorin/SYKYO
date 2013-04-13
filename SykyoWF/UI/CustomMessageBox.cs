using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.UI
{
    public partial class CustomMessageBox : Form
    {
        static System.Windows.Forms.RichTextBox richCustomTextBox = new System.Windows.Forms.RichTextBox();
        //static void Show()
        //{
            //CustomMessageBox.Show();
        //}
        #region CUSTOM INITIALIZER
        private void InitializeCustomComponent()
        {
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            richCustomTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            richCustomTextBox.Location = new System.Drawing.Point(12, 12);
            richCustomTextBox.Name = "richTextBox1";
            richCustomTextBox.Size = new System.Drawing.Size(260, 238);
            richCustomTextBox.TabIndex = 0;
            richCustomTextBox.Text = "";
            this.Controls.Remove(this.richTextBox1);
            this.Controls.Add(richCustomTextBox);
            this.ResumeLayout(false);

        }
        #endregion CUSTOM INITIALIZER

        public CustomMessageBox(string[] textMessage)
        {
            InitializeComponent();
            InitializeCustomComponent();
            StringBuilder sb = new StringBuilder();
            foreach (string text in textMessage)
            {
                sb.Append(text);
            }
            richCustomTextBox.Text = sb.ToString();
            Show();
        }
    }
}
