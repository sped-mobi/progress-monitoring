using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("pgdb.type")]
    public enum ProgressGradeDeterminedByType
    {
        [XmlEnum("Spec")]
        Spec,
        [XmlEnum("Gen")]
        Gen,
        [XmlEnum("Joint")]
        Joint
    }
}
