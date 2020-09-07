using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("students")]
    public class Students
    {
        [XmlElement("student")]
        public List<Student> Student;
    }
}
