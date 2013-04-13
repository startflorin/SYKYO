using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DataModel.DL.CodeEntity;

namespace DiagramCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Test the project
        /// </summary>
        /// <returns></returns>
        private bool Test()
        {
            Diagram diagram = new Diagram("HelloWorld");
            string output = diagram.ExecuteDiagram();
            if (string.IsNullOrEmpty(output))
            {
                richTextBox1.Text = "Diagram Created Successfully!";
            }
            else
            {
                richTextBox1.Text = output;
            }
            return true;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Test();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            List<CodeElement> codeElements = DataModel.BL.DataModel.rootCodeElements;
            string diagramSource = CreateDiagramSource(codeElements);
            string applicationPath =
            "C:\\Users\\PROFIMEDICA\\Documents\\Visual Studio 2010\\WindowsFormsApplication1\\DiagramCreator\\DiagramFiles\\";
            System.IO.StreamWriter file = new System.IO.StreamWriter(applicationPath+"default.gv");
            file.AutoFlush = true;
            file.WriteLine("digraph n { splines=true; sep=\"+25,25\"; overlap=scalexy; nodesep=0.6; node [fontsize=11];");
            file.WriteLine(diagramSource);
            file.WriteLine("}");
            file.Close();
            Diagram diagram = new Diagram("default");
            string output = diagram.ExecuteDiagram();
            if (string.IsNullOrEmpty(output))
            {
                richTextBox1.Text = "Diagram Created Successfully!";
                this.Close();
            }
            else
            {
                richTextBox1.Text = output;
            }
        }

        public void Diagram(ElementProgram program)
        {
            List<CodeElement> codeElements = DataModel.BL.DataModel.rootCodeElements;
            string diagramSource = DiagramWriter(program);
            string applicationPath =
            "C:\\Users\\PROFIMEDICA\\Documents\\Visual Studio 2010\\WindowsFormsApplication1\\DiagramCreator\\DiagramFiles\\";
            System.IO.StreamWriter file = new System.IO.StreamWriter(applicationPath + "diagram.gv");
            file.AutoFlush = true;
            file.WriteLine("digraph n { splines=true; sep=\"+25,25\"; overlap=scalexy; nodesep=0.6; node [fontsize=11];");
            file.WriteLine(diagramSource);
            file.WriteLine("}");
            file.Close();
            Diagram diagram = new Diagram("diagram");
            string output = diagram.ExecuteDiagram();
            if (string.IsNullOrEmpty(output))
            {
                richTextBox1.Text = "Diagram Created Successfully!";
                this.Close();
            }
            else
            {
                richTextBox1.Text = output;
            }

        }

        private string CreateDiagramSource(List<CodeElement> codeElements)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (CodeElement codeElement in codeElements)
            {
                stringBuilder.Append(CreateDiagramSource(codeElement));
            }
            return stringBuilder.ToString();
        }

        private string DiagramWriter(ElementProgram program)
        {
            StringBuilder sb = new StringBuilder();
            string applicationPath =
            "C:\\Users\\PROFIMEDICA\\Documents\\Visual Studio 2010\\WindowsFormsApplication1\\DiagramCreator\\DiagramFiles\\";
            foreach (CodeFile codeFile in program.CodeFiles)
            {
                //using (StreamWriter sw = new StreamWriter(applicationRoot + "\\" + codeFile.FileName + ".cs", true))
                using (StreamWriter sw = new StreamWriter(applicationPath + "\\" + "generated" + ".gw", true))
                {
                    foreach (ElementNamespace namespaceItem in codeFile.Namespaces)
                    {
                        sb.Append(" subgraph cluster_0 { ");
                        sb.Append(" style=filled; ");
                        sb.Append(" color=lightgrey; ");
                        sb.Append(" node [style=filled,color=white]; ");
                        foreach (ElementClass classItem in namespaceItem.ElementClasses)
                        {
                            sb.Append(" " + classItem.Name + "; ");
                        }
                        sb.Append(" label = \"" + namespaceItem .ElementName + "\"; ");
                        sb.Append(" } ");

                        foreach (ElementClass classItem in namespaceItem.ElementClasses)
                        {

                            /*
                            foreach (ElementProperty codeProperty in classItem.ElementProperties)
                            {
                                if (!codeProperty.ElementName.Equals("..P"))
                                    sw.WriteLine(codeProperty.ToBlock());
                            }

                            foreach (ElementMethod codeMethod in classItem.ElementMethods)
                            {
                                if (!codeMethod.ElementName.Equals(".."))
                                    if (!codeMethod.IsStatic)
                                        sw.WriteLine(codeMethod.ToBlock());
                            }

                            foreach (ElementProperty codeProperty in classItem.ElementProperties)
                            {
                                if (!codeProperty.ElementName.Equals("..P") && codeProperty.IsMandatory)
                                    sw.WriteLine("        public bool isNullable" + codeProperty.ElementName + "P = false;");
                            }

                            foreach (ElementProperty codeProperty in classItem.ElementProperties)
                            {
                                if (!codeProperty.ElementName.Equals("..P") && codeProperty.IsMandatory)
                                {
                                    sw.WriteLine("            if (" + codeProperty.ElementName.ToLower() + "P == null)");
                                    sw.WriteLine("            {");
                                    sw.WriteLine("                result = false;");
                                    sw.WriteLine("            }");
                                }
                            }

                            foreach (ElementMethod codeMethod in classItem.ElementMethods)
                            {
                                if (codeMethod.ElementName.StartsWith("Increasedby"))
                                {
                                    sw.WriteLine(codeMethod.ToBlock());
                                }
                                else
                                    if (codeMethod.ElementName.StartsWith("DecreasedBy"))
                                    {
                                        sw.WriteLine(codeMethod.ToBlock());
                                    }
                                    else
                                        if (codeMethod.ElementName.StartsWith("Increase"))
                                        {
                                            sw.WriteLine(codeMethod.ToBlock());
                                        }
                                        else
                                            if (codeMethod.ElementName.StartsWith("Decrease"))
                                            {
                                                sw.WriteLine(codeMethod.ToBlock());
                                            }

                            }
                             * */
                        }
                    }
                }
            }
            return sb.ToString();
        }


        private string CreateDiagramSource(CodeElement codeElement)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (codeElement.Childs != null)
            foreach (CodeElement codeSectionChild in codeElement.Childs)
            {
                if (!string.IsNullOrEmpty(codeElement.Name.Trim()) &&
                    !string.IsNullOrEmpty(codeSectionChild.Name.Trim()))
                {

                    string shape = "elipse";
                    switch (codeSectionChild.Visibility)
                    {
                        case 1:
                            shape = "diamond";
                            break;
                        case 2:
                            shape = "diamond";
                            break;
                        case 3:
                            shape = "diamond";
                            break;
                        case 4:
                            shape = "diamond";
                            break;
                        default:
                            shape = "elipse";
                            break;
                    }
                    string color = "white";
                    double complexity = (codeSectionChild.Childs.Count);
                    switch ((int) Math.Round(complexity))
                    {
                        case 0:
                            color = "yellow";
                            break;
                        case 1:
                            color = "yellow";
                            break;
                        case 2:
                            color = "orange";
                            break;
                        case 3:
                            color = "green";
                            break;
                        case 4:
                            color = "gray";
                            break;
                        case 5:
                            color = "magenta";
                            break;
                        default:
                            color = "red";
                            break;
                    }

                    string codeSectionChildIdentifier = codeSectionChild.Name.Replace('.', '_');
                    if (codeSectionChildIdentifier.Contains("[")) stringBuilder.ToString();
                    if (codeElement.Name.Contains("[")) stringBuilder.ToString();
                    if (codeSectionChildIdentifier.Contains("+")) stringBuilder.ToString();
                    if (codeElement.Name.Contains("+")) stringBuilder.ToString();
                    if (codeSectionChildIdentifier.Contains("~")) stringBuilder.ToString();
                    if (codeElement.Name.Contains("~")) stringBuilder.ToString();
                    if (string.IsNullOrEmpty(codeElement.Name.Trim()))
                    {
                        stringBuilder.ToString();
                    }
                    if (!codeSectionChildIdentifier.Contains("[") && !codeElement.Name.Contains("["))
                    {
                        if (!codeSectionChildIdentifier.Contains("~") && !codeElement.Name.Contains("~"))
                        {
                            if (!codeSectionChildIdentifier.Contains("+") && !codeElement.Name.Contains("+"))
                            {
                                if (!codeSectionChildIdentifier.Contains("=") && !codeElement.Name.Contains("="))
                                {

                                    stringBuilder.AppendLine(codeSectionChildIdentifier + "[fillcolor=" + color +
                                                             ", style=\"rounded,filled\", shape=" + shape + "];");
                                    stringBuilder.AppendLine(codeElement.Name.Replace('.', '_') + " -> " +
                                                             codeSectionChildIdentifier +
                                                             ";");
                                }
                            }
                        }
                    }
                }
                stringBuilder.Append(CreateDiagramSource(codeSectionChild));
            }
             //foreach (CodeElement codeElementChild in codeElement.Childs)
             {
                 
             }
            return stringBuilder.ToString();
        }
    }
}
