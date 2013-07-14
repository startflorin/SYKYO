using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.CD;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
using WindowsFormsApplication1.Level_Logic_From_Relations;
using DataPersistency.DL.ServerAccess;

namespace WindowsFormsApplication1
{
    class QueryEvaluator
    {
        // Loging Model
        static DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel = new DataPersistency.BL.UserOptions.LoggingSystemOptions();
        public static ServerAccessInterface serverAccess = new ServerAccessMySQL(null);

        RelationCollection logics = new RelationCollection(serverAccess, logingOptionsModel);
        SymbolCollection symbol = new SymbolCollection(serverAccess, logingOptionsModel);
        OperatorCollection relation = new OperatorCollection();
        
        List<object> questionArray = new List<object>();
        CodePhrase codePhrase = new CodePhrase();
        DecodePhrase decodePhrase = new DecodePhrase();

        public string Decode(string str)
        {
            string result = "";
            List<int[]> ids = decodePhrase.Decode(str);
            List<int[]> ids2 = new List<int[]>();
            bool many = false;
            foreach (int[] id in ids)
            {
                if (id.Count() == 1 && id[0] >1)
                {
                    result += id[0] + " ";
                    if (id[0] != 0)
                    {
                        many = true;
                    }
                }
                if (id.Count() > 1 && id[1] != 0)
                {
                    int symbNr = id[0];
                    int offset = id[1];
                    ids2.AddRange(relation.getRecord(symbNr, offset));
                }
                if (id.Count() > 1 && id[1] == 0)
                {
                    SymbolID d = new SymbolID();
                    d.Location.A = id[0];
                    List<SymbolID> symb = symbol.GetSymbolByID(d);
                    result += symb[0].Names[0];
                    if (many)
                    {
                        result += "s";
                    }
                    result += " ";
                }
            } 
            if (result=="")
            foreach (int[] id in ids2)
            {
                if (id.Count() > 1 && id[1] != 0)
                {
                    int symbNr = id[0];
                    int offset = id[1];
                    ids2 = relation.getRecord(symbNr, offset);
                }
                if (id.Count() > 1 && id[1] == 0)
                {
                    try
                    {

                        SymbolID d = new SymbolID();
                        d.Location.A = id[0];
                        List<SymbolID> symb = symbol.GetSymbolByID(d);
                        if (!symb[0].Names[0].Equals(" * "))
                        {
                            result += symb[0].Names[0] + " ";
                        }
                    }
                    catch (Exception t) { }
                }
            }
            return result;

        }

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
         

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="mode">0 = none, 1=symbols only, 2=all</param>
        /// <returns></returns>
        public string Evaluate(string query, int mode)
        {
            string HumanReadableResult = "";
            string internalRepresentation = codePhrase.CodeMyPhrase(query);
            internalRepresentation = codePhrase.codeSanitize(internalRepresentation);
            if (mode < 2)
            {
                MainForm.CodeTextBoxText = internalRepresentation;
                return HumanReadableResult;
            }

            List<int[]> intIDs = CodeStringToIntegerString(internalRepresentation);
            
            List<RelationItem> lir = logics.SolveCurrentRelation(intIDs);
            if (lir.Count == 0)
            {
                //if (RelationCollection.serverAccess.AcceptRelations)
                {
                    lir = logics.SolveCurrentRelation(intIDs);
                }
                //lir.Add(RelationItem.Unknown);
                //lir.Add(RelationItem.Unknown);
                if (lir.Count > 0)
                {
                    return lir[0].ToString();
                }
                return null;
            }
            else return lir[0].ToString();
            /*
            //List<int[]> ids = CodeStringToIntegerString(HumanReadableResult);
            List<string> ids = new List<string>();
            ids.AddRange(internalRepresentation.Split(' '));
            //intIDs;
            
                //= ids.ElementAt(0).Substring(1, ids.ElementAt(0).Length - 2).Split(':');
            
            string[] idString = ids.ElementAt(0).Substring(1, ids.ElementAt(0).Length-2).Split(':');
            int[] id = new int[]{ 0, 0 };
            int.TryParse(idString[0], out id[0]);
            int.TryParse(idString[1], out id[1]);
            SymbolID simA = new SymbolID(id);
            
            string[] idRel = ids.ElementAt(1).Substring(1, ids.ElementAt(1).Length-2).Split(':');
            int[] idR = new int[]{ 0, 0, 0, 0 };
            int.TryParse(idRel[0], out idR[0]);
            int.TryParse(idRel[1], out idR[1]);
            int.TryParse(idRel[2], out idR[2]);
            //int.TryParse(idRel[3], out idR[3]);
            int.TryParse(ids.ElementAt(2), out idR[3]);
            
            OperatorItem relation = new OperatorItem(idR);

            List<int[]> result = new List<int[]>();
            if (relation.RelationID[0] < 0)
            {
                if (relation.RelationID[1] == -5 || relation.RelationID[1] == 5 || relation.RelationID[1] >= 100 || relation.RelationID[1] <= -100 )
                {
                    result = relation.SolveAssociation(relation.RelationID, simA.SymbolID, new int[] { 0, 0 });
                }
                if (result.Count < 1)
                {
                    //if (RelationItem.CreateUnknown)
                    {
                        //int[] relationPosition = relation.CreateOrReassignValue(relation.RelationID, simA.SymbolID, new int[] { 0, 0 });
                        //result.Add(relationPosition);
                    }
                }
            }
            else
            {
                string[] idStringS = ids.ElementAt(2).Substring(1, ids.ElementAt(2).Length - 2).Split(':');
                int[] idS = new int[] { 0, 0 };
                int.TryParse(idStringS[0], out idS[0]);
                int.TryParse(idStringS[1], out idS[1]);
                Symbol simB = new Symbol(idS);
                result = relation.SolveRelation(relation.RelationID, simA.SymbolID, simB.SymbolID);

                if (result.Count < 1 || ((result.Count == 1) && (result[0][0] == 0 && result[0][1] == 0)))
                {
                    if (Relation.CreateUnknown)
                    {
                        int[] relationPosition = relation.CreateRelationIfNotExists(simA.SymbolID, simB.SymbolID);
                        result.Add(relationPosition);
                    }
                }
            }
            */

            /*
            int[] relationPosition = relation.GetRelationPosition(relation.RelationID, simA.SymbolID, simB.SymbolID);
            if (relationPosition[0] == 0 && relationPosition[1] == 0)
            {
                if (Relation.CreateUnknown)
                {
                    relationPosition = relation.CreateRelationIfNotExists(simA.SymbolID, simB.SymbolID);
                }
            }
            if (relationPosition[0] > 0) return true; else return false;
           
            if (result.Count > 0)
            {
                foreach (int[] ID in result)
                {
                    HumanReadableResult += "[" + ID[0] + ":" + ID[1] + "]";
                }
            }
            return HumanReadableResult;
              * */
        }

        public QueryEvaluator(DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions)
        {
            logingOptionsModel = logingOptions;
        }
    }
}
