﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.SignalR;
using Volo.Abp.Modularity;

namespace LibraRestaurant.PublicWeb.Components.Payment
{
	[DependsOn(typeof(SignalRBrowserScriptContributor))]
	public class PaymentWidgetScriptContributor : BundleContributor
	{
		public override void ConfigureBundle(BundleConfigurationContext context)
		{
			context.Files.AddIfNotContains("/components/payment/payment-widget.js");
		}
	}
}
