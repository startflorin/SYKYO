using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.SElement
{
    class SRelationCollection : AbstractCollection
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
