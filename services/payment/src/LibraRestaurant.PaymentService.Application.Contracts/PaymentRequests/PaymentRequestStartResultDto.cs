using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [Serializable]
    public class PaymentRequestStartResultDto
    {
        public string CheckoutLink { get; set; }
    }
}
