using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    public enum PaymentRequestState
    {
        Waiting = 0,
        Completed,
        Failed
    }
}
