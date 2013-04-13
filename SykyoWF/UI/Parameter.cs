using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using WindowsFormsApplication1.BL;

namespace WindowsFormsApplication1.DL
{
    public class Parameter : INotifyPropertyChanged
    {
        RelationCollection logics = new RelationCollection();

        private string _myName;

        public string MyName
        {
            get { return _myName; }
            set
            {
                if (_myName != value)
                {
                    _myName = value;
                    OnPropertyChanged("MyName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //private Symbol symbol = new Symbol();
        public int[] ID;

        public Parameter(int[] symbolID)
        {
            //symbol = new Symbol(symbolID);
            //ID = symbol.SymbolID;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        internal int RefreshValue()
        {
            //int[] value = logics.GetParameterValueAt(symbol.SymbolID);
            //return value[1];
            return 0;
        }

        public object Location { get; set; }
    }
}
