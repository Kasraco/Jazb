using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AmountCard
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public string AmountSerial { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PaymentNumber { get; set; }
        public int AzmoonId { get; set; }

    }
}
