using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;

namespace DataPersistency.DL.CommenAccess.ObjectsFromNumbers
{
    public class SymbolItem
    {
        public SymbolID ID;
        public string Name;
        public Multiplicity Multiplicity = new Multiplicity();
        public Location Location = new Location();
        private Location ID1;
        private string aName;

        public SymbolItem(Location Location, Multiplicity Multiplicity, string Name)
        {
            // TODO: Complete member initialization
            this.ID = new SymbolID(Location, Multiplicity);
            this.Name = Name;
            this.Location = Location;
            this.Multiplicity = Multiplicity;
        }

        public SymbolItem(Location ID1, string aName)
        {
            // TODO: Complete member initialization
            this.ID1 = ID1;
            this.aName = aName;
        }

        public override string ToString() 
        {
            string str = string.Empty;
            if (Multiplicity.G != 0 || Multiplicity.H != 0)
            {
                str += "(" + Multiplicity.H + " X) ";
            }

            if (Location.A != 0 || Location.B != 0)
            {
                str += Name + "[" + Location.A + ":" + Location.B + "]";
            }
            else
            {
                str += "null";
            }
            return str;
        }

        internal bool IsNotNull()
        {
            if (Location.A != 0 || Location.B != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool IsUnknown()
        {
            if (Location.A == 100000000 && Location.B == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SymbolItem Null { get { return new SymbolItem(new Location(), new Multiplicity(), "InexistentSymbol"); } }
        public static SymbolItem Unknown { get { return new SymbolItem(new Location(100000000), new Multiplicity(), "UnknownSymbol"); } }
    }
}
