using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("accommodationGroup")]
    public class AccommodationGroup
    {
        [XmlElement("accommodation")]
        public List<Accommodation> Accommodations;
    }
}
