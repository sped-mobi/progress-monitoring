using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("accommodations")]
    public class Accommodations
    {
        [XmlElement("accommodationGroup")]
        public List<AccommodationGroup> Groups;
    }
}
