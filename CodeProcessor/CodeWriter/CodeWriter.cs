using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeProcessor.Properties;
using DataModel.DL.CodeEntity;
namespace CodeProcessor.CodeWriter
{
    /// <summary>
    /// 
    /// </summary>
    internal class CodeWriter
    {
       private readonly List<CodeSection> codeSections = new List<CodeSection>();
        private readonly List<string> fileContents = new List<string>();

        public List<string> ReadCodeFile(string rootPath)
        {
            var access = new FileAccess();
            List<string> filePaths = access.GetFilePathList(null);
            if (filePaths != null)
            {
                access.GetFileList(filePaths);
                foreach (string filePath in filePaths)
                {
                    if (!filePath.Contains("CodeReader"))
                    {
                        //continue;
                    }
                    string fileContent = access.GetFileContent(filePath).ToString();
                    fileContents.Add(fileContent);
                    codeSections.Add(new CodeSection(filePath, fileContent, 0, 0));
                }
            }
            foreach (CodeSection codeSection in codeSections)
            {
                GetIdentifier(codeSection);
                ConvertToNaturalLanguage(codeSection);
            }
            return null;
        }

        private void ConvertToNaturalLanguage(CodeSection codeSection)
        {

            if (!string.IsNullOrEmpty(codeSection.Identifier)) // && codeSection.modifiers.Contains("public"))
            {
                if (codeSection.ReturnType != null && codeSection.ReturnType.Equals("class"))
                {
                    //Console.WriteLine("\t The " + codeSection.ReturnType + " " + codeSection.identifier + " ");
                    Console.WriteLine(Resources.String_tab + Resources.String__a_ + codeSection.Identifier);
                }
                if (codeSection.ReturnType != null && codeSection.ReturnType.Equals("namespace"))
                {
                    //Console.WriteLine("In " + codeSection.ReturnType + " " + codeSection.identifier + " ");
                    Console.WriteLine(Resources.String_In_ + codeSection.Identifier);
                }
            }
            foreach (CodeSection codeSectionChild in codeSection.CodeSections)
            {
                if (!string.IsNullOrEmpty(codeSectionChild.Identifier))
                {
                    // Only public methods
                    if (!codeSectionChild.Modifiers.Contains("public"))
                    {
                        // Console.WriteLine();
                        ConvertToNaturalLanguage(codeSectionChild);
                        continue;
                    }

                    if (codeSectionChild.Header.Contains("_"))
                    {
                        Console.Write(Resources.String_tab_arrow + Resources.String_react_to__ +
                                      codeSectionChild.Identifier);
                    }
                    else if (codeSectionChild.Header.Contains("("))
                    {
                        Console.Write(Resources.String_tab_arrow + Resources.String_can__ + codeSectionChild.Identifier);
                    }
                    else
                    {
                        if (codeSectionChild.ReturnType != null &&
                            (codeSectionChild.ReturnType.Equals("namespace") ||
                             codeSectionChild.ReturnType.Equals("class")))
                        {
                            Console.WriteLine();
                            ConvertToNaturalLanguage(codeSectionChild);
                            continue;
                        }
                        Console.Write(Resources.String_tab_arrow + Resources.String_ + codeSectionChild.Identifier);
                    }
                    if (codeSectionChild.ReturnType != null && !codeSectionChild.ReturnType.Equals("void"))
                    {
                        Console.Write(Resources.String__as_a_ + codeSectionChild.ReturnType);
                    }
                    string parameters = codeSectionChild.Parameters.Aggregate("",
                                                                              (current, parameter) =>
                                                                              current + parameter);

                    // has parameters but is not an event
                    if (!string.IsNullOrEmpty(parameters) && !codeSectionChild.Header.Contains("_"))
                    {
                        Console.Write(Resources.String__based_on_ + codeSectionChild.Parameters);
                    }
                    Console.WriteLine();
                }
                ConvertToNaturalLanguage(codeSectionChild);

            }
        }

