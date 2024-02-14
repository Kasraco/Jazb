using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonValentearResult
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
        public Valentear Valentear { get; set; }
        public int CountQuestions { get; set; }

        public int GeneralQuestionsCountTick { get; set; }
        public int GeneralQuestionsCountTrue { get; set; }
        public int GeneralQuestionsCountFalse { get; set; }
        public int GeneralQuestionsCountEmpty { get; set; }
        public int GeneralQuestionsCountTowOptions { get; set; }

        public int TechnicalQuestionsCountTick { get; set; }
        public int TechnicalQuestionsCountTrue { get; set; }
        public int TechnicalQuestionsCountFalse { get; set; }
        public int TechnicalQuestionsCountEmpty { get; set; }
        public int TechnicalQuestionsCountTowOptions { get; set; }


        public double GeneralPositiveScore { get; set; }
        public double GeneralNegativeScore { get; set; }
        public double GeneralFinalScore { get; set; }

        public double TechnicalPositiveScore { get; set; }
        public double TechnicalNegativeScore { get; set; }
        public double TechnicalFinalScore { get; set; }

        public double FinalScore { get; set; }

        public string StateResult { get; set; }


    }
}
