using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace PoeSyndicateBrowser.Model
{
    [DataContract]
    public abstract class Reward
    {
        [DataMember(Name = "isHighlight")]
        public bool IsHighlight { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "info")]
        public string Info { get; set; }

        [DataMember(Name = "detailInfo")]
        public string DetailInfo { get; set; }

        [DataMember(Name = "detailFlag")]
        public int DetailFlag { get; set; }

        [DataMember(Name = "detailType")]
        public string DetailType { get; set; }

        [DataMember(Name = "detailItems")]
        public List<string> DetailItems { get; set; }


        public void FormatImagePath()
        {
            Image = new StringBuilder(Properties.Resources.RewardImagePath).Append(Image).ToString();
        }

        public void FormatInfoLine()
        {
            const int MAX_LENGTH = 7;

            if (Info.Length > MAX_LENGTH)
            {
                StringBuilder ret = new StringBuilder();
                StringBuilder buffer = new StringBuilder();
                string[] strArray = Info.Split(' ');

                for (int idx = 0; idx < strArray.Length; idx++)
                {
                    if (buffer.Length == 0)
                    {
                        buffer.Append(strArray[idx]);
                    }
                    else
                    {
                        if (buffer.Length + strArray[idx].Length + 1 > MAX_LENGTH)
                        {
                            if (ret.Length > 0)
                            {
                                ret.Append('\n');
                            }

                            ret.Append(buffer);
                            buffer.Clear().Append(strArray[idx]);
                        }
                        else
                        {
                            buffer.Append(' ').Append(strArray[idx]);
                        }
                    }

                    if (idx == strArray.Length - 1)
                    {
                        if (ret.Length > 0)
                        {
                            ret.Append('\n');
                        }

                        ret.Append(buffer);
                    }
                }

                Info = ret.ToString();
            }
        }


        public string PriceInfo { get; set; }


    }

    [DataContract(Name = "transportation")]
    public class Transportation : Reward { }

    [DataContract(Name = "fortification")]
    public class Fortification : Reward { }

    [DataContract(Name = "research")]
    public class Research : Reward { }

    [DataContract(Name = "intervention")]
    public class Intervention : Reward { }

}
