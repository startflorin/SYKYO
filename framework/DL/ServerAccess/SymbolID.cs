using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.ServerAccess
{
    public class Location
    {
        /// <summary>
        /// Table ID.
        /// Corresponding Table Index found on Symbols table on IR column and on corresponding table on row A
        /// </summary>
        public int A;

        /// <summary>
        /// RowID.
        /// Table row offset
        /// </summary>
        public int B;

        /// <summary>
        /// Construct the unitializated location similar to NULL. Can be initializated after construction.
        /// </summary>
        public Location()
        {
            this.A = 0; // Keep 0
            this.B = 0; // Keep 0
        }

        /// <summary>
        /// Construct table location.
        /// </summary>
        /// <param name="A">Table ID</param>
        public Location(int A)
        {
            this.A = A;
            this.B = 0; // Keep 0
        }

        /// <summary>
        /// Construct row location
        /// </summary>
        /// <param name="A">Table ID</param>
        /// <param name="B">Row ID</param>
        public Location(int A, int B)
        {
            this.A = A;
            this.B = B;
        }

        /// <summary>
        /// Get the location as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[" + A + ":" + B + "]";
        }
    }
    public class Multiplicity
    {
        public int G;
        public int H;
        public override string ToString()
        {
            return "[" + G + ":" + H + "]";
        }
    }

    public class SymbolID
    {
        public Location Location;
        public Multiplicity Multiplicity;

        public SymbolID()
        {
            Location = new Location();
            Multiplicity = new Multiplicity();
            Names = new List<string>();
        }

        public SymbolID(Location location, Multiplicity multiplicity)
        {
            Location = location;
            Multiplicity = multiplicity;
            Names = new List<string>();
        }

        public SymbolID(Location location)
        {
            Location = location;
            Multiplicity = new Multiplicity();
            Names = new List<string>();
        }

        public SymbolID(Location location, Multiplicity multiplicity, string Name)
        {
            Location = location;
            Multiplicity = multiplicity;
            Names = new List<string>();
            this.Names.Add(Name);
        }

        public string ToString()
        {
            return "(L=" + Location.ToString() + ", M=" + Multiplicity.ToString() + ")";
        }

        public static SymbolID Null { 
            get 
            {
                return new SymbolID();
            }
            set
            {

            }
        }

        public bool IsUnknown()
        {
            return Location.A == 0 && Location.B == 0;
        }

        public bool Exists 
        {
            get
            { 
                return Location.A != 0 || Location.B != 0; 
            }
            set 
            { 
            }
        }

        public List<string> Names { get; set; }

        public static SymbolID Unknown { get; set; }
    }
}
