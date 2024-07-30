﻿/* This file is automatically generated by ABP framework to use MVC Controllers from javascript. */


// module ordering

(function () {

    // controller eShopOnAbp.orderingService.orders.order

    (function () {

        abp.utils.createNamespace(window, 'eShopOnAbp.orderingService.orders.order');

        eShopOnAbp.orderingService.orders.order.get = function (id, ajaxParams) {
            return abp.ajax($.extend(true, {
                url: abp.appPath + 'api/ordering/order/' + id + '',
                type: 'GET'
            }, ajaxParams));
        };

        eShopOnAbp.orderingService.orders.order.getMyOrders = function (input, ajaxParams) {
            return abp.ajax($.extend(true, {
                url: abp.appPath + 'api/ordering/order/my-orders' + abp.utils.buildQueryString([{ name: 'filter', value: input.filter }]) + '',
                type: 'GET'
            }, ajaxParams));
        };

        eShopOnAbp.orderingService.orders.order.getByOrderNo = function (orderNo, ajaxParams) {
            return abp.ajax($.extend(true, {
                url: abp.appPath + 'api/ordering/order/by-order-no' + abp.utils.buildQueryString([{ name: 'orderNo', value: orderNo }]) + '',
                type: 'GET'
            }, ajaxParams));
        };

        eShopOnAbp.orderingService.orders.order.create = function (input, ajaxParams) {
            return abp.ajax($.extend(true, {
                url: abp.appPath + 'api/ordering/order',
                type: 'POST',
                data: JSON.stringify(input)
            }, ajaxParams));
        };

    })();

})();