using Jazb.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IReportService
    {
        List<ValenteraFullReportsModel> GetAllValentear(int AzmoonId);
        List<ValenteraFullAcceptPasokhnamehReportsModel> GetAllAcceptValentear(int AzmoonId);
        List<ValenteraFullWithNumberReportsModel> GetAllValentearWithNumber(int AzmoonId);
        List<ValenteraFullAcceptPictureValentearModel> GetAllAcceptPictureValentear(int AzmoonId);
        List<ValenteraFullResultAnswerReportModel> GetAllValentearAnswer(int AzmoonId, int Grade);
    }
}
