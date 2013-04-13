using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;

namespace DiagramCreator
{
    public class DiagramWriter
    {
        public void ToDiagram(ElementProgram program)
        {
            string diagramSource = CreateFromProgram(program);
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
        }

        private string CreateFromProgram(ElementProgram program)
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
                        sb.Append(" subgraph cluster_" + namespaceItem.ElementName + " { ");
                        sb.Append(" style=filled; ");
                        sb.Append(" color=lightgrey; ");
                        sb.Append(" node [style=filled,color=white]; ");
                        foreach (ElementClass classItem in namespaceItem.ElementClasses)
                        {
                            if (!classItem.Name.Equals(".."))
                            {
                                sb.Append(" " + classItem.Name + "; ");
                            }
                        }
                        sb.Append(" label = \"" + namespaceItem.ElementName + "\"; ");
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

    }
}
