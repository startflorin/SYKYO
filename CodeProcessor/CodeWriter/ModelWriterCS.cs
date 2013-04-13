using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;

namespace CodeProcessor.CodeWriter
{
    public class ModelWriterCS
    {
        private string ToUp(string identifier)
        {
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }


 
        /// <summary>
        /// generate filesystem of the application
        /// </summary>
        private void GenerateCode(ElementProgram program)
        {
            foreach (CodeFile codeFile in program.CodeFiles)
            {
                //using (StreamWriter sw = new StreamWriter(applicationRoot + "\\" + codeFile.FileName + ".cs", true))
                using (StreamWriter sw = new StreamWriter(applicationRoot + "\\" + codeFile.FileName + ".cs", true))
                {
                    foreach (ElementNamespace namespaceItem in codeFile.Namespaces)
                    {
                        foreach (ElementClass classItem in namespaceItem.ElementClasses)
                        {

                            // Write anthet
                            sw.WriteLine("using System;");
                            sw.WriteLine("using System.Collections.Generic;");
                            sw.WriteLine("using System.Linq;");
                            sw.WriteLine("using System.Text;");
                            sw.WriteLine("using System.Collections.ObjectModel;");
                            sw.WriteLine("namespace Object.Model");
                            sw.WriteLine("{");
                            sw.WriteLine("public class " + classItem.Name + "List : ObservableCollection<" + classItem.Name + ">");
                            sw.WriteLine("{");
                            sw.WriteLine("public bool Add(object newItem)");
                            sw.WriteLine("{");
                            sw.WriteLine("base.Add(newItem as " + classItem.Name + ");");
                            sw.WriteLine("return true;");
                            sw.WriteLine("}");
                            sw.WriteLine("");
                            sw.WriteLine("public bool Remove(object unwantedItem)");
                            sw.WriteLine("        {");
                            sw.WriteLine("            base.Remove(unwantedItem as " + classItem.Name + ");");
                            sw.WriteLine("            return true;");
                            sw.WriteLine("        }");
                            sw.WriteLine("");
                            sw.WriteLine("        private static " + classItem.Name + "List instance;");
                            sw.WriteLine("");
                            sw.WriteLine("        public " + classItem.Name + "List(): base() ");
                            sw.WriteLine("        {");
                            sw.WriteLine("");
                            sw.WriteLine("        }");
                            sw.WriteLine("");
                            sw.WriteLine("        public static " + classItem.Name + "List GetInstance()");
                            sw.WriteLine("        {");
                            sw.WriteLine("            lock (typeof(" + classItem.Name + "List))");
                            sw.WriteLine("            {");
                            sw.WriteLine("                if (instance == null)");
                            sw.WriteLine("                {");
                            sw.WriteLine("                    instance = new " + classItem.Name + "List();");
                            sw.WriteLine("                }");
                            sw.WriteLine("                return instance;");
                            sw.WriteLine("            }");
                            sw.WriteLine("        }");
                            sw.WriteLine("    }");
                            sw.WriteLine("");
                            sw.WriteLine("    [Serializable]");
                            sw.WriteLine("    public class " + classItem.Name + "");
                            sw.WriteLine("    {");
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

                            sw.WriteLine("        private int id;");
                            sw.WriteLine("        public int ID");
                            sw.WriteLine("        {");
                            sw.WriteLine("            get { return id; }");
                            sw.WriteLine("            set { id = value; }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private string name;");
                            sw.WriteLine("        public string Name");
                            sw.WriteLine("        {");
                            sw.WriteLine("            get { return name; }");
                            sw.WriteLine("            set { name = value; }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private string description;");
                            sw.WriteLine("        public string Description");
                            sw.WriteLine("        {");
                            sw.WriteLine("            get { return description; }");
                            sw.WriteLine("            set { description = value; }");
                            sw.WriteLine("        }");
                            sw.WriteLine("        private string url;");
                            sw.WriteLine("        public string URL");
                            sw.WriteLine("        {");
                            sw.WriteLine("            get { return url; }");
                            sw.WriteLine("            set { url = value; }");
                            sw.WriteLine("        }");
                            sw.WriteLine("    }");
                            sw.WriteLine("}");
                            sw.WriteLine("");
                            sw.WriteLine("namespace Object.Meta");
                            sw.WriteLine("{");
                            sw.WriteLine("");
                            sw.WriteLine("    public class " + classItem.Name + "Metainformation");
                            sw.WriteLine("    {");

                            foreach (ElementProperty codeProperty in classItem.ElementProperties)
                            {
                                if (!codeProperty.ElementName.Equals("..P") && codeProperty.IsMandatory)
                                    sw.WriteLine("        public bool isNullable" + codeProperty.ElementName + "P = false;");
                            }


                            sw.WriteLine("");
                            sw.WriteLine("        public bool IsValid()");
                            sw.WriteLine("        {");
                            sw.WriteLine("            bool result = true;");

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

                            sw.WriteLine("            return result;");
                            sw.WriteLine("        }");
                            sw.WriteLine("    }");
                            sw.WriteLine("}");

                            sw.WriteLine("namespace Object.Influence");
                            sw.WriteLine("{");
                            sw.WriteLine("");
                            sw.WriteLine("    public class " + classItem.Name + "Influence");
                            sw.WriteLine("    {");
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
                            sw.WriteLine("    }");
                            sw.WriteLine("}");


                        }
                    }
                }
            }
        }

        string applicationRoot = @"..\\..\\..\\..\\GeneratedDataModel\\Model";

        /// <summary>
        /// Clean the generated application folder
        /// </summary>
        private void ClearProject()
        {
            //string[] acceptedFiles = new string[] { applicationRoot + "\\" + "GeneratedApplication.csproj", applicationRoot + "\\" + "Form1.cs", applicationRoot + "\\" + "Form1.Designer.cs", applicationRoot + "\\" + "Program.cs" };

            try
            {
                string[] files = Directory.GetFiles(applicationRoot);
                foreach (string f in files)
                {
                    //if (!acceptedFiles.Contains(f))
                    {
                        File.Delete(f);
                    }
                }
            }
            catch (Exception e) { }
        }

        public void ToProgrammingLanguage(ElementProgram program)
        {
            ClearProject();
            GenerateCode(program);
            //GeneratePersistency();
        }

    }
}
