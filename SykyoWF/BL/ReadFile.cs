using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Collections;

namespace WindowsFormsApplication1.BL
{
    public enum charType { WhiteSpace, LetterOrDigit, Punctuation, Symbol, String, Number, Identifier };
        
    class ReadFile
    {
        private IEnumerable<string[]> ReadArrays(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] curr = line.Split('\t');
                    for (int i = 0; i < curr.Length; i++)
                    {
                        curr[i] = curr[i].Trim();
                    }
                    yield return curr;
                }
            }
        }

        private string[] PrintBlock(IEnumerable<IEnumerable<Token>> block)
        {
            int openings = 0;
            List<string> bloc = new List<string>();
            int tabs = 0;
            foreach(IEnumerable<Token> tokenLine in block)
            {
                StringBuilder line = new StringBuilder("\n");
                tabs -= tokenLine.Count(t => t.name.Equals("}"));
                // create padding
                for (int i =0; i<tabs; i++)
                {
                    line.Append("\t");
                }
                tabs += tokenLine.Count(t => t.name.Equals("{"));
                // build line
                foreach (Token t in tokenLine)
                {
                    line.Append(" " + t.name);
                }

                bloc.Add(line.ToString());
            }
            return bloc.ToArray();
        }



        private IEnumerable<IEnumerable<Token>> BlockOf(IEnumerable<IEnumerable<Token>> TokenEnumeration, Token identifier)
        {
            List<IEnumerable<Token>> block = new List<IEnumerable<Token>>();
            int begin = -1;
            int end = -1;
            bool beginFound = false;
            bool endFound = false;
            int openingsBefore = 0;
            int openingsAfter = 0;
            bool inLine = true; // If I am on the same line, I will not count contraopenings :) take care !
            for (int i = 0; i > -1 && i < TokenEnumeration.Count(); i++)
            {
                for (int j = 0; j >= -1 && i > -1 && j < TokenEnumeration.ElementAt(i).Count() && i < TokenEnumeration.Count(); j++)
                {
                    if (TokenEnumeration.ElementAt(i).ElementAt(j).name == identifier.name)
                    {
                        while (!beginFound && i > -1)
                        {
                            IEnumerable<Token> ttt = TokenEnumeration.ElementAt(i);
                            if (!inLine)
                            {
                                openingsBefore -= TokenEnumeration.ElementAt(i).Where(t => t.name.Equals("}")).Count();
                            }
                            openingsBefore += TokenEnumeration.ElementAt(i).Where(t => t.name.Equals("{")).Count();
                            if (openingsBefore > 0)
                            {
                                beginFound = true;
                                begin = i;
                            }
                            else
                            {
                                inLine = false;
                                i--;
                            }
                        }
                        if (i == -1)
                        {
                            beginFound = true;
                            i = 0;
                            begin = i;
                        }
                    }
                    if (beginFound)
                    {
                        openingsBefore = 0; // reset counter
                        inLine = true;
                        while (!endFound)
                        {
                            if (!inLine)
                            {
                                openingsBefore -= TokenEnumeration.ElementAt(i).Where(t => t.name.Equals("{")).Count();
                            }
                            openingsBefore += TokenEnumeration.ElementAt(i).Where(t => t.name.Equals("}")).Count();
                            if (openingsBefore > 0)
                            {
                                block.Add(TokenEnumeration.ElementAt(i));
                                endFound = true;
                                end = i;
                            }
                            else
                            {
                                inLine = false;
                                block.Add(TokenEnumeration.ElementAt(i));
                                i++;
                            }
                        }
                    }
                    if (beginFound && endFound)
                    { break; }
                    //if (i == -1) i = TokenEnumeration.Count()+20;
                }
                if (beginFound && endFound)
                { break; }
            }
            return block;
        }

        private IEnumerable<string> ReadLines(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public IEnumerable<string[]> ReadExcel(string dictionaryPath)
        {
            IEnumerable<string[]> dic = new List<string[]>();
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook theWorkbook = excelApp.Workbooks.Open(            
                   dictionaryPath,//file name
                   0, 
                   true, 
                   5,
                    "",
                    "", 
                    true,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                    "\t",
                    false, 
                    false,
                    0,
                    true);
            Microsoft.Office.Interop.Excel.Sheets sheets = theWorkbook.Worksheets;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
                List<string[]> strArray = new List<string[]>();
            for (int i = 1; i <= 10; i++)
            {
                Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range("A" + i.ToString(), "J" + i.ToString());
                System.Array myvalues = (System.Array)range.Cells.Value;
                foreach (string value in myvalues)
                {
                    strArray.Add(new string[] { value });
                }
            }
            
            return strArray;
        }

        public ReadFile(string filePath, string dictionaryPath)
        {
            return;
            IEnumerable<string[]> dic = new List<string[]>();
            dic = ReadArrays(dictionaryPath);
            dic = OrderDictionary(dic);
            IEnumerable<string> lines = new List<string>();
            lines = ReadLines(filePath);
            lines = ClearComments(lines);
            lines = ClearLineBreaks(lines);
            lines = ReplaceConcatenator(lines);
            lines = SubstituteIfs(lines);
            lines = SubstituteLogics(lines);
            lines = ReversDeclarations(lines);
            lines = OpenBlocks(lines);
            lines = CloseBlocks(lines);
            //lines = 
            IEnumerable<IEnumerable<Token>> TokenEnumeration = TokenizeLines(lines);
            IEnumerable<IEnumerable<Token>> block = BlockOf(TokenEnumeration, new Token(0, 0, "IndexOutOfRangeException"));
            string[] toPrint = PrintBlock(block);
            //Enumerate(TokenEnumeration);
            lines = TranslateDisctionary(lines, dic);
            
            lines = FormatMultiline(lines);
            //UI.CustomMessageBox customMessageBox = new UI.CustomMessageBox(lines.ToArray());
            UI.CustomMessageBox customMessageBox = new UI.CustomMessageBox(toPrint);
            customMessageBox.Show();
        }

        private IEnumerable<IEnumerable<Token>> TokenizeLines(IEnumerable<string> lines)
        {
            List<IEnumerable<Token>> tk = new List<IEnumerable<Token>>();
            foreach (string line in lines)
            {
                IEnumerable<Token> lineT = TokenizeLine(line);
                tk.Add(lineT);
            }
            return tk;
        }

        QueryEvaluator qe = new QueryEvaluator(new DataPersistency.BL.UserOptions.LoggingSystemOptions());

        private void Enumerate(IEnumerable<Token> TokenEnumeration)
        {
            foreach (Token t in TokenEnumeration)
            {
                if (t.typ == charType.Identifier)
                {
                    //QueryEvaluator
                }
            }
        }

        private IEnumerable<string> Clone(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                string s = str;
                result.Add(s);
            }
            return result;
        }
        /*
        public static IEnumerable<string[]> SSS<string[]>(this IEnumerable<string[]> e, string[] value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }
        */
        static IEnumerable<string[]> OrderDictionary(IEnumerable<string[]> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                         orderby s[0].Length ascending
                         select s;
            return sorted;
        }

        private int[] GetNextToken(string line, int offset)
        {
            int[] coords = new int[] { 0, offset };
            bool inString = false;
            int from = offset;
            int to = 0;
            if (from > -1)
            {
                charType ts = charType.WhiteSpace;
                bool goodBegin = false;
                bool goodEnd = false;

                while (!goodBegin || from > line.Length - 1)
                {
                    if (char.IsWhiteSpace(line[from]))
                    {
                        from += 1;
                    }
                    else
                    {
                        goodBegin = true;
                        coords[0] = from;
                    }
                }

                if (char.IsLetterOrDigit(line[from]) || line[from].Equals('_'))
                {
                    ts = charType.LetterOrDigit;
                }
                else if (char.IsPunctuation(line[from]))
                {
                    if (line[from].Equals('\"'))
                    {
                        inString = !inString;

                        while (inString)
                        {
                            to++;
                            if (line[from + to].Equals('\"'))
                            {
                                inString = !inString;
                                ts = charType.String;
                                coords[1] = to+1;
                                goodEnd = true;
                            }
                        }
                    }
                    else
                    {
                        ts = charType.Punctuation;
                        to = 1;
                        coords[1] = to;
                        goodEnd = true;
                    }
                }
                else if (char.IsSymbol(line[from]))
                {
                    ts = charType.Symbol;
                    to = 1;
                    coords[1] = to;
                    goodEnd = true;
                }

                while (!goodEnd && from + to < line.Length)
                {
                    if (ts == charType.LetterOrDigit)
                    {
                        if (char.IsLetterOrDigit(line[from + to]) || line[from + to].Equals('_'))
                        {
                            if (line[from + to].Equals('_'))
                            {
                            }
                            to += 1;
                        }
                        else
                        {
                            goodEnd = true;
                            coords[1] = to;
                        }
                    }
                    else if (ts == charType.Punctuation)
                    {
                        if (char.IsPunctuation(line[from + to]))
                        {
                            to += 1;
                        }
                        else
                        {
                            goodEnd = true;
                            coords[1] = to;
                        }
                    }
                    else if (ts == charType.Symbol)
                    {
                        if (char.IsSymbol(line[from + to]))
                        {
                            to += 1;
                        }
                        else
                        {
                            goodEnd = true;
                            coords[1] = to;
                        }
                    }
                }
                if (from + to == line.Length)
                {
                    coords[1] = to;
                }
            }
            return coords;
        }

        private IEnumerable<string> TranslateDisctionary(IEnumerable<string> lines, IEnumerable<string[]> dic)
        {
            List<string> result = new List<string>();
            foreach (string[] wordPair in dic)
            {
                foreach (string lin in lines)
                {
                    int fix=0;
                    string line = lin;
                    while (fix>-1)
                    {
                        bool goodBegin = false;
                        bool goodEnd = false;
                        if (fix < wordPair[0].Length)
                        {
                            fix = line.IndexOf(wordPair[0], fix + 1);
                        }
                        else
                        {
                            fix = -1;
                        }
                        if (fix>-1)
                        {
                            if (fix > 0)
                            {
                                if (!char.IsLetter(line[fix - 1]) && !char.IsDigit(line[fix - 1]) && !line[fix - 1].Equals('_'))
                                {
                                    goodBegin = true;
                                }
                            }
                            else
                            {
                                goodBegin = true;
                            }
                        }
                        int fiz = fix + wordPair[0].Length;
                        if (fiz + wordPair[0].Length < line.Length - 1)
                        {
                            if (!char.IsLetter(line[line.Length - 1]) && !char.IsDigit(line[line.Length - 1]) && !line[line.Length - 1].Equals('_'))
                            {
                                goodEnd = true;
                            }
                            else
                            {
                                goodEnd = true;
                            }
                        }
                        else
                        {
                            goodEnd = true;
                        }
                        if (goodBegin && goodEnd)
                        {
                            line = line.Replace(wordPair[0], wordPair[1]);
                            fix += wordPair[1].Length;
                        }
                        result.Add(line);
                    }
                }
            }
            return result;
        }

        private IEnumerable<string> ReplaceConcatenator(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                result.Add(str.Replace("\" &", "\" +").Replace("& \"", "+ \"").Replace(" & ", " + "));
            }
            return result;
        }


        private IEnumerable<string> OpenBlocks(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                string s = str;
                if (s.IndexOf(" Function ") > -1 || s.IndexOf(" Sub ") > -1)
                {
                    s = s.Replace(" Function ", " ").Replace(" Sub ", " ");
                    s += " {";
                }
                if (s.IndexOf("End Function") > -1 || s.IndexOf("End Sub") > -1)
                {
                    s = s.Replace("End Function", " ").Replace("End Sub", " ");
                    s += " {";
                }
                result.Add(s);
            }
            return result;
        }

        private IEnumerable<string> CloseBlocks(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                string[] s = str.Trim().Split(' ');
                if (s.Length > 2 && s[0].Equals("End"))
                {
                    result.Add(str.Substring(0, str.IndexOf("End")) + "}");
                }
                else
                {
                    result.Add(str);
                }
            }
            return result;
        }



        private IEnumerable<string> ReversDeclarations(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                int WhiteBefore = str.Length - str.TrimStart().Length;
                string s = str;
                int ass = s.IndexOf(" As ");
                s = s.Replace("Dim ", "");
                string[] ssw = s.Split(' ');
                List<string> ss = new List<string>();
                for(int i = 0; i<ssw.Length; i++)
                {
                    if (! string.IsNullOrWhiteSpace(ssw[i]))
                    {
                        ss.Add(ssw[i]);
                    }
                }
                for(int i = 0; i<ss.Count; i++)
                {
                    if (ss[i].Equals("As") && (i > 0))
                    {
                        if (i == 2) { ss[0] = ss[0].ToLower(); }
                        string temp = ss[i - 1];
                        ss[i - 1] = ss[i + 1];
                        ss[i + 1] = temp;
                        ss.RemoveAt(i);
                        i--;
                    }
                }
                string sf = string.Empty;
                for (int i = 0; i < WhiteBefore; i++)
                {
                    sf += " ";
                }
                for (int i = 0; i < ss.Count; i++)
                {
                    sf += ss[i] + " ";
                }
                result.Add(sf.Substring(0, sf.Length-1));
            }
            return result;
        }

        private IEnumerable<string> SubstituteChecked(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                string s = str;
                result.Add(s.Replace(".value == vbChecked", ".checked").Replace(".value == Checked", ".checked").Replace(".value = vbChecked", ".checked = true").Replace(".value = Checked", ".checked = true")
                    .Replace(".value == vbUnchecked", ".checked == false").Replace(".value == Unchecked", ".checked == false").Replace(".value = vbUnchecked", ".checked = false").Replace(".value = Unchecked", ".checked = false"));
            }
            return result;
        }

        private IEnumerable<string> SubstituteLogics(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string str in lines)
            {
                string s = str;
                if (s.IndexOf('(')>-1 && s.IndexOf('(') < s.IndexOf('=') && s.IndexOf('=') < s.LastIndexOf(')'))
                {
                    s = s.Replace(" = ", " == ");
                }
                result.Add(s.Replace(" <> ", " != ").Replace(" And ", " && ").Replace(" Or ", " || ").Replace("Not ", " ! ").Replace(" Is ", " == ").Replace(" Nothing", " null "));
            }
            return result;
        }

        private IEnumerable<string> SubstituteIfs(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string s in lines)
            {
                result.Add(s.Replace(" Then", ") {").Replace(" ElseIf", " } else if ( ").Replace(" Else", " } else {").Replace("End If", "}").Replace("If ", "if ("));
            }
            return result;
        }

        private IEnumerable<string> FormatMultiline(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            bool attachNextLine = false;
            foreach (string s in lines)
            {
                result.Add(s+System.Environment.NewLine);
            }
            return result;
        }

        private IEnumerable<string> ClearLineBreaks(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            bool attachNextLine = false;
            foreach (string s in lines)
            {
                if (s[s.Length - 1].Equals('_'))
                {
                    if (attachNextLine)
                    {
                        result[result.Count-1] += s.Substring(0, s.Length - 2);
                    }
                    else
                    {
                        result.Add(s.Substring(0, s.Length-2));
                    }
                    attachNextLine = true;
                }
                else 
                {
                    if (attachNextLine)
                    {
                        result[result.Count-1] += s;
                    }
                    else
                    {
                        result.Add(s);
                    }
                    attachNextLine = false;
                }
            }
            return result;
        }

        private IEnumerable<string> ClearComments(IEnumerable<string> lines)
        {
            List<string> result = new List<string>();
            foreach (string s in lines)
            {
                int end = s.Length - 1;
                bool commentFound = false;
                bool inString = false;
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (s[i].Equals('\"'))
                    {
                        inString = !inString;
                    }
                    else if (s[i].Equals('\''))
                    {
                        if (!inString)
                        {
                            end = i;
                            commentFound = true;
                            break;
                        }
                    }
                }
                if (commentFound)
                {
                    if (!string.IsNullOrWhiteSpace(s.Substring(0, end)))
                    {
                        result.Add(s.Substring(0, end));
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        result.Add(s);
                    }
                }
            }
            return result;
        }

        private IEnumerable<Token> TokenizeLine(string line)
        {
            List<Token> tokenArray = new List<Token>();
            int[] coords = new int[] { 0, 0 };
            while (coords[1] > -1 && coords[0] + coords[1] < line.Length)
            {
                coords = GetNextToken(line, coords[0]+coords[1]);
                if (coords[1] > 0)
                {
                    tokenArray.Add(new Token(coords[0], coords[1], line.Substring(coords[0], coords[1])));
                }
                if (coords[1] == 0)
                {
                    coords[1] = -1;
                }
            }
            return tokenArray;
        }
    }


    //################################################################################################
    public class Token
    {
        public Token(int from, int length, int line)
        {
            this.from = from;
            this.length = length;
        }
        public Token(int from, int length, string name)
        {
            this.from = from;
            this.length = length;
            this.name = name;
            if (char.IsLetterOrDigit(name[0]))
            {
                int test = -1;
                if (int.TryParse(name, out test))
                {
                    typ = charType.Number;
                }
                else
                {
                    typ = charType.Identifier;
                }
            }
            if (char.IsSymbol(name[0]))
            {
                typ = charType.Symbol;
            }
        }
        public charType typ;
        public string name;
        public int line;
        public int from;
        public int length;
    }

    //################################################################################################
    public class TokenCollection : IEnumerable
    {
        private Token[] tokenArray;
        public TokenCollection(Token[] tokenArray)
        {
            this.tokenArray = new Token[tokenArray.Length];

            for (int i = 0; i < tokenArray.Length; i++)
            {
                this.tokenArray[i] = tokenArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public TokenEnum GetEnumerator()
        {
            return new TokenEnum(tokenArray);
        }
    }

    //################################################################################################
    public class TokenEnum : IEnumerator
    {
        public Token[] tokenArray;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public TokenEnum(Token[] list)
        {
            tokenArray = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < tokenArray.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Token Current
        {
            get
            {
                try
                {
                    return tokenArray[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    //################################################################################################

}
