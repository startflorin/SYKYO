using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeProcessor.CodeWriter;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.Collection;
using DiagramCreator;
using WpfApplication1.Controller.DocumentManager;
using System.IO;
using WpfApplication1.UserInterface.CollectionExplorer;
using WpfApplication1.UserInterface.ObjectExplorer;

namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HierarchyExplorer tmp = new HierarchyExplorer();
        ObjectExplorer objectExplorer = new ObjectExplorer();
        static DataPersistency.UI.Logging.SQLView mySqlView = null;

        public MainWindow() 
        {
            mySqlView = new DataPersistency.UI.Logging.SQLView();
            mySqlView.Show();
            
            InitializeComponent();
            tmp.Show();
            objectExplorer.Show();
        }

        private void richTextBox1_TextChanged(object sender, TextChangedEventArgs e) 
        {
            
            string lastString = GetInput(richTextBox1).Trim();
            EDocument document = new EDocument(lastString);
            //document.ParseDocument();
            ECodeDocument codeDocument = new ECodeDocument(lastString);
            codeDocument.ParseAsCode();
            //webBrowser1.Document.Blocks.Clear();
            string strHtml = "test";
            string fileName = "C:\\logs\\ParsedCode.html";
            FileStream fileStream = File.Create(fileName); fileStream.Close();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(codeDocument.ToString());
                sw.WriteLine();
                sw.Flush();


            } webBrowser1.Navigate("file:///C:/logs/ParsedCode.html");
            TreeViewItem rootItem = new TreeViewItem();
            rootItem.Tag = codeDocument.RootTocken;
            rootItem.Header = "rooot";
            treeView1.Items.Clear();
            treeView1.Items.Add(rootItem);
            //treeView1.Items.Add(document.RootTocken);
        
            
            
            //webBrowser1.Document = new WebBrowser.Document( document.ToString();
            string g = document.ToString();
            SObjectCollection objectCollection = new SObjectCollection();
            SOperatorCollection operatorCollection = new SOperatorCollection();
            
            objectCollection.Clear();
            operatorCollection.Clear();
            if (!string.IsNullOrEmpty(lastString))
            {
                int from = 0;
                if (lastString.LastIndexOf(" ") > -1 && !string.IsNullOrEmpty(lastString.Substring(lastString.LastIndexOf(" ")).Trim()))
                {
                    from = lastString.LastIndexOf(" ");
                    lastString = lastString.Substring(from).Trim();
                }
                // Mabe is an operator
                operatorCollection.AddByName(lastString);
                if (operatorCollection.SOperator.Count < 1)
                {
                    // No, is not a symbol
                    objectCollection.AddByName(lastString, 1);
                }
            }

            richTextBox2.Document.Blocks.Clear();

            FlowDocument myFlowDoc = new FlowDocument();

            // Add paragraphs to the FlowDocument.
            if (operatorCollection.SOperator.Count > 0)
            {
                myFlowDoc.Blocks.Add(new Paragraph(new Bold(new Run(operatorCollection.ToString())))
                {
                    Foreground = Brushes.Blue
                });
            }
            
            if (objectCollection.SObject.Count > 0)
            {
                myFlowDoc.Blocks.Add(new Paragraph(new Bold(new Run(objectCollection.ToString())))
                {
                    Foreground = Brushes.Red
                });
            }
            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richTextBox2.Document = myFlowDoc;

            tmp.UpdateDataSource(document);
        }

        private string GetInput(RichTextBox richTextBox) 
        {
            string input = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            if (input.Length > 2)
            {
                return input.Substring(0, input.Length - 2);
            }
            else
            {
                return string.Empty;
            }
        }

        private void richTextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string lastString = GetInput(richTextBox3).Trim();

            SObjectCollection objectCollection = new SObjectCollection();
            SOperatorCollection operatorCollection = new SOperatorCollection();
            objectCollection.Clear();
            operatorCollection.Clear();
            if (!string.IsNullOrEmpty(lastString))
            {
                int from = 0;
                if (lastString.LastIndexOf(" ") > -1 && !string.IsNullOrEmpty(lastString.Substring(lastString.LastIndexOf(" ")).Trim()))
                {
                    from = lastString.LastIndexOf(" ");
                    lastString = lastString.Substring(from).Trim();
                }
                // Mabe is a symbol
                operatorCollection.AddByName(lastString);
                if (operatorCollection.SOperator.Count < 1)
                {
                    // No, is not a symbol
                    objectCollection.AddByName(lastString, 1);
                }
            }

            richTextBox2.Document.Blocks.Clear();

            FlowDocument myFlowDoc = new FlowDocument();

            // Add paragraphs to the FlowDocument.
            if (operatorCollection.SOperator.Count > 0)
            {
                myFlowDoc.Blocks.Add(new Paragraph(new Run(operatorCollection.ToString())));
            }
            if (objectCollection.SObject.Count > 0)
            {
                myFlowDoc.Blocks.Add(new Paragraph(new Run(objectCollection.ToString())));
            }
            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richTextBox2.Document = myFlowDoc;


            //BUSINESS FOR EVALUATION
            EDocument document = new EDocument(GetInput(richTextBox3).Trim());
            document.ParseAsNatural();
            document.EvaluateDocument();
            //webBrowser1.Document.Blocks.Clear();
            string strHtml = "test";
            const string strAddress = "C:\\logs\\ParsedSentence.html";
            using (StreamWriter sw = new StreamWriter(strAddress))
            {
                sw.Write(document.ToString());
                sw.WriteLine();
                sw.Flush();


            }
            
            webBrowser1.Navigate("file:///C:/logs/ParsedSentence.html");
            TreeViewItem rootItem = new TreeViewItem();
            rootItem.Tag = document.RootTocken;
            rootItem.Header = "rooot";
            treeView1.Items.Clear();
            treeView1.Items.Add(rootItem);


            tmp.UpdateDataSource(document);
            //treeView1.Items.Add(document.RootTocken);

            EDocumentConvertor dc = new EDocumentConvertor();
            objectExplorer.UpdateTreeView(document);
            ModelWriterCS modelWriterCS = new ModelWriterCS();
            DiagramWriter diagramWriter = new DiagramWriter();
            ElementProgram program = dc.Wrap(document.RootTocken);

            modelWriterCS.ToProgrammingLanguage(program);
            diagramWriter.ToDiagram(program);
            //dc.ToProgrammingLanguage(document);

        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void treeView1_GotFocus(object sender, RoutedEventArgs e)
        {
            LoadNaturalChilds(treeView1.SelectedItem);
        }
        public void LoadNaturalChilds(object selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }
            if (((SToken)(((TreeViewItem)treeView1.SelectedItem).Tag)).TokenChilds != null)
            foreach (SToken token in ((SToken)(((TreeViewItem)treeView1.SelectedItem).Tag)).TokenChilds)
            {
                ((TreeViewItem)treeView1.SelectedItem).Items.Clear();
                SToken localToken = token;
                while (localToken != null)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = ": " + localToken.TokenString;
                    item.Tag = localToken;
                    ((TreeViewItem)treeView1.SelectedItem).Items.Add(item);
                   //*** localToken = localToken.TokenAfter;
                }
            }
        }

        public void LoadCodeChilds(object selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }
            foreach (Controller.DocumentManager.SCodeToken token in ((Controller.DocumentManager.SCodeToken)(((TreeViewItem)treeView1.SelectedItem).Tag)).TokenChild)
            {
                SCodeToken localToken = token;
                while (localToken != null)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = ": " + localToken.TokenString;
                    item.Tag = localToken;
                    ((TreeViewItem)treeView1.SelectedItem).Items.Add(item);
                    localToken = localToken.TokenAfter;
                }
            }
        }

    }
}
