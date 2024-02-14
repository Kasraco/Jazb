using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel;
using Jazb.Model.Report;
using Jazb.Model.ValentearModel;
using Jazb.Servicelayer.EFServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IValentearService
    {

        int Count { get; }
        int CountValentear(int AzmoonId);
        ReportSplitValentear CountGenderByJobPlace(int AzmoonId, int JobId, long PlaceId, int BlockState, int ActionType);

        IList<Valentear> GetAllValentearDataTable(string term, int page, int count, ValentearSearchBy searchBy);
        IList<Valentear> GetAllValentearDataTable(int AzmoonId, string term, int page, int count, ValentearSearchBy searchBy);
        int GetAllValentearDataTable(int AzmoonId, string term, ValentearSearchBy searchBy);
        IList<Valentear> GetBlockedValentears(int AzmoonId);
        IList<Valentear> GetValentears(int AzmoonId);
        IList<Valentear> GetValentears(int AzmoonId, int JobId, long PlaceId, int BlockState, int ActionType);
        
        IList<string> SearchByNationalId(string nationalId);
        IList<string> SearchByBirthCertificateNo(string birthCertificateNo);
        IList<string> SearchByFirstName(string firstName);
        IList<string> SearchByLastName(string lastName);
        IList<string> SearchByFatherName(string fatherName);

        IList<ValentearReportModel> GetDevotions(int azmoonId);
        IList<ValentearReportModel> GetAllValentear(int azmoonId);
        IList<ValentearCardReportModel> GetAllCardValentear(int azmoonId);
        IList<ValentearChairNumberReportModel> GetAllChairNumberValentear(int azmoonId);
        int SetChairNumber(int AzmoonId);


    }
}
