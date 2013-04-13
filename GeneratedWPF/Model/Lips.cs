using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Object.Model
{
    public class LipsList : ObservableCollection<Lips>
    {
        public LipsList()
            : base()
        {
        }
    }

    [Serializable]
    public class Lips
    {
    }
}
namespace Object.Meta
{
    public class LipsMetainformation
    {
    }
}
