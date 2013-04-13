using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1.SElement;

namespace WpfApplication1.sDataAccessBase
{
    public interface ServerAccessInterface
    {
        bool AcceptSymbols { get; set; }
        bool AcceptOperators { get; set; }
        bool AcceptRelations { get; set; }
        
        // get current provider name
        string GetProviderName();

        string getConnectionState();

        // +GetSymbolsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        List<SObject> GetSymbolsByName(string SymbolsNamePart, int Method, int LimitOfResults = 10);

        // +GetSymbolsByID(SymbolID : int [2] = {0,0}, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        List<string> GetSymbolNamesByID(int[] SymbolID, int Mode, int LimitOfResults = 10);

        // +GetRelationsByName(RelationsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedRelations : List<int[4]>)
        List<SOperator> GetOperatorsByName(string OperatorNamePart, int Mode = 0, int LimitOfResults = 10);

        // +GetSymbolsByID(SymbolID : int [4] = {0,0,0,0}, LimitOfResults : int = 10, ResultedRelations : List<int[4]>)
        List<string> GetOperatorNamesByID(int[] OperatorID, int Mode = 0, int LimitOfResults = 10);

        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        List<SObject> CreateSymbolByName(string SymbolName, bool AcceptDuplicates);

        List<int[]> IsTransitive(List<int[]> list, int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc, int limit);

        List<SOperator> GetAllOperators();

        List<int[]> CreateRelation(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc);

        List<SOperator> GetOperatorsByName(string OperatorName);

        List<int[]> GetContent(int[] relationID, int[] aMul, int[] aLoc, int Limit);


        List<int[]> getRecord(int symbNr, int offset);
    }
}
