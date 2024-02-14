using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class AzmooneducatedSkill
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
        public string AzmooneducatedSkillTitle { get; set; }
        public long AzmooneducatedSkillCode { get; set; }
    }
}
