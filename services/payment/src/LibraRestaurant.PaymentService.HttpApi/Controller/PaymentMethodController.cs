using LibraRestaurant.PaymentService.PaymentMethods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LibraRestaurant.PaymentService.Controller
{
    [RemoteService(Name = PaymentServiceRemoteServiceConsts.RemoteServiceName)]
    [Area("payment")]
    [Route("api/payment/methods")]
    public class PaymentMethodController : PaymentServiceController, IPaymentMethodAppService
    {
        protected IPaymentMethodAppService PaymentMethodAppService { get; }

        public PaymentMethodController(IPaymentMethodAppService paymentMethodAppService)
        {
            PaymentMethodAppService = paymentMethodAppService;
        }

        [HttpGet]
        public Task<List<PaymentMethodDto>> GetListAsync()
        {
            return PaymentMethodAppService.GetListAsync();
        }
    }
}
