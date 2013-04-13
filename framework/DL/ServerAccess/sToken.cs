using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.ServerAccess
{
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


}
