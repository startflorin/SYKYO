using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;

namespace DataPersistency.DL.Collection
{
    public class SRelationCollection : AbstractCollection
    {
        private List<SRelation> sRelation = new List<SRelation>();
        /// <summary>
        /// List of objects
        /// </summary>
        public List<SRelation> SRelation
        {
            get
            {
                return sRelation;
            }
            set
            {
                sRelation = value;
            }
        }
    }
}
