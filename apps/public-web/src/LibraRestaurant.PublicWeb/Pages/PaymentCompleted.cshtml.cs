using LibraRestaurant.PaymentService.PaymentRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LibraRestaurant.PublicWeb.Pages
{
    [Authorize]
    public class PaymentCompletedModel : AbpPageModel
    {
        private readonly IPaymentRequestAppService _paymentRequestAppService;

        public PaymentCompletedModel(IPaymentRequestAppService paymentRequestAppService)
        {
            _paymentRequestAppService = paymentRequestAppService;
        }

        [BindProperty(SupportsGet = true)] public string Token { get; set; }

        public PaymentRequestDto PaymentRequest { get; private set; }

        public bool IsSuccessful { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!HttpContext.Request.Cookies.TryGetValue(LibraRestaurantPaymentConsts.PaymentMethodCookie,
                    out var selectedPaymentMethod))
            {
                throw new InvalidOperationException("A payment type must be selected!");
            }

            PaymentRequest = await _paymentRequestAppService.CompleteAsync(
                // TODO: Use string name
                selectedPaymentMethod,
                new PaymentRequestCompleteInputDto() { Token = Token });

            IsSuccessful = PaymentRequest.State == PaymentRequestState.Completed;

            if (IsSuccessful)
            {
                // Remove cookie so that can be set again when default payment type is set
                HttpContext.Response.Cookies.Delete(LibraRestaurantPaymentConsts.PaymentMethodCookie);
                return RedirectToPage("OrderReceived", new { orderNo = PaymentRequest.OrderNo });
            }

            return Page();
        }
    }
}
