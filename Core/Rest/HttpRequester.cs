using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using PoeSyndicateBrowser.Model;
using PoeSyndicateBrowser.Model.Rest;

namespace PoeSyndicateBrowser.Core.Rest
{
    public class HttpRequester
    {
        private static readonly HttpClient client = new HttpClient();

        private const string REQUEST_HEADER = "application/json";

        public static async Task RunAndSaveToRewardPool(string targetLeague, RewardPool rewardPool)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(REQUEST_HEADER));

            // Currency
            var streamTask = client.GetStreamAsync(Properties.Resources.CurrencyRestUrl + targetLeague);
            var currencyRepository = DataContractJsonSerializer.SerializeRepository<CurrencyRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Currency, currencyRepository);

            // Fragment
            streamTask = client.GetStreamAsync(Properties.Resources.FragmentRestUrl + targetLeague);
            currencyRepository = DataContractJsonSerializer.SerializeRepository<CurrencyRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Fragment, currencyRepository);

            // Incubator
            streamTask = client.GetStreamAsync(Properties.Resources.IncubatorRestUrl + targetLeague);
            var itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Incubator, itemRepository);

            // Scarab
            streamTask = client.GetStreamAsync(Properties.Resources.ScarabRestUrl + targetLeague);
            itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Scarab, itemRepository);

            // Fossil
            streamTask = client.GetStreamAsync(Properties.Resources.FossilRestUrl + targetLeague);
            itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Fossil, itemRepository);

            // Essence
            streamTask = client.GetStreamAsync(Properties.Resources.EssenceRestUrl + targetLeague);
            itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Essence, itemRepository);

            // DivinationCard
            streamTask = client.GetStreamAsync(Properties.Resources.DivinationCardRestUrl + targetLeague);
            var divinationCardRepository = DataContractJsonSerializer.SerializeRepository<DivinationCardRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.DivinationCard, divinationCardRepository);

            // UniqueMap
            streamTask = client.GetStreamAsync(Properties.Resources.UniqueMapRestUrl + targetLeague);
            itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.UniqueMap, itemRepository);

            // Map
            streamTask = client.GetStreamAsync(Properties.Resources.MapRestUrl + targetLeague);
            itemRepository = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            // Save
            rewardPool.Save(RewardType.Map, itemRepository);

            ///*
            //// SkillGem
            //streamTask = client.GetStreamAsync(Properties.Resources.SkillGemRestUrl + targetLeague);
            //itemRepositories = DataContractJsonSerializer.SerializeRepository<ItemRepository>(await streamTask);
            //// Print 
            ////foreach (ItemLine itemLine in itemRepositories.Lines) { Debug.WriteLine(itemLine); }
            //*/

        }

    }

}
