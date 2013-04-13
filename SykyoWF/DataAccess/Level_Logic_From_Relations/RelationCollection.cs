#region Composition

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using MySql.Data.MySqlClient;
using WindowsFormsApplication1.DL;
using WindowsFormsApplication1.UI;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
using WindowsFormsApplication1.Level_Logic_From_Relations;
using DataPersistency.DL.ServerAccess;
using DataPersistency.DL.Logging;

#endregion Composition

namespace WindowsFormsApplication1.BL
{
    class RelationCollection
    {

        #region Variables
        // log options model
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions;
        public static ServerAccessInterface serverAccess;
        
        int[] IsExemplifiedBy = new int[] { 1, 100, 9999, 1 };
        int[] IsA = new int[] { 9999, 1, 1, 100 };

        int[] Include = new int[] { 2, 100, 9998, 1 };
        int[] IsIncludedIn = new int[] { 9998, 1, 2, 100 };

        int[] Have = new int[] { 3, 100, 9997, 1 };
        int[] IsPropertyOf = new int[] { 9997, 1, 3, 100 };

        int[] Increase = new int[] { 5, 10, 9995, 10 };
        int[] Decrease = new int[] { 5, -10, 9995, -10 };

        int[] IsDecreasedBy = new int[] { 9995, -10, 5, 10 };
        int[] IsIncreasedBy = new int[] { 9995, 10, 5, -10 };

        int[] Of = new int[] { 50, 50, 50, 50 };


        
        #endregion Variables

