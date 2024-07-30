﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentMethods
{
    public class PaymentMethodAppService : PaymentServiceAppService, IPaymentMethodAppService
    {
        private readonly IEnumerable<IPaymentMethod> _paymentMethods;

        public PaymentMethodAppService(IEnumerable<IPaymentMethod> paymentMethods)
        {
            this._paymentMethods = paymentMethods;
        }

        public Task<List<PaymentMethodDto>> GetListAsync()
        {
            return Task.FromResult(
                    _paymentMethods
                        .Select(p => new PaymentMethodDto { Name = p.Name })
                        .ToList()
                );
        }
    }
}
