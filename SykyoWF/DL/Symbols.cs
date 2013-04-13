using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.DL;

namespace WindowsFormsApplication1
{
    class Symbol
    {
        private int[] symbolID = new int[2];
        private string symbolName;
        public bool isNew = false;

        /// <summary>
        /// Internal representation of the ID
        /// </summary>
        public int[] SymbolID
        {
            get { return symbolID; }
        }
        /// <summary>
        /// Name of the relation in natural language
        /// </summary>
        public string Name
        {
            get 
            {
                
                if (string.IsNullOrEmpty(symbolName))
                {
                    symbolName = serverAccess.getSymbolNameByID(symbolID);
                }
                
                return symbolName;
            }
            set
            {
                symbolName = value;
                if (isNew)
                {
                    serverAccess.CreateSymbol(value);
                }
                else
                {
                    symbolID = serverAccess.getSymbolIDByName(symbolName);
                }
            }
        }



        public Symbol()
        {
        }

        public Symbol(string symbol)
        {
            Name = symbol;
        }
        public Symbol(string symbol, bool isNew)
        {
            this.isNew = true;
            Name = symbol;
        }

        /// <summary>
        /// When ID is provided, if the ID is not valid, it is created with the name reflecting ID; Else the name is determined.
        /// </summary>
        /// <param name="ID"></param>
        public Symbol(int[] ID)
        {
            symbolID = ID;

            if (!isExistentSymbolID())
            {
                if (string.IsNullOrEmpty(Name))
                {
                    symbolName = toString();
                }
                RegisterSymbolAsNew();
            }

            if (string.IsNullOrEmpty(Name))
            {
                symbolName = getSymbolName();
            }
        }


        private string getSymbolName()
        {
            return serverAccess.getSymbolNameByID(SymbolID);
        }

        private string toString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "[" + SymbolID[0] + ":" + SymbolID[1] + "]";
            }
            else
            {
                return Name;
            }
        }

        
        ServerAccess serverAccess = new ServerAccess();
        
        /// <summary>
        /// register a new symbol if the symbol does not exists
        /// </summary>
        public int[] RegisterSymbolIfNotExists()
        {
            int[] simbolID = {0, 0};
            if (isExistentSymbol())
                simbolID = GetSymbolId();
                else simbolID = RegisterSymbolAsNew();
            this.symbolID = simbolID;
            return simbolID;
        }

        private int[] GetSymbolId()
        {
            int[] simbolID = {0, 0};
            simbolID = serverAccess.getSymbolIDByName(Name);
            return simbolID;
        }

        private int[] RegisterSymbolAsNew()
        {
            int[] simbolID = { 0, 0 };
            simbolID = serverAccess.CreateSymbol(Name);
            return simbolID;
        }

        /// <summary>
        /// Check if there are symbols with this ID
        /// </summary>
        /// <returns></returns>
        private bool isExistentSymbolID()
        {
            if (serverAccess.CountOccurences(SymbolID) < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }        
        
        /// <summary>
        /// Check if there are symbols with this name
        /// </summary>
        /// <returns></returns>
        private bool isExistentSymbol()
        {
            if (serverAccess.CountOccurences(Name) < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CreateUnknown { get; set; }
    }
}
