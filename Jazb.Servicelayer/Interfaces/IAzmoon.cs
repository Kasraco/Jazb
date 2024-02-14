using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IAzmoon
    {
        int Count { get; }
        IList<AzmoonDataTableModel> GetDataTable(string term, int page, int count, Order order, AzmoonOrderBy orderBy, AzmoonSearchBy searchBy);
        AddAzmoonStatus AddAzmoon(AzmoonModel model);
        AddAzmoonStatus EditAzmoon(AzmoonEditModel model);
        AddAzmoonStatus SettingCardAzmoon(AzmoonCardModel model); 
        void Active(int Id);
        void DeActive(int Id);
        bool GetStatus(int Id);
    }
}
