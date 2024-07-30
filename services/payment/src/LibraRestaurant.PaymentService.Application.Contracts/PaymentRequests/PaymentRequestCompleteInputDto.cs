using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [Serializable]
    public class PaymentRequestCompleteInputDto
    {
        public string Token { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
