using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataModel.DL.CodeEntity;

namespace DataExplorer
{
    class TreeExplorer
    {
        public List<TreeNode> GetTreeChilds(List<CodeElement> codeElements)
        {
            List<TreeNode> childs = new List<TreeNode>();
            if (codeElements != null)
            {
                int elementOrdinal = 0;
                foreach (CodeElement codeElement in codeElements)
                {
                    TreeNode treeNode = new TreeNode();
                    List<TreeNode> childNodes = GetTreeChilds(codeElement.Childs);
                    int totalSubnodes = 0;
                    foreach (TreeNode childNode in childNodes)
                    {
                        treeNode.Nodes.Add(childNode);
                        totalSubnodes += 1 + (int)childNode.Tag;
                    }
                    treeNode.Tag = totalSubnodes;
                    if (totalSubnodes > 0 || !string.IsNullOrEmpty(codeElement.Name))
                    {
                        elementOrdinal++; string childsNumber = totalSubnodes > 0 ? " (" + totalSubnodes + ")": string.Empty;
                        if (codeElement.ReturnType == "event")
                        {
                            treeNode.ForeColor = Color.Red;
                        }
                        if (codeElement.ReturnType == "method")
                        {
                            treeNode.ForeColor = Color.Blue;
                        }
                        if (codeElement.ReturnType == "property")
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                        if (codeElement.ReturnType == "class")
                        {
                            treeNode.ForeColor = Color.Magenta;
                        }
                        treeNode.Text = elementOrdinal + ": " + codeElement.Name + childsNumber;
                        childs.Add(treeNode);
                    }
                } 
            }
            return childs;
        }

        internal void ConvertToTree(TreeView treeView1, List<CodeElement> codeElements)
        {
            int elementOrdinal = 0;
            foreach (CodeElement codeElement in codeElements)
            {
                TreeNode treeNode = new TreeNode();
                List<TreeNode> childNodes = GetTreeChilds(codeElement.Childs);
                foreach (TreeNode childNode in childNodes)
                {
                    //treeNode.Nodes.Add(childNode);
                }
                int totalSubnodes = 0;
                foreach (TreeNode childNode in childNodes)
                {
                    treeNode.Nodes.Add(childNode);
                    totalSubnodes += 1 + (int)childNode.Tag;
                }
                treeNode.Tag = totalSubnodes;
                
                if (totalSubnodes > 0 || !string.IsNullOrEmpty(codeElement.Name))
                {
                    elementOrdinal++;
                    if (codeElement.Childs != null)
                    {
                        treeNode.Text = elementOrdinal + ": " + codeElement.Name + " ("+totalSubnodes + ")";
                    }
                    else
                    {
                        treeNode.Text = codeElement.Name + " (" + totalSubnodes + ")";
                    } 
                    treeNode.ExpandAll();
                    treeView1.Nodes.Add(treeNode);
                }
            }
        }
    }
}
