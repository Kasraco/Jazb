using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob
{
    public class ChaireNumberModel
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public int ManFrom { get; set; }
        public int ManTo { get; set; }
        public int ManCapacity { get; set; }
        public int WomanFrom { get; set; }
        public int WomanTo { get; set; }
        public int WomanCapacity { get; set; }
        public string ManRegionExam { get; set; }
        public string WomanRegionExam { get; set; }
    }

    public class ChaireNumberViewModel
    {
        public int AzmoonId { get; set; }
        public List<ChaireNumberModel> ChairNModel { get; set; }
    }
}
