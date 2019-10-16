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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;

namespace PoeSyndicateBrowser.View
{
    /// <summary>
    /// MemberControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MemberControl : UserControl
    {
        public MemberPool MemberPool;

        private const string MEMBER_FILE_PATH = "Json/member.json";

        public MemberControl()
        {
            InitializeComponent();

            MemberPool = MemberPool.Factory.Create(MEMBER_FILE_PATH);
            MemberItems.ItemsSource = MemberPool.Data;
        }

        // test
        public void TestConnection()
        {
            Debug.WriteLine("- MemberControl.TestConnection()");
        }

    }

    [DataContract(Name = "memberPool")]
    public class MemberPool
    {
        [DataMember(Name = "member")]
        public ObservableCollection<Member> Data { get; set; }

        public static class Factory
        {
            public static MemberPool Create(string jsonFilePath)
            {
                MemberPool ret;

                using (StreamReader jsonStreamReader = new StreamReader(jsonFilePath))
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

    [DataContract]
    public class Member
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "grade")]
        public int Grade { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "korName")]
        public string KorName { get; set; }

        [DataMember(Name = "engName")]
        public string EngName { get; set; }

        [DataMember(Name = "recommend")]
        public string Recommend { get; set; }

        [DataMember(Name = "recommendCaptain")]
        public string RecommendCaptain { get; set; }

        [DataMember(Name = "transportation")]
        public Transportation Transportation { get; set; }

        [DataMember(Name = "fortification")]
        public Fortification Fortification { get; set; }

        [DataMember(Name = "research")]
        public Research Research { get; set; }

        [DataMember(Name = "intervention")]
        public Intervention Intervention { get; set; }

        private bool IsInitialized = false;

        private const string IMAGE_PATH = "/Resources/";
        private const string RECOMMEND_DELIMITER = " / ";
        private const string RECOMMEND_CAPTAIN_APPENDER = " 대장";

        public void Initialize()
        {
            if(!IsInitialized)
            {
                FormatRecommend();
                FormatRecommendCaptain();

                FormatImagePath();
                Transportation.FormatImagePath();
                Fortification.FormatImagePath();
                Research.FormatImagePath();
                Intervention.FormatImagePath();

                Transportation.FormatInfoLine();
                Fortification.FormatInfoLine();
                Research.FormatInfoLine();
                Intervention.FormatInfoLine();

                IsInitialized = true;
            }
        }

        private void FormatRecommend()
        {
            if (Recommend.Length > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int index = 0; index < Recommend.Length; index++)
                {
                    if (stringBuilder.Length == 0)
                        stringBuilder.Append(Recommend.ElementAt(index));
                    else
                        stringBuilder.Append(RECOMMEND_DELIMITER).Append(Recommend.ElementAt(index));
                }

                Recommend = stringBuilder.ToString();
            }
        }

        private void FormatRecommendCaptain()
        {
            if (RecommendCaptain.Length > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int index = 0; index < RecommendCaptain.Length; index++)
                {
                    if (stringBuilder.Length == 0)
                        stringBuilder.Append(RecommendCaptain.ElementAt(index));
                    else
                        stringBuilder.Append(RECOMMEND_DELIMITER).Append(RecommendCaptain.ElementAt(index));
                }

                RecommendCaptain = stringBuilder.Append(RECOMMEND_CAPTAIN_APPENDER).ToString();
            }
        }

        private void FormatImagePath()
        {
            Image = new StringBuilder(IMAGE_PATH).Append(Image).ToString();
        }

    }

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
            Image = new StringBuilder(IMAGE_PATH).Append(Image).ToString();
        }

        public string PriceInfo { get; set; }

        private const string IMAGE_PATH = "/Resources/";

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