        public List<SymbolID> getLeafsByRelation(SymbolID symbol, RelationItem relation)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> returnedSymbols = new List<SymbolID>();
            List<int[]> listID = new List<int[]>();
            //listID = serverAccess.getLeafsByRelation(relation.RelationID, symbol.SymbolID, 0);
            foreach (int[] ID in listID)
            {
                //listSymbol.Add(new Symbol(ID));
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "SymbolID symbol", symbol.ToString() });
                thisMethod.Parameters.Add(new string[] { "RelationItem relation", relation.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID SymbolID in returnedSymbols)
                {
                    result += "{" + SymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedSymbols;
        }

        internal List<SymbolID> getFirstType(SymbolID symbol)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            //RelationItem relation = new RelationItem(new int[] { 9999, 1, 1, 100 });
            List<SymbolID> returnedSymbols = new List<SymbolID>();
            List<int[]> listID = new List<int[]>();
            //listID = serverAccess.getLeafsByRelation(relation.RelationID, symbol.SymbolID, 1);
            foreach (int[] ID in listID)
            {
                //listSymbol.Add(new SymbolID(ID));
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "SymbolID symbol", symbol.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID SymbolID in returnedSymbols)
                {
                    result += "{" + SymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedSymbols;
        }

        public List<SymbolID> PrepareRelationParticipant(List<int[]> symbolIDs)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> returnedSymbols = new List<SymbolID>();
            int currSymb = -1;
            int nextMul = 1;
                
            foreach (int[] symbID in symbolIDs)
            {
                currSymb++;
                if (symbID.Length == 1 && (symbID[0] != 0))
                {
                    if (currSymb < symbolIDs.Count)
                    {
                        //if (SymbIDs[currSymb + 1].Length>2)
                        {
                            nextMul = symbID[0];//SymbIDs[currSymb + 1][2] = symbID[0];
                        }
                    }
                    //preopedand.Add(new SymbolID(symbID, new int[] { 1, 0 }, string.Empty));
                }
                else if (symbID.Length == 2 && (symbID[0] != 0 || symbID[0] != 0))
                {
                    //=// returnedSymbols.Add(new SymbolID(symbID, new int[] { nextMul, 0 }, string.Empty));
                    nextMul = 1;
                }
                else if (!(symbID[0] != 0 || symbID[0] != 0))
                {
                    returnedSymbols.Add(new SymbolID(new Location(symbID[0])));
                }
                else
                {
                    //=// returnedSymbols.Add(new SymbolID(symbID, new int[] { 1, 0 }, "what"));
                }

            }
            if (symbolIDs.Count > 1)
            {
                List<SymbolID> preopedandIDs = AggregatePossesionToLeft(returnedSymbols);
                returnedSymbols = IntsToSymbs(preopedandIDs);
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> symbolIDs", symbolIDs.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID SymbolID in returnedSymbols)
                {
                    result += "{" + SymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedSymbols;
        }

        List<SymbolID> IntsToSymbs(List<SymbolID> ints)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> returnedSymbols = new List<SymbolID>();
            foreach (SymbolID ID in ints)
            {
                //if (ID.Length > 7)
                {
                    //=// returnedSymbols.Add(new SymbolID(new int[] { ID[0], ID[8] }, new int[] { 1, ID[7] }, string.Empty));
                }
                //else
                {
                    //=// returnedSymbols.Add(new SymbolID(new int[] { ID[0], ID[1] }, new int[] { ID[2], ID[3] }, string.Empty));
                }
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> ints", ints.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID SymbolID in returnedSymbols)
                {
                    result += "{" + SymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedSymbols;
        }

        List<SymbolID> AggregatePossesionToLeft(List<SymbolID> simbs)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> initial = new List<SymbolID>();
            foreach (SymbolID ini in simbs)
            {
                initial.Add(ini);
            }
            List<SymbolID> returnedIDs = new List<SymbolID>();
            //if (simbs.Count > 1)
            //{
                int i = 1;
                while (initial.Count > 0)
                {
                    returnedIDs = new List<SymbolID>();
                    i = -1;
                    for (int j = 0; j < initial.Count; j++)
                    {
                        //=// Find if is relation or is symbol
                        //if (initial[j].Length > 4)
                        {
                           // if (initial[j].Location.A == 0 && initial[j].Location.B == 0 && initial[j][2] == 50 && initial[j][3] == 50 && initial[j][4] == 0 && initial[j][5] == 0)
                            {
                                i = j;
                            }
                        }
                    }
                    if (i > 0)
                    { // I found the right term
                        List<SymbolID> right = new List<SymbolID>();
                        right.AddRange(initial);
                        right.RemoveRange(0, i+1);
                        int z = -1;
                        for (int j = 0; j < i; j++)
                        {
                            //=//
                            //if (initial[j].Length > 4)
                            {
                                //if (initial[j].Location.A == 0 && initial[j].Location.B == 0 && initial[j][2] == 50 && initial[j][3] == 50 && initial[j][4] == 0 && initial[j][5] == 0)
                                {
                                    z = j;
                                }
                            }
                        }

                        List<SymbolID> left = new List<SymbolID>();
                        left.AddRange(initial);
                        left.RemoveRange(i, left.Count-i);
                        left.RemoveRange(0, z + 1);

                        List<SymbolID> RightLeft = new List<SymbolID>();
                        RightLeft.AddRange(right);
                        RightLeft.AddRange(left);

                        returnedIDs.AddRange(AggregatePossesionToRight(RightLeft));
                        if (z > 0)
                        {
                            initial.RemoveRange(i - 1, initial.Count -i+1);
                            initial.AddRange(returnedIDs);
                        }
                        else 
                        {
                            initial.Clear();
                        }
                    }
                    else
                    {
                        returnedIDs.AddRange(AggregatePossesionToRight(initial));
                        initial.Clear();
                    }
                }
                
            //}
                #region LOG
                //=== CUSTOM LOGGER===================================================================================
                ElementMethod thisMethod = new ElementMethod();
                int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
                if (logLevel >= 3) // code
                {
                    System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                    thisMethod.ElementNamespaceName = this.GetType().Namespace;
                    thisMethod.ElementClassName = this.GetType().Name;
                    thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
                }
                if (logLevel >= 2) // parameters
                {
                    thisMethod.Parameters.Add(new string[] { "List<SymbolID> simbs", simbs.ToString() });
                }
                if (logLevel >= 1) // results
                {
                    string result = string.Empty;
                    foreach (SymbolID ID in returnedIDs)
                    {
                        result += "{" + ID.ToString() + "}";
                    }
                    thisMethod.Result = result;
                }
                LoggingSystem.LogMethod = thisMethod;

                //====================================================================================================
                #endregion LOG
                return returnedIDs;
        }

        List<SymbolID> AggregatePossesionToRight(List<SymbolID> simbs)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            if (simbs.Count > 1)
            for (int i = 1; i < simbs.Count; i++)
            {
                //while (currentIncrement.)
                //{
                //List<SymbolID> currentIncrement = SolvePossesion(new List<int[]> { new int[] { simbs[i - 1][0], simbs[i - 1][simbs[i - 1].Length-1] } }, new List<int[]> { simbs[i] });
                //if (currentIncrement.Count == 1)
                {
                    simbs.RemoveRange(i - 1, 2);
                    //simbs.Insert(i-1, currentIncrement[0]);
                    i--;
                }
                //}
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> simbs", simbs.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID rl in simbs)
                {
                    result += "{" + (rl) + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return simbs;
        }

        #region DeterminePosesion

        List<int[]> SolvePossesion (List<int[]> simbA, List<int[]> simbB)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<int[]> returnedIDs = new List<int[]>();
            returnedIDs = HaveA(simbA, simbB);
            if (returnedIDs.Count > 0 && (returnedIDs[0][0] != 0 || returnedIDs[0][1] != 0))
            {
                return returnedIDs;
            }
            returnedIDs = IncludeA(simbA, simbB);
            if (returnedIDs.Count>0 && (returnedIDs[0][0] != 0 && returnedIDs[0][1] != 0))
            {
                return returnedIDs;
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> simbA", simbA.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (int[] rl in returnedIDs)
                {
                    result += "{" + ArrayToString(rl) + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return new List<int[]>() { simbA[0], simbB[0] };
        }

        List<int[]> HaveA(List<int[]> simbA, List<int[]> simbB)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<int[]> returnedIDs = new List<int[]>();
            //=// returnedIDs = serverAccess.IsTransitive(new List<int[]>(), Have, simbA[0], new int[] { 1, 0 }, simbB[0], new int[] { 1, 0 }, 10);
            if (returnedIDs.Count > 0 && (returnedIDs[0][0] != 0 && returnedIDs[0][1] != 0))
            {
                return returnedIDs;
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> simbA", simbA.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (int[] rl in returnedIDs)
                {
                    result += "{" + ArrayToString(rl) + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedIDs;
        }

        List<int[]> IncludeA(List<int[]> simbA, List<int[]> simbB)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<int[]> returnedIDs = new List<int[]>();
            //=// returnedIDs = serverAccess.IsTransitive(new List<int[]>(), Include, simbA[0], new int[] { 1, 0 }, simbB[0], new int[] { 1, 0 }, 10);
            if (returnedIDs.Count > 0 && (returnedIDs[0][0] != 0 && returnedIDs[0][1] != 0))
            {
                return returnedIDs;
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<int[]> simbA", simbA.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (int[] rl in returnedIDs)
                {
                    result += "{" + ArrayToString(rl) + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedIDs;
        }

        #endregion DeterminePosesion

        public List<RelationItem> isTransitive(List<SymbolID> preoperand, List<OperatorID> relation, List<SymbolID> postoperand)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // first of all prepare solve logical opperations
            List<int[]> returnedIDs = new List<int[]>();
            List<RelationItem> returnedRelations = new List<RelationItem>();
            foreach (SymbolID preoperandItem in preoperand)
            {
                foreach (SymbolID postoperandItem in postoperand)
                {
                    if (postoperandItem.Location.A == -100 && postoperandItem.Location.B == 100)
                    {
                        //returnedIDs.AddRange(serverAccess.GetContent(relation[0].ID/*new int[] { 2, 100, 1, 0, 9998, 1 }*/, preoperandItem.Multiplicity, preoperandItem.Location, 10));
                        //returnedIDs.AddRange(serverAccess.GetContent(relation[0].ID/*new int[] { 2, 100, 1, 0, 9998, 1 }*/, preoperandItem.Multiplicity, preoperandItem.Location, 10));
                    }
                    else
                    {
                        //=// returnedIDs.AddRange(serverAccess.IsTransitive(new List<int[]>(), relation[0].ID/*new int[] { 2, 100, 1, 0, 9998, 1 }*/, preoperandItem.Location, preoperandItem.Multiplicity, postoperandItem.Location, postoperandItem.Multiplicity, 10));
                    }

                    if (returnedIDs.Count > 0)
                    {
                        /*
                        bool HaveNoSuchReference = true;
                        //find existent reference
                        List<int[]> referedSymbols = new List<int[]>();
                            //referedSymbols = serverAccess.GetValue(new int[] { 2, 100, 9998, 1 }, preoperandItem.Location, postoperandItem.Location, 10); // 4 items

                        if (referedSymbols.Count < 1)
                        {
                            //referedSymbols.Add(serverAccess.AssignValue(new int[] { 2, 100, 0, 1 }, preoperandItem.Location, postoperandItem.Location)); //r2r3
                        }
                        else
                        {
                            //referedSymbols = serverAccess.GetReferince(new int[] { 2, 100, 9998, 1 }, preoperandItem.Location, postoperandItem.Location, 10); // 4 items
                        }

                        List<int[]> ValidReferinces = new List<int[]>();
                        foreach (int[] MultiplicityReference in referedSymbols)
                        {
                            if (MultiplicityReference[2] != 0 && MultiplicityReference[3] != 0)
                            {
                                ValidReferinces.Add(MultiplicityReference);
                            }
                            else
                            {
                                HaveNoSuchReference = true;
                            }
                        }
                        if (referedSymbols.Count < 0 && HaveNoSuchReference == true)
                        {
                            //create reference
                            //referedSymbols.Add(serverAccess.ReAssignValue(new int[] { 2, 100, 1998, 0 }, candidate[0], SymbID[1]));
                            //referedSymbols.Add(serverAccess.ReAssignValue(new int[] { 2, 100, 1998, 0 }, preoperandItem.Location, postoperandItem.Location));
                        }
                        foreach (int[] MultiplicityReference in ValidReferinces)
                        {
                            result.Add(new int[] { MultiplicityReference[2], MultiplicityReference[3] });
                        }
                        */
                    }
                    else
                    {
                        //if (S
                        //serverAccess.CreateRelation(new int[] { 2, 100, 1, 0, 1998, 1 }, preoperandItem.Multiplicity, preoperandItem.Location, postoperandItem.Multiplicity, postoperandItem.Location);
                        //serverAccess.CreateRelationAsNew(new int[] { 2, 100, 1998, 1 }, SymbID[0], SymbID[1]);
                    }

                    foreach (int[] itemID in returnedIDs)
                    {
                        if (itemID.Count() > 7)
                        {
                            int offset = itemID[8];
                            int[] rel = new int[] { itemID[2], itemID[3] };
                            int[] refA = new int[] { itemID[0], itemID[1] };
                            int[] mul = new int[] { itemID[6], itemID[7] };
                            int[] refB = new int[] { itemID[4], itemID[5] };
                            //sresult.Add(new RelationItem(offset, rel, refA, refB, mul, string.Empty));
                            SymbolCollection symbolCollectionA = new SymbolCollection();
                            //=//symbolCollectionA.AddRange(IntsToSymbs(new List<int[]>() { refA }));
                            SymbolCollection symbolCollectionB = new SymbolCollection();
                            //=//symbolCollectionB.AddRange(IntsToSymbs(new List<int[]>() { refB }));
                            //=// returnedRelations.Add(new RelationItem(symbolCollectionA, new OperatorID(new int[] { }, ""), symbolCollectionB));
                        }
                    }
                }
            }

            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "relation", relation.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (RelationItem rl in returnedRelations)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnedRelations;
        }

        public static string ProcessMode { get; set; }

        internal int[] GetParameterValueAt(int[] symID)
        {
            //Relation r = new Relation();
            //return serverAccess.GetAssociations(new int[] { -100, 100, 0, 0 }, symID, new int[] { 0, 0 });
            return new int[] { 0 };
        }

        private List<RelationItem> createRelation(List<SymbolID> preop, List<OperatorID> res, List<SymbolID> postop)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<int[]> resultIDs = new List<int[]>();
            List<RelationItem> sresult = new List<RelationItem>();
            //=// if (postop[0].Multiplicity[0] != 0 && postop[0].Multiplicity[1] == 0)
            {
                //res[0].ID[3] = postop[0].Multiplicity[0];
            }

            //=// resultIDs = serverAccess.CreateRelation(res[0].ID, preop[0].Multiplicity, preop[0].Location, postop[0].Multiplicity, postop[0].Location);
            foreach (int[] itemID in resultIDs)
            {
                int offset = itemID[6];
                int[] rel = new int[] { itemID[2], itemID[3] };
                int[] mul = new int[] { itemID[6], itemID[7] };
                int[] refA = new int[] { itemID[0], itemID[1] };
                int[] refB = new int[] { itemID[4], itemID[5] };
                //sresult.Add(new RelationItem(offset, rel, refA, refB, mul, string.Empty));
                //sresult.Add(new RelationItem(offset, rel, refA, refB, mul, string.Empty));
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "List<SymbolID> preop", preop.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (RelationItem rl in sresult)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return sresult;
        }

        int isLoopFlag = -1;

        public RelationCollection(ServerAccessInterface serverAccessReference, DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsReferince)
        {
            logingOptions = logingOptionsReferince;
            serverAccess = serverAccessReference;
        }

        public RelationCollection()
        {
            // TODO: Complete member initialization
        }
        int IsLoopFlag(int[] token)
        {
            return -1;
        }

        /// <summary>
        /// Find relation component. Prepare relation members
        /// </summary>
        internal List<RelationItem> SolveCurrentRelation(List<int[]> relation)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<RelationItem> returnedRelations = new List<RelationItem>();

            string loopFlag = string.Empty;
            bool relationFound = false;
            List<int[]> Temp = new List<int[]>();
            List<int[]> Preoperand = new List<int[]>();
            List<int[]> Operator = new List<int[]>();
            List<int[]> Postopperand = new List<int[]>();
            foreach (int[] token in relation)
            {
                isLoopFlag = IsLoopFlag(token);
                if (isLoopFlag > -1)
                {
                    //loopFlag = "if";
                }
                if (relationFound)
                {
                    Postopperand.Add(token);
                }
                else
                {
                    if (token.Length == 6)
                    {
                        if (token[0] != 0 || token[1] != 0 || token[2] != 50 || token[3] != 50 || token[4] != 0 || token[5] != 0)
                        {
                            Operator.Add(token);
                            relationFound = true;
                        }
                        else
                        {
                            Preoperand.Add(token);
                        }
                    }
                    else
                    {
                        Preoperand.Add(token);
                    }
                }
            }
            Temp.AddRange(Preoperand);
            List<SymbolID> preop = PrepareRelationParticipant(Temp);

            if (Preoperand.Count > 0 && Operator.Count > 0 && Postopperand.Count > 0)
            {
                //List<SymbolID> preop = PrepareRelationParticipant(Preoperand);
                List<SymbolID> postop = PrepareRelationParticipant(Postopperand);
                List<OperatorID> res = new List<OperatorID>();
                //=// res.Add(new OperatorID(Operator[0], string.Empty));
                returnedRelations = isTransitive(preop, res, postop);
                if (returnedRelations.Count < 1 && serverAccess.AcceptRelations)
                {
                    returnedRelations = createRelation(preop, res, postop);
                }
            }
            else
            {
                foreach (SymbolID item in preop)
                {
                    returnedRelations.Add(SymbolToRel(item));
                }
            }

            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "relation", relation.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (RelationItem rl in returnedRelations)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return returnedRelations;
        }

        RelationItem SymbolToRel(SymbolID s)
        {

            SymbolCollection symbolCollectionA = new SymbolCollection();
            symbolCollectionA.AddRange(new List<SymbolID>() { s });
            //return new RelationItem(0, new int[] { 0, 0 }, s.ID, new int[] { 0, 0 }, new int[] { 0, 0 }, string.Empty);
            //return new RelationItem(0, new int[] { 0, 0 }, s.ID, new int[] { 0, 0 }, new int[] { 0, 0 }, string.Empty);
            return null;//=//  new RelationItem(symbolCollectionA, new OperatorID(), new SymbolCollection());
        }

        private string ArrayToString(int[] array)
        {
            string result = string.Empty;
            result += "[";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
                if (i < array.Length - 1)
                {
                    result += ":";
                }
            }
            result += "]";
            return result;
        }
    }
}
