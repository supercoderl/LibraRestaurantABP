﻿(function () {
    //set anonymous user
    let anonymousId = abp.utils.getCookieValue("anonymous_id");
    if (anonymousId != null) {
        console.info("Anonymous user set:" + anonymousId);
    }

    $(function () {
        $('.add-basket-button').click(function () {
            var $this = $(this);
            var productId = $this.attr('data-product-id');
            eShopOnAbp.basketService.basket.addProduct({
                productId: productId,
                anonymousId: anonymousId,
            }).then(function () {
                $('.abp-widget-wrapper[data-widget-name="CartWidget"]')
                    .data('abp-widget-manager')
                    .refresh();

                abp.notify.success("Added product to your basket.", "Successfully added"); //TODO: localize the msg
                var $itemAdded = $this.parent(".product-item-bottom").find(".item-added");
                $($itemAdded).css({ display: "flex" });
                $this.css({ opacity: 0.5 });
                $this.animate({ opacity: 1 }, 2000);
            }).catch(err => {
                abp.notify.error(err.message, "Failed while adding item");  //TODO: localize the msg
            });
        });
    });

    $(function () {
        $(".item-link").click(function () {
            var $this = $(this);
            var productId = $this.attr('data-product-id');
            location.href += "products/" + productId;
        });
    })
})();