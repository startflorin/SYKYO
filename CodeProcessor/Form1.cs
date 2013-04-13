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

namespace CodeProcessor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CodeReader codeReader = new CodeReader();
            codeReader.ReadCodeFile("");
            CustomInitialize();
        }

        private void CustomInitialize()
        {
            comboBox1.Items.Add("C:\\Leonardo10\\trunk\\Frontend\\wowi");
            comboBox1.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModelReaderCS modelReader = new ModelReaderCS(comboBox1.SelectedItem.ToString());
            CodeProcessor.CodeWriter.ModelWriterCS modelWriter = new CodeProcessor.CodeWriter.ModelWriterCS();
            modelWriter.ToProgrammingLanguage(modelReader.ElementProgram);

            CodeReader codeReader = new CodeReader();
            List<CodeElement> codeElements = codeReader.ReadCodeFile(comboBox1.SelectedItem.ToString());
            DataModel.BL.DataModel.rootCodeElements = codeElements;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CodeWriter.CodeWriter codeWriter = new CodeWriter.CodeWriter();
            string codeSource = codeWriter.WriteCode(DataModel.BL.DataModel.rootCodeElements, string.Empty);
            string applicationPath =
            "C:\\Users\\PROFIMEDICA\\Documents\\Visual Studio 2010\\WindowsFormsApplication1\\CodeProcessor\\CodeFiles\\";
            System.IO.StreamWriter file = new System.IO.StreamWriter(applicationPath + "default.php");
            file.AutoFlush = true;
            file.WriteLine("<style>");

            file.WriteLine("body {font-family:Verdana, Arial, Helv, Helvetica, sans-serif; font-size: small;} ");
            file.WriteLine(".returnType {color: SteelBlue} ");
            file.WriteLine(".parameterType {color: gray} ");
            file.WriteLine(".identifier {color: black} ");
            file.WriteLine(".extended {color: red} ");
            file.WriteLine(".control {color: blue} ");
            file.WriteLine(".modifier {color: green} ");
            file.WriteLine(".parameter {color: magenta} ");
            file.WriteLine("</style>");

            file.WriteLine(codeSource);
            file.Close();
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            comboBox1.Items.Add(folderBrowserDialog1.SelectedPath);
        }
    }
}
