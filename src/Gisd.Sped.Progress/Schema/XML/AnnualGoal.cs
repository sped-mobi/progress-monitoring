using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    public class AnnualGoal
    {
        [XmlElement("timeframe")]
        public string TimeFrame;
        [XmlElement("conditions")]
        public string Conditions;
        [XmlElement("behavior")]
        public string Behavior;
        [XmlElement("criteria")]
        public string Criteria;

        [XmlAttribute("code")]
        public string Code;

        [XmlAttribute("focus")]
        public GoalFocus Focus;

        [XmlElement("objectives")]
        public Objectives Objectives;


        public string GetFocus()
        {
            return Focus switch
            {
                GoalFocus.FineArts => "Fine Arts",
                GoalFocus.Math => "Math",
                GoalFocus.Health => "Health",
                GoalFocus.PhysicalEducation => "Physical Education",
                GoalFocus.Science => "Science",
                GoalFocus.SocialStudies => "Social Studies",
                GoalFocus.VocationalSkills => "Vocational Skills",
                GoalFocus.Speech => "Speech",
                GoalFocus.SocialEmotional => "Social/Emotional",
                GoalFocus.Reading => "Reading",
                GoalFocus.EnglishLanguageArts => "English/Language Arts",
                GoalFocus.ActivitiesOfDailyLivingII => "Activities of Daily Living II",
                GoalFocus.WrittenExpression => "Written Expression",
                _ => "[Missing Focus]",
            };
        }

        public string ToStatement()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(TimeFrame.Trim());
            sb.Append(", ");
            sb.Append(Conditions.Trim());
            sb.Append(", ");
            sb.Append(Behavior.Trim());
            sb.Append(" ");
            sb.Append(Criteria.Trim());
            sb.Append(".");
            return sb.ToString();
        }
    }
}
