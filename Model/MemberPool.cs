using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PoeSyndicateBrowser.Model
{
    [DataContract(Name = "memberPool")]
    public class MemberPool
    {
        [DataMember(Name = "member")]
        public ObservableCollection<Member> Data { get; set; }

        public static class Factory
        {
            public static MemberPool Create()
            {
                MemberPool ret;

                using (StreamReader jsonStreamReader = new StreamReader(Properties.Resources.MemberJsonPath))
                {
                    ret = new DataContractJsonSerializer(typeof(MemberPool)).ReadObject(jsonStreamReader.BaseStream) as MemberPool;

                    foreach (Member member in ret.Data)
                    {
                        member.Initialize();
                    }
                }

                return ret;
            }
        }
    }

}
