using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jazb.Model.AdminModel.ManageAmCard;
using Jazb.Datalayer.Context;
using System.Data.Entity;
using Jazb.DomainClasses.Entities;
using EntityFramework.BulkInsert.Extensions;

namespace Jazb.Servicelayer.EFServices
{
    public class AmountCardService : IAmountCardService
    {

        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AmountCard> _amountCard;

        public AmountCardService(IUnitOfWork uow)
        {
            _uow = uow;
            _amountCard = _uow.Set<AmountCard>();
            _searchTakeCount = 10;
        }

        public int Add(AddAmountCardModel model)
        {
           
            AmountCard AC;
            try
            {
             
                AC = new AmountCard
                {
                    Amount = model.Amount,
                    AmountSerial = model.AmountSerial,
                    AzmoonId = model.AzmoonId,
                    EmailAddress = "",
                    FirstName = "",
                    LastName = "",
                    PaymentNumber = "",
                    PhoneNumber = ""
                };
                _amountCard.Add(AC);

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int Add(List<AmountCard> model)
        {
            _uow.BulkInsertAllData(model);
            _uow.SaveChanges();
            return 1;
        }


        public int DeleteUseLess(int AzmoonId)
        {

            try
            {
                var qlis = _amountCard.Where(x => x.AzmoonId== AzmoonId && x.FirstName == string.Empty).ToList();
                foreach (var a in qlis)
                {
                    _amountCard.Remove(a);
                }
                _uow.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int Edit(EditAmountCardModel model)
        {
          try
            {
                var ac = _amountCard.Find(model.Id);
                ac.Amount = model.Amount;
                ac.AmountSerial = model.AmountSerial;
                ac.AzmoonId = model.AzmoonId;
                ac.EmailAddress = model.EmailAddress;
                ac.FirstName = model.FirstName;
                ac.LastName = model.LastName;
                ac.PaymentNumber = model.PaymentNumber;
                ac.PhoneNumber = model.PhoneNumber;

                _uow.SaveChanges();
                return 1;

            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public AmountCard GetAmountCard(int Id)
        {

            var ac = _amountCard.Find(Id);
            return ac;
        }

        public List<AmountCard> GetFreeAmountCard(int AzmoonId)
        {
            var lstAC = _amountCard.Where(x => x.FirstName == string.Empty).ToList();
            return lstAC;
        }

        public List<string> GetSerials()
        {
            var lstSerial = _amountCard.Select(x => x.AmountSerial).ToList();
            return lstSerial;
        }

        

        private string GenerateSerial()
        {
            GenerateUniqCode GUC = new GenerateUniqCode();

            bool bln = true;
            string Code = "";
            var lstSerials = GetSerials();
            while (bln)
            {
                Code = GUC.GetPaymentUniqCode(12);

                var pcount = lstSerials.Where(x => x == Code).Count();
                if (pcount == 0)
                    bln = false;
            }

            return Code;


        }
        public bool ExistSerialNumber(int AzmoonId, string Serialnumber)
        {
            var lstac = _amountCard.Any(x => x.AzmoonId == AzmoonId && x.AmountSerial == Serialnumber);
            return lstac;
        }
        public bool IsPayments(int AzmoonId, string Serialnumber)
        {
            var lstac = _amountCard.Where(x => x.AzmoonId == AzmoonId && x.AmountSerial == Serialnumber).ToList();
            bool bln = false;
            if(lstac.Count()>0)
            {
                var AC = lstac.First();
                if (!string.IsNullOrEmpty(AC.PaymentNumber))
                    bln= true;
                else
                    bln= false;
            }
            return bln;
        }

        public int GetCount(int AzmoonId)
        {
            return _amountCard.Where(x => x.AzmoonId == AzmoonId).Count();
        }

      
    }
}
