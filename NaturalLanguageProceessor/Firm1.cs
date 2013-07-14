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
using NaturalLanguageProcessor.Test;

namespace NaturalLanguageProcessor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void naturalFromInternalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NaturalFromInternal naturalFromInternal = new NaturalFromInternal();
            naturalFromInternal.Show();
        }

        private void internalFromNaturalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternalFromNatural internalFromNatural = new InternalFromNatural();
            internalFromNatural.Show();
        }

        private void naturalWriterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CodeElement> codeElements = DataModel.BL.DataModel.rootCodeElements;
            NaturalWriter naturalWriter = new NaturalWriter();
            string diagramSource = naturalWriter.ConvertToNaturalLanguage(codeElements);
            string applicationPath =
            "P:\\SYKYO\\NaturalLanguageProceessor\\NaturalFiles\\";
            System.IO.StreamWriter file = new System.IO.StreamWriter(applicationPath + "default.php");
            file.AutoFlush = true;
            file.WriteLine("");
            file.WriteLine(diagramSource);
            file.WriteLine("");
            file.Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CodeElement> codeElements = new List<CodeElement>();
            string expression = "cat is an animal";
            CodeElement objA = new ElementClass();
            objA.Name = "Cat";
            CodeElement objB = new ElementClass();
            objB.Name = "Leg";
            CodeElement objC = new ElementClass();
            objC.Name = "Head";
            CodeElement objD = new ElementClass();
            objD.Name = "Tail";
            CreateRelationHasA(objA, objB, CodeRelation.ReletionType.HasA, CodeRelation.ReletionMultiplicity.Some);
            CreateRelationHasA(objA, objC, CodeRelation.ReletionType.HasA, CodeRelation.ReletionMultiplicity.One);
            CreateRelationHasA(objA, objD, CodeRelation.ReletionType.HasA, CodeRelation.ReletionMultiplicity.One);

            DataModel.BL.DataModel.rootCodeElements = new List<CodeElement>() {objA};
        }

        private void CreateRelationHasA(CodeElement objA, CodeElement objB, CodeRelation.ReletionType relationType, CodeRelation.ReletionMultiplicity relationMultiplicity)
        {
            CodeRelation codeRelation = null;

            if (objA.Properties == null)
            {
                objA.Properties = new List<CodeRelation>();
            }
            if (null == (codeRelation = objA.Properties.FirstOrDefault(p => p.RelationType == (int)relationType)))
            {
                codeRelation = new CodeRelation(relationType);
                //codeRelation.Childs = new List<Multiplicity>();
                objA.Properties.Add(codeRelation);
            }
            if (codeRelation.Childs.FirstOrDefault(p => p.CodeElement.Name == objB.Name) == null)
            {
                Arrow multiplicity = new Arrow();
                multiplicity.CodeElement = objB;
                multiplicity.Multiplicity = (int)relationMultiplicity;
                codeRelation.Childs.Add(multiplicity);
            }
        }

        private void CreateRelationIsA(CodeElement objA, CodeElement objB)
        {
            //objA;
        }
    }
}
