/* 
the //following
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1
{
        enum CommentType { toEnd, multiline };
        enum Level { NamespaceLevel, ClassLevel };

    class CodeAccess
    {
        Level codeLevel; // block type
        string myNamespace; // current namespace name
        int NamespaceID; // namespace id
        string VisibilityModifier; // type of visibility
        bool StaticModifier; // is static modifier
        string IdentifierType; // the type of the identifier
        bool TypeFound; // type of the identifier was found
        string IdentifierName; // name of identifier
        bool IdentifierFound; // identifier was found
        string myClass; // current class name
        int ClassID; // id of the current class
        bool alowSpace; // is space allowd avoid unnecessary spaces
        private string filePath; // current file path

        /// <summary>
        /// file path of the current inspected source file
        /// </summary>
        public string FilePath 
        {
            get { return filePath; }
            set { filePath = value; }
        }


        /// <summary>
        /// If line is still partialy in block comment
        /// </summary>
        private bool inComment = false; CommentType commentType;
        private bool inString = false;

        private string GetDeclaration(string line)
        {
            line = line.Trim();

            //reset flags
            VisibilityModifier = string.Empty;
            StaticModifier = false;
            IdentifierFound = false;
            TypeFound = false;
            int possibleModifiersNumber = 2;

            string[] lineArray = line.Split();
            for (int i = 0; i < lineArray.Length; i++)
            {
                while (possibleModifiersNumber > 0)
                {
                    switch (lineArray[i])
                    {
                        case "public":
                            {
                                VisibilityModifier = lineArray[i];
                                possibleModifiersNumber--; i++;
                            }
                            break;
                        case "protected":
                            {
                                VisibilityModifier = lineArray[i];
                                possibleModifiersNumber--; i++;
                            }
                            break;
                        case "private":
                            {
                                VisibilityModifier = lineArray[i];
                                possibleModifiersNumber--; i++;
                            }
                            break;
                        case "static":
                            {
                                StaticModifier = true;
                                possibleModifiersNumber--; i++;
                            }
                            break;
                    }
                }
                if (TypeFound == false) IdentifierType = lineArray[i]; // for functions, this is the return type
                if ((TypeFound == true) && (IdentifierFound == false)) IdentifierName = lineArray[i]; // identifier
            }
            return line;
        }
        List<string> kg;

        /// <summary>
        /// Return not commented content of a line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ReformatSource(string line)
        {
            bool skipThis = false;
            bool breakAfter = false;
            int level = 0;
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case ' ':
                        if ((!inComment) && (!inString))
                        {
                            if (!alowSpace)
                            {
                                skipThis = true;
                            }
                            else
                            {
                                alowSpace = false;
                            }
                        }
                        break;
                    case '{': alowSpace = false;
                        if ((!inComment) && (!inString))
                        {
                            if ((i > 0) && (line[i - 1]) == '\'') break;
                            breakAfter = true;
                        }
                        break;
                    case ';': alowSpace = false;
                        if ((!inComment) && (!inString))
                        {
                            if ((i > 0) && (line[i - 1]) == '\'') break;
                            breakAfter = true;
                        }
                        break;
                    case '}': alowSpace = false;
                        if ((!inComment) && (!inString))
                        {
                            if ((i > 0) && (line[i - 1]) == '\'') break;
                            breakAfter = true;
                        }
                        break;
                    case '/':
                        if ((!inComment) && (!inString))
                        {
                            if (i < line.Length - 1)
                            {
                                switch (line[i + 1])
                                {
                                    case '/':
                                        inComment = true;
                                        commentType = CommentType.toEnd;
                                        break;
                                    case '*':
                                        inComment = true;
                                        commentType = CommentType.multiline;
                                        break;
                                }
                            }
                        }
                        if (inComment && (commentType == CommentType.multiline))
                        {
                            if (i > 0)
                            {
                                if (line[i - 1] == '*')
                                {
                                    skipThis = true;
                                    commentType = CommentType.toEnd; //just for safe
                                    inComment = false;
                                    break;
                                }
                            }
                        }
                        break;
/*                    case '*':
                        if (inComment && (commentType == CommentType.multiline)) // never can be in both comment and string
                        {
                            if (i < line.Length - 1)
                            {
                                switch (line[i + 1])
                                {
                                    case '/':
                                        commentType = CommentType.toEnd; //just for safe
                                        inComment = false;
                                        break;
                                }
                            }
                        }
                        break;*/
                    case '"': 
                        if (! inComment)
                        {
                            if (i > 0)
                            {
                                switch (line[i-1])
                                {
                                    case '@':
                                        if (!inString)
                                        {
                                            inString = true;
                                            commentType = CommentType.multiline;
                                        }
                                        break;
                                    case '\\':
                                        if (inString && (commentType == CommentType.multiline))
                                        {
                                            skipThis = true;
                                            inString = false;
                                            commentType = CommentType.toEnd;//just for safe
                                        }
                                        break;
                                    case '"':
                                        if (inString && (commentType == CommentType.multiline))
                                        {
                                            if ((i > 0) && (line[i-1] == '"')) break;
                                            if ((i < line.Length) && (line[i+1] == '"')) break;
                                        }
                                        inString = !inString;
                                        if (!inString) skipThis = true;
                                        commentType = CommentType.toEnd;//just for safe
                                        break;
                                    case '\'':
                                        break;
                                    default:
                                        if (inString && (commentType == CommentType.multiline))
                                        {
                                            if (i < line.Length - 1)
                                            {
                                                if (line[i + 1] == '"') break;
                                            }
                                        }
                                        else
                                        {
                                            inString = !inString;
                                            if (!inString) skipThis = true;
                                            commentType = CommentType.toEnd;//just for safe 
                                            //inString = false;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    default: 
                        alowSpace = true; 
                        break;
                }
                if ((!inComment) && (!inString))
                {
                    if (skipThis)
                    {
                        skipThis = false;
                    }
                    else
                    {
                        Console.Write(line[i]);
                        if (breakAfter)
                        {
                            //alowSpace = false;
                            Console.Write("\n");
                            breakAfter = false;
                        }
                    }
                }
            }
            return line;
        }


        private string SubstractComments2(string line)
        {
            int codeUntil = -1;
            if (!inComment)
            {
                codeUntil = line.IndexOf("//");
                if ((line.IndexOf("/*") > -1) && (line.IndexOf("/*") < codeUntil))
                {
                    codeUntil = line.IndexOf("/*");
                    inComment = true;
                }
            }

            if (inComment)
            {
                if ((line.IndexOf("/*") > -1) && (line.IndexOf("/*") < line.IndexOf("*/")))
                    line = line.Substring(0, line.IndexOf("/*")) + line.Substring(line.IndexOf("*/") + 2);
                else line = line.Substring(line.IndexOf("*/") + 2);
                inComment = false;
                line = ReformatSource(line);
            }
            else
            {
                if (codeUntil > -1)
                    line = line.Substring(0, codeUntil);
            }
            return line;
        }

        /// <summary>
        /// Get All public declarations in a line
        /// </summary>
        /// <param name="filePath"></param>
        public void GetPublicDeclarations(string filePath)
        {
            string acodeLevel = "Namespace";
            myNamespace = "*PublicNamespace*";
            FileInfo currentFile = new FileInfo(filePath);
            StreamReader reader = currentFile.OpenText();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (commentType == CommentType.toEnd)
                {
                    inComment = false; inString = false; alowSpace = false;
                }
                line = ReformatSource(line).Trim();
                if (!string.IsNullOrEmpty(line))
                ExtractPublicDeclarations(line);
                //if (line.Contains("public "))
                    //Console.WriteLine(line);
                //Match m = Regex.Match(@"^\d+\t(\d+)\t.+?\t(item\\[^\t]+\.ddj)");
                //if (m.Success)
                {
                    //int myInt = int.Parse(m.Group(1).Value);
                    //string path = m.Group(2).Value;

                    // At this point, `myInteger` and `path` contain the values we want
                    // for the current line. We can then store those values or print them,
                    // or anything else we like.
                }
            }
        }

        /// <summary>
        /// Extract identifier
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string GetIdentifier(string line)
        {
            int end = line.Length;
            int index;
            if (((index = line.IndexOf(" ")) > -1) && (index < end))
            {
                end = index;
            }
            if (((index = line.IndexOf("(")) > -1) && (index < end))
            {
                end = index;
            }
            if (((index = line.IndexOf(":")) > -1) && (index < end))
            {
                end = index;
            }
            if (((index = line.IndexOf("[")) > -1) && (index < end))
            {
                end = index;
            }
            return line.Substring(0, end);
        }
        private void ExtractPublicDeclarations(string line)
        {/*
            if (line.StartsWith("namespace ")) 
            {
                myNamespace = line.Substring(10);
                Symbol simbol = new Symbol();
                simbol.Name = myNamespace;
                NamespaceID = simbol.RegisterSymbolIfNotExists();
                //Console.WriteLine(myNamespace);
            }
            if (line.StartsWith("class "))
            {
                myClass = GetIdentifier(line.Substring(6).Trim());
                Symbol simbol = new Symbol();
                simbol.Name = myClass;
                ClassID = simbol.RegisterSymbolIfNotExists();
                Relation relation = new Relation();
                relation.Name = "contain";
                relation.CreateRelationIfNotExists(NamespaceID, ClassID);
                //Console.WriteLine("\t" + myClass);
            }
            if (line.StartsWith("public class "))
            {
                myClass = GetIdentifier(line.Substring(13).Trim());
                Symbol simbol = new Symbol();
                simbol.Name = myClass;
                ClassID = simbol.RegisterSymbolIfNotExists();
                Relation relation = new Relation();
                relation.Name = "contain";
                relation.CreateRelationIfNotExists(NamespaceID, ClassID);
                //Console.WriteLine("\t" + myClass);
            }
            if (line.StartsWith("static class "))
            {
                myClass = GetIdentifier(line.Substring(13).Trim());
                Symbol simbol = new Symbol();
                simbol.Name = myClass;
                ClassID = simbol.RegisterSymbolIfNotExists();
                Relation relation = new Relation();
                relation.Name = "contain";
                relation.CreateRelationIfNotExists(NamespaceID, ClassID);
                //Console.WriteLine("\t" + myClass);
            }
            if (line.StartsWith("public partial class "))
            {
                myClass = GetIdentifier(line.Substring(21).Trim());
                Symbol simbol = new Symbol();
                simbol.Name = myClass;
                ClassID = simbol.RegisterSymbolIfNotExists();
                Relation relation = new Relation();
                relation.Name = "contain";
                relation.CreateRelationIfNotExists(NamespaceID, ClassID);
                //Console.WriteLine("\t" + myClass);
            }
            if (line.StartsWith("partial class "))
            {
                myClass = GetIdentifier(line.Substring(14).Trim());
                Symbol simbol = new Symbol();
                simbol.Name = myClass;
                ClassID = simbol.RegisterSymbolIfNotExists();
                Relation relation = new Relation();
                relation.Name = "contain";
                relation.CreateRelationIfNotExists(NamespaceID, ClassID);
                //Console.WriteLine("\t" + myClass);
            }*/
        }
    }
}
