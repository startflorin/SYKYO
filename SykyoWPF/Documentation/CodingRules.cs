using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1.Documentation
{
    class CodingRules
    {
        //=======================================================
        // Real Symbols [+; +/0/-]
        // Construction [-; +/0/-] // Loop, Conditional
        //=======================================================
        /*

                   // Algebric logics
                   // null        == [ 0,  0]   // nothing, ignorance, null
                   // measure     == [ 0,+/-]   // levels, needs, satisfacrion, states
                   // event       == [ +,  -]   // decisions, state modifiers, loops
                   // object      == [ +,0/+]   // static objects, existence
                   // incertitude == [ -,  +]   // cumulator, unoriented, unknown
                   // collision   == [ -,  -]   // collision, boundary, buffer, meta
         * 
         * 
         * 
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
    }
}
