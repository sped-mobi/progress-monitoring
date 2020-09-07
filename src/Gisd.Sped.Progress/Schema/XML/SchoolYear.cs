using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("schoolyear")]
    public class SchoolYear
    {
        [XmlAttribute("identifier")]
        public SchoolYearType Identifier;

        [XmlElement("students")]
        public Students Students;
    }
}
