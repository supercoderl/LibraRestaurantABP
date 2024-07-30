using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;

namespace LibraRestaurant.OrderingService.Orders.Specifications
{
    public class MonthsAgoSpecification : Specification<Order>
    {
        protected int NumberOfMonths { get; set; }

        public MonthsAgoSpecification(int months)
        {
            NumberOfMonths = months;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            var monthsAgo = DateTime.UtcNow.AddMonths(-NumberOfMonths);
            return query => query.OrderDate >= monthsAgo;
        }
    }
}