        private void GetIdentifier(CodeSection codeSection)
        {
            var modifiers = new List<string>();
            var parameters = new List<string>();
            string modifiersString = string.Empty;
            string retutnType = "";
            string identifier = "";
            string parametersString = "";
            if (!string.IsNullOrEmpty(codeSection.Header))
            {
                string header = codeSection.Header;
                int roundBracketIndex = header.IndexOf('(');
                if (roundBracketIndex > -1)
                {
                    parametersString = header.Substring(roundBracketIndex);
                    int beginOfIdentifier = header.Substring(0, roundBracketIndex).Trim().LastIndexOf(' ');
                    if (beginOfIdentifier > -1)
                    {
                        identifier = header.Substring(beginOfIdentifier, roundBracketIndex - beginOfIdentifier);
                        retutnType = header.Substring(0, beginOfIdentifier);
                    }
                    else
                    {
                        identifier = header.Substring(0, roundBracketIndex);
                    }
                }
                else
                {
                    int beginOfIdentifier = header.LastIndexOf(' ');
                    if (beginOfIdentifier > -1)
                    {
                        identifier = header.Substring(beginOfIdentifier);
                        retutnType = header.Substring(0, beginOfIdentifier);
                    }
                }
                if (!string.IsNullOrEmpty(retutnType))
                {
                    string[] modifiersArray = retutnType.Trim().Split(' ');
                    modifiers = modifiersArray.ToList();

                    // Constructors have no return types
                    if (modifiersArray.Length != 1 || !modifiersArray[0].Equals("public"))
                    {
                        retutnType = modifiersArray[modifiersArray.Length - 1];
                        modifiers.RemoveAt(modifiers.Count - 1);
                    }
                    else
                    {
                        retutnType = "";
                    }
                    foreach (string modifier in modifiers)
                    {
                        modifiersString += modifier + " ";
                    }
                    if (parametersString.Length > 2)
                    {
                        parameters =
                            parametersString.Trim().Substring(1, parametersString.Length - 2).Split(',').ToList();
                        parametersString = "(.";
                        foreach (string parameter in parameters)
                        {
                            parametersString += parameters + ",";
                        }
                        parametersString += ".)";

                    }
                    if (modifiers.Contains("private"))
                    {
                        codeSection.Visibility = (int) CodeSection.Visible.Private;
                    }
                    if (modifiers.Contains("protected"))
                    {
                        codeSection.Visibility = (int) CodeSection.Visible.Protected;
                    }
                    if (modifiers.Contains("public"))
                    {
                        codeSection.Visibility = (int) CodeSection.Visible.Public;
                    }
                    codeSection.Identifier = identifier;
                    codeSection.Modifiers = modifiers;
                    codeSection.Parameters = parameters;
                    codeSection.ReturnType = retutnType;
                    //Console.WriteLine(modifiersString + " || R:" + retutnType + " || " + identifier + " || " + parametersString);
                    //Console.WriteLine(modifiersString + " || R:" + retutnType + " || " + identifier + " || " + parametersString);
                }
            }
            foreach (CodeSection codeSectionChild in codeSection.CodeSections)
            {
                GetIdentifier(codeSectionChild);
                if (!string.IsNullOrEmpty(identifier.Trim()) &&
                    !string.IsNullOrEmpty(codeSectionChild.Identifier.Trim()))
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
                    double complexity = (codeSectionChild.CodeSections.Count);
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

                    string codeSectionChildIdentifier = codeSectionChild.Identifier.Replace('.', '_');
                    if (codeSectionChildIdentifier.Contains("[")) return;
                    if (codeSection.Identifier.Contains("[")) return;
                    if (codeSectionChildIdentifier.Contains("+")) return;
                    if (codeSection.Identifier.Contains("+")) return;
                    if (codeSectionChildIdentifier.Contains("~")) return;
                    if (codeSection.Identifier.Contains("~")) return;
                    if (string.IsNullOrEmpty(codeSection.Identifier.Trim()))
                    {
                        return;
                    }
                    Console.WriteLine(codeSectionChildIdentifier + "[fillcolor=" + color +
                                      ", style=\"rounded,filled\", shape=" + shape + "];");
                    Console.WriteLine(codeSection.Identifier.Replace('.', '_') + " -> " + codeSectionChildIdentifier +
                                      ";");
                }
            }
        }

        private bool isInString;
        private char lastCharacter = '\0';

        private bool IsNonExecutable(char c)
        {
            if (isInString)
            {
                if (c == '\"')
                {
                    if (lastCharacter != '\\')
                    {
                        isInString = false;
                    }
                }
            }
            else if (isInBlockComment)
            {
                if (c == '\\')
                {
                    if (lastCharacter == '*')
                    {
                        isInBlockComment = false;
                    }
                }
            }
            else if (isInLineComment)
            {
                if (c == '\n')
                {
                    if (lastCharacter == '\r')
                    {
                        isInLineComment = false;
                    }
                }
            }
            else
            {
                if (c == '\"')
                {
                    if (lastCharacter != '\\')
                    {
                        isInString = true;
                    }
                }
                if (c == '/')
                {
                    if (lastCharacter == '/')
                    {
                        isInLineComment = true;
                    }
                }
                if (c == '*')
                {
                    if (lastCharacter == '/')
                    {
                        isInBlockComment = true;
                    }
                }
            }
            lastCharacter = c;
            return isInString || isInBlockComment || isInLineComment;
        }

        private bool isInLineComment;
        private bool isInBlockComment;

