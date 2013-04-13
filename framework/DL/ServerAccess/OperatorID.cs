using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.ServerAccess
{
    public class OperatorLocation
    {
        public int IR;
        public int Var;
        public int IR2;
        public int Var2;
        public string ToString()
        {
            return "[" + IR + ":" + Var + ":" + IR2 + ":" + Var2 + "]";
        }
    }

    public class OperatorMultiplicity
    {
        public int Direction;
        public int Level;
        public string ToString()
        {
            return "[" + "]";
        }
    }

    public class OperatorID
    {
        public OperatorLocation Location;
        public OperatorMultiplicity Multiplicity;
        public List<string> Names;
        public OperatorID(OperatorLocation location)
        {
            Location = location;
            Multiplicity = new OperatorMultiplicity();
        }

        public OperatorID()
        {
            Location = new OperatorLocation();
            Multiplicity = new OperatorMultiplicity();
        }

        public string ToString()
        {
            return "[" + Location.ToString() + ":" + Multiplicity.ToString() + "]";
        }

        public bool Exists { get; set; }

        public bool hasMultiplicity { get; set; }
    }

}
