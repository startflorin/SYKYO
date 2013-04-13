using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity 
{
    public abstract class CodeElement
    {
        /// <summary>
        /// The identifier of the code element
        /// </summary>
        public string Name { get; set; }

        public List<CodeElement> Childs { get; set; }

        public string Extend;

        public List<string> Modifiers = new List<string>();

        public enum Visible { Internal = 0, Private = 1, Protected = 2, Public = 3 };
        public int Visibility { get; set; }

        public string ReturnType { get; set; }

        public List<string> Parameters { get; set; }

        public List<CodeRelation> Properties { get; set; }
    }

    public class CodeRelation
    {
        public enum ReletionType
        {
            HasA, IsA, Increase, Decrease
        }
        public enum ReletionMultiplicity
        {
            One, Some, No, All
        }

        public List<Arrow> Childs = new List<Arrow>();
        public int RelationType;
        private int relationType;
        private CodeElement objB;

        public CodeRelation(CodeRelation.ReletionType relationType)
        {
            this.relationType = (int)relationType;
        }
    }

    public class Arrow
    {
        public CodeElement CodeElement { get; set; }
        public int Multiplicity;
    }
}
