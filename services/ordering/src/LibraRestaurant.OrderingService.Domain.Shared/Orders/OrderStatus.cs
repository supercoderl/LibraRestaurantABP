﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.OrderingService.Orders
{
    public enum OrderStatus
    {
        Placed,
        Paid,
        Shipped,
        Cancelled
    }
}