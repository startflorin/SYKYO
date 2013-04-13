using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel.DL.CodeEntity;

namespace DataExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateTree();
        }

        private void CreateTree()
        {
            List<CodeElement> codeElements = DataModel.BL.DataModel.rootCodeElements;
            TreeExplorer treeExplorer = new TreeExplorer();
            treeExplorer.ConvertToTree(treeView1, codeElements);
        }
    }
}
