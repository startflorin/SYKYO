namespace WindowsFormsApplication1
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
            this.QueryTextBox = new System.Windows.Forms.TextBox();
            this.EvaluateButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nSDNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wEBViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataExlorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sugestAllToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.databaseOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceOprtionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameworkManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naturalLanguageProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naturalWriterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagramCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createGraphVizDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SugestionListView = new System.Windows.Forms.ListView();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.DatabaseSelection = new System.Windows.Forms.ComboBox();
            this.answaeTF = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryTextBox.Location = new System.Drawing.Point(12, 27);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(317, 153);
            this.QueryTextBox.TabIndex = 1;
            this.QueryTextBox.Text = "The cat of my brother`s wife have 4 mounts";
            this.QueryTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.QueryTextBox_KeyUp);
            // 
            // EvaluateButton
            // 
            this.EvaluateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EvaluateButton.Location = new System.Drawing.Point(254, 299);
            this.EvaluateButton.Name = "EvaluateButton";
            this.EvaluateButton.Size = new System.Drawing.Size(75, 23);
            this.EvaluateButton.TabIndex = 2;
            this.EvaluateButton.Text = "Evaluate";
            this.EvaluateButton.UseVisualStyleBackColor = true;
            this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(12, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "E";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(521, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.readToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nSDNToolStripMenuItem,
            this.sourceFileToolStripMenuItem,
            this.codeToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // nSDNToolStripMenuItem
            // 
            this.nSDNToolStripMenuItem.Name = "nSDNToolStripMenuItem";
            this.nSDNToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.nSDNToolStripMenuItem.Text = "NSDN";
            this.nSDNToolStripMenuItem.Click += new System.EventHandler(this.nSDNToolStripMenuItem_Click);
            // 
            // sourceFileToolStripMenuItem
            // 
            this.sourceFileToolStripMenuItem.Name = "sourceFileToolStripMenuItem";
            this.sourceFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sourceFileToolStripMenuItem.Text = "Source File";
            // 
            // codeToolStripMenuItem
            // 
            this.codeToolStripMenuItem.Name = "codeToolStripMenuItem";
            this.codeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.codeToolStripMenuItem.Text = "Code";
            this.codeToolStripMenuItem.Click += new System.EventHandler(this.codeToolStripMenuItem_Click);
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.fileToolStripMenuItem1.Text = "File";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wEBViewToolStripMenuItem,
            this.sQLViewToolStripMenuItem,
            this.tESTToolStripMenuItem,
            this.dataExlorerToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // wEBViewToolStripMenuItem
            // 
            this.wEBViewToolStripMenuItem.Name = "wEBViewToolStripMenuItem";
            this.wEBViewToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.wEBViewToolStripMenuItem.Text = "WEB - View";
            this.wEBViewToolStripMenuItem.Click += new System.EventHandler(this.wEBViewToolStripMenuItem_Click);
            // 
            // sQLViewToolStripMenuItem
            // 
            this.sQLViewToolStripMenuItem.Name = "sQLViewToolStripMenuItem";
            this.sQLViewToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.sQLViewToolStripMenuItem.Text = "SQL - View";
            this.sQLViewToolStripMenuItem.Click += new System.EventHandler(this.sQLViewToolStripMenuItem_Click);
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.symbolsToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.relationToolStripMenuItem});
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.tESTToolStripMenuItem.Text = "TEST";
            // 
            // symbolsToolStripMenuItem
            // 
            this.symbolsToolStripMenuItem.Name = "symbolsToolStripMenuItem";
            this.symbolsToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.symbolsToolStripMenuItem.Text = "Symbols";
            this.symbolsToolStripMenuItem.Click += new System.EventHandler(this.symbolsToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.serverToolStripMenuItem.Text = "Server";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // relationToolStripMenuItem
            // 
            this.relationToolStripMenuItem.Name = "relationToolStripMenuItem";
            this.relationToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.relationToolStripMenuItem.Text = "Relation";
            this.relationToolStripMenuItem.Click += new System.EventHandler(this.relationToolStripMenuItem_Click);
            // 
            // dataExlorerToolStripMenuItem
            // 
            this.dataExlorerToolStripMenuItem.Name = "dataExlorerToolStripMenuItem";
            this.dataExlorerToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.dataExlorerToolStripMenuItem.Text = "Data Exlorer";
            this.dataExlorerToolStripMenuItem.Click += new System.EventHandler(this.dataExlorerToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.sugestAllToolStripMenuItem,
            this.toolStripComboBox1,
            this.toolStripComboBox2,
            this.databaseOptionsToolStripMenuItem,
            this.frameworkManagerToolStripMenuItem,
            this.naturalLanguageProcessorToolStripMenuItem,
            this.dataModelToolStripMenuItem,
            this.codeProcessorToolStripMenuItem,
            this.diagramCreatorToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.Items.AddRange(new object[] {
            "Evaluate",
            "Adopt",
            "Force Adopt"});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(250, 23);
            this.modeToolStripMenuItem.Text = "Evaluate";
            this.modeToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.modeToolStripMenuItem_SelectedIndexChanged);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.advancedToolStripMenuItem.Text = "Advanced";
            this.advancedToolStripMenuItem.Click += new System.EventHandler(this.advancedToolStripMenuItem_Click);
            // 
            // sugestAllToolStripMenuItem
            // 
            this.sugestAllToolStripMenuItem.Items.AddRange(new object[] {
            "Suggest All",
            "Predict All"});
            this.sugestAllToolStripMenuItem.Name = "sugestAllToolStripMenuItem";
            this.sugestAllToolStripMenuItem.Size = new System.Drawing.Size(250, 23);
            this.sugestAllToolStripMenuItem.Text = "Sugest All";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Only Existent",
            "Symbols Only",
            "Relations Only",
            "Both Relations and Symbols"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(250, 23);
            this.toolStripComboBox1.Text = "Only Existent";
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "Real Time Evaluation",
            "Evaluation on Demand"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(250, 23);
            this.toolStripComboBox2.Text = "Real Time Evaluation";
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            // 
            // databaseOptionsToolStripMenuItem
            // 
            this.databaseOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseOptionToolStripMenuItem,
            this.traceOprtionsToolStripMenuItem});
            this.databaseOptionsToolStripMenuItem.Name = "databaseOptionsToolStripMenuItem";
            this.databaseOptionsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.databaseOptionsToolStripMenuItem.Text = "Database Options";
            // 
            // databaseOptionToolStripMenuItem
            // 
            this.databaseOptionToolStripMenuItem.Name = "databaseOptionToolStripMenuItem";
            this.databaseOptionToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.databaseOptionToolStripMenuItem.Text = "Database Option";
            this.databaseOptionToolStripMenuItem.Click += new System.EventHandler(this.databaseOptionToolStripMenuItem_Click);
            // 
            // traceOprtionsToolStripMenuItem
            // 
            this.traceOprtionsToolStripMenuItem.Name = "traceOprtionsToolStripMenuItem";
            this.traceOprtionsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.traceOprtionsToolStripMenuItem.Text = "Trace Oprtions";
            this.traceOprtionsToolStripMenuItem.Click += new System.EventHandler(this.traceOprtionsToolStripMenuItem_Click);
            // 
            // frameworkManagerToolStripMenuItem
            // 
            this.frameworkManagerToolStripMenuItem.Name = "frameworkManagerToolStripMenuItem";
            this.frameworkManagerToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.frameworkManagerToolStripMenuItem.Text = "Data Persistency";
            this.frameworkManagerToolStripMenuItem.Click += new System.EventHandler(this.frameworkManagerToolStripMenuItem_Click);
            // 
            // naturalLanguageProcessorToolStripMenuItem
            // 
            this.naturalLanguageProcessorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.naturalWriterToolStripMenuItem});
            this.naturalLanguageProcessorToolStripMenuItem.Name = "naturalLanguageProcessorToolStripMenuItem";
            this.naturalLanguageProcessorToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.naturalLanguageProcessorToolStripMenuItem.Text = "Natural Language Processor";
            this.naturalLanguageProcessorToolStripMenuItem.Click += new System.EventHandler(this.naturalLanguageProcessorToolStripMenuItem_Click);
            // 
            // naturalWriterToolStripMenuItem
            // 
            this.naturalWriterToolStripMenuItem.Name = "naturalWriterToolStripMenuItem";
            this.naturalWriterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.naturalWriterToolStripMenuItem.Text = "Natural Writer";
            this.naturalWriterToolStripMenuItem.Click += new System.EventHandler(this.naturalWriterToolStripMenuItem_Click);
            // 
            // dataModelToolStripMenuItem
            // 
            this.dataModelToolStripMenuItem.Name = "dataModelToolStripMenuItem";
            this.dataModelToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.dataModelToolStripMenuItem.Text = "Data Model";
            // 
            // codeProcessorToolStripMenuItem
            // 
            this.codeProcessorToolStripMenuItem.Name = "codeProcessorToolStripMenuItem";
            this.codeProcessorToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.codeProcessorToolStripMenuItem.Text = "CodeProcessor";
            this.codeProcessorToolStripMenuItem.Click += new System.EventHandler(this.codeProcessorToolStripMenuItem_Click);
            // 
            // diagramCreatorToolStripMenuItem
            // 
            this.diagramCreatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGraphVizDiagramToolStripMenuItem});
            this.diagramCreatorToolStripMenuItem.Name = "diagramCreatorToolStripMenuItem";
            this.diagramCreatorToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.diagramCreatorToolStripMenuItem.Text = "DiagramCreator";
            // 
            // createGraphVizDiagramToolStripMenuItem
            // 
            this.createGraphVizDiagramToolStripMenuItem.Name = "createGraphVizDiagramToolStripMenuItem";
            this.createGraphVizDiagramToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createGraphVizDiagramToolStripMenuItem.Text = "Create GraphViz Diagram";
            this.createGraphVizDiagramToolStripMenuItem.Click += new System.EventHandler(this.createGraphVizDiagramToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // SugestionListView
            // 
            this.SugestionListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SugestionListView.Location = new System.Drawing.Point(335, 27);
            this.SugestionListView.Name = "SugestionListView";
            this.SugestionListView.Size = new System.Drawing.Size(174, 295);
            this.SugestionListView.TabIndex = 6;
            this.SugestionListView.UseCompatibleStateImageBehavior = false;
            this.SugestionListView.View = System.Windows.Forms.View.List;
            this.SugestionListView.SelectedIndexChanged += new System.EventHandler(this.SugestionListView_SelectedIndexChanged);
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeTextBox.Location = new System.Drawing.Point(12, 186);
            this.CodeTextBox.Multiline = true;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CodeTextBox.Size = new System.Drawing.Size(317, 38);
            this.CodeTextBox.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(40, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "O";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(174, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Bind";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DatabaseSelection
            // 
            this.DatabaseSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DatabaseSelection.FormattingEnabled = true;
            this.DatabaseSelection.Location = new System.Drawing.Point(68, 301);
            this.DatabaseSelection.Name = "DatabaseSelection";
            this.DatabaseSelection.Size = new System.Drawing.Size(100, 21);
            this.DatabaseSelection.TabIndex = 12;
            this.DatabaseSelection.SelectedIndexChanged += new System.EventHandler(this.DatabaseSelection_SelectedIndexChanged);
            // 
            // answaeTF
            // 
            this.answaeTF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.answaeTF.Location = new System.Drawing.Point(12, 230);
            this.answaeTF.Name = "answaeTF";
            this.answaeTF.Size = new System.Drawing.Size(317, 63);
            this.answaeTF.TabIndex = 13;
            this.answaeTF.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 334);
            this.Controls.Add(this.answaeTF);
            this.Controls.Add(this.DatabaseSelection);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CodeTextBox);
            this.Controls.Add(this.SugestionListView);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.EvaluateButton);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = " ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox QueryTextBox;
        private System.Windows.Forms.Button EvaluateButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListView SugestionListView;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nSDNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox sugestAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox DatabaseSelection;
        private System.Windows.Forms.ToolStripMenuItem databaseOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.RichTextBox answaeTF;
        private System.Windows.Forms.ToolStripMenuItem databaseOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traceOprtionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wEBViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameworkManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naturalLanguageProcessorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeProcessorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diagramCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createGraphVizDiagramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naturalWriterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataExlorerToolStripMenuItem;
    }
}

