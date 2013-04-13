using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Object.Model
{

    public class HeadList : ObservableCollection<Head>
    {
        public bool Add(object newItem)
        {
            base.Add(newItem as Head);
            return true;
        }

        public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as Head);
            return true;
        }

        private static HeadList instance;

        public HeadList()
            : base()
        {

        }

        public static HeadList GetInstance()
        {
            lock (typeof(HeadList))
            {
                if (instance == null)
                {
                    instance = new HeadList();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class Head
    {
        private decimal id;
        public decimal ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Lips lipsP;
        public Lips LipsP
        {
            get { return lipsP; }
            set { lipsP = value; }
        }
    }
}
namespace Object.Meta
{
    public class HeadMetainformation
    {
    }
}

