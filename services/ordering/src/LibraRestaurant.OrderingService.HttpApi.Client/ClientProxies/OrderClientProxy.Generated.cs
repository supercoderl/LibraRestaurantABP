﻿using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace LibraRestaurant.OrderingService.ClientProxies
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IOrderAppService), typeof(OrderClientProxy))]
    public partial class OrderClientProxy : ClientProxyBase<IOrderAppService>, IOrderAppService
    {
        public virtual async Task<OrderDto> GetAsync(Guid id)
        {
            return await RequestAsync<OrderDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
        }

        public virtual async Task<List<OrderDto>> GetMyOrdersAsync(GetMyOrdersInput input)
        {
            return await RequestAsync<List<OrderDto>>(nameof(GetMyOrdersAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetMyOrdersInput), input }
        });
        }

        public virtual async Task<List<OrderDto>> GetOrdersAsync(GetOrdersInput input)
        {
            return await RequestAsync<List<OrderDto>>(nameof(GetOrdersAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetOrdersInput), input }
        });
        }

        public virtual async Task<PagedResultDto<OrderDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return await RequestAsync<PagedResultDto<OrderDto>>(nameof(GetListPagedAsync), new ClientProxyRequestTypeValue
        {
            { typeof(PagedAndSortedResultRequestDto), input }
        });
        }

        public virtual async Task<DashboardDto> GetDashboardAsync(DashboardInput input)
        {
            return await RequestAsync<DashboardDto>(nameof(GetDashboardAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DashboardInput), input }
        });
        }

        public virtual async Task<OrderDto> GetByOrderNoAsync(int orderNo)
        {
            return await RequestAsync<OrderDto>(nameof(GetByOrderNoAsync), new ClientProxyRequestTypeValue
        {
            { typeof(int), orderNo }
        });
        }

        public virtual async Task SetAsCancelledAsync(Guid id)
        {
            await RequestAsync(nameof(SetAsCancelledAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
        }

        public virtual async Task SetAsShippedAsync(Guid id)
        {
            await RequestAsync(nameof(SetAsShippedAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
        }

        public virtual async Task<OrderDto> CreateAsync(OrderCreateDto input)
        {
            return await RequestAsync<OrderDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(OrderCreateDto), input }
        });
        }
    }
}
