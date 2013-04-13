using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;

namespace WindowsFormsApplication1.Level_Logic_From_Relations
{
    class RelationItem
    {
        SymbolCollection collectionA;
        SymbolCollection collectionB;
        OperatorItem operatorItem;

        /// <summary>
        /// Create new relation
        /// </summary>
        /// <param name="collectionA"></param>
        /// <param name="operatorItem"></param>
        /// <param name="collectionB"></param>
        public RelationItem(SymbolCollection collectionA, OperatorItem operatorItem, SymbolCollection collectionB)
        {
            this.collectionA = collectionA;
            this.operatorItem = operatorItem;
            this.collectionB = collectionB;
        }

        /// <summary>
        /// Display as string
        /// </summary>
        /// <returns>String representation of the relation</returns>
        public string ToString()
        {
            string relationAsString = collectionA.ToString() + "  " + collectionB.ToString();
            //string relationAsString = collectionA.ToString() + " " + operatorItem.ToString() + " " + collectionB.ToString();
            return relationAsString;
        }
    }
}
