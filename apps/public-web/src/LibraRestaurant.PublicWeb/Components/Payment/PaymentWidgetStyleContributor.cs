﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace LibraRestaurant.PublicWeb.Components.Payment
{
	public class PaymentWidgetStyleContributor : BundleContributor
	{
		public override void ConfigureBundle(BundleConfigurationContext context)
		{
			context.Files.AddIfNotContains("/components/payment/payment-widget.css");
		}
	}
}
