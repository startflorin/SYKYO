using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GeneratedWF
{
    partial class Console
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            richTextBox1 = new RichTextBox();
            this.buttonServer = new Button();
            this.buttonClient = new Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(260, 213);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // buttonServer
            // 
            this.buttonServer.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonServer.Location = new Point(197, 231);
            this.buttonServer.Name = "buttonServer";
            this.buttonServer.Size = new Size(75, 23);
            this.buttonServer.TabIndex = 1;
            this.buttonServer.Text = "Server";
            this.buttonServer.UseVisualStyleBackColor = true;
            this.buttonServer.Click += new EventHandler(this.buttonServer_Click);
            // 
            // buttonClient
            // 
            this.buttonClient.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonClient.Location = new Point(116, 231);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new Size(75, 23);
            this.buttonClient.TabIndex = 2;
            this.buttonClient.Text = "Client";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new EventHandler(this.buttonClient_Click);
            // 
            // Console
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 262);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.buttonServer);
            this.Controls.Add(richTextBox1);
            this.Name = "Console";
            this.Text = "Console";
            this.ResumeLayout(false);

        }

        #endregion

        private static RichTextBox richTextBox1;
        private Button buttonServer;
        private Button buttonClient;
    }
}