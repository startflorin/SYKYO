using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Object.Model
{
public class OrganismList : ObservableCollection<Organism>
{
public bool Add(object newItem)
{
base.Add(newItem as Organism);
return true;
}

public bool Remove(object unwantedItem)
        {
            base.Remove(unwantedItem as Organism);
            return true;
        }

        private static OrganismList instance;

        public OrganismList(): base() 
        {

        }

        public static OrganismList GetInstance()
        {
            lock (typeof(OrganismList))
            {
                if (instance == null)
                {
                    instance = new OrganismList();
                }
                return instance;
            }
        }
    }

    [Serializable]
    public class Organism
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

    public class OrganismMetainformation
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

    public class OrganismInfluence
    {
    }
}
