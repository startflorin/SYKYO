using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Object.Model
{
public class ..List : ObservableCollection<..>
{
public bool Add(object newItem)
{
base.Add(newItem as ..);
return true;
}

public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as ..);
            return true;
        }

        private static ..List instance;

        public ..List(): base() 
        {

        }

        public static ..List GetInstance()
        {
            lock (typeof(..List))
            {
                if (instance == null)
                {
                    instance = new ..List();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class ..
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

    public class ..Metainformation
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

    public class ..Influence
    {
    }
}
