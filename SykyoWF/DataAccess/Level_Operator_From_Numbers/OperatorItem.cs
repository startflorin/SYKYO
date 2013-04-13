using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;

namespace WindowsFormsApplication1.Level_Operator_From_Numbers
{
    class OperatorItem
    {
        public int LocationDirection;
        public int LocationLevel;

        public int MultiplicityType;
        public int MultiplicityLevel;

        public int ReverseDirection;
        public int ReverseLevel;

        public OperatorLocation ID;
        public string Name;

        public OperatorItem(OperatorLocation ID, string Name)
        {
            // TODO: Complete member initialization
            this.ID = ID;
            this.Name = Name;
        }

        public override string ToString()
        {
            if (ID.IR != 0 || ID.Var != 0)
            {
                return Name + "[" + ID.IR + ":" + ID.Var + " " + ID.IR2 + ":" + ID.Var2 + "]";
            }
            else
            {
                return "null";
            }
        }

        internal bool IsNotNull()
        {
            if (ID.IR != 0 || ID.Var != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
