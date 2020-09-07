using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("schoolyear.type")]
    public enum SchoolYearType
    {
        [XmlEnum("2019–2020")]
        _2019_2020,
        [XmlEnum("2020–2021")]
        _2020_2021,
        [XmlEnum("2021–2022")]
        _2021_2022
    }
}
