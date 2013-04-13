using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.DL;

namespace WindowsFormsApplication1.Level_Objects_From_Numbers
{
    public class SymbolItem
    {
        public SymbolID ID = new SymbolID();
        public string Name;
        public Multiplicity Multiplicity = new Multiplicity();
        public Location Location = new Location();
        
        public SymbolItem(Location Location, Multiplicity Multiplicity, string Name)
        {
            // TODO: Complete member initialization
            this.ID = new SymbolID(Location, Multiplicity);
            this.Name = Name;
            this.Location = Location;
            this.Multiplicity = Multiplicity;
        }

        public override string ToString() 
        {
            string str = string.Empty;
            if (Multiplicity.G != 0 || Multiplicity.H != 0)
            {
                str += "(" + Multiplicity.H + " X) ";
            }

            if (ID.Location.A != 0 || ID.Multiplicity.H != 0)
            {
                str += Name + "[" + ID.Location.A + ":" + ID.Location.B + "]";
            }
            else
            {
                str += "null";
            }
            return str;
        }

        internal bool IsNotNull()
        {
            if (ID.Location.A != 0 || ID.Location.B != 0)
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
            if (ID.Location.A == 100000000 && ID.Location.B == 0)
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
