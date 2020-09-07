using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("subject.type")]
    public enum SubjectType
    {
        ELA,
        Rdg,
        Math,
        SC,
        PE,
        SS,
        FA
    }
}
