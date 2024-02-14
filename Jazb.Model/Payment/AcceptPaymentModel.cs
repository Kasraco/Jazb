using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Payment
{
    public class AcceptPaymentModel
    {
        public string RefrenceNumber { get; set; }
        public string Message { get; set; }
        public string SaleReferenceId { get; set; }
        public string PaymentId { get; set; }
        public int AzmoonId { get; set; }

        public int ValentearID { get; set; }

    }

  
}
