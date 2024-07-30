﻿using LibraRestaurant.PaymentService.PaymentRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.PaymentService.PaymentMethods
{
    [ExposeServices(typeof(IPaymentMethod), typeof(DemoPaymentMethod))]
    public class DemoPaymentMethod : IPaymentMethod
    {
        public string Name => PaymentMethodNames.Demo;

        public Task<PaymentRequestStartResultDto> StartAsync(PaymentRequest paymentRequest, PaymentRequestStartDto input)
        {
            if (paymentRequest.Products.Sum(x => x.TotalPrice) <= 0)
            {
                throw new ArgumentException("Price can't be zero or less.");
            }

            return Task.FromResult(new PaymentRequestStartResultDto
            {
                CheckoutLink = input.ReturnUrl + "?token=" + input.PaymentRequestId
            });
        }

        public async Task<PaymentRequest> CompleteAsync(IPaymentRequestRepository paymentRequestRepository, string token)
        {
            var paymentRequest = await paymentRequestRepository.GetAsync(Guid.Parse(token));

            paymentRequest.SetAsCompleted();

            return await paymentRequestRepository.UpdateAsync(paymentRequest);
        }

        public Task HandleWebhookAsync(string payload)
        {
            return Task.CompletedTask;
        }
    }
}
