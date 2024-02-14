using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonAnswerKey
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
        public int AnswerCode { get; set; }
        public string AnswerKey { get; set; }

        public int GeneralQuestionsFrom { get; set; }
        public int GeneralQuestionsTo { get; set; }
        public int GeneralQuestionPerQuestion { get; set; }
        public double GeneralQuestionsRatio { get; set; }
        public int GeneralQuestionPerNegative { get; set; }
        public double GeneralQuestionsNegativeRatio { get; set; }

        public int TechnicalQuestionsFrom { get; set; }
        public int TechnicalQuestionsTo { get; set; }
        public int TechnicalQuestionsPerQuestion { get; set; }
        public double TechnicalQuestionsRatio { get; set; }
        public int TechnicalQuestionsPerNeative { get; set; }
        public double TechnicalQuestionsNegativeRatio { get; set; }

    }
}
