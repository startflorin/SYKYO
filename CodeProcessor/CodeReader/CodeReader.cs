using System;
using System.Collections.Generic;
using System.Linq;
using CodeProcessor.Properties;
using DataModel.DL.CodeEntity;

namespace CodeProcessor
{
    /// <summary>
    /// 
    /// </summary>
    class CodeReader
    {
        readonly List<CodeSection> codeSections = new List<CodeSection>();
        readonly List<CodeElement> codeElements = new List<CodeElement>();
        readonly List<string> fileContents = new List<string>();

        public List<CodeElement> ReadCodeFile(string rootPath)
        {
            var access = new FileAccess();
            List<string> filePaths = access.GetFilePathList(rootPath);
            if (filePaths != null)
            {
                access.GetFileList(filePaths);
                foreach(string filePath in filePaths)
                {
                    if (!filePath.Contains("CodeReader"))
                    {
                        //continue;
                    }
                    string fileContent = access.GetFileContent(filePath).ToString();
                    fileContents.Add(fileContent);
                    CodeSection codeSection = new CodeSection(filePath, fileContent, 0, 0);
                    codeSections.Add(codeSection);
                }
            }
            foreach (CodeSection codeSection in codeSections)
            {
                GetIdentifier(codeSection);
                codeElements.Add(ConvertToDataModel(codeSection));
            }
            return codeElements;
        }

        public CodeElement ConvertToDataModel(CodeSection codeSection)
        {
            CodeElement currentElement = null;
            codeSection.BlockType = codeSection.ReturnType;
            switch (codeSection.ReturnType)
            {
                case "namespace":
                    currentElement = new ElementNamespace();
                    break;
                case "class":
                    currentElement = new ElementClass();
                    break;
                default:
                    currentElement = new ElementMethod();
                    break;
            }
            currentElement.BlockType = codeSection.BlockType;
            currentElement.Name = codeSection.Identifier;
            currentElement.ReturnType = codeSection.ReturnType;
            currentElement.Parameters = codeSection.Parameters;
            currentElement.Extend = codeSection.Extend;
            if (codeSection.Header.Contains("("))
            {
                if (codeSection.Identifier.Contains("_"))
                {
                    currentElement.ReturnType = "eventHandler";
                }
                else
                {
                    //currentElement.ReturnType = "method";
                }
            }
            currentElement.Modifiers = codeSection.Modifiers;
            currentElement.Visibility = codeSection.Visibility;
            if (codeSection.CodeSections.Count > 0)
            {
                currentElement.Childs = new List<CodeElement>();
            }
            foreach (CodeSection codeSectionChild in codeSection.CodeSections)
            {
                CodeElement child = ConvertToDataModel(codeSectionChild);
                if (!string.IsNullOrEmpty(child.Name))
                {
                    currentElement.Childs.Add(child);
                }
            }
            return currentElement;
        }

