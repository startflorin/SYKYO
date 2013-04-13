using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using NaturalLanguageProcessor.Properties;

namespace NaturalLanguageProcessor
{
    public class NaturalWriter
    {
        public string ConvertToNaturalLanguage(List<CodeElement> codeSections)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (CodeElement codeSection in codeSections)
            {
                stringBuilder.Append(ConvertToNaturalLanguage(codeSection));
            }
            return stringBuilder.ToString();
        }

        private string ConvertToNaturalLanguage(CodeElement codeSection)
        {

            StringBuilder stringBuilder = new StringBuilder();
            
            if (!string.IsNullOrEmpty(codeSection.Name)) // && codeSection.modifiers.Contains("public"))
            {
                if (codeSection.ReturnType != null && codeSection.ReturnType.Equals("class"))
                {
                    //Console.WriteLine("\t The " + codeSection.ReturnType + " " + codeSection.identifier + " ");
                    stringBuilder.AppendLine("\t" + " a " + codeSection.Name);
                }
                if (codeSection.ReturnType != null && codeSection.ReturnType.Equals("namespace"))
                {
                    //Console.WriteLine("In " + codeSection.ReturnType + " " + codeSection.identifier + " ");
                    stringBuilder.AppendLine(" In " + codeSection.Name);
                }
            }
            if (codeSection.Childs!=null)
            foreach (CodeElement codeSectionChild in codeSection.Childs)
            {
                if (!string.IsNullOrEmpty(codeSectionChild.Name))
                {
                    // Only public methods
                    if (!codeSectionChild.Modifiers.Contains("public"))
                    {
                        // Console.WriteLine();
                        stringBuilder.AppendLine(ConvertToNaturalLanguage(codeSectionChild));
                        continue;
                    }

                    if (codeSectionChild.ReturnType.Equals("eventHandler"))
                    {
                        stringBuilder.AppendLine("\t\t => " + "React to: " +
                                      codeSectionChild.Name);
                    }
                    else if (codeSectionChild.ReturnType.Equals("method"))
                    {
                        stringBuilder.AppendLine("\t\t => " + "Can: " + codeSectionChild.Name);
                    }
                    else
                    {
                        if (codeSectionChild.ReturnType != null &&
                            (codeSectionChild.ReturnType.Equals("namespace") ||
                             codeSectionChild.ReturnType.Equals("class")))
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.AppendLine(ConvertToNaturalLanguage(codeSectionChild));
                            continue;
                        }
                        stringBuilder.AppendLine("\t\t => " + " " + codeSectionChild.Name);
                    }
                    if (codeSectionChild.ReturnType != null && !codeSectionChild.ReturnType.Equals("void"))
                    {
                        stringBuilder.Append(" as a " + codeSectionChild.ReturnType);
                    }
                    string parameters ="";
                    if (codeSectionChild.Parameters != null)
                    {
                        parameters += codeSectionChild.Parameters.Aggregate("",
                                                                                  (current, parameter) =>
                                                                                  current + parameter);
                    }

                    // has parameters but is not an event
                    if (!string.IsNullOrEmpty(parameters) && !codeSectionChild.ReturnType.Equals("eventHandler"))
                    {
                        stringBuilder.Append(" based on " + codeSectionChild.Parameters);
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine(ConvertToNaturalLanguage(codeSectionChild));

            }
            
            return stringBuilder.ToString();

        }

    }
}
