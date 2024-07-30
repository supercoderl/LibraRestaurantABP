using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [Serializable]
    public class PaymentRequestStartDto
    {
        public int PaymentTypeId { get; set; }
        public Guid PaymentRequestId { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }
    }
}
