using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PoeSyndicateBrowser.Model
{
    public class TestModel : ObservableObject
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public TestModel(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
