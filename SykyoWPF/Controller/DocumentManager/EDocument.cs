using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.Collection;
using DataPersistency.DL.ServerAccess;
using System.IO;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;

namespace WpfApplication1.Controller.DocumentManager
{
    public class EDocumentConvertor
    {

        /// <summary>
        /// The root of the generated application in the filesystem
        /// </summary>
        string applicationRoot = @"..\..\..\GeneratedDataModel\Model";

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
            catch(Exception e){}
        }

        /// <summary>
        /// generate filesystem of the application
        /// </summary>
        private void GenerateCode()
        {
            foreach (CodeFile codeFile in program.CodeFiles)
            {
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
                                if (!codeProperty.Name.Equals("..P"))
                                    sw.WriteLine(codeProperty.ToBlock());
                            }

                            foreach (ElementMethod codeMethod in classItem.ElementMethods)
                            {
                                if (!codeMethod.Name.Equals(".."))
                                    if(!codeMethod.IsStatic)
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
                                if (!codeProperty.Name.Equals("..P") && codeProperty.IsMandatory)
                                    sw.WriteLine("        public bool isNullable" + codeProperty.Name + "P = false;");
                            }

                            
                            sw.WriteLine("");
                            sw.WriteLine("        public bool IsValid()");
                            sw.WriteLine("        {");
                            sw.WriteLine("            bool result = true;");

                            foreach (ElementProperty codeProperty in classItem.ElementProperties)
                            {
                                if (!codeProperty.Name.Equals("..P") && codeProperty.IsMandatory)
                                {
                                    sw.WriteLine("            if ("+codeProperty.Name.ToLower()+"P == null)");
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
                                if (codeMethod.Name.StartsWith("Increasedby"))
                                {
                                    sw.WriteLine(codeMethod.ToBlock());
                                }else
                                if (codeMethod.Name.StartsWith("DecreasedBy"))
                                {
                                    sw.WriteLine(codeMethod.ToBlock());
                                }else
                                if (codeMethod.Name.StartsWith("Increase"))
                                {
                                    sw.WriteLine(codeMethod.ToBlock());
                                }else
                                if (codeMethod.Name.StartsWith("Decrease"))
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
        /// <summary>
        /// generate persistency of the application
        /// </summary>
        private void GeneratePersistency()
        {
            string programDatabeseName = "System";

            DataPersistency.BL.Syntax.SyntaxInterface oracleSyntax = new DataPersistency.BL.Syntax.SyntaxMySQL();

            foreach (CodeFile codeFile in program.CodeFiles)
            {
                using (StreamWriter sw = new StreamWriter(applicationRoot + "\\" + codeFile.FileName + "TableAdaptor.cs", true))
                {
                    foreach (ElementNamespace namespaceItem in codeFile.Namespaces)
                    {
                        sw.WriteLine("using System.Collections.Generic;" + Environment.NewLine);
                        sw.WriteLine("namespace " + namespaceItem.Name + "");
                        sw.WriteLine("{");
                        foreach (ElementClass classItem in namespaceItem.ElementClasses)
                        {
                            sw.WriteLine("  public class " + classItem.Name + "");
                            sw.WriteLine("  {");
                           // sw.WriteLine(oracleSyntax.GenerateMethodCreateTable(classItem.Name, programDatabeseName));
                           // sw.WriteLine(oracleSyntax.GenerateMethodDropTable(classItem.Name, programDatabeseName));
                            sw.WriteLine("  }");
                        }
                        sw.WriteLine("}");
                    }
                }
            }
        }

        ElementProgram program;
        
        public EDocumentConvertor()
        {
            program = new ElementProgram();
            program.programName = "AutogeneratedProgram";
        }

        static bool convertToC = true;

        public void ToProgrammingLanguage(EDocument document)
        {
            ClearProject();
            Wrap(document.RootTocken);
            GenerateCode();
            //GeneratePersistency();
        }

        private void Wrap(SToken token)
        {

            while (token != null)
            {
                //if (token.TokenType == SElementType.Symbol)
                {

                    CodeFile newCodeFile = new CodeFile();
                    ElementNamespace namespaceItem = new ElementNamespace();
                    ElementClass classItem = new ElementClass();

                    bool isNeededCodeFileCreation = true;
                    foreach (CodeFile existentCodeFile in program.CodeFiles)
                    {
                        if (existentCodeFile.FileName == ToUp(token.TokenString))
                        {
                            isNeededCodeFileCreation = false;
                            newCodeFile = existentCodeFile;
                        }
                    }
                    if (isNeededCodeFileCreation)
                    {
                        newCodeFile.FileName = ToUp(token.TokenString);
                        program.CodeFiles.Add(newCodeFile);
                        namespaceItem.Name = "GeneratedApplication";
                        newCodeFile.Namespaces.Add(namespaceItem);
                        classItem.Name = ToUp(token.TokenString);
                        namespaceItem.ElementClasses.Add(classItem);
                    }


                    if (token.TokenProperties != null)
                    {
                        SToken tokenToRead = token;
                         if (token.TokenProperties[0].TokenNodeType == SElementType.FoldOpen)
                          {
                             foreach (SToken localTokenF in token.TokenProperties[0].TokenChilds)
                              {
                                 SToken localToken = localTokenF;
                                 while (localToken != null)
                                 {
                                     Wrap(localToken);
                                     ElementProperty newProperty = new ElementProperty();
                                     switch (localToken.TokenMultiplicity)
                                     {
                                         case SElementMultiplicity.Some:
                                             newProperty.Name = ToUp(localToken.TokenString) + "s";
                                             newProperty.PropertyType = "List<" + ToUp(localToken.TokenString) + ">";
                                             break;
                                         case SElementMultiplicity.Explicit:
                                             newProperty.Name = ToUp(localToken.TokenString) + "s";
                                             newProperty.PropertyType = "" + ToUp(localToken.TokenString) + " " + token.TokenMultiplicityLevel + "";
                                             break;
                                         default:
                                             newProperty.Name = ToUp(localToken.TokenString) + "P";
                                             newProperty.PropertyType = ToUp(localToken.TokenString);
                                             break;
                                     }
                                     //newProperty.PropertyType = localToken.TokenString;
                                     newProperty.ElementClassName = ToUp(localToken.TokenString);
                                     classItem.ElementProperties.Add(newProperty);
                                     if (localToken.TokenAfter != null)
                                     {
                                         localToken = localToken.TokenAfter[0];
                                     }
                                     else
                                     {
                                         localToken = null;
                                     }
                                 }
                             }
                         }
                         else
                         foreach (SToken localToken in token.TokenProperties)
                        {

                            Wrap(localToken);
                            ElementProperty newProperty = new ElementProperty();
                            switch (localToken.TokenMultiplicity)
                            {
                                case SElementMultiplicity.Some:
                                    newProperty.Name = ToUp(localToken.TokenString) + "s";
                                    newProperty.PropertyType = "List<" + ToUp(localToken.TokenString) + ">";
                                    break;
                                case SElementMultiplicity.Explicit:
                                    newProperty.Name = ToUp(localToken.TokenString) + "s";
                                    newProperty.PropertyType = "" + ToUp(localToken.TokenString) + " " + token.TokenMultiplicityLevel + "";
                                    break;
                                default:
                                    newProperty.Name = ToUp(localToken.TokenString) + "P";
                                    newProperty.PropertyType = ToUp(localToken.TokenString);
                                    break;
                            }
                            //newProperty.PropertyType = localToken.TokenString;
                            newProperty.ElementClassName = ToUp(localToken.TokenString);
                            classItem.ElementProperties.Add(newProperty);
                        }
                    }
                    if (token.TokenActivities != null)
                     {
                        foreach (SToken localToken in token.TokenActivities)
                        {
                            Wrap(localToken);
                            ElementMethod newMethod = new ElementMethod();

                            newMethod.Name = ToUp(localToken.TokenString);
                            newMethod.ElementAccessType = ElementMethod.DataAccessType.isPublic;
                            
                            //newProperty.PropertyType = localToken.TokenString;
                            newMethod.ElementClassName = ToUp(localToken.TokenString);
                            classItem.ElementMethods.Add(newMethod);
                        }
                    }


                    List<string> influencesA = new List<string>();
                    List<string> influencesB = new List<string>();
                    List<string> influencesC = new List<string>();
                    List<string> influencesD = new List<string>();
                        
                    if (token.TokenInfluenceA != null)
                    {

                        foreach (SToken localToken in token.TokenInfluenceA)
                        {
                            /*{
                                foreach (SToken tokenA in token.TokenInfluenceA)
                                {
                                    if (token.TokenString.Equals(tokenA))
                                    {
                                        System.Windows.Forms.MessageBox.Show("No need to repeat yourself");
                                        break;
                                    }
                                }
                            }*/
                            if (!influencesA.Contains(localToken.TokenString))
                            {
                                influencesA.Add(localToken.TokenString);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No need to repeat yourself!");
                                break;
                            }
                            if (influencesB.Contains(localToken.TokenString))
                            {
                                 System.Windows.Forms.MessageBox.Show("Your statement is inconsistent!");
                                break;
                            }
                            if (influencesD.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("You defined a rebound effect!");
                                break;
                            }
                            
                            //Wrap(localToken);
                            ElementMethod newMethod = new ElementMethod();

                            newMethod.Name = ToUp(localToken.TokenString);
                            newMethod.ElementAccessType = ElementMethod.DataAccessType.isPublic;
                            newMethod.IsStatic = true;
                            //newProperty.PropertyType = localToken.TokenString;
                            newMethod.Name = "Increase" + ToUp(localToken.TokenString);
                            classItem.ElementMethods.Add(newMethod);
                        }
                    }

                    if (token.TokenInfluenceB != null)
                    {
                        foreach (SToken localToken in token.TokenInfluenceB)
                        {
                            if (!influencesB.Contains(localToken.TokenString))
                            {
                                influencesB.Add(localToken.TokenString);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No need to repeat yourself!");
                                break;
                            }
                            if (influencesA.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("Your statement is inconsistent!");
                                break;
                            }
                            if (influencesC.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("You defined a rebound effect!");
                                break;
                            }
                            //Wrap(localToken);
                            ElementMethod newMethod = new ElementMethod();

                            newMethod.Name = ToUp(localToken.TokenString);
                            newMethod.ElementAccessType = ElementMethod.DataAccessType.isPublic;
                            newMethod.IsStatic = true;
                            
                            //newProperty.PropertyType = localToken.TokenString;
                            newMethod.Name = "Decrease" + ToUp(localToken.TokenString);
                            classItem.ElementMethods.Add(newMethod);
                        }
                    }

                    if (token.TokenInfluenceC != null)
                    {
                        foreach (SToken localToken in token.TokenInfluenceC)
                        {
                            if (!influencesC.Contains(localToken.TokenString))
                            {
                                influencesC.Add(localToken.TokenString);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No need to repeat yourself!");
                                break;
                            }
                            if (influencesD.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("Your statement is inconsistent!");
                                break;
                            }
                            if (influencesB.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("You defined a rebound effect!");
                                break;
                            }
                            Wrap(localToken);
                            ElementMethod newMethod = new ElementMethod();

                            newMethod.Name = ToUp(localToken.TokenString);
                            newMethod.ElementAccessType = ElementMethod.DataAccessType.isPublic;
                            newMethod.IsStatic = true;
                            
                            //newProperty.PropertyType = localToken.TokenString;
                            newMethod.Name = "IncreasedBy" + ToUp(localToken.TokenString);
                            classItem.ElementMethods.Add(newMethod);
                        }
                    }

                    if (token.TokenInfluenceD != null)
                    {
                        foreach (SToken localToken in token.TokenInfluenceD)
                        {
                            if (!influencesD.Contains(localToken.TokenString))
                            {
                                influencesD.Add(localToken.TokenString);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No need to repeat yourself!");
                                break;
                            }
                            if (influencesC.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("Your statement is inconsistent!");
                                break;
                            }
                            if (influencesA.Contains(localToken.TokenString))
                            {
                                System.Windows.Forms.MessageBox.Show("You defined a rebound effect!");
                                break;
                            }
                            Wrap(localToken);
                            ElementMethod newMethod = new ElementMethod();

                            newMethod.Name = ToUp(localToken.TokenString);
                            newMethod.ElementAccessType = ElementMethod.DataAccessType.isPublic;
                            newMethod.IsStatic = true;
                            
                            //newProperty.PropertyType = localToken.TokenString;
                            newMethod.Name = "DecreasedBy" + ToUp(localToken.TokenString);
                            classItem.ElementMethods.Add(newMethod);
                        }
                    }

                }
                foreach (SToken tokenlist in token.TokenChilds)
                {
                    Wrap(tokenlist);
                }
                if (token.TokenAfter != null)
                {
                    token = token.TokenAfter[0];
                }
                else
                {
                    token = null;
                }
            }
        }

        private string ToUp(string identifier)
        {
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }

        private void AddPropertyToClass(SToken token, SToken localToken, SElementMultiplicity sElementMultiplicity)
        {
            throw new NotImplementedException();
        }
    }

    public class EDocument 
    {
        private bool nextIsAPropertyFoTheCurrent = false;

        int ReplaceAt = 0;

        #region CONSTRUCTOR

        public EDocument(string documentText)
        {
            DocumentText = documentText;
            RootTocken = new SToken();
            SToken newTocken = new SToken();

            RootTocken.Configure(0, string.Empty + "Program", SElementType.Undefined, string.Empty + " * ");
            newTocken.Configure(0, string.Empty + "..", SElementType.Undefined, string.Empty);
            RootTocken.TokenNodeType = SElementType.Undefined;
            newTocken.TokenNodeType = SElementType.Undefined;

            RootTocken.Link(SToken.NULL, SToken.NULL, null, null);
            newTocken.Link(rootTocken, SToken.NULL, null, null);

            RootTocken.TokenChilds.Add(newTocken);
            parentToken = RootTocken;
            currentToken = RootTocken.TokenChilds[0];

            InitializePersons();            
        }

        private void InitializePersons()
        {
            tokenI = new SToken();
            tokenYou = new SToken();
            tokenShe = new SToken();
            tokenHe = new SToken();
            tokenWe = new List<SToken>();
            tokenYous = new List<SToken>();
            tokenThey = new List<SToken>();

            tokenI.TokenString = "Florin";
            tokenYou.TokenString = "Irina";
            tokenShe.TokenString = "Irina";


            tokenI.TokenMultiplicity = SElementMultiplicity.One;
            tokenYou.TokenMultiplicity = SElementMultiplicity.One;
            tokenShe.TokenMultiplicity = SElementMultiplicity.One;
            tokenHe.TokenMultiplicity = SElementMultiplicity.One;


            tokenI.TokenType = SElementType.Symbol;
            tokenYou.TokenType = SElementType.Symbol;
            tokenShe.TokenType = SElementType.Symbol;
            tokenHe.TokenType = SElementType.Symbol;

            tokenWe.Add(tokenI);
            tokenWe.Add(tokenYou);
        }

        #endregion CONSTRUCTOR

        #region PROPERTIES

        #region ORIGINAL TEXT
        private string documentText;
        /// <summary>
        /// The text of the document
        /// </summary>
        public string DocumentText
        {
            get { return documentText; }
            set { documentText = value; }
        }
        #endregion ORIGINAL TEXT

        #region TOKENS
        private List<SToken> tokenCollection;
        /// <summary>
        /// The text of the document
        /// </summary>
        public List<SToken> TokenCollection
        {
            get { return tokenCollection; }
            set { tokenCollection = value; }
        }
        #endregion TOKENS

        #region PERSONS

        #region I

        private SToken tokenI;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SToken TokenI
        {
            get { return tokenI; }
            set { tokenI = value; }
        }

        #endregion I

        #region YOU

        private SToken tokenYou;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SToken TokenYou
        {
            get { return tokenYou; }
            set { tokenYou = value; }
        }

        #endregion YOU

        #region SHE

        private SToken tokenShe;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SToken TokenShe
        {
            get { return tokenShe; }
            set { tokenShe = value; }
        }

        #endregion SHE

        #region HE

        private SToken tokenHe;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SToken TokenHe
        {
            get { return tokenHe; }
            set { tokenHe = value; }
        }

        #endregion HE

        #region WE

        private List<SToken> tokenWe;
        /// <summary>
        /// The text of the document
        /// </summary>
        public List<SToken> TokenWe
        {
            get { return tokenWe; }
            set { tokenWe = value; }
        }

        #endregion WE

        #region YOUs

        private List<SToken> tokenYous;
        /// <summary>
        /// The text of the document
        /// </summary>
        public List<SToken> TokenYous
        {
            get { return tokenYous; }
            set { tokenYous = value; }
        }

        #endregion YOUs

        #region THEY

        private List<SToken> tokenThey;
        /// <summary>
        /// The text of the document
        /// </summary>
        public List<SToken> TokenThey
        {
            get { return tokenThey; }
            set { tokenThey = value; }
        }

        #endregion THEY

        #endregion PERSONS

        /// <summary>
        /// If is about the same object
        /// Cats are here. They can byte
        /// </summary>
        bool newEvaluation = true;

        bool isPreoperand = true;
        bool sticked = false;

        #region LAST / CURRENT / PARRENT / ROOT

        private SToken rootTocken;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SToken RootTocken
        {
            get { return rootTocken; }
            set { rootTocken = value; }
        }


        private List<SToken> knownTockens = new List<SToken>();
        /// <summary>
        /// The text of the document
        /// </summary>
        public List<SToken> KnownTockens
        {
            get { return knownTockens; }
            set { knownTockens = value; }
        }


        public SToken parentToken;
        /// <summary>
        /// 
        /// </summary>
        private SToken ParentTtoken
        {
            get { return parentToken; }
            set { parentToken = value; }
        }

        private SToken currentToken;
        private SToken CurrentToken
        {
            get { return currentToken; }
            set 
            {
                if (lastToken != currentToken && lastTokenCollection != currentToken && isPreoperand)
                {
                    if (currentToken.TokenMultiplicity == SElementMultiplicity.One)
                    {
                        lastToken = currentToken;
                    }
                    else
                    {
                        lastTokenCollection = currentToken;
                    }
                } 
                currentToken = value;
                
            }
        }

        private SToken lastToken;
        private SToken LastToken
        {
            get { return lastToken; }
            set { lastToken = value; }
        }

        private SToken lastTokenCollection;
        private SToken LastTokenCollection
        {
            get { return lastTokenCollection; }
            set { lastTokenCollection = value; }
        }

        #endregion LAST / CURRENT / PARRENT / ROOT

        #endregion PROPERTIES

        #region BUSINESS

        #region PERSON

        private void RegisterPerson()
        {
            switch (currentToken.TokenMultiplicity)
            {
                case SElementMultiplicity.One:
                    tokenHe = currentToken;
                    break;
                case SElementMultiplicity.Explicit:
                    tokenHe = currentToken;
                    break;
            }
        }

        #endregion PERSON

        public void EvaluateDocument()
        {

        }

        public void ParseAsNatural()
        {
            StringBuilder currentPhrase = new StringBuilder();
            bool handled = false;
            foreach (string c in documentText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                switch (c)
                {
                    case ".":
                        //currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        AddBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case "he":
                    case "she":
                    case "it":
                        handled = true;
                        currentToken = LastToken;
                        break;
                    case "increase": // Set influenc
                    case "decrease":
                    case "increasedBy": // Set influence
                    case "decreasedBy":
                        handled = true;
                        SetInfluence(currentToken, c);
                        currentPhrase = new StringBuilder();
                        break;
                    case "they":
                        handled = true;
                        newEvaluation = true;
                        CurrentToken = lastTokenCollection;
                        AppendToken(c);
                        //SetNext();
                        break;
                    case ",":
                        if (FoldNeeded)
                        {
                            CreateNewCollection();
                        }
                        else 
                        {
                            AppendToCollection();
                        }
                        break;
                    case "and":
                    case "or":
                         EscapeFromCollection(c);
                        //currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        //Append();
                        //AddToCollection(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        //AppendT(c);
                        break;
                    case "can": // Set a function
                         handled = true;
                         if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                        if (currentToken.TokenDescribed.TokenActivities == null)
                        {
                            currentToken.TokenDescribed.TokenActivities = new List<SToken>();
                        }
                        currentToken.TokenDescribed.TokenActivities.Add(currentToken);
                        currentToken.Link(currentToken.TokenParent, null, null, null);
                        currentPhrase = new StringBuilder();
                        isPreoperand = false;
                        break;
                    case "have": // Set a property
                        handled = true;
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                        if (currentToken.TokenDescribed.TokenProperties == null)
                        {
                            currentToken.TokenDescribed.TokenProperties = new List<SToken>();
                        }
                        currentToken.TokenDescribed.TokenProperties.Add(currentToken);
                        currentToken.TokenDescribed.TokenAfter = null;
                        currentToken.Link(currentToken.TokenParent, null, null, null);
                        currentPhrase = new StringBuilder();
                        nextIsAPropertyFoTheCurrent = true;
                        isPreoperand = false;
                        break;
                    case "a":
                    case "an":
                    case "some":
                    case "many":
                    case "all":
                    case "one":
                    case "no":
                    case "1":
                    case "0":
                        handled = true;
                        SetMultiplicityFlag(c);
                        currentPhrase = new StringBuilder();
                        break;
                    default:
                        // is it a multiplication factor?
                        int multiplicityLevel = 0;
                        int.TryParse(c, out multiplicityLevel);
                        if (multiplicityLevel != 0)
                        {
                            handled = true;
                            SetMultiplicityLevel(multiplicityLevel);
                            currentPhrase = new StringBuilder();
                            break;
                        }
                        handled = true;
                        string actualWord = c;
                        if (c.EndsWith("`s"))
                        {
                            nextIsAPropertyFoTheCurrent = true;
                            actualWord = c.Substring(0, c.Length - 2);
                        }
                        CurrentToken.TokenString = actualWord;

                        SToken existentToken = KnownTockens.FirstOrDefault(p => p.TokenString == actualWord);
                        if (existentToken != null)
                        {
                            CurrentToken = existentToken;
                        }
                        else
                        {
                            KnownTockens.Add(CurrentToken);
                        }
                        
                        if (!SubstituteSymbols(currentToken))
                        {
                            if (CurrentToken.TokenMultiplicity == SElementMultiplicity.Undefined)
                                CurrentToken.TokenMultiplicity = currentToken.TokenParent.TokenMultiplicity;
                        }
                        //AppendToken(c);
                        if (CurrentToken.TokenDescribed != null)
                        {
                            if (CurrentToken.TokenDescribed.TokenInfluenceC != null)
                            {
                                foreach (SToken token in CurrentToken.TokenDescribed.TokenInfluenceC)
                                {
                                    if (CurrentToken.TokenDescribed.TokenInfluenceC.Count > 0)
                                    {
                                        if (token.TokenInfluenceA == null)
                                        {
                                            token.TokenInfluenceA = new List<SToken>();
                                        }
                                    }
                                    token.TokenInfluenceA.Add(CurrentToken.TokenDescribed);
                                }
                                CurrentToken.TokenDescribed.TokenInfluenceC = null;
                            }
                            if (CurrentToken.TokenDescribed.TokenInfluenceD != null)
                            {
                            }
                        }
                        AppendT(c);
                        
                        break;
                }
                if (!handled)
                {
                    currentPhrase.Append(" " + c);
                }
                else
                {
                    handled = false;
                }
                //RefreshPersons();
            }
        }

        private void SetInfluence(SToken currentToken, string c)
        {
            switch (c)
            {
                case "increase":
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceA == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceA = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceA.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                    /*
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceA == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceA = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceA.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                     * */
                    break;
                case "decrease":
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceB == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceB = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceB.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                    /*
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceB == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceB = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceB.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                     * */
                    break;
                case "increasedBy":
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceC == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceC = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceC.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                    /*
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceB == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceB = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceB.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                     * */
                    break;
                case "decreasedBy":
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceD == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceD = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceD.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                    /*
                    if (currentToken.TokenDescribed == null)
                        currentToken.TokenDescribed = currentToken.TokenBefore;
                    if (currentToken.TokenDescribed.TokenInfluenceB == null)
                    {
                        currentToken.TokenDescribed.TokenInfluenceB = new List<SToken>();
                    }
                    currentToken.TokenDescribed.TokenInfluenceB.Add(currentToken);
                    currentToken.Link(currentToken.TokenParent, null, null, null);
                     * */
                    break;
            }
        }

        private void AppendT(string c)
        {
            SToken newToken = new SToken();
            newToken.TokenString = "..";
            if (!sticked && ( CurrentToken.TokenParent.TokenType == SElementType.CollectionAnd || CurrentToken.TokenParent.TokenType == SElementType.CollectionOr) )
            {
                if (CurrentToken.TokenParent.TokenAfter != null)
                {
                    newToken.Link(CurrentToken.TokenParent.TokenParent, CurrentToken.TokenParent.TokenAfter[0], null, null);
                    CurrentToken.TokenParent.TokenAfter[0].TokenAfter = new List<SToken>() { newToken };
                }
                else
                {
                    newToken.Link(CurrentToken.TokenParent.TokenParent, null, null, null);
                }
                CurrentToken = newToken;
                return;
            }
            newToken.Link(CurrentToken.TokenParent, CurrentToken, null, null);
            CurrentToken.TokenAfter = new List<SToken>() { newToken };
            CurrentToken = newToken;
        }

        private void EscapeFromCollection(string c)
        {

            switch (c)
            {
                case "and":
                    currentToken.TokenParent.TokenType = SElementType.CollectionAnd;
                    currentToken.TokenParent.TokenString = "( && ";
                    break;
                case "or":
                    currentToken.TokenParent.TokenType = SElementType.CollectionOr;
                    currentToken.TokenParent.TokenString = "( || ";
                    break;
            }
            sticked = false;
        }

        private void AppendToCollection()
        {
            if (currentToken.TokenParent.TokenType != SElementType.Collection)
             {
                currentToken.TokenBefore.TokenAfter = null;
                currentToken.TokenBefore = currentToken.TokenParent.TokenAfter[0];
                currentToken.TokenParent.TokenAfter[0].TokenAfter = new List<SToken>() { currentToken };
            }
            //FoldNeeded = true;
        }

        private void CreateNewCollection()
        {
            sticked = true;

            //Prepare Brackets

            SToken nextToken = CurrentToken;
            CurrentToken = CurrentToken.TokenBefore;
            CurrentToken.TokenAfter = null;
            
            SToken bracketOpenToken = new SToken();
            SToken bracketCloseToken = new SToken();

            bracketOpenToken.Configure(0, "(", SElementType.Collection, "");
            bracketCloseToken.Configure(0, ")", SElementType.Collection, "");

            bracketOpenToken.TokenNodeType = SElementType.FoldOpen;
            bracketCloseToken.TokenNodeType = SElementType.FoldClose;

            // Wire Up

            bracketOpenToken.Link(CurrentToken.TokenParent, CurrentToken.TokenBefore, bracketCloseToken, new List<SToken>() { CurrentToken });
            bracketCloseToken.Link(CurrentToken.TokenParent, bracketOpenToken, null, null);
            nextToken.Link(bracketOpenToken, CurrentToken, null, null);
            if (CurrentToken.TokenBefore != null)
            {
                CurrentToken.TokenBefore.TokenAfter[0] = bracketOpenToken;
            }
            if (CurrentToken.TokenDescribed != null)
            {
                CurrentToken.TokenDescribed.TokenProperties[0] = bracketOpenToken;
            }
            CurrentToken.Link(bracketOpenToken, null, nextToken, null);
            CurrentToken = nextToken;
        }

        private void SetMultiplicityLevel(int multiplicityLevel)
        {
            currentToken.TokenMultiplicityLevel = multiplicityLevel;
            currentToken.TokenMultiplicity = SElementMultiplicity.Explicit;
        }

        private void SetMultiplicityFlag(string c)
        {
            switch (c)
            {
                case "all":
                    currentToken.TokenMultiplicity = SElementMultiplicity.All;
                    break;
                case "an":
                case "a":
                case "one":
                case "1":
                    currentToken.TokenMultiplicity = SElementMultiplicity.One;
                    break;
                case "no":
                case "0":
                    currentToken.TokenMultiplicity = SElementMultiplicity.Denyed;
                    break;
                case "some":
                case "many":
                    currentToken.TokenMultiplicity = SElementMultiplicity.Some;
                    break;
            }
        }

        public void ParseDocument()
        {
            // PrepareRelations();
            SubstituteFlags();
            SupressArticles();
            SubstituteOperators();
            AddIncertitude();
            AddValueWrapers();
            SubstituteLogics();
            SubstituteSymbols(currentToken);
            SubstituteNumbering();
        }

        #endregion BUSINESS

        #region COLLECTION COMPOZE

        private void AddToCollection(string tokenText, string c)
        {
            currentToken.TokenString = tokenText;
            if (!sticked && currentToken.TokenParent.TokenNodeType == SElementType.FoldOpen)
            {
                //*** currentToken = currentToken.TokenParent.TokenAfter;
            }
            currentToken = FoldBelowWithPrewious(c, new SElementType[] { SElementType.CollectionAnd, SElementType.CollectionOr });
            if (currentToken.TokenParent.TokenType == SElementType.Collection)
            {
                //string c = "";
                switch (c)
                {
                    case "and":
                        currentToken.TokenParent.TokenType = SElementType.CollectionAnd;
                        currentToken.TokenParent.TokenString = "( && ";
                        break;
                    case "or":
                        currentToken.TokenParent.TokenType = SElementType.CollectionOr;
                        currentToken.TokenParent.TokenString = "( || ";
                        break;
                }
            }
            SToken newTocken = new SToken();
            newTocken.Configure(0, string.Empty, SElementType.Undefined, string.Empty);
            newTocken.Link(currentToken.TokenParent, currentToken, SToken.NULL, new List<SToken>());
            //currentToken.TokenMultiplicity = currentToken.TokenParent.TokenMultiplicity;
            //*** currentToken.TokenAfter = newTocken;
            currentToken = newTocken;
            //if (currentToken.TokenParent.TokenType == SElementType.Collection)
            sticked = true;
        }

        private void AddBlock(string tokenText, string c)
        {
            currentToken.TokenString = tokenText + c;
            SToken aToken = new SToken();
            aToken.Configure(0, string.Empty, SElementType.Undefined, "");
            aToken.Link(SToken.NULL, SToken.NULL, parentToken, new List<SToken>());
            parentToken.TokenChilds.Add(aToken);
            currentToken = aToken;
        }

        private SToken AppendToken(string tokenText, string c)
        {
            //ParseDocument();
            if (!sticked)
            {
                if (currentToken.TokenParent.TokenNodeType == SElementType.FoldOpen || currentToken.TokenParent.TokenType == SElementType.CollectionAnd || currentToken.TokenParent.TokenType == SElementType.CollectionOr)
                {
                    //*** currentToken.TokenBefore.TokenAfter = currentToken.TokenAfter; // link
                    //*** currentToken.TokenBefore = currentToken.TokenParent.TokenAfter;
                    //*** currentToken.TokenParent.TokenAfter.TokenAfter = currentToken;
                    currentToken.TokenParent = currentToken.TokenParent.TokenParent;
                }
                else
                {
                    SToken newToken = new SToken();
                    newToken.Configure(0, "", SElementType.Undefined, "");
                    newToken.Link(currentToken.TokenParent, currentToken, null, null);
                    // if the multilplicity is not defined get the colection multiplicity
                    if (currentToken.TokenMultiplicity == SElementMultiplicity.Undefined)
                    {
                        newToken.TokenMultiplicity = currentToken.TokenParent.TokenMultiplicity;
                    }
                    //currentToken = newToken;
                    currentToken.TokenString = c;
                    //*** currentToken.TokenAfter = newToken;
                    //*** currentToken = currentToken.TokenAfter;
                }
            }
            else
            {
                currentToken.TokenString = c;

                //currentToken = newToken;
            }
            sticked = false;
            return currentToken;
        }

        void AppendToken(string c)
        {
            SElementType[] conditionType = new SElementType[] { SElementType.CollectionAnd, SElementType.CollectionOr };
            SToken newToken = SetNext(currentToken);
            newToken.Configure(0, "..", SElementType.Symbol, "");
            newToken.TokenNodeType = SElementType.Undefined;
            if (!sticked && nextIsAPropertyFoTheCurrent)
            {
                if (currentToken.TokenProperties == null)
                {
                    currentToken.TokenProperties = new List<SToken>();
                }
                newToken.Link(currentToken.TokenParent, null, null, null);
                currentToken.TokenProperties.Add(newToken);
                newToken.TokenDescribed = currentToken;
                currentToken = newToken;

                nextIsAPropertyFoTheCurrent = false;
                return;
            }

           
            if (!sticked && conditionType.Contains(currentToken.TokenParent.TokenType))
            {
                currentToken = currentToken.TokenParent.TokenAfter[currentToken.TokenParent.TokenAfter.Count-1];
            }
            newToken.Link(currentToken.TokenParent, currentToken, null, null);
            //currentToken = newToken;
            AdoptAsCurrent(newToken);

        }

        void AppendTokenDown()
        {
            SToken newToken = new SToken();
            newToken.Configure(0, "append", SElementType.Undefined, "");
            newToken.Link(currentToken.TokenParent, currentToken, null, null);
            //*** currentToken.TokenAfter = newToken;
            //*** currentToken = currentToken.TokenAfter;
        }

        void FoldToken(string c)
        {
            bool foldNeeded = true;
            SElementType[] conditionType = new SElementType[] { SElementType.CollectionAnd, SElementType.CollectionOr };
            if (currentToken.TokenBefore != null) // if there is nothing before fold
            {
                if (conditionType.Contains(currentToken.TokenBefore.TokenType)) // if there is another terminated fold before then fold
                {
                    //foldNeeded = false;
                }
                if (currentToken.TokenBefore.TokenType == SElementType.Collection && currentToken.TokenString == "and") // Fold needed ?
                {
                    foldNeeded = false;
                }
                if (currentToken.TokenParent.TokenNodeType == SElementType.FoldOpen)
                {
                    foldNeeded = false;
                }
            }
            if (foldNeeded)
            {
                SToken firstToFold = currentToken;
                if (currentToken.TokenBefore != null)
                {
                    if (currentToken.TokenBefore.TokenBefore != null && currentToken.TokenBefore.TokenNodeType == SElementType.FoldClose && currentToken.TokenBefore.TokenBefore.TokenNodeType != SElementType.CollectionAnd)
                    {
                        while (firstToFold.TokenBefore != null && firstToFold.TokenNodeType != SElementType.FoldOpen)
                        {
                            firstToFold = firstToFold.TokenBefore;
                        }
                    }
                    else
                    {
                        firstToFold = currentToken.TokenBefore;
                    }
                }
                // Insert Folding Markers
                SToken bracketOpenToken = new SToken();
                SToken bracketCloseToken = new SToken();

                bracketOpenToken.Configure(0, "(", SElementType.Collection, "");
                bracketCloseToken.Configure(0, ")", SElementType.Collection, "");

                bracketOpenToken.TokenNodeType = SElementType.FoldOpen;
                bracketCloseToken.TokenNodeType = SElementType.FoldClose;

                if (currentToken.TokenBefore != null)
                {
                    bracketOpenToken.TokenMultiplicity = currentToken.TokenBefore.TokenMultiplicity;
                }


                bracketOpenToken.Link(firstToFold.TokenParent, firstToFold.TokenBefore, bracketCloseToken, new List<SToken>() { firstToFold });
                bracketCloseToken.Link(firstToFold.TokenParent, bracketOpenToken, null, null);
                firstToFold.Link(bracketOpenToken, null, null, firstToFold.TokenChilds);

                if (bracketOpenToken.TokenBefore != null)
                {
                   bracketOpenToken.TokenBefore.TokenAfter[0] = bracketOpenToken;
                }
                SToken localToken = firstToFold;
                while (localToken != null && localToken.TokenAfter != null)
                {
                    localToken.TokenParent = bracketOpenToken;
                    localToken = localToken.TokenAfter[0];
                }
                // If bracketOpenToken is the first in clilds replace the lider
                if (bracketOpenToken.TokenBefore == null)
                {
                    bracketOpenToken.TokenParent.TokenChilds.Remove(firstToFold);
                    bracketOpenToken.TokenParent.TokenChilds.Add(bracketOpenToken);
                }
            //currentToken = bracketCloseToken;
            }

            switch (c)
            {
                case "and":
                    currentToken.TokenParent.TokenType = SElementType.CollectionAnd;
                    currentToken.TokenParent.TokenString = "( && ";
                    break;
                case "or":
                    currentToken.TokenParent.TokenType = SElementType.CollectionOr;
                    currentToken.TokenParent.TokenString = "( || ";
                    break;
            }
            
            if (currentToken.TokenParent.TokenNodeType != SElementType.FoldOpen)
            {
                //currentToken = currentToken.TokenParent;
            }
            else
            {
                //AppendToken();
            }
            
            
        }

        #endregion COLLECTION COMPOZE

        #region COLLECTION EXPLORE

        private SToken GetNext(SToken token)
        {
            if (token.TokenAfter == null)
            {
                return null;
            }
            else
            {
                return token.TokenAfter[0];
            }
        }

        private SToken SetNext(SToken token)
        {
            SToken newToken = new SToken();
            if (token.TokenAfter == null)
            {
                token.TokenAfter = new List<SToken>();
                token.TokenAfter.Add(newToken);
            }
            else
            {
                if (newEvaluation)
                {
                    token.TokenAfter.Add(newToken);
                }
            }
            newEvaluation = false;
            return newToken;
        }

        #endregion COLLECTION EXPLORE

        #region REPLACE SYMBOLS

        private void SubstituteOperators()
        {
            SOperatorCollection operatorCollection = new SOperatorCollection();
            operatorCollection.GetAllOperators();
            foreach (OperatorID operatorSymbol in operatorCollection.SOperator)
            {
                // TODO is it ok used? Location = position of row?
                AdoptOperator(operatorSymbol.Location.IR, operatorSymbol.Names[0], new List<string>() { operatorSymbol.Names[0] }, operatorSymbol.ToString());
            }
        }

        private bool SubstituteSymbols(SToken localCurrentToken)
        {
            try
            {
                bool simbolFound = false;
                //SToken localCurrentToken = currentToken;
                //while (localCurrentToken.TokenBefore != null && localCurrentToken.TokenBefore.TokenType == SElementType.Undefined)
                {
                }
                while (localCurrentToken != null)
                {
                    int offset = 0;
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                        while (!NothingMoreToreplace(localCurrentToken, offset))
                        {
                            string candidate = GetFirstCandidateToReplace(localCurrentToken, offset);
                            candidate = candidate.Trim();
                            offset += candidate.Length;
                            while (!string.IsNullOrEmpty(candidate))
                            {
                                bool found = false;
                                string partialCandidate = candidate;
                                while (!found)
                                {
                                    SObjectCollection objectCollection = new SObjectCollection();
                                    objectCollection.AddByName(singularize(partialCandidate), 0); // logics.getSymbsBySubstring(partialCandidate, 0, 100);
                                    if (objectCollection.SObject.Count < 1)
                                    {
                                        if (partialCandidate.LastIndexOf(' ') > -1)
                                        {
                                            partialCandidate = partialCandidate.Substring(0, partialCandidate.LastIndexOf(' '));
                                        }
                                        else
                                        {
                                            candidate = candidate.Substring(partialCandidate.Length).Trim();//
                                            /*for (int i = 0; i < candidate.Length; i++)
                                            {
                                                if (char.IsWhiteSpace(candidate[i]))
                                                {
                                                    ReplaceAt++;
                                                }
                                                else
                                                {
                                                    candidate = candidate.Substring(i);
                                                    return;
                                                }
                                            }*/
                                            //string substitute = "[0:0]";
                                            /***>> if (Symbol.CreateUnknown && (char.IsWhiteSpace(SubsritutedQuery[SubsritutedQuery.Length - 1]) || (SubsritutedQuery[SubsritutedQuery.Length - 1] == '!')))
                                            {
                                                Symbol symbol = new Symbol(partialCandidate, true);
                                                substitute = "[" + symbol.SymbolID[0] + ":" + symbol.SymbolID[1] + "]";
                                            } <<***/

                                            //SubsritutedQuery = SubsritutedQuery.Substring(0, ReplaceAt) + substitute + SubsritutedQuery.Substring(ReplaceAt + partialCandidate.Length);
                                            //ReplaceAt = ReplaceAt + substitute.Length + 1; // because i have a spave after substitude
                                            found = true;
                                        }
                                    }
                                    else
                                    {
                                        bool needReplaceAtPluse = false;
                                        string substitute = "";
                                        foreach (SymbolID symbol in objectCollection.SObject)
                                        {
                                            substitute = symbol.ToString();
                                            simbolFound = AdoptSymbol(symbol.Location.A, symbol.Names[0], new List<string>() { partialCandidate }, "[" + symbol.Location.A + ":0]", pluralize(symbol.Names[0]).Equals(partialCandidate) ? SElementMultiplicity.All : SElementMultiplicity.One);

                                            currentToken = currentToken.TokenParent.TokenChilds[0];
                                            //*** while (currentToken.TokenAfter != null)
                                            {
                                                //*** currentToken = currentToken.TokenAfter;
                                            }
                                            //currentToken.TokenParent.TokenChilds
                                            //currentToken.TokenType = localCurrentToken.TokenType;
                                            //currentToken.TokenNodeType = localCurrentToken.TokenNodeType;
                                            //currentToken.TokenMultiplicity = localCurrentToken.TokenMultiplicity;
                                            //currentToken = currentToken.TokenParent.TokenChilds[0];
                                            //*** if (currentToken.TokenAfter != null)
                                            {
                                                //*** currentToken.TokenAfter.TokenBefore = currentToken;
                                            }
                                            //offset = 0;
                                        }
                                        if (partialCandidate.Length < candidate.Length)
                                        {
                                            candidate = candidate.Substring(partialCandidate.Length + 1); // this is not possible if we are at the end of candidate
                                            needReplaceAtPluse = true;
                                        }
                                        else
                                        {
                                            candidate = string.Empty;
                                        }
                                        if (needReplaceAtPluse)
                                        {
                                            //ReplaceAt++;
                                        }
                                        found = true;
                                    }
                                }
                            }
                        }
                    localCurrentToken = GetNext(localCurrentToken);
                }
                if (simbolFound && false)
                {
                    SToken newToken = new SToken();
                    newToken.Configure(0, "", SElementType.Undefined, "");
                    newToken.Link(currentToken.TokenParent, currentToken, null, null);
                    //*** currentToken.TokenAfter = newToken;
                    currentToken = newToken;
                }
                return simbolFound;
            }
            catch (Exception ss)
            {
            }
            return false;
        }

        private SToken AdoptAsCurrent(SToken token)
        {
            if (lastToken != currentToken && lastTokenCollection != currentToken && isPreoperand)
            {
                if (currentToken.TokenMultiplicity == SElementMultiplicity.One)
                {
                    lastToken = currentToken;
                }
                else
                {
                    lastTokenCollection = currentToken;
                }
                currentToken = token;
            }
            else
            {
                currentToken = token;
            }
            return currentToken;
        }

        private string GetFirstCandidateToReplace(SToken currentToken, int offset)
        {
            bool found = false;
            int start = -1, end = -1;
            for (int i = offset; i < currentToken.TokenString.Length; i++)
            {
                if (char.IsLetter(currentToken.TokenString[i]))
                {
                    if (start < 0)
                   {
                        start = i;
                        end = i;
                        found = true;
                    }
                    else
                    {
                        end = i;
                    }
                }
                else
                {
                    if (found)
                    {
                        //                        if (!char.IsWhiteSpace(SubsritutedQuery[i]))
                        {
                            ReplaceAt = start;
                            return currentToken.TokenString.Substring(start, end + 1 - start);
                        }
                    }
                }

            }
            if (found)
            {
                ReplaceAt = start;
                return currentToken.TokenString.Substring(start, end + 1 - start);
            }
            else
                return string.Empty;
        }

        private bool NothingMoreToreplace(SToken currentToken, int offset)
        {
            for (int i = offset; i < currentToken.TokenString.Length; i++)
            {
                if (char.IsLetter(currentToken.TokenString[i]))
                { return false; }
            }
            return true;
        }

        #endregion REPLACE SYMBOL

        #region HELPERS

        private SToken FoldBelowWithPrewious(string c, SElementType[] conditionType)
        {
            // Fold Needed ?
            bool foldNeeded = true;
            if (currentToken.TokenBefore != null) // if there is nothing before fold
            {
                if (conditionType.Contains(currentToken.TokenBefore.TokenType)) // if there is another terminated fold before then fold
                {
                    //foldNeeded = false;
                }
                if (currentToken.TokenBefore.TokenType == SElementType.Collection && c == "and") // Fold needed ?
                {
                    foldNeeded = false;
                }
                if (true)
                {

                }
            }

            // Fold Location ?
            SToken firstToFold = currentToken;
            if (foldNeeded)
            {
                if (currentToken.TokenNodeType == SElementType.FoldClose && currentToken.TokenBefore.TokenNodeType != SElementType.CollectionAnd)
                {
                    while (firstToFold.TokenBefore != null && firstToFold.TokenNodeType != SElementType.FoldOpen)
                    {
                        firstToFold = firstToFold.TokenBefore;
                    }
                }


                // Insert Folding Markers
                SToken bracketOpenToken = new SToken();
                bracketOpenToken.Configure(0, "(", SElementType.Collection, "");
                bracketOpenToken.TokenNodeType = SElementType.FoldOpen; 
                bracketOpenToken.TokenMultiplicity = currentToken.TokenMultiplicity;
                SToken bracketCloseToken = new SToken();
                bracketCloseToken.Configure(0, ")", SElementType.Collection, "");
                bracketCloseToken.TokenNodeType = SElementType.FoldClose;
                bracketOpenToken.Link(firstToFold.TokenParent, firstToFold.TokenBefore, bracketCloseToken, new List<SToken>() { firstToFold });
                if (bracketOpenToken.TokenBefore != null)
                {
                    //*** bracketOpenToken.TokenBefore.TokenAfter = bracketOpenToken;
                }
                SToken localToken = firstToFold;
                while (localToken != null)
                {
                    localToken.TokenParent = bracketOpenToken;
                    //*** localToken = localToken.TokenAfter;
                }
                bracketCloseToken.Link(firstToFold.TokenParent, bracketOpenToken, null, null);
                // If bracketOpenToken is the first in clilds replace the lider
                if (bracketOpenToken.TokenBefore == null)
                {
                    bracketOpenToken.TokenParent.TokenChilds.Remove(firstToFold);
                    bracketOpenToken.TokenParent.TokenChilds.Add(bracketOpenToken);
                }
                // Fold element
                //*** firstToFold.Link(bracketOpenToken, null, firstToFold.TokenAfter, firstToFold.TokenChilds);

            }
            else
            {
                SToken localToken = null;
                if (currentToken.TokenParent.TokenChilds.Count > 0)
                {
                    localToken = currentToken.TokenParent.TokenChilds[0];
                    //*** while (localToken.TokenAfter != null)
                    {
                        //*** localToken = localToken.TokenAfter;
                    }
                }
                currentToken = localToken;
               // localToken.TokenAfter = new SToken(0, "", SElementType.Undefined, "");
               // currentToken.TokenBefore.TokenAfter = currentToken.TokenAfter;
               // currentToken.TokenBefore = localToken;
               // currentToken.TokenParent = localToken.TokenParent;
               // currentToken.TokenBefore.TokenAfter = currentToken;
            }
            // Right
            //sticked = true; // keep next token into array 
            return currentToken;
        }
        private void StepForward()
        {

        }
        private void FloatUp()
        {

        }

        #region ADOPT

        public void AdoptOperator(int operatorId, string operatorText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(operatorId, string.Empty, SElementType.Operator, operatorText);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            currentToken.TokenParent.TokenChilds.InsertRange(currentToken.TokenParent.TokenChilds.IndexOf(localCurrentToken), explodedToken);
                            currentToken.TokenParent.TokenChilds.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public bool AdoptSymbol(int symbolId, string symbolText, List<string> forms, string code, SElementMultiplicity multiplicity)
        {
            bool simbolFound = false;
            SToken token = new SToken();
            if (currentToken.TokenBefore != null)
            {
                token = currentToken;
            }
            token.Configure(symbolId, symbolText, SElementType.Symbol, "[" + symbolId + ":0]");
            token.Link(currentToken.TokenParent, currentToken, null, null);
            if (currentToken.TokenBefore != null)
            {
                token.TokenBefore = currentToken.TokenBefore;
            }
            if (token.TokenMultiplicity == SElementMultiplicity.Undefined)
            token.TokenMultiplicity = multiplicity;
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            // Try child
                            int ii = localCurrentToken.TokenParent.TokenChilds.IndexOf(localCurrentToken);
                            if (ii > -1)
                            {
                                localCurrentToken.TokenParent.TokenChilds.Insert(localCurrentToken.TokenParent.TokenChilds.IndexOf(localCurrentToken), explodedToken[0]);
                                localCurrentToken.TokenParent.TokenChilds.Remove(localCurrentToken);
                                i = 0;
                                //if (explodedToken[explodedToken.Count - 1]!=null)
                                localCurrentToken = explodedToken[explodedToken.Count - 1];
                                simbolFound = true;
                            }
                            // Try properties
                            if (currentToken.TokenDescribed != null)
                            {
                                ii = localCurrentToken.TokenDescribed.TokenProperties.IndexOf(localCurrentToken);
                                if (ii > -1)
                                {
                                    localCurrentToken.TokenDescribed.TokenProperties.Insert(localCurrentToken.TokenDescribed.TokenProperties.IndexOf(localCurrentToken), explodedToken[0]);
                                    localCurrentToken.TokenDescribed.TokenProperties.Remove(localCurrentToken);
                                    i = 0;
                                    //if (explodedToken[explodedToken.Count - 1]!=null)
                                    localCurrentToken = explodedToken[explodedToken.Count - 1];
                                    simbolFound = true;
                                }
                            }
                        }
                    }
                }
                localCurrentToken = GetNext(localCurrentToken);
            }
            return simbolFound;
        }
        public void AdoptMultiplicity(int multiplicityId, string multiplicatorText, List<string> forms, string code)
        {
            SToken token = new SToken();
            //token.Configure();
            //token.Link();
            //multiplicityId, SElementType.Multiplicity, multiplicatorText, SToken.NULL, SToken.NULL, code);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptComparator(int comparatorId, string comparatorText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(comparatorId, comparatorText, SElementType.Comparator, code);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptIterator(int iteratorId, string iteratorText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(iteratorId, code, SElementType.Iterator, iteratorText);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptAsignement(int asignementId, string asignementText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(asignementId, asignementText, SElementType.Asignement, code);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
               //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptIncertitude(int incertitudeId, string incertitudeText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(incertitudeId, code, SElementType.Incertitude, incertitudeText);
            token.Link(SToken.NULL, SToken.NULL, null, null );
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptSelector(int selectorId, string selectorText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(selectorId, code, SElementType.Selector, selectorText);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.InsertRange(tokenCollection.IndexOf(localCurrentToken), explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = tokenCollection[0];
                            //currentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }
        public void AdoptLogic(int logicId, string logicText, List<string> forms, string code)
        {
            SToken token = new SToken();
            token.Configure(logicId, code, SElementType.Logic, logicText);
            token.Link(SToken.NULL, SToken.NULL, null, null);
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken localCurrentToken = currentToken;
            while (localCurrentToken != null)
            {
                for (int i = 0; i < forms.Count; i++)
                {
                    if (localCurrentToken.TokenType == SElementType.Undefined)
                    {
                        if (localCurrentToken.TokenString.Contains(forms[i]))
                        {
                            List<SToken> explodedToken = ExplodeStoken(localCurrentToken, token, forms[i]);
                            tokenCollection.AddRange(explodedToken);
                            tokenCollection.Remove(localCurrentToken);
                            i = 0;
                            localCurrentToken = explodedToken[explodedToken.Count - 1];
                        }
                    }
                }
                //*** localCurrentToken = localCurrentToken.TokenAfter;
            }
        }

        #endregion ADOPT

        #region NATURAL PROCESSING

        private void SubstituteFlags()
        {
            AdoptIterator(1, "if", new List<string>() { " if " }, " [1:-10] "); //if
            AdoptIterator(2, "foreach", new List<string>() { " foreach " }, " [1:-20] "); //foreach
            AdoptIterator(3, "while", new List<string>() { " while " }, " [1:-30] "); //while
        }

        private void SupressArticles()
        {
            AdoptIncertitude(2, " one ", new List<string>() { " an ", " a " }, " 1 ");
            // Comparer opperators
            AdoptSelector(2, "the", new List<string>() { " the ", "The ", "the " }, "");
        }

        private void SubstituteNumbering()
        {
            AdoptIncertitude(2, " one ", new List<string>() { " an ", " a " }, " 1 ");
        }

        private void SubstituteLogics()
        {

            //AdoptOperator("If ", " [-100:1] { "); // set inside loop
            //AdoptOperator(" if ", " [-100:1] { "); // set inside loop
            AdoptLogic(1, "and", new List<string>() { " , ", ", ", " and ", " && ", " & " }, " [-100:1] ");
            AdoptLogic(2, "or", new List<string>() { " or ", " || ", " | " }, " [-100:-1] ");
        }

        private void AddValueWrapers()
        {
            // Comparer opperators
            AdoptSelector(1, "of", new List<string>() { " of ", "`s ", "'s " }, " [0:0:50:50:0:0] ");
            AdoptSelector(2, "the", new List<string>() {" the ", "The "}, "");
            
            // Comparer opperators
            AdoptComparator(1, "is not equal with", new List<string>() { " != ", " is not equal with " }, " [0:0:-100:-5:0:0] ");
            AdoptComparator(2, "is equal with", new List<string>() { " == ", " is equal with " }, " [0:0:-100:5:0:0] ");
            AdoptComparator(3, "is greather then", new List<string>() { " > ", " is greather then " }, " [0:0:-100:100:0:0] ");
            AdoptComparator(4, "is smaller then", new List<string>() { " < ", " is smaller then " }, " [0:0:-100:-100:0:0] ");
            AdoptComparator(5, "is equal or greather then", new List<string>() { " >= ", " is equal or greather then ", " is greather or equal with " }, " [0:0:-100:110:0:0] ");
            AdoptComparator(6, "is equal or smaller then", new List<string>() { " <= ", "is equal or smaller then", " is smaller or equal with " }, " [0:0:-100:-11:0:0] ");



            // Multiplication values wrapper
            AdoptAsignement(1, "has value of", new List<string>() { " = " }, " [0:0:-100:0:0:0] ");
            AdoptAsignement(2, "increment", new List<string>() { " ++ " }, " [0:0:-100:1:0:0] ");
            AdoptAsignement(3, "increment", new List<string>() { " -- " }, " [0:0:-100:-1:0:0] ");
            AdoptAsignement(4, "increment with", new List<string>() { " += " }, " [0:0:-100:10:0:0] ");
            AdoptComparator(5, "decrement with", new List<string>() { " -= " }, " [0:0:-100:-10:0:0] ");
        }

        private void AddIncertitude()
        {
            AdoptIncertitude(1, "what", new List<string>() { " what ", " what ", " what", "what ", "What " }, " [-100:0:0:0] ");
        }

        #endregion NATURAL PROCESSING
        
        #region EXPLODE

        private List<SToken> ExplodeStoken(SToken sToken, SToken token, string form)
        {
            List<SToken> tokens = new List<SToken>();
            //foreach (string form in forms)
            {
                string text = sToken.TokenString;
                string textBefore = string.Empty;
                string textAfter = string.Empty;

                if (text.Contains(form))
                {
                    textBefore = " "+text.Substring(0, text.IndexOf(form)).Trim()+" ";
                    textAfter = " "+text.Substring(text.IndexOf(form) + form.Length).Trim()+" ";
                }

               // while (text.Contains(form))
                {
                    SToken tokenInside = new SToken();
                    SToken tokenBefore = new SToken();
                    SToken tokenAfter = new SToken();
                    tokenInside.Configure(token.TokenId, token.TokenString, token.TokenType, token.TokenCode);
                    tokenInside.Link(sToken.TokenParent, tokenBefore, tokenAfter, sToken.TokenChilds);
                    tokenInside.TokenMultiplicity = sToken.TokenMultiplicity;
                    tokenInside.TokenMultiplicityLevel = sToken.TokenMultiplicityLevel;
                    tokenBefore.Configure(sToken.TokenId, textBefore, sToken.TokenType, sToken.TokenCode);
                    tokenBefore.Link(sToken.TokenParent, sToken.TokenBefore, tokenInside, token.TokenChilds);
                    tokenAfter.Configure(sToken.TokenId, textAfter, sToken.TokenType, sToken.TokenCode);
                    //*** tokenAfter.Link(sToken.TokenParent, tokenInside, sToken.TokenAfter, token.TokenChilds);

                   //***  tokenInside.TokenAfter = tokenAfter;

                    if (string.IsNullOrEmpty(tokenBefore.TokenString.Trim()))
                    {
                        tokenInside.TokenBefore = tokenBefore.TokenBefore;
                    }
                    if (string.IsNullOrEmpty(tokenAfter.TokenString.Trim()))
                    {
                        //*** tokenInside.TokenAfter = tokenAfter.TokenAfter;
                    }

                    if (sToken.TokenMultiplicity != SElementMultiplicity.Undefined)
                    {
                        tokenInside.TokenMultiplicity = sToken.TokenMultiplicity;
                        tokenBefore.TokenMultiplicity = sToken.TokenMultiplicity;
                        tokenInside.TokenMultiplicity = sToken.TokenMultiplicity;
                    }
                    else
                    {
                        tokenInside.TokenMultiplicity = token.TokenMultiplicity;
                        tokenBefore.TokenMultiplicity = token.TokenMultiplicity;
                        tokenInside.TokenMultiplicity = token.TokenMultiplicity;
                    }
                    tokenAfter.TokenBefore = tokenBefore;
                    if (sToken.TokenBefore != null)
                    {
                        //*** sToken.TokenBefore.TokenAfter = tokenBefore;
                    }
                    //*** if (sToken.TokenAfter != null)
                    {
                        //*** sToken.TokenAfter.TokenBefore = tokenAfter;
                    }
                    
                    if (!string.IsNullOrEmpty(tokenBefore.TokenString.Trim()))
                    {
                        tokens.Add(tokenBefore);
                    } 
                    tokens.Add(tokenInside);
                    if (!string.IsNullOrEmpty(tokenAfter.TokenString.Trim()))
                    {
                        tokens.Add(tokenAfter);
                    }
                }
            }
            return tokens;
        }

        #endregion EXPLODE

        #region TOKENIZE INPUT
        /// <summary>
        /// Create a collection of tokens from the input string
        /// </summary>
        /// <returns></returns>
        public List<SToken> TokenizeInput(string tokenizableString)
        {
            List<SToken> tokenizedInput = new List<SToken>();
            SToken newToken = new SToken();
            newToken.Configure(0, string.Empty, SElementType.Undefined, tokenizableString);
            newToken.Link(SToken.NULL, SToken.NULL, null, null);
            tokenizedInput.Add(newToken);
            return tokenizedInput;
        }
        #endregion TOKENIZE INPUT

        #region TO STRING

        /*
        
        public string ToString()
        {
            StringBuilder tokenCollectionAsString = new StringBuilder();
            SToken token = tokenCollection[0];
            int flag = tokenCollection.Count - 2;
            while (flag != 0)
            {
                flag++;
                string color;
                switch (token.TokenType)
                {
                    case SElementType.Asignement:
                        color = "red";
                        break;
                    case SElementType.Comparator:
                        color = "orange";
                        break;
                    case SElementType.Incertitude:
                        color = "magenta";
                        break;
                    case SElementType.Iterator:
                        color = "orange";
                        break;
                    case SElementType.Logic:
                        color = "red";
                        break;
                    case SElementType.Multiplicity:
                        color = "green";
                        break;
                    case SElementType.Operator:
                        color = "blue";
                        break;
                    case SElementType.Selector:
                        color = "green";
                        break;
                    case SElementType.Symbol:
                        color = "purple";
                        break;
                    default:
                        color = "black";
                        break;
                }
                //tokenCollectionAsString.Append("<span style='color:" + color + "'>" + token.TokenString + "<span style='color:silver'>" + token.TokenCode + "</span></span> ");
                tokenCollectionAsString.Append("<span style='color:" + color + "'>" + pluralize(token) + "</span> ");
                token = token.TokenAfter;
                if (flag > 0 && token.TokenAfter == SToken.NULL)
                {
                    flag = -1;
                }
            }
            
            return tokenCollectionAsString.ToString();
        }
        
        */
        
        #endregion TO STRING

        #region TO STRING

        public string ToString()
        {
            StringBuilder allCodeAsString = new StringBuilder();
            allCodeAsString.Append(ToString(rootTocken) + "<hr>");
            return allCodeAsString.ToString();
        }

        public string ToString(SToken token)
        {
            int acoladeBracketBlock = 0;
            int angleBracketBlock = 0;
            int rightBracketBlock = 0;
            int roundBracketBlock = 0;

            StringBuilder tokenCollectionAsString = new StringBuilder();

            while (token != null)
            {
                string color;
                switch (token.TokenType)
                {
                    case SElementType.Asignement:
                        color = "red";
                        break;
                    case SElementType.Comparator:
                        color = "orange";
                        break;
                    case SElementType.Incertitude:
                        color = "magenta";
                        break;
                    case SElementType.Iterator:
                        color = "orange";
                        break;
                    case SElementType.Logic:
                        color = "red";
                        break;
                    case SElementType.Multiplicity:
                        color = "green";
                        break;
                    case SElementType.Operator:
                        color = "blue";
                        break;
                    case SElementType.Selector:
                        color = "green";
                        break;
                    case SElementType.Symbol:
                        color = "purple";
                        break;
                    default:
                        color = "black";
                        break;
                }
                //*** if (((!(token.TokenAfter == null) && !(token.TokenAfter.TokenAfter == null) && (!(token.TokenAfter.TokenAfter.TokenAfter == null && !string.IsNullOrEmpty(token.TokenAfter.TokenString) && ")]>}".Contains(token.TokenAfter.TokenString[0]))))) || token.TokenType == SElementType.Undefined)
                if (token.TokenAfter != null)
                {
                    tokenCollectionAsString.Append("<span style='color:" + color + "'>" + System.Security.SecurityElement.Escape(pluralize(token)) + "</span> ");
                    //tokenCollectionAsString.Append("<span style='color:" + color + "'>" + System.Security.SecurityElement.Escape(token.TokenString) + "</span> ");
                }
                else
                {
                    tokenCollectionAsString.Append("<span style='color:" + color + "'>" + System.Security.SecurityElement.Escape(pluralize(token)) + "</span> ");
                    //tokenCollectionAsString.Append("<span style='color:" + color + "'>" + System.Security.SecurityElement.Escape(token.TokenString) + "</span> ");
                }
                if (token.TokenChilds != null)
                    foreach (SToken codeTokenlist in token.TokenChilds)
                    {
                        tokenCollectionAsString.Append(ToString(codeTokenlist));
                        /*if (token.TokenType == SElementType.AcoladeBracketBlock && tokenCollectionAsString.ToString().TrimEnd().EndsWith(";"))
                        {
                            tokenCollectionAsString.Append("</br>");
                        }*/
                    }
                if (token.TokenProperties != null)
                    foreach (SToken codeTokenlist in token.TokenProperties)
                    {
                        tokenCollectionAsString.Append(ToString(codeTokenlist));
                        /*if (token.TokenType == SElementType.AcoladeBracketBlock && tokenCollectionAsString.ToString().TrimEnd().EndsWith(";"))
                        {
                            tokenCollectionAsString.Append("</br>");
                        }*/
                    } 
                if (token.TokenActivities != null)
                    foreach (SToken codeTokenlist in token.TokenActivities)
                    {
                        tokenCollectionAsString.Append(ToString(codeTokenlist));
                        /*if (token.TokenType == SElementType.AcoladeBracketBlock && tokenCollectionAsString.ToString().TrimEnd().EndsWith(";"))
                        {
                            tokenCollectionAsString.Append("</br>");
                        }*/
                    }
                token = GetNext(token);

           }

            return tokenCollectionAsString.ToString();
        }

        #endregion TO STRING

        #region PLURALIZE / SINGULARIZE

        string pluralize(SToken token)
        {
            switch (token.TokenMultiplicity)
            {
                case SElementMultiplicity.All:
                    return "*" + pluralize(token.TokenString);
                    break;
                case SElementMultiplicity.One:
                    return "1" + token.TokenString;
                    break;
                case SElementMultiplicity.Explicit:
                    switch (token.TokenMultiplicityLevel)
                    {
                        case 0:
                            return "No " + token.TokenString + "s";
                        case 1:
                            return "One " + token.TokenString + "s";
                        default:
                            return token.TokenMultiplicityLevel+"X " + token.TokenString + "s";
                            break;
                    }
                    break;
                case SElementMultiplicity.Some:
                    return "" + token.TokenString + "s[]";
                    break;
            }
            return token.TokenString;
        }

        string pluralize(string word)
        {
            return word.Trim() + "s";
        }

        string singularize(string word)
        {
            if (!word.Contains(" "))
            {
                if (word.EndsWith("s"))
                {
                    word = word.Substring(0, word.Length-1);
                }
            }
            return word;
        }

        #endregion PLURALIZE / SINGULARIZE

        #endregion HELPERS

        private bool foldNeeded = true;
        public bool FoldNeeded 
        { 
            get {
                if (CurrentToken.TokenBefore == null)
                {
                    return true;
                }
                else if (CurrentToken.TokenParent.TokenType == SElementType.Collection)
                {
                    return false;
                }
                return foldNeeded;
            } 
            set{foldNeeded = value;} 
        }
    }

    class ECodeDocument 
    {

        #region CONSTRUCTOR

        public ECodeDocument(string documentText)
        {
            DocumentText = documentText;
            RootTocken = new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, string.Empty, SCodeToken.NULL, new List<SCodeToken>());
            RootTocken.TokenChild.Add( new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, string.Empty, rootTocken, new List<SCodeToken>()) );
            parentToken = RootTocken;
            currentToken = RootTocken.TokenChild[0];
        }

        #endregion CONSTRUCTOR

        #region PROPERTIES

        #region MULTIPPLICITY

        private SElementMultiplicity tokenMultiplicity;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SElementMultiplicity TokenMultiplicity
        {
            get { return tokenMultiplicity; }
            set { tokenMultiplicity = value; }
        }

        #endregion MULTIPPLICITY

        #region ORIGINAL TEXT

        private string documentText;
        /// <summary>
        /// The text of the document
        /// </summary>
        public string DocumentText
        {
            get { return documentText; }
            set { documentText = value; }
        }

        #endregion ORIGINAL TEXT

        #region CURRENT / PARRENT / ROOT

        private SCodeToken rootTocken;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SCodeToken RootTocken
        {
            get { return rootTocken; }
            set { rootTocken = value; }
        }

        
        public SCodeToken currentToken = null;
        public SCodeToken parentToken = null;

        #endregion CURRENT / PARRENT / ROOT

        #endregion PROPERTIES

        #region BUSINESS
        
        #region CODE PROCESSING

        public void ParseAsCode()
        {
            StringBuilder currentPhrase = new StringBuilder();
            bool handled = false;
            foreach (char c in documentText)
            {
                switch (c)
                {
                    case ';':
                        //currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        AddBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '{':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        OpenAcoladeBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '}':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        CloseAcoladeBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '<':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        OpenAngleBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '>':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        CloseAngleBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '(':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        OpenRoundBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case ')':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        CloseRoundBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case '[':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        OpenRightBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                    case ']':
                        currentToken.TokenString = currentPhrase.ToString();
                        handled = true;
                        CloseRightBlock(currentPhrase.ToString(), c);
                        currentPhrase = new StringBuilder();
                        break;
                }
                if (!handled)
                {
                    currentPhrase.Append(c);
                }
                else
                {
                    handled = false;
                }
                currentToken.TokenString = currentPhrase.ToString();
            }
        }

        #endregion CODE PROCESSING

        #endregion BUSINESS
    
        #region HELPERS

        #region CODE HELPERS

        private void AddBlock(string tokenText, char c) {
            currentToken.TokenString = tokenText + c;
            SCodeToken aToken = new SCodeToken(0, SCElementType.Undefined, "", SCodeToken.NULL, SCodeToken.NULL, "", parentToken, new List<SCodeToken>());
            parentToken.TokenChild.Add(aToken);
            currentToken = aToken;
        }
        private void OpenAcoladeBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            // DownLevel
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.AcoladeBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", parentToken, new List<SCodeToken>());
            currentToken.TokenAfter = bracketToken;
            currentToken = bracketToken;
            // Right
            SCodeToken bracketContentToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, "", bracketToken, new List<SCodeToken>());
            currentToken.TokenChild.Add(bracketContentToken);
            parentToken = currentToken; // mouve down
            currentToken = bracketContentToken;
        }
        private void CloseAcoladeBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            currentToken = parentToken;
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.AcoladeBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", currentToken.TokenParent, new List<SCodeToken>());
            parentToken = currentToken.TokenParent;
            currentToken.TokenAfter = bracketToken;
            SCodeToken nextToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, bracketToken, SCodeToken.NULL, "", bracketToken.TokenParent, new List<SCodeToken>());
            bracketToken.TokenAfter = nextToken;
            currentToken = nextToken;
        }
        private void OpenAngleBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            // DownLevel
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.AngleBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", parentToken, new List<SCodeToken>());
            currentToken.TokenAfter = bracketToken;
            currentToken = bracketToken;
            // Right
            SCodeToken bracketContentToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, "", bracketToken, new List<SCodeToken>());
            currentToken.TokenChild.Add(bracketContentToken);
            parentToken = currentToken; // mouve down
            currentToken = bracketContentToken;
        }
        private void CloseAngleBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            currentToken = parentToken;
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.AngleBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", currentToken.TokenParent, new List<SCodeToken>());
            parentToken = currentToken.TokenParent;
            currentToken.TokenAfter = bracketToken;
            SCodeToken nextToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, bracketToken, SCodeToken.NULL, "", bracketToken.TokenParent, new List<SCodeToken>());
            bracketToken.TokenAfter = nextToken;
            currentToken = nextToken;
        }
        private void OpenRoundBlock(string tokenText, char c)
        {
            currentToken.TokenString = tokenText; 
            // DownLevel
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.RoundBracketBlock, string.Empty+c, currentToken, SCodeToken.NULL, "", parentToken, new List<SCodeToken>());
            currentToken.TokenAfter = bracketToken;
            currentToken = bracketToken;
            // Right
            SCodeToken bracketContentToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, "", bracketToken, new List<SCodeToken>());
            currentToken.TokenChild.Add(bracketContentToken);
            parentToken = currentToken; // mouve down
            currentToken = bracketContentToken;
        }
        private void CloseRoundBlock(string tokenText, char c)
        {
            currentToken.TokenString = tokenText;
            currentToken = parentToken;
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.RoundBracketBlock, string.Empty+c, currentToken, SCodeToken.NULL, "", currentToken.TokenParent, new List<SCodeToken>());
            parentToken = currentToken.TokenParent;
            currentToken.TokenAfter = bracketToken;
            SCodeToken nextToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, bracketToken, SCodeToken.NULL, "", bracketToken.TokenParent, new List<SCodeToken>());
            bracketToken.TokenAfter = nextToken;
            currentToken = nextToken;
        }
        private void OpenRightBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            // DownLevel
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.RightBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", parentToken, new List<SCodeToken>());
            currentToken.TokenAfter = bracketToken;
            currentToken = bracketToken;
            // Right
            SCodeToken bracketContentToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, SCodeToken.NULL, SCodeToken.NULL, "", bracketToken, new List<SCodeToken>());
            currentToken.TokenChild.Add(bracketContentToken);
            parentToken = currentToken; // mouve down
            currentToken = bracketContentToken;
        }
        private void CloseRightBlock(string tokenText, char c) 
        {
            currentToken.TokenString = tokenText;
            currentToken = parentToken;
            SCodeToken bracketToken = new SCodeToken(0, SCElementType.RightBracketBlock, string.Empty + c, currentToken, SCodeToken.NULL, "", currentToken.TokenParent, new List<SCodeToken>());
            parentToken = currentToken.TokenParent;
            currentToken.TokenAfter = bracketToken;
            SCodeToken nextToken = new SCodeToken(0, SCElementType.Undefined, string.Empty, bracketToken, SCodeToken.NULL, "", bracketToken.TokenParent, new List<SCodeToken>());
            bracketToken.TokenAfter = nextToken;
            currentToken = nextToken;
        }

        #endregion CODE HELPERS

        #region EXPLODE

        private List<SToken> ExplodeStoken(SToken sToken, SToken token, string form)
        {
            List<SToken> tokens = new List<SToken>();
            //foreach (string form in forms)
            {
                string text = sToken.TokenString;
                string textBefore = string.Empty;
                string textAfter = string.Empty;

                if (text.Contains(form))
                {
                    textBefore = " "+text.Substring(0, text.IndexOf(form)).Trim()+" ";
                    textAfter = " "+text.Substring(text.IndexOf(form) + form.Length).Trim()+" ";
                }

               // while (text.Contains(form))
                {
                    SToken tokenInside = new SToken();
                    tokenInside.Configure(token.TokenId, token.TokenString, token.TokenType, token.TokenCode);
                    tokenInside.Link(SToken.NULL, SToken.NULL, null, null);
                    SToken tokenBefore = new SToken();
                    tokenBefore.Configure(sToken.TokenId, textBefore, sToken.TokenType,  sToken.TokenCode);
                    tokenBefore.Link(sToken.TokenBefore, tokenInside, null, null);
                    SToken tokenAfter = new SToken();
                    tokenAfter.Configure(sToken.TokenId, textAfter, sToken.TokenType, sToken.TokenCode);
                    //*** tokenAfter.Link(tokenInside, sToken.TokenAfter, null, null);
                    tokenInside.TokenBefore = tokenBefore;
                    if (sToken.TokenBefore != null)
                    {
                       //***  sToken.TokenBefore.TokenAfter = tokenBefore;
                    }
                    //*** if (sToken.TokenAfter != null)
                    {
                        //*** sToken.TokenAfter.TokenBefore = tokenAfter;
                    }
                    //*** tokenInside.TokenAfter = tokenAfter;
                    
                    tokens.Add(tokenBefore);
                    tokens.Add(tokenInside);
                    tokens.Add(tokenAfter);
                }
            }
            return tokens;
        }

        #endregion EXPLODE
        
        #region TO STRING

        public string ToString()
        {
            StringBuilder allCodeAsString = new StringBuilder();
            allCodeAsString.Append(ToString(rootTocken) + "<hr>");
            return allCodeAsString.ToString();
        }
        
        public string ToString(SCodeToken token)
        {
            int acoladeBracketBlock = 0;
            int angleBracketBlock = 0;
            int rightBracketBlock = 0;
            int roundBracketBlock = 0;

            StringBuilder tokenCollectionAsString = new StringBuilder();

            while (token != null)
            {
                string color;
                switch (token.TokenType)
                {
                    case SCElementType.AcoladeBracketBlock:
                        color = "red";
                        break;
                    case SCElementType.AngleBracketBlock:
                        color = "blue";
                        break;
                    case SCElementType.RightBracketBlock:
                        color = "magenta";
                        break;
                    case SCElementType.RoundBracketBlock:
                        color = "orange";
                        break;
                    case SCElementType.Undefined:
                        color = "green";
                        break;
                    default:
                        color = "black";
                        break;
                }
                if (((!(token.TokenAfter == null) && !(token.TokenAfter.TokenAfter == null) && (!(token.TokenAfter.TokenAfter.TokenAfter == null && ! string.IsNullOrEmpty(token.TokenAfter.TokenString) &&")]>}".Contains(token.TokenAfter.TokenString[0]))))) || token.TokenType == SCElementType.Undefined)
                {
                    tokenCollectionAsString.Append("<span style='color:" + color + "'>" + System.Security.SecurityElement.Escape(token.TokenString + ":" + token.TokenCode) + "</span> ");
                }
                else
                {
                    tokenCollectionAsString.Append("<span style='background-color:yellow; color:" + color + "'>" + System.Security.SecurityElement.Escape(token.TokenString) + "</span> ");
                }
                
                foreach (SCodeToken codeTokenlist in token.TokenChild)
                {
                    tokenCollectionAsString.Append(ToString(codeTokenlist));
                    if (token.TokenType==SCElementType.AcoladeBracketBlock && tokenCollectionAsString.ToString().TrimEnd().EndsWith(";"))
                    {
                        tokenCollectionAsString.Append("</br>");
                    }
                }
                token = token.TokenAfter;
                
            }
            
            return tokenCollectionAsString.ToString();
        }

        #endregion TO STRING

        #endregion HELPERS

    }

    public enum SElementType { Symbol, Operator, Multiplicity, Undefined, Comparator, Asignement, Logic, Selector, Incertitude, Iterator, CollectionAnd, CollectionOr, Collection, FoldOpen, FoldClose }

    public enum SElementMultiplicity { Undefined, One, All, Some, Explicit, Denyed }

    public class SToken 
    {

        #region CONSTRUCT

        #endregion CONSTRUCT

        #region PROPERTIES

        #region MULTIPPLICITY

        private SElementMultiplicity tokenMultiplicity;
        /// <summary>
        /// The text of the document
        /// </summary>
        public SElementMultiplicity TokenMultiplicity
        {
            get { return tokenMultiplicity; }
            set { tokenMultiplicity = value; }
        }

        #endregion MULTIPPLICITY

        #region ID
        private int tokenId;
        /// <summary>
        /// Position of identifier
        /// </summary>
        public int TokenId
        {
            get { return tokenId; }
            set { tokenId = value; }
        }
        #endregion ID

        #region STRING
        private string tokenString;
        /// <summary>
        /// Substring of identifier
        /// </summary>
        public string TokenString
        {
            get { return tokenString; }
            set { tokenString = value; }
        }
        #endregion STRING

        #region TYPE
        private SElementType tokenType;
        /// <summary>
        /// Element type of identifier
        /// </summary>
        public SElementType TokenType
        {
            get { return tokenType; }
            set { tokenType = value; }
        }
        #endregion TYPE

        #region TYPE
        private SElementType tokenNodeType;
        /// <summary>
        /// Element type of identifier
        /// </summary>
        public SElementType TokenNodeType
        {
            get { return tokenNodeType; }
            set { tokenNodeType = value; }
        }
        #endregion TYPE

        #region CODE

        private string tokenCode = "??";
        public string TokenCode
        {
            get { return tokenCode; }
            set { tokenCode = value; }
        }

        #endregion CODE

        #region TOKEN RELATIONS

        #region BEFORE

        private SToken tokenBefore = null;
        public SToken TokenBefore
        {
            get { return tokenBefore; }
            set { tokenBefore = value; }
        }

        #endregion BEFORE

        #region AFTER LIST
        
        private List<SToken> tokenAfter = null;
        public List<SToken> TokenAfter
        {
            get { return tokenAfter; }
            set { tokenAfter = value; }
        }
        
        #endregion AFTER LIST

        #region PARENT

        private SToken tokenParent = null;
        public SToken TokenParent
        {
            get { return tokenParent; }
            set { tokenParent = value; }
        }

        #endregion PARENT

        #region CHILDS LIST

        private List<SToken> tokenChilds = null;
        public List<SToken> TokenChilds
        {
            get { return tokenChilds; }
            set { tokenChilds = value; }
        }

        #endregion CHILDS LIST

        #endregion TOKEN RELATIONS

        #region TOKEN PROPERTIES

        private SToken tokenDescribed = null;
        public SToken TokenDescribed
        {
            get { return tokenDescribed; }
            set { tokenDescribed = value; }
        }

        private List<SToken> tokenProperties = null;
        public List<SToken> TokenProperties
        {
            get { return tokenProperties; }
            set { tokenProperties = value; }
        }
        private List<SToken> tokenActivities = null;
        public List<SToken> TokenActivities
        {
            get { return tokenActivities; }
            set { tokenActivities = value; }
        }

        private List<SToken> tokenInfluenceA = null;
        public List<SToken> TokenInfluenceA
        {
            get { return tokenInfluenceA; }
            set { tokenInfluenceA = value; }
        }

        private List<SToken> tokenInfluenceB = null;
        public List<SToken> TokenInfluenceB
        {
            get { return tokenInfluenceB; }
            set { tokenInfluenceB = value; }
        }

        private List<SToken> tokenInfluenceC = null;
        public List<SToken> TokenInfluenceC
        {
            get { return tokenInfluenceC; }
            set { tokenInfluenceC = value; }
        }

        private List<SToken> tokenInfluenceD = null;
        public List<SToken> TokenInfluenceD
        {
            get { return tokenInfluenceD; }
            set { tokenInfluenceD = value; }
        }

        #endregion TOKEN PROPERTIES

        #endregion PROPERTIES

        #region NULL

        public static SToken NULL = null;
        /*
        private int p;
        private string p_2;
        private SElementType sElementType;
        private string p_3;
        */
        #endregion NULL

        #region DEFINE

        internal void Link(SToken tokenParent, SToken tokenBefore, SToken tokenAfter, List<SToken> tokenChilds)
        {
            TokenParent = tokenParent;
            TokenBefore = tokenBefore;
            if (tokenAfter != null)
            {
                if (TokenAfter == null)
                {
                    TokenAfter = new List<SToken>();
                    TokenAfter.Add(tokenAfter);
                    if (tokenAfter.TokenChilds == null)
                    {
                        tokenAfter.TokenChilds = new List<SToken>();
                    }
                }
            }
            if (tokenChilds == null)
            {
                tokenChilds = new List<SToken>();
            }
            TokenChilds = tokenChilds;
        }

        internal void Configure(int tokenId, string tokenString, SElementType tokenType, string tokenCode)
        {
            TokenId = tokenId;
            TokenString = tokenString;
            TokenType = tokenType;
            TokenCode = tokenCode;
        }

        #endregion DEFINE

        public int TokenMultiplicityLevel { get; set; }
    }

    enum SCElementType { AcoladeBracketBlock, AngleBracketBlock, RoundBracketBlock, RightBracketBlock, Undefined };
    
    class SCodeToken 
    {

        #region CONSTRUCT

        public SCodeToken(int tokenId, SCElementType tokenType, string tokenString, SCodeToken tokenBefore, SCodeToken tokenAfter, string tokenCode, SCodeToken tokenParent, List<SCodeToken> tokenChilds )
        {
            TokenId = tokenId;
            TokenType = tokenType;
            TokenString = tokenString;
            TokenBefore = tokenBefore;
            TokenAfter = tokenAfter;
            TokenCode = tokenCode;
            TokenParent = tokenParent;
            TokenChild = tokenChilds != null ? new List<SCodeToken>() : tokenChilds;
        }

        #endregion CONSTRUCT

        #region PROPERTIES

        #region ID
        private int tokenId;
        /// <summary>
        /// Position of identifier
        /// </summary>
        public int TokenId
        {
            get { return tokenId; }
            set { tokenId = value; }
        }
        #endregion ID

        #region STRING
        private string tokenString;
        /// <summary>
        /// Substring of identifier
        /// </summary>
        public string TokenString
        {
            get { return tokenString; }
            set { tokenString = value; }
        }
        #endregion STRING

        #region TYPE
        private SCElementType tokenType;
        /// <summary>
        /// Element type of identifier
        /// </summary>
        public SCElementType TokenType
        {
            get { return tokenType; }
            set { tokenType = value; }
        }
        #endregion TYPE

        #region CODE

        private string tokenCode = "??";
        public string TokenCode
        {
            get { return tokenCode; }
            set { tokenCode = value; }
        }

        #endregion CODE

        #region TOKEN RELATIONS

        #region BEFORE

        private SCodeToken tokenBefore = null;
        public SCodeToken TokenBefore
        {
            get { return tokenBefore; }
            set { tokenBefore = value; }
        }

        #endregion BEFORE

        #region AFTER

        private SCodeToken tokenAfter = null;
        public SCodeToken TokenAfter
        {
            get { return tokenAfter; }
            set { tokenAfter = value; }
        }

        #endregion AFTER

        #region PARENT

        private SCodeToken tokenParent = null;
        public SCodeToken TokenParent
        {
            get { return tokenParent; }
            set { tokenParent = value; }
        }

        #endregion PARENT

        #region CHILDS

        private List<SCodeToken> tokenChilds = null;
        public List<SCodeToken> TokenChild
        {
            get { return tokenChilds; }
            set { tokenChilds = value; }
        }

        #endregion CHILDS

        #endregion TOKEN RELATIONS
        
        #endregion PROPERTIES

        #region NULL

        public static SCodeToken NULL = null;

        #endregion NULL

    }

}
