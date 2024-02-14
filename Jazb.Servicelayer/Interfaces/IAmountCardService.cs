using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel.ManageAmCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IAmountCardService
    {

        int Add(AddAmountCardModel model);
        int Add(List<AmountCard> model);
        int Edit(EditAmountCardModel model);

        AmountCard GetAmountCard(int Id);
        List<AmountCard> GetFreeAmountCard(int AzmoonId);
        bool IsPayments(int AzmoonId, string Serialnumber);
        bool ExistSerialNumber(int AzmoonId, string Serialnumber);
        List<string> GetSerials();
       int GetCount(int AzmoonId);
       

        int DeleteUseLess(int AzmoonId);
    }
}
