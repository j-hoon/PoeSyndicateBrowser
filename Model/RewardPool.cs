using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoeSyndicateBrowser.Model.Rest;

namespace PoeSyndicateBrowser.Model
{

    public static class RewardType
    {
        public static readonly Type Currency        = new Type(Properties.Resources.Currency, true);
        public static readonly Type Fragment        = new Type(Properties.Resources.Fragment, false);
        public static readonly Type Incubator       = new Type(Properties.Resources.Incubator, true);
        public static readonly Type Scarab          = new Type(Properties.Resources.Scarab, false);
        public static readonly Type Fossil          = new Type(Properties.Resources.Fossil, true);
        public static readonly Type Essence         = new Type(Properties.Resources.Essence, true);
        public static readonly Type DivinationCard  = new Type(Properties.Resources.DivinationCard, true);
        public static readonly Type UniqueMap       = new Type(Properties.Resources.UniqueMap, true);
        public static readonly Type Map             = new Type(Properties.Resources.Map, true);

        public class Type
        {
            public string Value { get; }
            public bool HasStatistics { get; }

            public Type(string value, bool hasStatistics) => (Value, HasStatistics) = (value, hasStatistics);
        }

        public static Type[] Get()
        {
            return new Type[]
            {
                Currency, Fragment, Incubator, Scarab, Fossil, Essence, DivinationCard, UniqueMap, Map
            };
        }

    }


    public class RewardPool
    {
        public Dictionary<string, Dictionary<string, dynamic>> Data { get; private set; }
        public Dictionary<string, double[]> Statistics { get; private set; }

        public RewardPool()
        {
            Data = new Dictionary<string, Dictionary<string, dynamic>>();
            Statistics = new Dictionary<string, double[]>();

            foreach (RewardType.Type type in RewardType.Get())
            {
                Data.Add(type.Value, new Dictionary<string, dynamic>());

                if (type.HasStatistics)
                {
                    Statistics.Add(type.Value, new double[3]);
                }
            }
        }

        public void Save(RewardType.Type rewardType, dynamic repository)
        {
            Type repositoryType = repository.GetType();
            double priceSum = 0, maxPrice = 0, midPrice = 0, minPrice = 0;
            bool isWriteStatistics = (rewardType.HasStatistics) ? true : false;

            if (repositoryType.Equals(typeof(CurrencyRepository)))
            {
                var repo = repository as CurrencyRepository;

                foreach (CurrencyLine line in repo.Lines)
                {
                    Data[rewardType.Value][line.Name()] = line.ChaosValue();

                    if (isWriteStatistics)
                    {
                        priceSum += line.ChaosValue();
                    }
                }

                if (isWriteStatistics)
                {
                    maxPrice = repo.Lines[0].ChaosValue();
                    midPrice = priceSum / repo.Lines.Count();
                    minPrice = repo.Lines[repo.Lines.Count() - 1].ChaosValue();
                }
            }
            else if (repositoryType.Equals(typeof(ItemRepository)))
            {
                var repo = repository as ItemRepository;

                foreach (ItemLine line in repo.Lines)
                {
                    Data[rewardType.Value][line.Name()] = line.ChaosValue();

                    if (isWriteStatistics)
                    {
                        priceSum += line.ChaosValue();
                    }
                }

                if (isWriteStatistics)
                {
                    maxPrice = repo.Lines[0].ChaosValue();
                    midPrice = priceSum / repo.Lines.Count();
                    minPrice = repo.Lines[repo.Lines.Count() - 1].ChaosValue();
                }
            }
            else if (repositoryType.Equals(typeof(DivinationCardRepository)))
            {
                var repo = repository as DivinationCardRepository;

                foreach (DivinationCardLine line in repo.Lines)
                {
                    Data[rewardType.Value][line.Name()] = new DivinationCard(line.ChaosValue(), line.StackSize());
                    priceSum += line.ChaosValue();
                }

                maxPrice = repo.Lines[0].ChaosValue();
                midPrice = priceSum / repo.Lines.Count();
                minPrice = repo.Lines[repo.Lines.Count() - 1].ChaosValue();
            }
            else
            {
                Debug.WriteLine("Invalid Repository Type...");
                return;
            }

            if (isWriteStatistics)
            {
                Statistics[rewardType.Value][0] = maxPrice;
                Statistics[rewardType.Value][1] = midPrice;
                Statistics[rewardType.Value][2] = minPrice;
            }
        }

    }

}
