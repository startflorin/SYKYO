using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Object.Model
{
public class TheList : ObservableCollection<The>
{
public bool Add(object newItem)
{
base.Add(newItem as The);
return true;
}

public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as The);
            return true;
        }

        private static TheList instance;

        public TheList(): base() 
        {

        }

        public static TheList GetInstance()
        {
            lock (typeof(TheList))
            {
                if (instance == null)
                {
                    instance = new TheList();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class The
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

    public class TheMetainformation
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

    public class TheInfluence
    {
    }
}
