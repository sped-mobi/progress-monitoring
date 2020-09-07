using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("course")]
    public class Course
    {
        [XmlAttribute("gen")]
        public string GeneralEducationTime;

        [XmlAttribute("spec")]
        public string SpecialEducationTime;

        [XmlAttribute("pgdb")]
        public ProgressGradeDeterminedByType ProgressGradeDeterminedBy;
    }
}
