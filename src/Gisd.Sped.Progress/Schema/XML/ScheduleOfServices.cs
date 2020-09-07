using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("scheduleOfServices")]
    public class ScheduleOfServices
    {
        [XmlElement("course")]
        public List<Course> Courses;
    }
}
