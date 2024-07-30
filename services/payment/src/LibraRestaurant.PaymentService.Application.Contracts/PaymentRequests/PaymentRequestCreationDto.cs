using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [Serializable]
    public class PaymentRequestCreationDto
    {
        [Required]
        [MaxLength(PaymentRequestConsts.MaxCurrencyLength)]
        public string Currency { get; set; }

        [Required]
        [MinLength(PaymentRequestConsts.MinOrderIdLength)]
        [MaxLength(PaymentRequestConsts.MaxOrderIdLength)]
        public string OrderId { get; set; }

        [Required]
        public int OrderNo { get; set; }

        public string BuyerId { get; set; }
        public List<PaymentRequestProductCreationDto> Products { get; set; }
    }
}
