#region USING
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataModel.DL.CodeEntity;
//using DataModel.DL.DocumentManager;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using DataPersistency.DL.ServerAccess;

#endregion USING

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Fill combo box
    /// </summary>
    public partial class WebView : Form
    {
        SymbolCollection symbolCollection = new SymbolCollection();
        OperatorCollection relationCollection = new OperatorCollection();
        OperatorID r2;
        List<SymbolID> visibleSymbols = new List<SymbolID>();

        private string ToUp(string identifier)
        {
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }

        private static int[] actualID;
        public int[] ActualID
        {
            get { return actualID; }
            set {
                actualID = value;
                if (textBox1 != null)
                {
                    textBox1.Text = actualID[0].ToString();
                }
            }
        }
        /*
        internal void UpdateTreeView(EDocument document)
        {
            treeView1.Nodes.Clear();
            Wrap(document.RootTocken);
            foreach (TreeNode rootNode in treeView1.Nodes)
            {
                rootNode.ExpandAll();
            }
            ImageList imageList = new ImageList();
            imageList.Images.Add("class", Image.FromFile("..\\..\\IMG\\class.png"));
            imageList.Images.Add("property", Image.FromFile("..\\..\\IMG\\property.png"));
            imageList.Images.Add("method", Image.FromFile("..\\..\\IMG\\method.png"));
            imageList.Images.Add("increase", Image.FromFile("..\\..\\IMG\\increase.png"));
            imageList.Images.Add("decrease", Image.FromFile("..\\..\\IMG\\decrease.png"));
            imageList.Images.Add("increasedBy", Image.FromFile("..\\..\\IMG\\increasedBy.png"));
            imageList.Images.Add("decreasedBy", Image.FromFile("..\\..\\IMG\\decreasedBy.png"));
            treeView1.ImageList = imageList;
        }
        */
        /*
        List<SToken> words = new List<SToken>();
        */
        /*
        private void Wrap(SToken token)
        {
            while (token != null)
            {
                if (token.TokenType == SElementType.Symbol)
                {
                    TreeNode classNode = null;
                    CodeFile newCodeFile = new CodeFile();
                    ElementNamespace namespaceItem = new ElementNamespace();
                    ElementClass classItem = new ElementClass();

                    bool isNeededCodeFileCreation = true;
                    if (isNeededCodeFileCreation)
                    {
                        classNode = new TreeNode(token.TokenString);
                        classNode.ImageKey = "class";
                        classNode.Tag = token;
                        treeView1.Nodes.Add(classNode);
                    }


                    if (token.TokenProperties != null)
                    {
                        SToken tokenToRead = token;
                        if (token.TokenProperties[0].TokenNodeType == SElementType.FoldOpen)
                        {
                            foreach (SToken localTokenF in token.TokenProperties[0].TokenChilds)
                            {
                                SToken localToken = localTokenF;
                                while (localToken != null)
                                {
                                    Wrap(localToken);
                                    ElementProperty newProperty = new ElementProperty();
                                    TreeNode propertyNode = null;
                                    propertyNode.ImageKey = "property";
                                    switch (localToken.TokenMultiplicity)
                                    {
                                        case SElementMultiplicity.Some:
                                            propertyNode = new TreeNode(localToken.TokenString);
                                            propertyNode.Tag = token;
                                            classNode.Nodes.Add(propertyNode);
                                            break;
                                        case SElementMultiplicity.Explicit:
                                            propertyNode = new TreeNode(localToken.TokenString);
                                            propertyNode.Tag = token;
                                            classNode.Nodes.Add(propertyNode);
                                            break;
                                        default:
                                            propertyNode = new TreeNode(localToken.TokenString);
                                            propertyNode.Tag = token;
                                            classNode.Nodes.Add(propertyNode);
                                            break;
                                    }
                                    //newProperty.PropertyType = localToken.TokenString;
                                    newProperty.ElementClassName = ToUp(localToken.TokenString);
                                    classItem.ElementProperties.Add(newProperty);
                                    if (localToken.TokenAfter != null)
                                    {
                                        localToken = localToken.TokenAfter[0];
                                    }
                                    else
                                    {
                                        localToken = null;
                                    }
                                }
                            }
                        }
                        else
                            foreach (SToken localToken in token.TokenProperties)
                            {
                                //Wrap(localToken);
                                ElementProperty newProperty = new ElementProperty();
                                TreeNode propertyNode = null;
                                switch (localToken.TokenMultiplicity)
                                {
                                    case SElementMultiplicity.Some:
                                        propertyNode = new TreeNode(localToken.TokenString);
                                        propertyNode.Tag = token;
                                        propertyNode.ImageKey = "property";
                                        classNode.Nodes.Add(propertyNode);
                                        break;
                                    case SElementMultiplicity.Explicit:
                                        propertyNode = new TreeNode(localToken.TokenString);
                                        propertyNode.Tag = token;
                                        propertyNode.ImageKey = "property";
                                        classNode.Nodes.Add(propertyNode);
                                        break;
                                    default:
                                        propertyNode = new TreeNode(localToken.TokenString);
                                        propertyNode.Tag = token;
                                        propertyNode.ImageKey = "property";
                                        classNode.Nodes.Add(propertyNode);
                                        break;
                                }
                                //newProperty.PropertyType = localToken.TokenString;
                                newProperty.ElementClassName = ToUp(localToken.TokenString);
                                classItem.ElementProperties.Add(newProperty);
                            }
                    }
                    if (token.TokenActivities != null)
                    {
                        TreeNode actionNode = null;
                        foreach (SToken localToken in token.TokenActivities)
                        {
                            Wrap(localToken);
                            actionNode = new TreeNode(localToken.TokenString);
                            actionNode.Tag = token;
                            actionNode.ImageKey = "method";
                            classNode.Nodes.Add(actionNode);

                        }
                    }
                    if (token.TokenInfluenceA != null)
                    {
                        TreeNode actionNode = null;
                        foreach (SToken localToken in token.TokenInfluenceA)
                        {
                            ///if ()
                            //Wrap(localToken);
                            actionNode = new TreeNode(localToken.TokenString);
                            actionNode.Tag = token;
                            actionNode.ImageKey = "increase";
                            classNode.Nodes.Add(actionNode);

                        }
                    }

                    if (token.TokenInfluenceB != null)
                    {
                        TreeNode actionNode = null;
                        foreach (SToken localToken in token.TokenInfluenceB)
                        {
                            // Wrap(localToken);
                            actionNode = new TreeNode(localToken.TokenString);
                            actionNode.Tag = token;
                            actionNode.ImageKey = "decrease";
                            classNode.Nodes.Add(actionNode);

                        }
                    }

                    if (token.TokenInfluenceC != null)
                    {
                        TreeNode actionNode = null;
                        foreach (SToken localToken in token.TokenInfluenceC)
                        {
                            Wrap(localToken);
                            actionNode = new TreeNode(localToken.TokenString);
                            actionNode.Tag = token;
                            actionNode.ImageKey = "increasedBy";
                            classNode.Nodes.Add(actionNode);

                        }
                    }

                    if (token.TokenInfluenceD != null)
                    {
                        TreeNode actionNode = null;
                        foreach (SToken localToken in token.TokenInfluenceD)
                        {
                            Wrap(localToken);
                            actionNode = new TreeNode(localToken.TokenString);
                            actionNode.Tag = token;
                            actionNode.ImageKey = "decreasedBy";
                            classNode.Nodes.Add(actionNode);

                        }
                    }

                }
                foreach (SToken tokenlist in token.TokenChilds)
                {
                    Wrap(tokenlist);
                }
                if (token.TokenAfter != null)
                {
                    token = token.TokenAfter[0];
                }
                else
                {
                    token = null;
                }
            }
        }
        */

        private void PrepareRelations()
        {
            List<SymbolID> rels = new List<SymbolID>();
            relationCollection.GetAllOperators();
            r2 = relationCollection.operatorCollection[0];
            foreach (OperatorID r in relationCollection.operatorCollection)
            {
                comboBox1.Items.Add(r.Names[0]);

            }
            comboBox1.SelectedIndex = 2;
        }

        RelationCollection logics = new RelationCollection();
        ImageList iListe = new ImageList();
        List<string> wantedMappedTitles = new List<string>();
        
        private string rootSymbol;

        //static WebView(int symbolID)
        //{
        //    ActualID = new int[] { symbolID, 0 };
        //}
        public WebView()
        {
            InitializeComponent();
            PrepareRelations();
        }

        void attachLeaf(TreeNode root, SymbolID symbol, int level)
        {

            //prepare relation for leafs
            SymbolID relation = (SymbolID)comboBox1.Tag;//new Relation(new int[] { 2, 100, 9998, 1 }); // simbol include leafs
            //prepare leafs
            List<SymbolID> listSymbol = new List<SymbolID>();//=//  relationCollection.GetLeafsByRelation(symbol, relation);
            foreach (SymbolID leaf in listSymbol)
            {
                //if  (leaf.ID[0] == 408 || leaf.Name.Equals("Windows.Forms.Application1.Symbol")) return ;
                TreeNode newRoot = root.Nodes.Add(leaf.Names[0]);
                newRoot.Tag = leaf;
                if (leaf.Names[0] != root.Text && leaf.Names[0] != "InexistentSymbol")
                {
                    level++;
                    if (newRoot.Level < 4)
                    {
                        if (visibleSymbols.Where(p => p.Location.A == leaf.Location.A && p.Location.B == leaf.Location.B).FirstOrDefault() == null)
                        {
                            if (leaf.Names[0] == "Muscle") 
                            {
                            }
                            visibleSymbols.Add(leaf);
                            attachLeaf(newRoot, leaf, level++);
                        }
                    }
                }
            }
        }

        public WebView(string rootSymbol)
        {
            InitializeComponent();
            PrepareRelations();
            PrepareRelationsImageList();

            //prepare root
            SymbolID symbol = symbolCollection.GetSymbolCollection(rootSymbol).FirstOrDefault();
            
        }

        private void PrepareRelationsImageList()
        {
            wantedMappedTitles.AddRange(new string[] { "namespace", "operator", "delegate", "constructor", "class", "structure", "interface", "enumeration", "Property", "Method", "Field", "Member", "Conversion", "Event" });
            foreach (string typ in wantedMappedTitles)
            {
                iListe.Images.Add(new Icon("img\\"+typ + ".ico"));
            }
            treeView1.ImageList = iListe;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            return;
            SymbolID rootSymbol = (SymbolID)(((TreeView)sender).SelectedNode.Tag);
            RegenerateTree((TreeView)sender, rootSymbol);
            textBox1.Text = rootSymbol.Location.A.ToString();
        }

        void RegenerateTree(TreeView treeView, SymbolID rootSymbol)
        {
            this.treeView1.Nodes.Clear();
            TreeNode root = treeView.Nodes.Add(rootSymbol.Names[0]);
            root.Tag = rootSymbol;
            //prepare relation for leafs
            OperatorID relation = (OperatorID)comboBox1.Tag;//new Relation(new int[] { 2, 100, 9998, 1 }); // simbol include leafs
            //prepare leafs
            List<SymbolID> listSymbol = relationCollection.GetLeafsByRelation(rootSymbol, relation);
            List<SymbolID> listSymbol2 = new List<SymbolID>();
            foreach (SymbolID leaf in listSymbol)
            {
                listSymbol2.Add(symbolCollection.GetSymbolByID(leaf).FirstOrDefault());
            }
            foreach (SymbolID leaf in listSymbol2)
            {
                if (leaf == null) return;
                TreeNode newRoot = root.Nodes.Add(leaf.Names[0]);
                newRoot.Tag = leaf;
                string type = "class";// logics.getFirstType(leaf).FirstOrDefault().Name;
                while (type.IndexOf(' ') > 0)
                type = type.Substring( type.IndexOf(' ')+1 );
                newRoot.ImageIndex = wantedMappedTitles.IndexOf(type.ToLower());
                //newRoot.ImageKey = wantedMappedTitles.IndexOf(type.ToLower());
                attachLeaf(newRoot, leaf, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] rootID = new int[] { 0, 0, 0, 0 };
            int.TryParse(textBox1.Text, out rootID[0]); 
            //=// RegenerateTree(treeView1, symbolCollection.GetSymbolByID(rootID).FirstOrDefault());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Tag = relationCollection.operatorCollection[comboBox1.SelectedIndex];
        }

        private void comboBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int nextIndex = comboBox1.SelectedIndex + 1;
            if (nextIndex == comboBox1.Items.Count) nextIndex = 0;
            comboBox1.SelectedIndex = nextIndex;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


