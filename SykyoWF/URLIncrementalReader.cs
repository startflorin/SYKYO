using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;
using System.Xml;
//using System;
//using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Interaktionslogik für Page2.xaml
    /// </summary>
    public class URLIncrementalReader
    {
        // Custom constructor to pass expense report data
        public URLIncrementalReader(bool fakeData)
        {
            Symbol parrent = new Symbol(".NET Framework Class Library");
                Class1 c1 = new Class1();
                System.IO.StreamWriter file = File.AppendText("P:\\TEST\\test.html");
                file.Write("<style> a { text-decoration: none; color: black; } </style>\n\r");
                file.Close();
                string URL = "http://msdn.microsoft.com/en-us/library/gg145045.aspx";
                c1.Fetch(URL, parrent);
        }
    }



    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class Class1
    {
        SymbolItem symbol = new SymbolItem();
        RelationItem relation = new RelationItem();
        
        bool found = false;
            Uri URL = new Uri("http://msdn.microsoft.com/en-us/library/gg145045.aspx");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="level"></param>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        internal XDocument LoadURL(string URLstring)
            {
                URL = new Uri(URLstring);
                WebResponse myResponse = null;
                while (myResponse == null)
                {
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
                    myRequest.Method = "POST";  // or GET - depends 

                    byte[] data = new byte[3000];
                    myRequest.ContentType = "text/xml; encoding=utf-8";
                    myRequest.ContentLength = data.Length;

                    using (Stream reqStream = myRequest.GetRequestStream())
                    {
                        // Send the data.
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }

                    // Get Response
                    myResponse = myRequest.GetResponse();
                }
                //myRequest.Timeout = 1000;
                XmlDocument _xmlDoc = new XmlDocument();

                Stream responseStream = myResponse.GetResponseStream();
                XDocument doc = XDocument.Load(responseStream);
                return doc;
            }


        string pageTitle;
        string collapsableTitle;
        bool needTable = false; // once a wanted collapsable title is found, I need to find a table
        bool needRow = false; // once a wanted fable is found, I need to find a row in that wanted table
        bool needHref = false; // once a row was found, I need to find td(s)
        string rowData; // some elements have data as visibility and element type.
        string elementHref; // The href of the element.
        string elementIdentifier; // Identifier
        string[] wantedCollapsableTitles =  { "Operators", "Delegates", "Constructors", "Classes", "Structures", "Interfaces", "Enumerations", "Properties", "Methods", "Fields", "Members", "Conversions", "Events" }; //in namespace
        string[] wantedMappedTitles =       { "Operator" , "Delegate" , "Constructor" , "Class"  , "Structure" , "Interface" , "Enumeration" , "Property"  , "Method" , "Field" , "Member" , "Conversion" , "Event" }; //in namespace
        string[] unwantedToExplore =        { "Operators", "Delegates", "Constructors", "Properties", "Methods", "Fields", "Members", "Conversions", "Events" }; //in namespace
        string[] unwantedCollapsableTitles = { "" }; //in namespace
        bool newRow = true;
        List<string[]> ParsePage(XElement el, int level)
        {
            List<string[]> identifiers = new List<string[]>();
            IEnumerable<XElement> elements = el.Elements();
            foreach (XElement elr in elements)
            {
                bool MoreItterationNeeded = true;
                string elsss = elr.GetType().ToString();
                if (elsss == "System.Xml.Linq.XElement")
                {
                    if (elr.Name.LocalName == "span")
                    {
                        IEnumerable<XAttribute> atributes = elr.Attributes();
                        foreach (XAttribute atr in atributes)
                        {
                            if (atr.Name.LocalName.Equals("class"))
                            {
                                if (atr.Value.Equals("LW_CollapsibleArea_Title"))
                                {
                                    collapsableTitle = elr.Value;
                                    if (wantedCollapsableTitles.Contains(collapsableTitle))
                                    {
                                        for (int i = 0; i < wantedCollapsableTitles.Length; i++)
                                        {
                                            if (collapsableTitle.Equals(wantedCollapsableTitles[i]))
                                            {
                                                collapsableTitle = wantedMappedTitles[i];
                                            }
                                        }
                                        // I found an interesting collapsableTitle
                                        needTable = true;
                                        needRow = false; //just for safe so that if I am waiting for a table to ignore rows
                                        needHref = false; //just for safe so that if I am waiting for a table to ignore td(s)
                                    }
                                    else 
                                    {
                                        if (!wantedCollapsableTitles.Contains(collapsableTitle))
                                        {
                                            needTable = false;
                                            needRow = false; //just for safe so that if I am not waiting for a table to ignore also rows
                                            needHref = false; //just for safe so that if I am not waiting for a table to ignore alse td(s)
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (needTable && elr.Name.LocalName == "table")
                    {
                        IEnumerable<XAttribute> tableAtributes = elr.Attributes();
                        foreach (XAttribute tableAtr in tableAtributes)
                        {
                            if (tableAtr.Name.LocalName.Equals("class"))
                            {
                                if (tableAtr.Value.Equals("members"))
                                {
                                    needRow = true;
                                    needHref = false; //just for safe so that if I am waiting for a row to ignore td(s)
                                }
                                else
                                {
                                    needRow = false;
                                    needHref = false; //just for safe so that if I am waiting for a row to ignore also td(s)
                                }
                            }
                        }
                    }

                    if (needTable && needRow && elr.Name.LocalName == "tr")
                    {
                        IEnumerable<XAttribute> rowAtributes = elr.Attributes();
                        rowData = ""; // Because not all rows have data
                        foreach (XAttribute rowAtr in rowAtributes)
                        {
                            if (rowAtr.Name.LocalName.Equals("data"))
                            {
                                rowData = rowAtr.Value;
                                needHref = true;
                            }
                        }
                        newRow = true;
                    }

                    // inside tab I can find a href or a span
                    if (needTable && needRow && needHref && elr.Name.LocalName == "a")
                    {
                        IEnumerable<XAttribute> hrefAtributes = elr.Attributes();
                        foreach (XAttribute hrefAtr in hrefAtributes)
                        {
                            if (hrefAtr.Name.LocalName.Equals("href"))
                            {
                                elementHref = hrefAtr.Value;
                                elementIdentifier = elr.Value;
                                string a = elr.Value; /* with atributes */ a = rowData; /* is a */ a = collapsableTitle; /* in the */ a = pageTitle;
                                //registerNow = true;
                            }
                        }
                    } 
                    if (needTable && needRow && needHref && elr.Name.LocalName == "span")
                    {
                        elementHref = "";
                        if (newRow) elementIdentifier = elr.Value;
                    }
                    if (newRow && (!string.IsNullOrEmpty(elementIdentifier)))
                    {
                        if (wantedMappedTitles.Contains(collapsableTitle))
                        {
                            if (! string.IsNullOrEmpty(elementIdentifier))
                            {
                                identifiers.Add(new string[] { elementIdentifier, collapsableTitle, elementHref });
                                MoreItterationNeeded = false;
                                elementIdentifier = null;
                                newRow = false;
                            }
                        }
                    }
                }
                if (MoreItterationNeeded) identifiers.AddRange(ParsePage(elr, level++)); 
            }
            return identifiers;
        }

        Symbol registerNamespace(Symbol symA, string child)
        {
            Symbol symB = new Symbol(child);
            if (symB.SymbolID[0] == 0 && symB.SymbolID[1] == 0) // simbol does not exists
            {
                symB.RegisterSymbolIfNotExists();
                Symbol symC = new Symbol(".NET framework 4 NAMESPACE");
                if (symC.SymbolID[0] == 0 && symC.SymbolID[1] == 0) // category does not extsts
                {
                    symC.RegisterSymbolIfNotExists();
                }

                Relation derivationRelation = new Relation(new int[] { 9999, 1, 1, 100 }); // is a
                derivationRelation.CreateRelationIfNotExists(symB.SymbolID, symC.SymbolID); // simB is a simC
            }

            //if (string.IsNullOrEmpty(parrent)) 
            if (symA == null) return symB;
            //Symbol symA = new Symbol(parrent);
            if (symA.SymbolID[0] == 0 && symA.SymbolID[1] == 0)
            {
                symA.RegisterSymbolIfNotExists();
            }
            Relation relation = new Relation(new int[] { 2, 100, 9998, 1 }); // include
            relation.CreateRelationIfNotExists(symA.SymbolID, symB.SymbolID);
            return symB;
        }

        Symbol registerCollapsableTitle(Symbol symA, string child, string Type)
        {
            Symbol symB = new Symbol(child);
            if (symB.SymbolID[0] == 0 && symB.SymbolID[1] == 0) // simbol does not exists
            {
                symB.RegisterSymbolIfNotExists();
                Symbol symC = new Symbol(".NET framework 4 "+Type.ToUpper());
                if (symC.SymbolID[0] == 0 && symC.SymbolID[1] == 0) // category does not extsts
                {
                    symC.RegisterSymbolIfNotExists();
                }
                if (Type != "Class")
                {

                }
                Relation derivationRelation = new Relation(new int[] { 9999, 1, 1, 100 }); // is a
                derivationRelation.CreateRelationIfNotExists(symB.SymbolID, symC.SymbolID); // simB is a simC
            }

            //if (string.IsNullOrEmpty(parrent)) 
            if (symA == null) return symB;
            //Symbol symA = new Symbol(parrent);
            if (symA.SymbolID[0] == 0 && symA.SymbolID[1] == 0)
            {
                symA.RegisterSymbolIfNotExists();
            }
            Relation relation = new Relation(new int[] { 2, 100, 9998, 1 }); // include
            relation.CreateRelationIfNotExists(symA.SymbolID, symB.SymbolID);
            return symB;
        }

        List<string[]> ParseDoc(XElement el, int level, Symbol parrentNamespace)
        {
            List<string[]> URIs = new List<string[]>();
            IEnumerable<XElement> elements = el.Elements();
            foreach (XElement elr in elements)
            {
                
                string elsss = elr.GetType().ToString();
                if (elsss == "System.Xml.Linq.XElement")
                {
                    
                    if (elr.Name.LocalName == "div")
                    {
                        if (elr.HasAttributes)
                            if (elr.FirstAttribute.Value.Contains(" current"))
                            {
                                // process current page
                                IEnumerable<XElement> childs = el.Elements();
                                foreach (XElement elC in childs)//if (elr.Name.LocalName == "a")
                                {
                                    if (elC.HasAttributes)
                                    if (elC.FirstAttribute.Value.Contains(" children"))
                                    {
                                        List<XElement> divs = elC.Elements().ToList();
                                        int i = 1;
                                        for (; i < divs.Count; i += 2) // iterate through all subtitles.
                                        {
                                            List<XElement> a = divs[i].Elements().ToList();
                                            string line = a[0].Value.ToString();
                                            for (int j = 0; j < level; j++) Console.Write("*");
                                            Console.WriteLine(line);
                                            string typ = line;
                                            string nume = line;
                                            while (typ.IndexOf(" ") > -1)
                                            {
                                                typ = typ.Substring(typ.IndexOf(" ")+1);
                                            }
                                            if (!typ.Equals(nume))
                                            {
                                                nume = nume.Substring(0, nume.Length - typ.Length - 1);
                                            }
                                            bool getURL = false;
                                            //decide URL to follow
                                            List<string> wantedTitlesList = wantedCollapsableTitles.ToList();
                                            wantedTitlesList.Remove("Namespaces");
                                            if (!(wantedTitlesList.Contains(typ) || wantedMappedTitles.Contains(typ)))
                                            {
                                                getURL = true;
                                            }
                                         
                                            XAttribute href = a.Attributes().Where(p => p.Name.LocalName == "href").FirstOrDefault();
                                            
                                            if (getURL)
                                            {
                                                //URIs.Add(new string[] { " "+level });
                

                                                //Symbol newParrent = parrentNamespace;
                                                if (!typ.Equals("Namespaces"))
                                                {
                                                    if (nume != "System")
                                                    {
                                                        //newParrent = 
                                                        //registerNamespace(parrentNamespace, identifier);
                                                        //parrentNamespace = image;
                                                    }
                                                }

                                                //if (href.Value.ToString() == "/en-us/library/system.componentmodel.licenseprovider.aspx") this.found=true;
                                                //URIs.Add(new string[] { "http://msdn.microsoft.com" + href.Value.ToString(), line, newParrent.SymbolID[0] + ":" + newParrent.SymbolID[1] + ":" + newParrent.Name }); //URLstring, identifierName
                                                URIs.Add(new string[] { nume, typ, "http://msdn.microsoft.com" + href.Value.ToString(), line }); //URLstring, identifierName
                                                //**************************************************************************************
                                                //reset vars
                                                pageTitle = line;
                                                //this.extractHrefs(doc, level--, parrentNamespace);  
                 
                                            }
                                        }
                                        
                                        // thoes are childrens each cildren contain 2 div. The second div contain a href
                                        
                                    }
                                }
                            }
                    }
                    

                }
                List<string[]> moreURI = ParseDoc(elr, level, parrentNamespace);
                    URIs.AddRange(moreURI);
            }
            return URIs;
        }

        internal void Fetch(string URL, Symbol parrent)
        {
            XDocument doc = LoadURL(URL);
            IEnumerable<XElement> elements = doc.Elements();
            List<string[]> hrefs = new List<string[]>();
            foreach ( XElement elr in elements)
            {
                hrefs.AddRange(ParseDoc(elr, 0, parrent)); // extract hrefs from menu and add namespaces from content (not typed entries) <parsing content>
                // href // line // type // identifier
                // name // type // href // line
            }
            for (int i = 0; i < hrefs.Count; i++)
            {
                Symbol newParrent = parrent;
                if (!("Namespaces".Equals(hrefs[i][1])) && (!wantedCollapsableTitles.Contains(hrefs[i][1])))
                {
                    newParrent = registerNamespace(parrent, hrefs[i][0]); //register namespaces
                }

                if (!unwantedToExplore.Contains(hrefs[i][1])) //.Equals("Namespaces"))
                {
                    Fetch(hrefs[i][2], newParrent); //fetch files
                }
            }
            List<string[]> identifiers = new List<string[]>();
            foreach (XElement elr in elements)
            {
                identifiers.AddRange(ParsePage(elr, 0)); // add namespaces from page content <parsing elements>
            }
            foreach (string[] identifier in identifiers)
            {
                if (wantedMappedTitles.Contains(identifier[1]))
                {
                    Symbol symbol = registerCollapsableTitle(parrent, identifier[0], identifier[1]); //register namespaces
                }
                else if (!unwantedCollapsableTitles.Contains(identifier[1]))
                {

                }
            }
        }

    }
}
