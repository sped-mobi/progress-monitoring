using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    public class Objectives
    {
        [XmlElement("objective")]
        public List<Objective> Objective;
    }
}
