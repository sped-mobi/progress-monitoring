using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("accommodation")]
    public class Accommodation
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("subject")]
        public List<SubjectType> Subject;
    }
}
