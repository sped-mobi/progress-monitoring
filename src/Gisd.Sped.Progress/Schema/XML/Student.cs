using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("student")]
    public class Student
    {
        [XmlAttribute("firstName")]
        public string FirstName;

        [XmlAttribute("lastName")]
        public string LastName;

        [XmlAttribute("dob")]
        public string DateOfBirth;

        [XmlAttribute("gender")]
        public GenderType Gender;

        [XmlAttribute("grade")]
        public string Grade;

        [XmlAttribute("campus")]
        public string Campus;

        [XmlAttribute("localID")]
        public string LocalID;

        [XmlElement("accommodations")]
        public Accommodations Accommodations;

        [XmlElement("scheduleOfServices")]
        public ScheduleOfServices ScheduleOfServices;

        [XmlElement("annualGoals")]
        public AnnualGoals AnnualGoals;

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
