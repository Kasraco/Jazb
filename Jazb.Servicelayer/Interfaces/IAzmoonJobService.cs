using Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IAzmoonJobService
    {
        int AddAzmoonJob(AzmoonJobModel model);
        int EditAzmoonJobs(ChaireNumberViewModel model);
        int EditAzmoonJob(EditAzmoonJobModel model);
        ChaireNumberViewModel GetAzmoonJobsModel(int AzmoonId);

        EditAzmoonJobModel GetAzmoonJob(int Id);
        bool ExistJobPlace(ExsitAzmoonJobModel model);
    }
}
