using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.Collection
{
    public class SRelation
    {

        private List<string> relationName = new List<string>();
        /// <summary>
        /// Name of the Relation
        /// </summary>
        public List<string> RelationName
        {
            get 
            {
                return relationName;
            }
            set
            {
                relationName = value;
            }
        }


        private int relationId = -1;
        /// <summary>
        /// The line of the Relation
        /// </summary>
        public int RelationId
        {
            get
            {
                return relationId;
            }
            set
            {
                relationId = value;
            }
        }
    }
}
