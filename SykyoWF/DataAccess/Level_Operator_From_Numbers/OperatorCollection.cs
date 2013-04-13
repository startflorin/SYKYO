using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;
using WindowsFormsApplication1.BL;

namespace WindowsFormsApplication1.Level_Operator_From_Numbers
{
    class OperatorCollection
    {
        #region VARIABLES

        public List<OperatorID> operatorCollection = new List<OperatorID>();

        public static ServerAccessInterface serverAccess;
        bool CreateUnknown = false;

        OperatorID ID = new OperatorID();
        string Name = string.Empty;

        #endregion VARIABLES
        
        #region CONSTRUCTOR

        public OperatorCollection()
        {
        }

        public OperatorCollection(ServerAccessInterface serverAccessReference)
        {
            serverAccess = serverAccessReference;
        }

        #endregion CONSTRUCTOR

        #region GET

        public List<SymbolID> SolveRelation(SymbolID symbol1, OperatorID operatorID,  SymbolID symbol2, int limit = 10)
        {
            // Algebric logics
            // null        == [ 0,  0]   // nothing, ignorance
            // measure     == [ 0,+/-]   // levels, needs, satisfacrion, states
            // event       == [ +,  -]   // decisions, state modifiers
            // object      == [ +,0/+]   // static objects, existence
            // incertitude == [ -,  +]   // cumulator, unoriented, unknown
            // collision   == [ -,  -]   // collision, boundary, buffer, meta
            List<SymbolID> resultNULL = new List<SymbolID>();


            // CASE 0:
            // relation is unknown
            if (operatorID.Location.IR == 0 && operatorID.Location.Var == 0 && operatorID.Multiplicity.Direction == 0 && operatorID.Multiplicity.Level == 0) // SOMETHING ? SOMETHING
            {
                List<SymbolID> result = new List<SymbolID>();
                return result;
            }

            // CASE 1:
            // both operands are nulls
            if (symbol1.Location.A == 0 && symbol1.Location.B == 0 && symbol2.Location.A == 0 && symbol2.Location.B == 0) // [ 0,  0] => [ 0,  0]  // NULL => NULL
            {
                List<SymbolID> result = new List<SymbolID>();
                result.Add(SymbolID.Null);
                return result; // NULL
            }

            // CASE 2:
            // first operand is null
            if ((symbol1.Location.A == 0 && symbol1.Location.B == 0) && (symbol2.Location.A > 0 && symbol2.Location.B >= 0)) // [ 0,  0] => [ +,  +]  // NULL => SOMETHING
            {
                List<SymbolID> result = new List<SymbolID>();
                result.Add(SymbolID.Null);
                return result; // NULL
            }

            // CASE 3:
            // second operand is null
            if ((symbol1.Location.A > 0 && symbol1.Location.B >= 0) && (symbol2.Location.A == 0 && symbol2.Location.B == 0)) // [ +,  +] => [ 0,  0] // SOMETHING => NULL
            {
                List<SymbolID> result = new List<SymbolID>();
                result.Add(SymbolID.Null);
                return result; // NULL
            }

            // CASE 4:
            // both operands are not-nulls
            if ((symbol1.Location.A < 0 && symbol1.Location.B > 0) && (symbol2.Location.A < 0 || symbol2.Location.B > 0)) // [ -,  +] => [ -,  +]  // ? => ?
            {
                List<SymbolID> result = new List<SymbolID>();
                result.Add(SymbolID.Null);
                return result; // NULL
            }

            // CASE 5:
            // unknown preoperand
            if ((symbol1.Location.A < 0 && symbol1.Location.B > 0) && (symbol2.Location.A > 0 || symbol2.Location.B >= 0)) // [ -,  +] => [ +,  +]  // ? => SOMETHING
            {
                //return FindContainer(r, a, b, limit);
            }

            // CASE 6:
            // both unknown postoperand
            if ((symbol1.Location.A > 0 && symbol1.Location.B >= 0) && (symbol2.Location.A < 0 && symbol2.Location.B > 0)) // [ +,  +] => [ -,  +]  // SOMETHING => ?
            {
                //return FindContent(r, a, b, limit);
            }

            // CASE 4:
            // both operands are not-nulls
            if ((symbol1.Location.A > 0 && symbol1.Location.B >= 0) && (symbol2.Location.A > 0 || symbol2.Location.B >= 0)) // [ +,  +] => [ -,  +]  // SOMETHING => SOMETHING
            {
                return IsTransitive(operatorID, symbol1, symbol2, 10);
            }

            return resultNULL;
        }

        private List<SymbolID> IsTransitive(OperatorID operatorId, SymbolID symbolFrom, SymbolID symbolTo, int limit)
        {
            List<SymbolID> resultedSymbols = new List<SymbolID>();
            List<SymbolID> ReturnedIDs = new List<SymbolID>();//=// serverAccess.IsTransitive(new List<Location>(), operatorId.Location, symbolFrom.Location, symbolTo.Location, limit);
            foreach (SymbolID ints in ReturnedIDs)
            {
                if (ints.Exists)
                {
                    resultedSymbols.Add(new SymbolID(ints.Location, ints.Multiplicity, ""));
                }
                else
                {
                    resultedSymbols.Add(SymbolID.Null);
                }
            }
            if (resultedSymbols.Count < 1)
            {
                resultedSymbols.Add(SymbolID.Null);
            }
            return resultedSymbols;
        }

