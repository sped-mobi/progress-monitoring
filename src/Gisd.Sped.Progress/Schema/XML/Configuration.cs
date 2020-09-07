using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("configuration")]
    public class Configuration
    {
        [XmlElement("schoolyear")]
        public List<SchoolYear> SchoolYear;
    }
}
