using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.ServerAccess
{
    public interface ServerAccessInterface
    {
        #region Properties
        
        /// <summary>
        /// If is human readable all toString methods will return the human readable representation
        /// </summary>
        bool IsHumanReadable { get; set; }

        /// <summary>
        /// User rights for new symbol creation
        /// </summary>
        bool AcceptSymbols { get; set; }

        /// <summary>
        /// User rights for new operator creation
        /// </summary>
        bool AcceptOperators { get; set; }

        /// <summary>
        /// User rights for new relation creation
        /// </summary>
        bool AcceptRelations { get; set; }

        /// <summary>
        /// Provider name for the current connection
        /// </summary>
        /// <returns></returns>
        string GetProviderName();

        /// <summary>
        /// Get the status of the current connection
        /// </summary>
        /// <returns></returns>
        string getConnectionState();

        bool TryToOpenConnection();

        #endregion Properties

        #region Symbol Interface

        /// <summary>
        /// Get symbol names by symbolID
        /// </summary>
        /// <param name="symbolID">The given symbolID to search for</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: The list of names associated with the given symbolID</returns>
        List<string> GetOperatorNamesByID(OperatorID operatorID, int LimitOfResults = 10);

        /// <summary>
        /// Get symbolIDs from a partial symbol name (according to search mode options). 
        /// </summary>
        /// <param name="symbolNamePart">The partial name of the symbol to find</param>
        /// <param name="searchMode">Search mode according to the related enumeration</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>The existent symbolIDs associated with the symbol name OR the new created symbol if there was no symbol and if the permissions are right</returns>
        List<SymbolID> GetSymbolsByName(string SymbolsNamePart, int Method, int LimitOfResults = 10);

        /// <summary>
        /// Create symbol by name. If duplicates are not accepted return the existent symbolID. If the name does not exist or duplicates are accepted insert the name  and return it`s symbolID
        /// </summary>
        /// <param name="symbolName">The exact name of the symbol to find</param>
        /// <param name="acceptDuplicates">True if duplicates are permitted</param>
        /// <returns>ResultSet: 1) The existent symbolID if it exists and duplicates are not permited 2) The new created symbolID</returns>
        List<SymbolID> CreateSymbolByName(string SymbolName, bool AcceptDuplicates);

        /// <summary>
        /// Create a new alias for a given IR if the permissions are right and the given symbolID exists
        /// </summary>
        /// <param name="symbolAliasName">The new alias</param>
        /// <param name="existentID">The existent IR</param>
        /// <returns>ResultSet: 1) Empty set if IR does not exists or DB does not accept this change, 2) Existent symbol if already assigned, 3) The new symbol if DB accept this change</returns>
        List<SymbolID> CreateSymbolAlias(string SymbolAliasName, int ExistentID = -1);

        #endregion Symbol Interface

        #region Operator Interface

        List<OperatorID> GetAllOperators();

        List<string> GetSymbolNamesByID(SymbolID symbolID, int LimitOfResults = 10);

        List<OperatorID> GetOperatorsByName(string OperatorNamePart, int Mode = 0, int LimitOfResults = 10);

        #endregion Operator Interface

        #region Relation Interface

        /// <summary>
        /// Get the proprieties of a symbol restricted to a relation type if provided
        /// </summary>
        /// <param name="operatorID">The relation to restrict the resultset if provided</param>
        /// <param name="multiplicationOrder">the multiplication order requested if provided</param>
        /// <param name="symbolID">The symbolID to expore for properties</param>
        /// <param name="limitOfResults">>Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: The descriptopn of the given symbol restricted to the relation if provided</returns>
        List<SymbolID> GetContent(OperatorID operatorID, SymbolID symbolID, int Limit);

        /// <summary>
        /// check if the relation is transitive
        /// </summary>
        /// <param name="path">path so far</param>
        /// <param name="operatorID">Given operator</param>
        /// <param name="symbolID1">from symbol A</param>
        /// <param name="symbolID2">to symbol B</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>the symbols succession from A to B</returns>
        List<SymbolID> IsTransitive(List<SymbolID> list, OperatorID operatorID, SymbolID symbolID1, SymbolID symbolID2, int limit);

        /// <summary>
        /// Create new relation
        /// </summary>
        /// <param name="operatorID">relation type</param>
        /// <param name="symbolID1">preoperand symbol</param>
        /// <param name="symbolID2">postoperand symbol</param>
        /// <returns>ResultSet: The new created relation</returns>
        List<SymbolID> CreateRelation(OperatorID operatorID, SymbolID symbolA, SymbolID symbolB);

        #endregion Relation Interface

    }
}
