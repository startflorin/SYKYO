using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.DL;

namespace WindowsFormsApplication1
{
    class Relation
    {
        public Relation()
        {

        }

        ServerAccess serverAccess = new ServerAccess();
        string relationName;
        int[] relationID = new int[2];


        public Relation(string relationName)
        {
            this.Name = relationName;
        }

        public Relation(int[] relationID)
        {
            this.relationID = relationID;
            this.relationName = serverAccess.getRelationNameByID(relationID);
        }

        /// <summary>
        /// Internal representation of the ID
        /// </summary>
        public int[] RelationID
        {
            get { return relationID; }
        }

        /// <summary>
        /// Name of the relation in natural language
        /// </summary>
        public string Name
        {
            get { return relationName; }
            set
            {
                relationName = value;
                relationID = serverAccess.getRelationID(relationName);
            }
        }

        bool transitive;
        bool associative;
        private int[] ID;
        //private string Name_2;
        bool IsAssociative
        {
            set { associative = value; }
            get { return associative; }
        }

        public int[] GetValuePosition(int[] r, int[] a, int[] b)
        {
            int[] valuePosition = { 0, 0 };
            if (ExistsValues(r, a, b))
            {
                valuePosition = serverAccess.GetAssociationPosition(r, a, b);
            }
            return valuePosition;
        }
        public int[] GetAssociations(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            if (CountAssociation(r, a, b)>0)
            {
                relationPosition = serverAccess.GetAssociatedValues(r, a, b);
            }
            return relationPosition;
        }

        public int[] GetRelationPosition(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            if (ExistsRelation(r, a, b))
            {
                relationPosition = serverAccess.GetRelationPosition(r, a, b);
            }
            return relationPosition;
        }

        public List<int[]> SolveRelation(int[] r, int[] a, int[] b)
        {
            List<int[]> ListOfResults = new List<int[]>();
            return serverAccess.SolveRelation(r, a, b, 10);
        }

        public List<int[]> SolveAssociation(int[] r, int[] a, int[] b)
        {
            /*
            
            RELATIONS DICTIONARY
             * 
            LOGICAL OPPERATORS
            SubsritutedQuery = SubsritutedQuery.Replace(" and ", " [0: -10] ");
            
             * 
            VALUE MODIFIERS
            SubsritutedQuery = SubsritutedQuery.Replace(" = ", " [-100:0:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" ++ ", " [-100:1:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" -- ", " [-100:-1:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" += ", " [-100:10:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" -= ", " [-100:-10:0:0] ");
             * 
            COMPARATION OPPERATORS
            SubsritutedQuery = SubsritutedQuery.Replace(" != ", " [-100:-5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" == ", " [-100:5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" > ", " [-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" < ", " [-100:-100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" >= ", " [-100:110:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" <= ", " [-100:-110:0:0] ");
            
             */
            List<int[]> ListOfResults = new List<int[]>();
            if (r[0] == -100 && r[1] == 0) // =
            {
                //return serverAccess.SolveValue(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == -5) // !=
            {
                return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == 5) // ==
            {
                return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == 100) // >
            {
                return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == -100) // <
            {
                return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == 110) // >=
            {
                //return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == -110) // <=
            {
                //return serverAccess.SolveComparation(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == 1) // ++
            {
                //return serverAccess.SolveValue(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == -1) // --
            {
                //return serverAccess.SolveValue(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == 10) // +=
            {
                //return serverAccess.SolveValue(r, a, b, 10);
            }
            if (r[0] == -100 && r[1] == -10) // -=
            {
                //return serverAccess.SolveValue(r, a, b, 10);
            }
            return ListOfResults;
        }

        private bool ExistsRelation(int[] r, int[] a, int[] b)
        {
            if (serverAccess.ExistsRelation(r, a, b) < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ExistsValues(int[] r, int[] a, int[] b)
        {
            if (serverAccess.ExistsValues(r, a, b) < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private int CountAssociation(int[] r, int[] a, int[] b)
        {
            return serverAccess.ExistsAssociatedValues(r, a, b);
        }

        /// <summary>
        /// Create a new relation if the relation is not found
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] CreateRelationIfNotExists(int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            relationPosition = GetRelationPosition(relationID, a, b);
            if (relationPosition[1] < 1)
            {
                relationPosition = RegisterRelationAsNew(relationID, a, b);
            }
            return relationPosition;
        }        
        
        /// <summary>
        /// Create a new relation if the relation is not found
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] CreateOrReassignValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            bool exist = ExistsValues(relationID, a, b);
            if (! exist)
            {
                //relationPosition = RegisterRelat(relationID, a, b);
            }
            else
            {
                relationPosition = serverAccess.ReAssignValue(relationID, a, b);
            }
            return relationPosition;
        }

        public int[] SetValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            relationPosition = GetValuePosition(r, a, b);
            if (relationPosition[1] < 1)
            {
                CreateOrReassignValue(r, a, b);
            }
            relationPosition = AlterValue(r, a, b);
            return relationPosition;
        }

        public int[] GetValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            relationPosition = GetValuePosition(r, a, b);
            if (relationPosition[1] < 1)
            {
                CreateOrReassignValue(r, a, b);
            }
            relationPosition = AlterValue(r, a, b);
            return relationPosition;
        }

        private int[] AlterValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = {0, 0};
            relationPosition = serverAccess.ReAssignValue(r, a, b);
            return relationPosition;
        }
        private int[] RegisterRelationAsNew(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = {0, 0};
            relationPosition = serverAccess.CreateRelationAsNew(r, a, b);
            return relationPosition;
        }

        internal string toString()
        {
            return "[" + RelationID[0] + ":" + RelationID[1] + ":" + RelationID[2] + ":" + RelationID[3] +"]";
        }

        public static bool CreateUnknown { get; set; }
    }
}
