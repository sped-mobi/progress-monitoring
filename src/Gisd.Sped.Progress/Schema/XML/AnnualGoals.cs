﻿using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Gisd.Sped.Progress
{
    public class AnnualGoals
    {
        [XmlElement("annualGoal")]
        public List<AnnualGoal> Goals;
    }
}
