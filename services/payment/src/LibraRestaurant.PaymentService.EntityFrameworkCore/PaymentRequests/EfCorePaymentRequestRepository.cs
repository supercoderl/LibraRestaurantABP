using LibraRestaurant.PaymentService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    public class EfCorePaymentRequestRepository :
            EfCoreRepository<
                IPaymentServiceDbContext,
                PaymentRequest,
                Guid>,
            IPaymentRequestRepository
    {
        public EfCorePaymentRequestRepository(IDbContextProvider<IPaymentServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override async Task<IQueryable<PaymentRequest>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync())
                .Include(p => p.Products);
        }
    }
}
