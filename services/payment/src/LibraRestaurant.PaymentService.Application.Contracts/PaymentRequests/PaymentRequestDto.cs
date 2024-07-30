﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [Serializable]
    public class PaymentRequestDto : CreationAuditedEntityDto<Guid>
    {
        [Required]
        [MaxLength(PaymentRequestConsts.MaxCurrencyLength)]
        public string Currency { get; set; }

        public string OrderId { get; set; }

        public int OrderNo { get; set; }

        public string BuyerId { get; set; }

        public bool IsDeleted { get; set; }

        public PaymentRequestState State { get; set; }

        public List<PaymentRequestProductDto> Products { get; set; }
    }
}
