#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
#endregion USING

namespace WindowsFormsApplication1.CD
{
    class CodePhrase
    {
        #region VARIABLES
        int ReplaceAt = 0;
        string OriginalQuery = "";
        string SubsritutedQuery = "";
        List<OperatorItem> relations = new List<OperatorItem>();
        SymbolCollection symbolCollection = new SymbolCollection();
        OperatorCollection relationCollection = new OperatorCollection();
        List<int[]> questionArray = new List<int[]>();
        bool InsideLoopConditon;
        #endregion VARIABLES

        public string CodeMyPhrase(string phrase)
        {
            OriginalQuery = phrase;
            SubsritutedQuery = OriginalQuery;
            
            PrepareRelations();
            SubstituteFlags();
            SupressArticles();
            SubstituteRelations();
            AddIncertitude();
            AddValueWrapers();
            SubstituteLogics();
            SubstituteSymbols();
            SubstituteNumbering();
            return SubsritutedQuery;
         }

        private void SubstituteFlags()
        {
            SubsritutedQuery = SubsritutedQuery.Replace(" if ", " [1:-10] "); //if
            SubsritutedQuery = SubsritutedQuery.Replace(" foreach ", " [1:-20] "); //foreach
            SubsritutedQuery = SubsritutedQuery.Replace(" while ", " [1:-30] "); //while
        }

        private void SupressArticles()
        {
            SubsritutedQuery = SubsritutedQuery.Replace(" a ", " ");
            SubsritutedQuery = SubsritutedQuery.Replace(" an ", " ");
            // Comparer opperators
            SubsritutedQuery = SubsritutedQuery.Replace(" the ", "");
            SubsritutedQuery = SubsritutedQuery.Replace("the ", "");
            SubsritutedQuery = SubsritutedQuery.Replace("The ", "");
        }

        private void SubstituteNumbering()
        {
            SubsritutedQuery = SubsritutedQuery.Replace(" a ", " 1 ");
            SubsritutedQuery = SubsritutedQuery.Replace(" an ", " 1 ");
        }

