using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;

namespace LibraRestaurant.OrderingService.Orders.Specifications
{
    public class Last30DaysSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression()
        {
            var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
            return query => query.OrderDate >= daysAgo30
                ;
            // && query.OrderDate <= DateTime.UtcNow;
        }
    }
}
