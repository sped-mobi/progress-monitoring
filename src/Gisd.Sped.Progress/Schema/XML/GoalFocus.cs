using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    [XmlType("focus.type")]
    public enum GoalFocus
    {
        [XmlEnum("Fine Arts")]
        FineArts,
        Math,
        Health,
        [XmlEnum("Physical Education")]
        PhysicalEducation,
        Science,
        [XmlEnum("Social Studies")]
        SocialStudies,
        [XmlEnum("Vocational Skills")]
        VocationalSkills,
        Speech,
        [XmlEnum("Social/Emotional")]
        SocialEmotional,
        Reading,
        [XmlEnum("English/Language Arts")]
        EnglishLanguageArts,
        [XmlEnum("Activities of Daily Living II")]
        ActivitiesOfDailyLivingII,
        [XmlEnum("Written Expression")]
        WrittenExpression,


    }
}
