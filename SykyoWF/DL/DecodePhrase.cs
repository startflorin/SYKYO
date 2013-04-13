using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;

namespace WindowsFormsApplication1.CD
{
    class DecodePhrase
    {
        DataAccess.Options.LoggingSystemOptions logingOptions = new DataAccess.Options.LoggingSystemOptions();
        SymbolCollection symbolCollection = new SymbolCollection();
        OperatorCollection relationCollection = new OperatorCollection();

        /// <summary>
        /// transform a string of codes into a list of codes int[]
        /// </summary>
        /// <param name="internalRepresentation"></param>
        /// <returns></returns>
        List<int[]> CodeStringToIntegerString(string internalRepresentation)
        {
            List<string> ids = new List<string>();
            ids.AddRange(internalRepresentation.Split(' '));

            List<string[]> AllIDs = new List<string[]>();
            foreach (string StringID in ids)
            {
                if (StringID.Contains(':'))
                {
                    AllIDs.Add(StringID.Substring(1, StringID.Length - 2).Split(':'));
                }
                else
                {
                    AllIDs.Add(new string[] { StringID });
                }
            }

            List<int[]> intIDs = new List<int[]>();
            int[] currentID;// = new int[StringID.Length];
            foreach (string[] StringID in AllIDs)
            {
                currentID = new int[StringID.Length];
                for (int i = 0; i < StringID.Length; i++)
                {
                    int.TryParse(StringID[i], out currentID[i]);
                }
                intIDs.Add(currentID);
            }
            return intIDs;
        }

        public List<int[]> Decode(string text)
        {
            string ans = string.Empty;
            List<int[]> ints = CodeStringToIntegerString(text);


           // List<SymbolItem> result = symbolCollection.GetSymbolByID( new int[]{ ints[1][0], ints[1][1], 1, 0} );
                //
          //  string name = result[0].Name;
            return ints;
        }
    }
}
