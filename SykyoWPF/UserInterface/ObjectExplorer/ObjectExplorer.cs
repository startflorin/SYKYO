using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataModel.DL.CodeEntity;
using WpfApplication1.Controller.DocumentManager;

namespace WpfApplication1.UserInterface.ObjectExplorer
{
    public partial class ObjectExplorer : Form
    {

        private string ToUp(string identifier)
        {
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }

        public ObjectExplorer()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

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

        List<SToken> words = new List<SToken>();


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

    }
}
