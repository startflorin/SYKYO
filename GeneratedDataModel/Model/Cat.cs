using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Object.Model
{
public class CatList : ObservableCollection<Cat>
{
public bool Add(object newItem)
{
base.Add(newItem as Cat);
return true;
}

public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as Cat);
            return true;
        }

        private static CatList instance;

        public CatList(): base() 
        {

        }

        public static CatList GetInstance()
        {
            lock (typeof(CatList))
            {
                if (instance == null)
                {
                    instance = new CatList();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class Cat
    {
        private int id;
        public int ID
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
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string url;
        public string URL
        {
            get { return url; }
            set { url = value; }
        }
    }
}

namespace Object.Meta
{

    public class CatMetainformation
    {

        public bool IsValid()
        {
            bool result = true;
            return result;
        }
    }
}
namespace Object.Influence
{

    public class CatInfluence
    {
    }
}
