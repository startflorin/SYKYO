using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
using DataPersistency.DL.ServerAccess;

namespace WindowsFormsApplication1.UI
{
    public partial class MSDNReader : Form
    {
        public MSDNReader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             URLIncrementalReader(true);

        }

        public void URLIncrementalReader(bool fakeData)
        {
                Class1 c1 = new Class1();
                //System.IO.StreamWriter file = File.AppendText("P:\\TEST\\test.html");
                //file.Write("<style> a { text-decoration: none; color: black; } </style>\n\r");
                //file.Close();
                string URL = "http://msdn.microsoft.com/en-us/library/gg145045.aspx";
                List<SymbolID> sims = new SymbolCollection().GetSymbolCollection("MSDN");
                c1.Fetch(URL, sims[0]);
        }
    }

        /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class Class1
    {
        SymbolCollection symbolCollection = new SymbolCollection();
        OperatorCollection relationCollection = new OperatorCollection();
                
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
        bool needIdentifyer = false;
        bool needTable = false; // once a wanted collapsable title is found, I need to find a table
        bool needRow = false; // once a wanted fable is found, I need to find a row in that wanted table
        bool needHref = false; // once a row was found, I need to find td(s)
        string rowData; // some elements have data as visibility and element type.
        string elementHref; // The href of the element.
        string elementIdentifier; // Identifier
        string[] wantedCollapsableTitles = { "Namespaces", "Operators", "Delegates", "Constructors", "Classes", "Structures", "Interfaces", "Enumerations", "Properties", "Methods", "Fields", "Members", "Conversions", "Events" }; //in namespace
        string[] wantedMappedTitles = { "Namespace", "Operator", "Delegate", "Constructor", "Class", "Structure", "Interface", "Enumeration", "Property", "Method", "Field", "Member", "Conversion", "Event" }; //in namespace
        string[] unwantedToExplore =        { "Operators", "Delegates", "Constructors", "Properties", "Methods", "Fields", "Members", "Conversions", "Events" }; //in namespace
        string[] unwantedCollapsableTitles = { "" }; //in namespace
        bool newRow = true;
        List<ELEMENT> ParsePage(XElement el, int level)
        {
            List<ELEMENT> identifiers = new List<ELEMENT>();
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
                                        ELEMENT.Type = collapsableTitle;
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
                                            needIdentifyer = false;
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
                                    needIdentifyer = false;
                                    needHref = false; //just for safe so that if I am waiting for a row to ignore td(s)
                                }
                                else
                                {
                                    needRow = false;
                                    needIdentifyer = false;
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
                                ELEMENT.Attributes = rowAtr.Value;
                                rowData = rowAtr.Value;
                            }
                        }
                        needHref = true;
                        newRow = true;
                        needIdentifyer = true;
                        

                                
                    }
                    /*
                    if (newRow)
                    {
                        IEnumerable<XElement> tds = elr.Elements();
                        foreach (XElement td in tds)
                        {*/
                            // inside tab I can find a href or a span
                    if (needTable && needRow && needIdentifyer && needHref && elr.Name.LocalName == "a")
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
                                        ELEMENT.URL = hrefAtr.Value;
                                        ELEMENT.Name = elr.Value;
                                        needIdentifyer = false;
                        
                                    }
                                }
                            }
                    if (needTable && needRow && needIdentifyer && needHref && elr.Name.LocalName == "span")
                            {
                                elementHref = "";
                                if (newRow) elementIdentifier = elr.Value;
                                ELEMENT.Name = elr.Value;
                                needIdentifyer = false;
                        
                            }
                            if (newRow && (!string.IsNullOrEmpty(elementIdentifier)))
                            {
                                if (wantedMappedTitles.Contains(collapsableTitle))
                                {
                                    if (!string.IsNullOrEmpty(elementIdentifier))
                                    {
                                        if (!string.IsNullOrEmpty(ELEMENT.Name))
                                        {
                                            identifiers.Add(new ELEMENT());
                                            ELEMENT.Name = string.Empty;
                                        }//identifiers.Add(new ELEMENT());
                                        MoreItterationNeeded = false;
                                        elementIdentifier = null;
                                        newRow = false;
                                        needIdentifyer = false;
                        
                                    }
                                }/*
                            }
                        }*/
                    }
                }
                //MoreItterationNeeded = ! string.IsNullOrEmpty(ELEMENT.URL);
                if (MoreItterationNeeded)
                {
                    identifiers.AddRange(ParsePage(elr, level++));
                }
            }
            return identifiers;
        }

        internal void Fetch(string URL, SymbolID parrent)
        {


            XDocument doc = LoadURL(URL);
            IEnumerable<XElement> elements = doc.Elements();
            List<ELEMENT> SYMBOLS = new List<ELEMENT>();
            foreach ( XElement elr in elements)
            {
                SYMBOLS.AddRange(ParsePage(elr, 0)); // extract hrefs from menu and add namespaces from content (not typed entries) <parsing content>
            }

            foreach (ELEMENT e in SYMBOLS)
            {
                List<SymbolID> syms = symbolCollection.ForceSymbolCollection(e.myName);
                relationCollection.GetAllOperators();
                //List<SymbolID> rels = relationCollection.SetRelation(new List<SymbolID> { parrent }, new List<OperatorItem> {rrr[0]}, syms);
                if (!string.IsNullOrEmpty(e.myURL))
                {
                    Fetch(e.myURL, syms[0]);
                }
            }
        }
    }

    public class ELEMENT
    {
        public static string Type;
        public static string Name;
        public static string URL;
        public static string Attributes;

        public string myType;
        public string myName;
        public string myURL;
        public string myAttributes;

        public ELEMENT() 
        {
            this.myName = ELEMENT.Name;
            this.myType = ELEMENT.Type;
            this.myURL = ELEMENT.URL;
            this.myAttributes = ELEMENT.Attributes;
        }
        public ELEMENT(string Name, string Type, string URL )
        {
            ELEMENT.Type = Type;
            ELEMENT.Name = Name;
            ELEMENT.URL = URL;
        }
    }

}