        public List<CodeSection> GetAllCodeElements(string filePath, string fileContent, int beginSection, int level)
        {
            string header = "";
            bool acceptBracket = false;
            bool branchContainExpression = false;
            int lastCodeEndedAt = beginSection;
            var ChildCodeSections = new List<CodeSection>();
            var codeSectionsChilds = new List<CodeSection>();
            for (int i = beginSection; i < fileContent.Length; i++)
            {
                if (IsNonExecutable(fileContent[i]))
                {
                    continue;
                }
                if (fileContent[i] == ';')
                {
                    branchContainExpression = true;
                    var codeSection = new CodeSection(filePath, fileContent, lastCodeEndedAt, i + 1, level);
                    ChildCodeSections.Add(codeSection);
                    lastCodeEndedAt = i + 1;
                }
                else if (fileContent[i] == '}')
                {

                    if (!acceptBracket)
                    {
                        //CodeSection codeSection = new CodeSection(filePath, fileContent, lastCodeEndedAt, i + 1, level);
                        //codeSections.Add(codeSection);
                        return ChildCodeSections;
                    }
                    if (branchContainExpression)
                    {
                        var codeSection = new CodeSection(filePath, fileContent, lastCodeEndedAt, i + 1, level)
                            {
                                Header = header.Trim()
                            };
                        if (codeSectionsChilds != null)
                        {
                            codeSection.CodeSections.AddRange(codeSectionsChilds);
                        }
                        ChildCodeSections.Add(codeSection);
                        lastCodeEndedAt = i + 1;
                    }
                    acceptBracket = false;
                }
                else if (fileContent[i] == '{')
                {
                    branchContainExpression = false;
                    //if (codeSectionsChilds.Count < 1 && !acceptBracket)
                    {
                        header = fileContent.Substring(lastCodeEndedAt, i - lastCodeEndedAt);
                    }
                    level++;
                    codeSectionsChilds = GetAllCodeElements(filePath, fileContent, i + 1, level);
                    if (codeSectionsChilds != null && codeSectionsChilds.Count > 0)
                    {
                        i = codeSectionsChilds[codeSectionsChilds.Count - 1].EndSection;
                        branchContainExpression = true;
                    }
                    acceptBracket = true;
                }
            }
            return ChildCodeSections;
        }

        private enum ReturnType
        {
        };

        private bool asHTML = true;

        private string writeMethod(string ident, int format)
        {
            return string.Empty;
        }

        private string writeNamespace(string ident, int format)
        {
            return string.Empty;
        }

        private string writeClass(string ident, int format)
        {
            return string.Empty;
        }