        private void GetIdentifier(CodeSection codeSection)
        {
            var modifiers = new List<string>();
            var parameters = new List<string>();
            string modifiersString = string.Empty;
            string retutnType = "";
            string identifier = "";
            string extend = "";
            string parametersString = "";
            if (!string.IsNullOrEmpty(codeSection.Header))
            {
                string header = codeSection.Header;

                // Find the header:
                int headerIndex = 0;
                char lastChar = '\0';
                bool ignore = false;
                int i = 0;
                foreach (char c in header)
                {
                    i++;
                    if (!ignore)
                    {
                        if (c == '#')
                        {
                            ignore = true;
                        }
                        if (c == '/')
                        {
                            if (lastChar == '/')
                            {
                                ignore = true;
                            }
                        }
                        if (c == '[')
                        {
                            ignore = true;
                        }
                    }
                    else
                    {
                        if (c == '\n')
                        {
                            if (lastChar == '\r')
                            {
                                ignore = false;
                                headerIndex = i;
                            }
                        }
                    }

                    lastChar = c;
                }
                string t = header;
                //header = t;
                header = header.Substring(headerIndex).Trim();
                int indexOfImplement = header.IndexOf(':');
                if (indexOfImplement > 0)
                {
                    extend = header.Substring(indexOfImplement+1);
                    header = header.Substring(0, indexOfImplement).Trim();
                }

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
                        parameters = parametersString.Trim().Substring(1, parametersString.Length - 2).Split(',').ToList();
                        parametersString = "(.";
                        foreach (string parameter in parameters)
                        {
                            parametersString += parameters + ",";
                        }
                        parametersString += ".)";
                        
                    }
                    if (modifiers.Contains("private"))
                    {
                        codeSection.Visibility = (int)CodeSection.Visible.Private;
                    }
                    if (modifiers.Contains("protected"))
                    {
                        codeSection.Visibility = (int)CodeSection.Visible.Protected;
                    }
                    if (modifiers.Contains("public"))
                    {
                        codeSection.Visibility = (int)CodeSection.Visible.Public;
                    }

                    //codeSection. = identifier;
                    codeSection.Identifier = identifier;
                    codeSection.Modifiers = modifiers;
                    codeSection.Parameters = parameters;
                    codeSection.ReturnType = retutnType;
                    codeSection.Extend = extend;
                }
            }
            foreach (CodeSection codeSectionChild in codeSection.CodeSections)
            {
                GetIdentifier(codeSectionChild);
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
            for (int i = beginSection; (i + lastCodeEndedAt) < fileContent.Length; i++)
            {
                if (IsNonExecutable(fileContent[i]))
                {
                    continue;
                }
                if (fileContent[i] == ';')
                {
                    branchContainExpression = true;
                    string code = fileContent.Substring(lastCodeEndedAt, i + 1);
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
                    ChildCodeSections.AddRange(GetAllCodeElements(filePath, fileContent, i + 1, level));
                    if (ChildCodeSections != null && ChildCodeSections.Count > 0)
                    {
                        i = ChildCodeSections[ChildCodeSections.Count - 1].EndSection;
                        branchContainExpression = true;
                    }
                    acceptBracket = true;
                }
                char o = fileContent[i];
            }

            return ChildCodeSections;
        }
    }

    class CodeSection
    {
        // The visibility of the identifier
        public enum Visible { Internal=0, Private=1, Protected=2, Public=3};
        public int Visibility { get; set; }

        // Extended
        public string Extend = "";
        
        // The name of the identifier
        public string Identifier = "";

        // The type of the code
        public string BlockType;

        // The return type of the identifier
        public string ReturnType
        {
            get;
            set;
        }

        public int Complexity { get; set; }

        public List<string> Modifiers = new List<string>();
        public List<string> Parameters = new List<string>();
            
        public string Header = string.Empty;
        readonly int beginSection;
        int endSection;
        private string filePath = string.Empty;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public string CodeContent = "";
        public List<CodeSection> CodeSections = new List<CodeSection>();
        private string fileContent;

        public int EndSection
        {
            set
            {
                endSection = value;
                CodeContent = fileContent.Substring(beginSection, endSection - beginSection).Trim();
            }
            get
            {
                return endSection;
            }
        }



        public CodeSection(string filePath, string fileContent, int beginSection, int level)
        {
            Visibility = (int) Visible.Internal;
            FilePath = filePath;
            this.beginSection = beginSection;
            //this.endSection = endSection;
            //this.codeContent = fileContent.Substring(beginSection, endSection - beginSection).Trim();
            CodeReader codeReader = new CodeReader();
            CodeSections = codeReader.GetAllCodeElements(filePath, fileContent, beginSection, level+1);
            ReturnType = "Undefined";
        }

        public CodeSection(string filePath, string fileContent, int beginSection, int endSection, int level)
        {
            Visibility = (int)Visible.Internal;
            FilePath = filePath;
            this.beginSection = beginSection;
            this.endSection = endSection;
            CodeContent = fileContent.Substring(beginSection, endSection - beginSection).Trim();
            //Console.WriteLine(codeContent);
            //this.codeSections = CodeReader.GetAllCodeElements(filePath, fileContent, beginSection);
            ReturnType = "Undefined";
        }
    }
}
