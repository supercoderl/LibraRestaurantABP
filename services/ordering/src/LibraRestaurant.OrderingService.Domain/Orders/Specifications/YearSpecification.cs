using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;

namespace LibraRestaurant.OrderingService.Orders.Specifications
{
    public class YearSpecification : Specification<Order>
    {
        protected int Year { get; set; }

        public YearSpecification(int year)
        {
            Year = year;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return query => query.OrderDate.Year == Year;
        }
    }
}
