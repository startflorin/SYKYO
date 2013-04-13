using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GeneratedWindowsFormsApplication
{
	class CatCollection
	{
        public List<CatModel> Cats = new List<CatModel>();
        public void Initialize()
        {
            Cats = new List<CatModel>();
            CatModel cat1 = new CatModel();
            cat1.AgeP = 9;
            cat1.NameP = "Sam";
            CatModel cat2 = new CatModel();
            cat2.AgeP = 3;
            cat2.NameP = "Heidy";
            Cats.Add(cat1);
            Cats.Add(cat2);
        }
        public void AddItem()
        {

        }
    }
}
