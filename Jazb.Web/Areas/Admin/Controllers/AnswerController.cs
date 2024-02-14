using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Jazb.Model.Report;

namespace Jazb.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public partial class AnswerController : Controller
    {
        //
        // GET: /Admin/Answer/
        private JazbDbContext context = new JazbDbContext();


        private readonly IUnitOfWork _uow;

        public AnswerController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public virtual ActionResult Index()
        {
            //var qn = context.AzmoonValentearAnswers.ToList();
            //foreach (var t in qn)
            //{
            //    t.Count = t.ValentearAnswer.Length;
            //    context.AzmoonValentearAnswers.Attach(t);
            //    context.Entry(t).Property(x => x.Count).IsModified = true;
            //    context.SaveChanges();
            //}
            // ReadKeies();

          
            return View();
        }

        public virtual ActionResult SetScore(int AzmoonId,bool HasBistoPanjDarsad)
        {
            int Capacity = 0;
            decimal zarb15 = Convert.ToDecimal("1/5");
            decimal zarb3 = Convert.ToDecimal("3");
            int Capacity15 = 0;
            int Capacity3 = 0;

            int countTFPDevotion = 0;
            int countFivePercent = 0;
            decimal countOther = 0;
            int indexRowScore = 0;
            List<Valentear> ResetValentear = new List<Valentear>();
            List<Valentear> lstTFPDevotionValentear = new List<Valentear>();
            List<Valentear> lstTFPDevotionValentear2 = new List<Valentear>();
            List<Valentear> lstFiveDevotionValentear = new List<Valentear>();
            List<Valentear> lstQoutaValentear = new List<Valentear>();
            List<Valentear> lstOtherValentear = new List<Valentear>();

            var vlas = context.Valentears.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).Select(x => x.Id).ToList();
            foreach(var t in vlas)
            {
                var V = context.Valentears.Find(t);
                V.score = 0;
                V.grade = 0;
                //ResetValentear.Add(t);
                context.Valentears.Attach(V);
                context.Entry(V).Property(x => x.score).IsModified = true;
                context.Entry(V).Property(x => x.grade).IsModified = true;
                context.SaveChanges();
            }

           // _uow.BulkUpdateAllData(ResetValentear);

            var AJPlist = context.AzmoonPlaces.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            foreach(var ajp in AJPlist)
            {

                lstTFPDevotionValentear = new List<Valentear>();
                lstTFPDevotionValentear2 = new List<Valentear>();
                lstFiveDevotionValentear = new List<Valentear>();
                lstQoutaValentear = new List<Valentear>();
                lstOtherValentear = new List<Valentear>();
                indexRowScore = 0;

                Capacity = ajp.Capacity;
                var c15 = decimal.Multiply(Capacity, zarb15);
                var c3 = decimal.Multiply(Capacity, zarb3);
                Capacity15 = int.Parse(Math.Round(c15, MidpointRounding.ToEven).ToString());
                Capacity3 = int.Parse(Math.Round(c3, MidpointRounding.ToEven).ToString());


                ///////////////////////////// Calculate 25% Devotions //////////////////////////////
                if (HasBistoPanjDarsad)
                {
                    var TwentyFivePercent = decimal.Parse(((Capacity * 25) / 100).ToString());
                    countTFPDevotion = int.Parse(Math.Round(TwentyFivePercent, MidpointRounding.ToEven).ToString());

                    var vlistFirstDevotion = context.AzmoonValentearResults.Include(x => x.Azmoon).Include(x => x.Valentear)
                                                  .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                                         x.Valentear.Accept == 1 &&
                                                         x.Valentear.JobId == ajp.JobId &&
                                                         x.Valentear.PlaceId==ajp.PlaceCode &&
                                                         x.Valentear.SumDevotionType > 31)
                             .OrderByDescending(x => x.Valentear.MaxDevotionType)
                             .ThenByDescending(x => x.Valentear.MaxQoutaType)
                             .ThenByDescending(x => x.FinalScore)
                             .ThenByDescending(x => x.TechnicalFinalScore)
                             .ThenByDescending(x => x.GeneralFinalScore)
                             .Take(countTFPDevotion)
                             .ToList();






                    foreach (var vd in vlistFirstDevotion)
                    {
                        indexRowScore = indexRowScore + 1;

                        if (indexRowScore <= Capacity)
                            vd.Valentear.grade = 1;
                        else if (indexRowScore > Capacity && indexRowScore <= Capacity15)
                            vd.Valentear.grade = 2;
                        else if (indexRowScore > Capacity15 && indexRowScore <= Capacity3)
                            vd.Valentear.grade = 3;
                        else
                            vd.Valentear.grade = 4;

                        vd.Valentear.score = indexRowScore;
                        //lstTFPDevotionValentear.Add(vd.Valentear);

                        context.Valentears.Attach(vd.Valentear);
                        context.Entry(vd.Valentear).Property(x => x.score).IsModified = true;
                        context.Entry(vd.Valentear).Property(x => x.grade).IsModified = true;
                        context.SaveChanges();
                    }

                   // _uow.BulkUpdateAllData(lstTFPDevotionValentear);

                    if (vlistFirstDevotion.Count< countTFPDevotion)
                    {
                        var cnt = countTFPDevotion - vlistFirstDevotion.Count();
                        var vlistFirstDevotion2 = context.Valentears.Include(x => x.Azmoon)
                                                  .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                                         x.Accept == 1 &&
                                                         x.JobId == ajp.JobId &&
                                                          x.PlaceId == ajp.PlaceCode &&
                                                         x.score == 0 &&
                                                         x.SumDevotionType > 31)
                             .OrderByDescending(x => x.MaxDevotionType)
                             .ThenByDescending(x => x.MaxQoutaType)
                             .Take(cnt)
                             .ToList();


                        foreach (var vd in vlistFirstDevotion2)
                        {
                            indexRowScore = indexRowScore + 1;

                            if (indexRowScore <= Capacity)
                                vd.grade = 1;
                            else if (indexRowScore > Capacity && indexRowScore <= Capacity15)
                                vd.grade = 2;
                            else if (indexRowScore > Capacity15 && indexRowScore <= Capacity3)
                                vd.grade = 3;
                            else
                                vd.grade = 4;

                            vd.score = indexRowScore;
                            //lstTFPDevotionValentear2.Add(vd);

                            context.Valentears.Attach(vd);
                            context.Entry(vd).Property(x => x.score).IsModified = true;
                            context.Entry(vd).Property(x => x.grade).IsModified = true;
                            context.SaveChanges();
                        }

                        // _uow.BulkUpdateAllData(lstTFPDevotionValentear2);

                    }



                   
                }
              



                ///////////////////////////// Calculate 5% Devotions //////////////////////////////

                var cfp = decimal.Parse(((Capacity * 5) / 100).ToString());
                countFivePercent = int.Parse(Math.Round(cfp, MidpointRounding.ToEven).ToString());
                var vlistSecoundDevotion = context.AzmoonValentearResults.Include(x => x.Azmoon).Include(x => x.Valentear)
                               .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                         x.Valentear.Accept == 1 &&
                                         x.Valentear.JobId == ajp.JobId &&
                                          x.Valentear.PlaceId == ajp.PlaceCode &&
                                         x.Valentear.SumDevotionType < 31)
                                         .Select(x => new
                                         {
                                             x.Valentear.Id,
                                             x.Valentear.MaxDevotionType,
                                             x.Valentear.MaxQoutaType,
                                             x.FinalScore,
                                             x.TechnicalFinalScore,
                                             x.GeneralFinalScore
                                         })
                               .OrderByDescending(x => x.MaxDevotionType)
                               .ThenByDescending(x => x.MaxQoutaType)
                               .ThenByDescending(x => x.FinalScore)
                               .ThenByDescending(x => x.TechnicalFinalScore)
                               .ThenByDescending(x => x.GeneralFinalScore)
                               .Take(countFivePercent)
                               .ToList();

                foreach (var vd in vlistSecoundDevotion)
                {
                    Valentear V = context.Valentears.Find(vd.Id);
                    indexRowScore = indexRowScore + 1;

                    if (indexRowScore <= Capacity)
                        V.grade = 1;
                    else if (indexRowScore > Capacity && indexRowScore <= Capacity15)
                        V.grade = 2;
                    else if (indexRowScore > Capacity15 && indexRowScore <= Capacity3)
                        V.grade = 3;
                    else
                        V.grade = 4;

                    V.score = indexRowScore;
                   // lstFiveDevotionValentear.Add(V);
                    context.Valentears.Attach(V);
                    context.Entry(V).Property(x => x.score).IsModified = true;
                    context.Entry(V).Property(x => x.grade).IsModified = true;
                    context.SaveChanges();
                }
                //_uow.BulkUpdateAllData(lstFiveDevotionValentear);

               
                ///////////////////////////////////////////////////////////
                countOther = Capacity - (countTFPDevotion + countFivePercent);
                ///////////////////////////////////////////////////////////

                var vlistQouta = context.AzmoonValentearResults.Include(x => x.Azmoon).Include(x => x.Valentear).AsNoTracking()
                              .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                        x.Valentear.Accept == 1 &&
                                        x.Valentear.JobId == ajp.JobId &&
                                         x.Valentear.PlaceId == ajp.PlaceCode &&
                                        x.Valentear.MaxQoutaType > 0 &&
                                        x.Valentear.score == 0)
                                        .Select(x => new
                                        {
                                            x.Valentear.Id,
                                            x.Valentear.MaxQoutaType,
                                            x.FinalScore,
                                            x.TechnicalFinalScore,
                                            x.GeneralFinalScore
                                        })
                              .OrderByDescending(x => x.MaxQoutaType)
                              .ThenByDescending(x => x.FinalScore)
                              .ThenByDescending(x => x.TechnicalFinalScore)
                              .ThenByDescending(x => x.GeneralFinalScore).ToList();


                foreach (var vd in vlistQouta)
                {
                    Valentear V = context.Valentears.Find(vd.Id);
                    indexRowScore = indexRowScore + 1;

                    if (indexRowScore <= Capacity)
                        V.grade = 1;
                    else if (indexRowScore > Capacity && indexRowScore <= Capacity15)
                        V.grade = 2;
                    else if (indexRowScore > Capacity15 && indexRowScore <= Capacity3)
                        V.grade = 3;
                    else
                        V.grade = 4;

                    V.score = indexRowScore;
                  //  lstQoutaValentear.Add(V);

                    context.Valentears.Attach(V);
                    context.Entry(V).Property(x => x.score).IsModified = true;
                    context.Entry(V).Property(x => x.grade).IsModified = true;
                    context.SaveChanges();
                }
               // _uow.BulkUpdateAllData(lstQoutaValentear);


                var vlistOther = context.AzmoonValentearResults.Include(x => x.Azmoon).Include(x => x.Valentear).AsNoTracking()
                              .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                        x.Valentear.Accept == 1 &&
                                        x.Valentear.JobId == ajp.JobId &&
                                         x.Valentear.PlaceId == ajp.PlaceCode &&
                                        x.Valentear.score == 0)
                                         .Select(x => new
                                         {
                                             x.Valentear.Id,
                                             x.FinalScore,
                                             x.TechnicalFinalScore,
                                             x.GeneralFinalScore
                                         })

                              .OrderByDescending(x => x.FinalScore)
                              .ThenByDescending(x => x.TechnicalFinalScore)
                              .ThenByDescending(x => x.GeneralFinalScore).ToList();


                foreach (var vd in vlistOther)
                {
                    Valentear V = context.Valentears.Find(vd.Id);
                    indexRowScore = indexRowScore + 1;

                    if (indexRowScore <= Capacity)
                        V.grade = 1;
                    else if (indexRowScore > Capacity && indexRowScore <= Capacity15)
                        V.grade = 2;
                    else if (indexRowScore > Capacity15 && indexRowScore <= Capacity3)                       
                        V.grade = 3;
                    else
                        V.grade = 4;


                    V.score = indexRowScore;
                  //  lstOtherValentear.Add(V);
                    context.Valentears.Attach(V);
                    context.Entry(V).Property(x => x.score).IsModified = true;
                    context.Entry(V).Property(x => x.grade).IsModified = true;
                    context.SaveChanges();
                }
             //   _uow.BulkUpdateAllData(lstOtherValentear);


            }
            return Content("SetScoreSuccess");
        }



        public virtual ActionResult CalculateAnswer(int AzmoonId)
        {
            ReadKeies(AzmoonId);
            return Content("Success");
        }
        public void ReadKeies(int AzmoonId)
        {
            var qAKList = context.AzmoonAnswerKeies.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            foreach (var Key in qAKList)
                ReadValentearAnswer(Key, AzmoonId);
        }

        public void ReadValentearAnswer(AzmoonAnswerKey Key,int AzmoonId)
        {
            var qVAList = context.AzmoonValentearAnswers.Where(x => x.KeyCode == Key.AnswerCode && x.Azmoon.AzmoonId == AzmoonId).ToList();
            AnswerResultModel ARM;
            List<AzmoonValentearResult> lstAzVR = new List<AzmoonValentearResult>();
           int i = 0;
            foreach (var VA in qVAList)
            {
                //if (VA.ChairCode.Equals("1370"))
                   // i++;
                ARM = CompayeAnswerWithKey(Key.AnswerKey, VA.ValentearAnswer, Key.GeneralQuestionsFrom, Key.GeneralQuestionsTo, Key.TechnicalQuestionsFrom,Key.TechnicalQuestionsTo,
                                           Key.GeneralQuestionPerQuestion, Key.GeneralQuestionsRatio,Key.GeneralQuestionPerNegative,Key.GeneralQuestionsNegativeRatio,
                                           Key.TechnicalQuestionsPerQuestion, Key.TechnicalQuestionsRatio,Key.TechnicalQuestionsPerNeative,Key.TechnicalQuestionsNegativeRatio);

                var qVList = context.Valentears.Include(x => x.Azmoon).Where(x => x.ChairCode == VA.ChairCode && x.Accept == 1 && x.Azmoon.AzmoonId == AzmoonId).ToList();
                if (qVList.Count() > 0)
                {
                    var V = qVList.First();
                    int Vid = V.Id;
                    AzmoonValentearResult AVR = new AzmoonValentearResult
                    {
                        CountQuestions = ARM.CountQuestions,
                        FinalScore = ARM.FinalScore,
                        GeneralFinalScore = ARM.GeneralFinalScore,
                        GeneralNegativeScore = ARM.GeneralNegativeScore,
                        GeneralPositiveScore = ARM.GeneralPositiveScore,
                        GeneralQuestionsCountEmpty = ARM.GeneralQuestionsCountEmpty,
                        GeneralQuestionsCountFalse = ARM.GeneralQuestionsCountFalse,
                        GeneralQuestionsCountTick = ARM.GeneralQuestionsCountTick,
                        GeneralQuestionsCountTowOptions = ARM.GeneralQuestionsCountTowOptions,
                        GeneralQuestionsCountTrue = ARM.GeneralQuestionsCountTrue,
                        TechnicalFinalScore = ARM.TechnicalFinalScore,
                        TechnicalNegativeScore = ARM.TechnicalNegativeScore,
                        TechnicalPositiveScore = ARM.TechnicalPositiveScore,
                        TechnicalQuestionsCountEmpty = ARM.TechnicalQuestionsCountEmpty,
                        TechnicalQuestionsCountFalse = ARM.TechnicalQuestionsCountFalse,
                        TechnicalQuestionsCountTick = ARM.TechnicalQuestionsCountTick,
                        TechnicalQuestionsCountTowOptions = ARM.TechnicalQuestionsCountTowOptions,
                        TechnicalQuestionsCountTrue = ARM.TechnicalQuestionsCountTrue,
                        Valentear = V,
                        Azmoon=V.Azmoon
                    };
                    lstAzVR.Add(AVR);
                }
            }

            _uow.BulkInsertAllData(lstAzVR);


        }


        public AnswerResultModel CompayeAnswerWithKey(string Key, string Answer, int OFrom, int OTo, int TFrom, int TTo,
                                                      int PerGP, double GS, int PerGN, double GNS,
                                                      int PerTP, double TS,int PerTN, double TNS)
        {

            AnswerResultModel ARM = new AnswerResultModel();
            int CountQuestions = Answer.Length;
            int GeneralQuestionsCountTick = 0;
            int GeneralQuestionsCountTrue = 0;
            int GeneralQuestionsCountFalse = 0;
            int GeneralQuestionsCountEmpty = 0;
            int GeneralQuestionsCountTowOptions = 0;
            int TechnicalQuestionsCountTick = 0;
            int TechnicalQuestionsCountTrue = 0;
            int TechnicalQuestionsCountFalse = 0;
            int TechnicalQuestionsCountEmpty = 0;
            int TechnicalQuestionsCountTowOptions = 0;

            double GeneralQuestionDividFalse = 0;
            double TechnicalQuestionDividFalse = 0;
            double GeneralPositiveScore = 0;
            double GeneralNegativeScore = 0;
            double GeneralFinalScore = 0;
            double TechnicalPositiveScore = 0;
            double TechnicalNegativeScore = 0;
            double TechnicalFinalScore = 0;
            double FinalScore = 0;

            if ((!string.IsNullOrEmpty(Answer)) || (!string.IsNullOrWhiteSpace(Answer)))
            {
                for (int index = (OFrom - 1); index < OTo; index++)
                {
                    if (Answer[index] == Key[index])
                    {
                        GeneralQuestionsCountTrue++;
                        GeneralQuestionsCountTick++;
                    }
                    if ((string.IsNullOrEmpty(Answer[index].ToString())) || (string.IsNullOrWhiteSpace(Answer[index].ToString())))
                    {
                        GeneralQuestionsCountEmpty++;
                    }
                    else if (Answer[index] == '*')
                    {
                        GeneralQuestionsCountTowOptions++;
                        GeneralQuestionsCountTick++;
                    }
                }
                var GcountKey = OTo - OFrom + 1;
                GeneralQuestionsCountFalse = GcountKey - (GeneralQuestionsCountTrue + GeneralQuestionsCountEmpty);
            }
         
           
           

            // ------------- Takhasosi ---------------
            if (TFrom != 0 && TFrom > OTo)
            {
                if ((!string.IsNullOrEmpty(Answer)) || (!string.IsNullOrWhiteSpace(Answer)))
                {
                    int CountEmpty = 0;
                    if (Answer.Length < TTo)
                    {
                        CountEmpty = TTo - Answer.Length;
                        TechnicalQuestionsCountEmpty += CountEmpty;
                    }

                    //for (int index = (TFrom - 1); index < TTo; index++)
                    for (int index = (TFrom - 1); index < Answer.Length; index++)
                    {

                        if (Answer[index] == Key[index])
                        {
                            TechnicalQuestionsCountTrue++;
                            TechnicalQuestionsCountTick++;
                        }
                        if ((string.IsNullOrEmpty(Answer[index].ToString())) || (string.IsNullOrWhiteSpace(Answer[index].ToString())))
                        {
                            TechnicalQuestionsCountEmpty++;
                        }
                        else if (Answer[index] == '*')
                        {
                            TechnicalQuestionsCountTowOptions++;
                            TechnicalQuestionsCountTick++;
                        }

                    }

                    var TCountKey = TTo - TFrom + 1;
                    TechnicalQuestionsCountFalse = TCountKey - (TechnicalQuestionsCountTrue + TechnicalQuestionsCountEmpty);
                }
            }



            GeneralQuestionDividFalse = PerGN / GNS;
            GeneralPositiveScore = GeneralQuestionsCountTrue * GS;
            GeneralNegativeScore = (GeneralQuestionsCountFalse / GeneralQuestionDividFalse) * GS;
            GeneralFinalScore = GeneralPositiveScore - GeneralNegativeScore;

            if (TFrom != 0 && TFrom > OTo)
            {
                TechnicalQuestionDividFalse = PerTN / TNS;
                TechnicalPositiveScore = TechnicalQuestionsCountTrue * TS;
                TechnicalNegativeScore = (TechnicalQuestionsCountFalse / TechnicalQuestionDividFalse) * TS;
                TechnicalFinalScore = TechnicalPositiveScore - TechnicalNegativeScore;
                FinalScore = TechnicalFinalScore + GeneralFinalScore;
            }
            else
            {
                TechnicalQuestionDividFalse = 0;
                TechnicalPositiveScore = 0;
                TechnicalNegativeScore = 0;
                TechnicalFinalScore = 0;
                FinalScore = GeneralFinalScore;
            }
            


            ARM.CountQuestions = CountQuestions;
            ARM.FinalScore = FinalScore;
            ARM.GeneralFinalScore = GeneralFinalScore;
            ARM.GeneralNegativeScore = GeneralNegativeScore;
            ARM.GeneralPositiveScore = GeneralPositiveScore;
            ARM.GeneralQuestionsCountEmpty = GeneralQuestionsCountEmpty;
            ARM.GeneralQuestionsCountFalse = GeneralQuestionsCountFalse;
            ARM.GeneralQuestionsCountTick = GeneralQuestionsCountTick;
            ARM.GeneralQuestionsCountTowOptions = GeneralQuestionsCountTowOptions;
            ARM.GeneralQuestionsCountTrue = GeneralQuestionsCountTrue;
            ARM.TechnicalFinalScore = TechnicalFinalScore;
            ARM.TechnicalNegativeScore = TechnicalNegativeScore;
            ARM.TechnicalPositiveScore = TechnicalPositiveScore;
            ARM.TechnicalQuestionsCountEmpty = TechnicalQuestionsCountEmpty;
            ARM.TechnicalQuestionsCountFalse = TechnicalQuestionsCountFalse;
            ARM.TechnicalQuestionsCountTick = TechnicalQuestionsCountTick;
            ARM.TechnicalQuestionsCountTowOptions = TechnicalQuestionsCountTowOptions;
            ARM.TechnicalQuestionsCountTrue = TechnicalQuestionsCountTrue;
            
            return ARM;            
        }


    }
}
