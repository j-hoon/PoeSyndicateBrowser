using System.IO;

namespace PoeSyndicateBrowser.Core.Rest
{
    public class DataContractJsonSerializer
    {
        public static T SerializeRepository<T>(Stream stream) where T : PoeSyndicateBrowser.Model.Rest.Repository
        {
            return new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T)).ReadObject(stream) as T;
        }
    }
}
