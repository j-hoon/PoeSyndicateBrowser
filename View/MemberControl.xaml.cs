using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
//using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.Collections.ObjectModel;

using PoeSyndicateBrowser.Model;
using PoeSyndicateBrowser.Core.Rest;
using PoeSyndicateBrowser.Model.Rest;

namespace PoeSyndicateBrowser.View
{
    /// <summary>
    /// MemberControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MemberControl : UserControl
    {
        public MemberPool MemberPool;
        private RewardPool RewardPool;


        public MemberControl()
        {
            InitializeComponent();

            MemberPool = MemberPool.Factory.Create();
            MemberItems.ItemsSource = MemberPool.Data;

            RewardPool = new RewardPool();

        }

        // test
        public async Task TestConnectionAsync()
        {
            Debug.WriteLine("- MemberControl.TestConnection()");

            await HttpRequester.RunAndSaveToRewardPool("Blight", RewardPool);
            //await HttpRequester.RunAndSaveToRewardPool("Blight");

            PrintDebugInternal(RewardPool.Data[RewardType.Currency.Value], RewardType.Currency.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.Fragment.Value], RewardType.Fragment.Value, false);
            PrintDebugInternal(RewardPool.Data[RewardType.Incubator.Value], RewardType.Incubator.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.Scarab.Value], RewardType.Scarab.Value, false);
            PrintDebugInternal(RewardPool.Data[RewardType.Fossil.Value], RewardType.Fossil.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.Essence.Value], RewardType.Essence.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.DivinationCard.Value], RewardType.DivinationCard.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.UniqueMap.Value], RewardType.UniqueMap.Value, true);
            PrintDebugInternal(RewardPool.Data[RewardType.Map.Value], RewardType.Map.Value, true);

        }

        private void PrintDebugInternal<T>(Dictionary<string, T> rewardDictionary, string header, bool isExistStatisticValue)
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("--- ").Append(header).Append(" ---").Append('\n');

            foreach (string key in rewardDictionary.Keys)
            {
                ret.Append(" * ").Append(key).Append(" | ").Append(rewardDictionary[key]).Append('\n');
            }

            if (isExistStatisticValue)
            {
                double maxPrice = 0, midPrice = 0, minPrice = 0;

                switch (header)
                {
                    case "Currency":
                    case "Incubator":
                    case "Fossil":
                    case "Essence":
                    case "DivinationCard":
                    case "UniqueMap":
                    case "Map":
                        maxPrice = RewardPool.Statistics[header][0];
                        midPrice = RewardPool.Statistics[header][1];
                        minPrice = RewardPool.Statistics[header][2];
                        break;
                    default:
                        break;
                }

                ret.Append(" # Max: ").Append(maxPrice).Append(" | Mid: ").Append(midPrice).Append(" | Min: ").Append(minPrice).Append('\n');
            }

            Debug.Write(ret);
        }

    }

}