        public List<SymbolID> InRelation(List<SymbolID> aa, List<OperatorID> r, List<SymbolID> bb)
        {
            List<SymbolID> ResultedSymbols = new List<SymbolID>();
            if (aa[0].IsUnknown())
            {
                foreach (SymbolID b in bb)
                {
                    List<SymbolID> resultedIDs = GetLeafsByRelation(b, r[0]);
                    ResultedSymbols.AddRange(resultedIDs);
                }
                return ResultedSymbols;
            } 
            if (bb[0].IsUnknown())
            {
                foreach (SymbolID a in aa)
                {
                    List<SymbolID> resultedIDs = GetLeafsByRelation(a, r[0]);
                    ResultedSymbols.AddRange(resultedIDs);
                }
                return ResultedSymbols;
            }
            foreach (SymbolID a in aa)
            {
                foreach (SymbolID b in bb)
                {
                    List<SymbolID> resultedIDs = IsTransitive(r[0], a, b, 10);
                    ResultedSymbols.AddRange(resultedIDs);
                }
            }
            return ResultedSymbols;
        }

        private List<OperatorID> GetRelationOperatorID(string Name)
        {
            List<OperatorID> ReturnedOperators = new List<OperatorID>();
            List<OperatorID> operatorIDs = serverAccess.GetOperatorsByName(Name, 0, 1);
            foreach (OperatorID ID in operatorIDs)
            {
                if (ID.Exists)
                {
                    ID.Names.Add(Name);
                    ReturnedOperators.Add(ID);
                }
            }
            return ReturnedOperators;
        }


        #endregion GET

        #region SET

        public List<SymbolID> SetRelation(List<SymbolID> aa, List<OperatorID> r, List<SymbolID> bb)
        {
            List<SymbolID> ResultedSymbols = new List<SymbolID>();
            foreach (SymbolID a in aa)
            {
                foreach (SymbolID b in bb)
                {
                    List<SymbolID> resultedIDs = new List<SymbolID>();//=// SetRelation(a, r, b);
                    ResultedSymbols.AddRange(resultedIDs);
                }
            }
            return ResultedSymbols;
        }

        private List<SymbolID> SetRelation(SymbolID a, OperatorID r, SymbolID b)
        {
            List<SymbolID> resultedSymbols = new List<SymbolID>();
            List<SymbolID> ReturnedIDs = new List<SymbolID>();//=//  serverAccess.CreateRelation(r, a.Location, b.Location);
            foreach (SymbolID ints in ReturnedIDs)
            {
                resultedSymbols.Add(new SymbolID(ints.Location, ints.Multiplicity, ""));
            }
            return resultedSymbols;
        }

        #endregion SET

        internal int GetAllOperators()
        {
            List<OperatorID> AllOperators = new List<OperatorID>();
            List<OperatorID> OperatorIDs = serverAccess.GetAllOperators();
            foreach (OperatorID relID in OperatorIDs)
            {
                List <string> relationNames = new List<string>();
                if (relID.Location.IR != 0)
                {
                    relID.Names = serverAccess.GetOperatorNamesByID(relID);
                }
                AllOperators.Add(relID);
            }
            operatorCollection = AllOperators;
            return Count();
        }

        private int Count()
        {
            return operatorCollection.Count;
        }

        internal int GetOperatorsByName(string operatorName)
        {
            List<OperatorID> AllOperators = new List<OperatorID>();
            List<OperatorID> OperatorIDs = serverAccess.GetOperatorsByName(operatorName, 1);
            foreach (OperatorID relID in OperatorIDs)
            {
                relID.Names.Add(operatorName);
                AllOperators.Add(relID);
            }
            operatorCollection = AllOperators;
            return Count();
        }

        internal List<SymbolID> GetLeafsByRelation(SymbolID rootSymbol, OperatorID relation)
        {
            List<SymbolID> resultedSymbols = new List<SymbolID>();
            List<SymbolID> ReturnedIDs = serverAccess.GetContent(relation, rootSymbol, 1000);
            foreach (SymbolID ints in ReturnedIDs)
            {
                if (ints.Exists)
                {
                    //=// ints.Names = serverAccess.GetSymbolNamesByID(ID, 1);
                    resultedSymbols.Add(ints);
                }
                else
                {
                    resultedSymbols.Add(SymbolID.Null);
                }
            }
            if (resultedSymbols.Count < 1)
            {
                resultedSymbols.Add(SymbolID.Null);
            }
            return resultedSymbols;
        }

        internal List<int[]> getRecord(int symbNr, int offset)
        {
            List<int[]> result = new List<int[]>();
            //result = serverAccess.getRecord(symbNr, offset);
            return result;
        }
    }
}