        internal string WriteCode(List<CodeElement> codeElements, string ident)
        {
            string newLine = "\n<br>";
            string tabulator = "\t&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            string endClass = "</span>";
            string classParameterType = "<span class='parameterType'>";
            string classType = "<span class='returnType'>";
            string classIdentifier = "<span class='identifier'>";
            string classExtended = "<span class='extended'>";
            string classControl = "<span class='control'>";
            string classModitier = "<span class='modifier'>";
            string classParameter = "<span class='parameter'>";
            StringBuilder stringBuilder = new StringBuilder();
            if (codeElements == null) return "";
            foreach (CodeElement codeElement in codeElements)
            {
                switch (codeElement.ReturnType)
                {
                    case "namespace":
                        string tab = ident + "";
                        stringBuilder.Append(
                            newLine +
                            tab + classType + codeElement.ReturnType + endClass + " " + classIdentifier +
                            codeElement.Name + endClass + newLine +
                            tab + "{" + newLine);
                        stringBuilder.Append(WriteCode(codeElement.Childs, tab));
                        stringBuilder.Append(
                            tab + "}" + newLine
                            );
                        break;
                    case "class":
                        string modifiers = string.Empty;
                        string extend = "";
                        foreach (string modifier in codeElement.Modifiers)
                        {
                            modifiers += modifier + " ";
                        }
                        if (!string.IsNullOrEmpty(codeElement.Extend))
                        {
                            extend = ": " + codeElement.Extend;
                        }
                        tab = ident + tabulator;
                        stringBuilder.Append(
                            newLine +
                            tab + classModitier + modifiers + endClass + " " + classType + codeElement.ReturnType +
                            endClass + " " + classIdentifier + codeElement.Name + endClass + extend + newLine +
                            tab + "{" + newLine);
                        stringBuilder.Append(WriteCode(codeElement.Childs, tab));
                        stringBuilder.Append(
                            tab + "}" + newLine
                            );
                        break;
                    case "eventHandler":
                        modifiers = string.Empty;
                        foreach (string modifier in codeElement.Modifiers)
                        {
                            modifiers += modifier + " ";
                        }
                        string parameters = string.Empty;
                        if (codeElement.Parameters != null)
                            foreach (string parameter in codeElement.Parameters)
                            {
                                if (parameter.Contains("obje"))
                                {
                                }
                                string parameterTimmed = parameter.Trim();
                                int identifierindex = parameterTimmed.LastIndexOf(' ');
                                if (identifierindex < 1)
                                {
                                    // ToDo debug
                                    continue;
                                }
                                string parameterType = parameterTimmed.Substring(0, identifierindex);
                                string parameterIdentifier = parameterTimmed.Substring(identifierindex + 1);
                                parameters += ", " + classParameterType + parameterType + endClass + " " +
                                              classParameter + parameterIdentifier + endClass + "";
                            }
                        if (!String.IsNullOrEmpty(parameters))
                        {
                            parameters = parameters.Substring(2);
                        }
                        tab = ident + tabulator;
                        stringBuilder.Append(
                            newLine + // go on the same line
                            tab + classModitier + modifiers + endClass + " " + classType + "void" + endClass + " " +
                            classIdentifier + codeElement.Name + endClass + "(" + parameters + ")" + newLine +
                            tab + "{" + newLine);
                        //stringBuilder.Append(WriteCode(codeElement.Childs, tab));
                        stringBuilder.Append(
                            tab + "}" + newLine
                            );
                        break;
                    case "Undefined":
                        tab = ident + "";
                        stringBuilder.Append(WriteCode(codeElement.Childs, tab));
                        break;
                    default:
                        modifiers = string.Empty;
                        foreach (string modifier in codeElement.Modifiers)
                        {
                            modifiers += modifier + " ";
                        }
                        parameters = string.Empty;
                        if (codeElement.Parameters != null)
                            foreach (string parameter in codeElement.Parameters)
                            {
                                string parameterTimmed = parameter.Trim();
                                int identifierindex = parameterTimmed.LastIndexOf(' ');
                                if (identifierindex < 1)
                                {
                                    // ToDo debug
                                    continue;
                                }
                                string parameterType = parameterTimmed.Substring(0, identifierindex);
                                string parameterIdentifier = parameterTimmed.Substring(identifierindex + 1);
                                parameters += ", " + classParameterType + parameterType + endClass + " " +
                                              classParameter + parameterIdentifier + endClass + "";
                            }
                        if (!String.IsNullOrEmpty(parameters))
                        {
                            parameters = parameters.Substring(2);
                        }
                        tab = ident + tabulator;
                        stringBuilder.Append(
                            newLine + // go on the same line
                            tab + classModitier + modifiers + endClass + " " + classType + codeElement.ReturnType +
                            endClass + " " + classIdentifier + codeElement.Name + endClass + "(" + parameters + ")" +
                            newLine +
                            tab + "{" + newLine);

                        foreach (CodeRelation codeRelation in codeElement.Properties)
                        {
                            foreach (Arrow arrow in codeRelation.Childs)
                            {
                                switch (arrow.Multiplicity)
                                {
                                    case (int) CodeRelation.ReletionMultiplicity.One:
                                        stringBuilder.Append(
                                            tab + tab + classType + arrow.CodeElement.Name + endClass + " " +
                                            arrow.CodeElement.Name.ToLower() +
                                            " = " + classControl + "new" + endClass + " " + classType +
                                            arrow.CodeElement.Name + endClass + "();" + newLine
                                            );
                                        break;
                                    case (int) CodeRelation.ReletionMultiplicity.Some:
                                        string greatherThen = "&gt;";
                                        string lessThen = "&lt;";
                                        stringBuilder.Append(
                                            tab + tab + classType + "List" + endClass + lessThen + classType +
                                            arrow.CodeElement.Name + endClass + greatherThen + " " +
                                            arrow.CodeElement.Name.ToLower() + "s" +
                                            " = " + classControl + "new" + endClass + " " + classType + "List" +
                                            endClass + lessThen + classType + arrow.CodeElement.Name + greatherThen +
                                            endClass + "();" + newLine
                                            );
                                        break;
                                    case (int) CodeRelation.ReletionMultiplicity.No:
                                        stringBuilder.Append(
                                            tab + tab + arrow.CodeElement.Name + " " + arrow.CodeElement.Name.ToLower() +
                                            " = " + classControl + "new" + endClass + " " + arrow.CodeElement.Name +
                                            "();" + newLine
                                            );
                                        break;
                                }
                            }

                        }

                        //stringBuilder.Append(WriteCode(codeElement.Childs, tab));
                        stringBuilder.Append(
                            tab + "}" + newLine
                            );
                        break;
                }
                //stringBuilder.Append(codeElement.Name);
            }
            return stringBuilder.ToString();
        }
    }
}
