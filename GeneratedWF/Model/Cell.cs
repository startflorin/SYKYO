using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Object.Model
{
public class CellList : ObservableCollection<Cell>
{
public bool Add(object newItem)
{
base.Add(newItem as Cell);
return true;
}

public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as Cell);
            return true;
        }

        private static CellList instance;

        public CellList(): base() 
        {

        }

        public static CellList GetInstance()
        {
            lock (typeof(CellList))
            {
                if (instance == null)
                {
                    instance = new CellList();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class Cell
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

    public class CellMetainformation
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

    public class CellInfluence
    {
    }
}
