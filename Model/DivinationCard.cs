using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeSyndicateBrowser.Model
{
    public class DivinationCard
    {
        public double ChaosValue { get; set; }
        public int StackSize { get; set; }

        public DivinationCard(double chaosValue, int stackSize)
        {
            ChaosValue = chaosValue;
            StackSize = stackSize;
        }
    }
}
