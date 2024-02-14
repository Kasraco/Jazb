using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class Payment
    {
        public Payment()
        {
            InsertDatetime = System.DateTime.Now;
        }
        public int PaymentId { get; set; }
        public string PaymentIdstring { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
      
        public string ReferenceNumber { get; set; }
        public long SaleReferenceId { get; set; }
        public string StatusPayment { get; set; }
        public bool PaymentFinished { get; set; }
        public long Amount { get; set; }
        public string BankName { get; set; }
        public int AzmoonId { get; set; }
        public int ValentearId { get; set; }
        public DateTime InsertDatetime { get; set; }
        public bool IsUses { get; set; }
      
    }
}
