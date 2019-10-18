using System.Runtime.Serialization;

namespace PoeSyndicateBrowser.Model.Rest
{
    [DataContract(Name = "lines", Namespace = "Rest.Repository")]
    public abstract class Line
    {
        abstract public string Name();
        abstract public double ChaosValue();

        abstract public override string ToString();
    }

    [DataContract(Name = "lines", Namespace = "Rest.CurrencyRepository")]
    public class CurrencyLine : Line
    {
        [DataMember(Name = "currencyTypeName")]
        private string mCurrencyTypeName;

        [DataMember(Name = "chaosEquivalent")]
        private double mChaosEquivalent;

        public override string Name()
        {
            return mCurrencyTypeName;
        }

        public override double ChaosValue()
        {
            return mChaosEquivalent;
        }

        public override string ToString()
        {
            return mCurrencyTypeName + " | " + mChaosEquivalent;
        }
    }

    [DataContract(Name = "lines", Namespace = "Rest.ItemRepository")]
    public class ItemLine : Line
    {
        [DataMember(Name = "name")]
        private string mName;

        [DataMember(Name = "chaosValue")]
        private double mChaosValue;

        public override string Name()
        {
            return mName;
        }

        public override double ChaosValue()
        {
            return mChaosValue;
        }

        public override string ToString()
        {
            return mName + " | " + mChaosValue;
        }
    }

    [DataContract(Name = "lines", Namespace = "Rest.DivinationCardRepository")]
    public class DivinationCardLine : Line
    {
        [DataMember(Name = "name")]
        private string mName;

        [DataMember(Name = "chaosValue")]
        private double mChaosValue;

        [DataMember(Name = "stackSize")]
        private int mStackSize;

        public override string Name()
        {
            return mName;
        }

        public override double ChaosValue()
        {
            return mChaosValue;
        }

        public int StackSize()
        {
            return mStackSize;
        }

        public override string ToString()
        {
            return mName + " | " + mChaosValue + "(" + mStackSize + ")";
        }
    }
}
