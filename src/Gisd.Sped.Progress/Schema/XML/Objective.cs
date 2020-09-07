using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    public class Objective
    {
        [XmlAttribute("code")]
        public string Code;

        [XmlElement("timeframe")]
        public string TimeFrame;
        [XmlElement("conditions")]
        public string Conditions;
        [XmlElement("behavior")]
        public string Behavior;
        [XmlElement("criteria")]
        public string Criteria;

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
