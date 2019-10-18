using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace PoeSyndicateBrowser.Model
{
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

        private const string RECOMMEND_DELIMITER = " / ";
        private const string RECOMMEND_CAPTAIN_APPENDER = " 대장";

        public void Initialize()
        {
            if (!IsInitialized)
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
            Image = new StringBuilder(Properties.Resources.MemberImagePath).Append(Image).ToString();
        }

    }

}
