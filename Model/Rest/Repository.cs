using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PoeSyndicateBrowser.Model.Rest
{
    [DataContract(Name = "repository", Namespace = "Rest.Repository")]
    public abstract class Repository
    {
    }

    [DataContract(Name = "currencyRepository", Namespace = "Rest.CurrencyRepository")]
    public class CurrencyRepository : Repository
    {
        [DataMember(Name = "lines")]
        public List<CurrencyLine> Lines { get; set; }
    }

    [DataContract(Name = "itemRepository", Namespace = "Rest.ItemRepository")]
    public class ItemRepository : Repository
    {
        [DataMember(Name = "lines")]
        public List<ItemLine> Lines { get; set; }
    }

    [DataContract(Name = "divinationCardRepository", Namespace = "Rest.DivinationCardRepository")]
    public class DivinationCardRepository : Repository
    {
        [DataMember(Name = "lines")]
        public List<DivinationCardLine> Lines { get; set; }
    }
}