        private void SubstituteLogics()
        {

            //SubsritutedQuery = SubsritutedQuery.Replace("If ", " [-100:1] { "); // set inside loop
            //SubsritutedQuery = SubsritutedQuery.Replace(" if ", " [-100:1] { "); // set inside loop
            SubsritutedQuery = SubsritutedQuery.Replace("`s ", " ");


            SubsritutedQuery = SubsritutedQuery.Replace("'s ", " ");
            SubsritutedQuery = SubsritutedQuery.Replace(" , ", " [-100:1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(", ", " [-100:1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" and ", " [-100:1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" && ", " [-100:1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" & ", " [-100:1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" or ", " [-100:-1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" || ", " [-100:-1] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" | ", " [-100:-1] ");
        }

        private void AddValueWrapers()
        {
            // Comparer opperators
            SubsritutedQuery = SubsritutedQuery.Replace(" of ", " [0:0:50:50:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" the ", "");
            SubsritutedQuery = SubsritutedQuery.Replace("The ", "");
            // Comparer opperators
            SubsritutedQuery = SubsritutedQuery.Replace(" != ", " [0:0:-100:-5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" == ", " [0:0:-100:5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" > ", " [0:0:-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" < ", " [0:0:-100:-100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" >= ", " [0:0:-100:110:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" <= ", " [0:0:-100:-11:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" -= ", " [0:0:-100:-10:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" is not equal with ", " [0:0:-100:-5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" is equal with ", " [0:0:-100:5:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" is greather then ", " [0:0:-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" is smaller then ", " [0:0:-100:-100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" is greather or equal with ", " [0:0:-100:110:0:0] ");

            // Multiplication values wrapper
            SubsritutedQuery = SubsritutedQuery.Replace(" = ", " [0:0:-100:0:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" ++ ", " [0:0:-100:1:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" -- ", " [0:0:-100:-1:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" += ", " [0:0:-100:10:0:0] ");
            }

        private void AddIncertitude()
        {
            SubsritutedQuery = SubsritutedQuery.Replace(" what ", " [-100:0:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" what ", " [-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace(" what", " [-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace("what ", " [-100:100:0:0] ");
            SubsritutedQuery = SubsritutedQuery.Replace("What ", " [-100:100:0:0] ");
        }

        private void SubstituteSymbols()
        {
            while (!NothingMoreToreplace())
            {
                string candidate = GetFirstCandidateToReplace();
                candidate = candidate.Trim();
                while (!string.IsNullOrEmpty(candidate))
                {
                    bool found = false;
                    string partialCandidate = candidate;
                    while (!found)
                    {
                        List<SymbolID> simbs = symbolCollection.GetSymbolCollectionByString(partialCandidate); // logics.getSymbsBySubstring(partialCandidate, 0, 100);
                        if (simbs.Count < 1 || (simbs.Count==1 && !simbs[0].Exists) )
                        {
                            if (partialCandidate.LastIndexOf(' ') > -1)
                            {
                                partialCandidate = partialCandidate.Substring(0, partialCandidate.LastIndexOf(' '));
                            }
                            else
                            {
                                candidate = candidate.Substring(partialCandidate.Length).Trim();//
                                /*for (int i = 0; i < candidate.Length; i++)
                                {
                                    if (char.IsWhiteSpace(candidate[i]))
                                    {
                                        ReplaceAt++;
                                    }
                                    else
                                    {
                                        candidate = candidate.Substring(i);
                                        return;
                                    }
                                }*/
                                string substitute = "[0:0]";
                                /***>> if (Symbol.CreateUnknown && (char.IsWhiteSpace(SubsritutedQuery[SubsritutedQuery.Length - 1]) || (SubsritutedQuery[SubsritutedQuery.Length - 1] == '!')))
                                {
                                    Symbol symbol = new Symbol(partialCandidate, true);
                                    substitute = "[" + symbol.SymbolID[0] + ":" + symbol.SymbolID[1] + "]";
                                } <<***/

                                SubsritutedQuery = SubsritutedQuery.Substring(0, ReplaceAt) + substitute + SubsritutedQuery.Substring(ReplaceAt + partialCandidate.Length);
                                ReplaceAt = ReplaceAt + substitute.Length + 1; // because i have a spave after substitude
                                found = true;
                            }
                        }
                        else
                        {
                            bool needReplaceAtPluse = false;
                            string substitute = "";
                            foreach (SymbolID symbol in simbs)
                            {
                                substitute = "[" + symbol.Location.A + ":" + symbol.Location.B + "]";
                            }
                            if (partialCandidate.Length < candidate.Length)
                            {
                                candidate = candidate.Substring(partialCandidate.Length + 1); // this is not possible if we are at the end of candidate
                                needReplaceAtPluse = true;
                            }
                            else
                            {
                                candidate = string.Empty;
                            }
                            SubsritutedQuery = SubsritutedQuery.Substring(0, ReplaceAt) + substitute + SubsritutedQuery.Substring(ReplaceAt + partialCandidate.Length);
                            ReplaceAt = ReplaceAt + substitute.Length; // because i have a spave after substitude
                            if (needReplaceAtPluse)
                            {
                                ReplaceAt++;
                            }
                            //if (ReplaceAt - substitute.Length < candidate.Length && (!string.IsNullOrEmpty(candidate)))
                            //    while (char.IsWhiteSpace(candidate[ReplaceAt - substitute.Length]))
                            //{
                            //    ReplaceAt++;
                            //}
                            found = true;
                        }
                    }
                }
            }
        }

        private string GetFirstCandidateToReplace()
        {
            bool found = false;
            int start = -1, end = -1;
            for (int i = 0; i < SubsritutedQuery.Length; i++)
            {
                if (char.IsLetter(SubsritutedQuery[i]))
                {
                    if (start < 0)
                    {
                        start = i;
                        end = i;
                        found = true;
                    }
                    else
                    {
                        end = i;
                    }
                }
                else
                {
                    if (found)
                    {
//                        if (!char.IsWhiteSpace(SubsritutedQuery[i]))
                        {
                            ReplaceAt = start;
                            return SubsritutedQuery.Substring(start, end +1 - start);
                        }
                    }
                }

            }
            if (found)
            {
                ReplaceAt = start;
                return SubsritutedQuery.Substring(start, end + 1 - start);
            }
            else
            return string.Empty;
        }

        private bool NothingMoreToreplace()
        {
            for (int i = 0; i < SubsritutedQuery.Length; i++)
            {
                if (char.IsLetter(SubsritutedQuery[i]))
                { return false; }
            }
            return true;
        }

        private void SubstituteRelations()
        {
            foreach (OperatorItem relation in relations)
            {
                //SubsritutedQuery = SubsritutedQuery.Replace(" " + relation.Name.Trim() + " ", " [" + relation.ID.IR + ":" + relation.ID.Var + ":" + relation.ID[2] + ":" + relation.ID[3] + ":" + relation.ID[4] + ":" + relation.ID[5] + "] ");
                SubsritutedQuery = SubsritutedQuery.Replace(" " + relation.Name.Trim() + " ", " [" + relation.ID.IR + ":" + relation.ID.Var + ":" + relation.LocationDirection + ":" + relation.LocationLevel + ":" + relation.MultiplicityType + ":" + relation.MultiplicityLevel + "] ");
            } 
        }

        private void PrepareRelations()
        {
            relationCollection.GetAllOperators();
        }

        public List<int[]> QueryStringToList(string queryString)
        {
            /*questionArray = queryString.Split(' ').ToList();
            int ListSize = questionArray.Count;
            for (int i = 0; i < ListSize; i++)
            {
                if (string.IsNullOrEmpty(questionArray[i]))
                {
                    questionArray.Remove(questionArray[i]);
                    ListSize--; i--;
                }
            }*/
            return questionArray;
        }

        internal string codeSanitize(string internalRepresentation)
        {
            return SmartSpacer(internalRepresentation);
            //return questionArray;
        }

        private string SmartSpacer(string s)
        {
            string result = "";
            char last = '\0';
            bool inCode = false;

            for (int i = 0; i < s.Length; i++)
            {
                bool spaceAfter = true;
                char c = s[i];
                if (!char.IsWhiteSpace(c))
                {

                    if (c == ']')
                    {
                        inCode = false;
                    }
                    //if ((!char.IsDigit(c)))
                    //{
                    //}
                    if ((!char.IsDigit(c)))// && (!char.IsLetter(s[i])))
                    {
                        if ((c == ':' && char.IsDigit(last)))//|| (s[i] == ']' && char.IsDigit(last)))
                        {
                            spaceAfter = false;
                        }
                        if (s.Length > i + 1)
                        {
                            if ((char.IsDigit(s[i + 1])) || (s[i + 1] == '-'))
                            {
                                if ((c == '-') || (c == '+') || (c == '['))
                                {
                                    spaceAfter = false;
                                }
                            }
                        }
                    }

                    
                    else
                    {
                        if ((s.Length > i + 1) && (! char.IsDigit(s[i + 1])) && (! inCode))
                        {
                            spaceAfter = true;
                        }
                        else
                        {
                            spaceAfter = false;
                        }
                    }


                    if (c == '[')
                    {
                        inCode = true;
                    }

                    if (spaceAfter)
                    {
                        result += c + " ";
                    }
                    else
                    {
                        result += c;
                    }
                }
                last = c;
            }
            return result.Trim();
        }

        private List<object> SmartSplit(string internalRepresentation)
        {
            List<object> objects = new List<object>();
            foreach(object o in objects)
            {
                if (o.GetType() == typeof(Object))
                {
                }
            }
            return objects;
        }
    }
}
