using System.Net;
using Application.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
	public class HomeController : Controller
	{
		#region Constructors

		public HomeController(ILoggerFactory loggerFactory)
		{
			this.Logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
		}

		#endregion

		#region Properties

		protected internal virtual ILogger Logger { get; }

		#endregion

		#region Methods

		protected internal virtual async Task<HomeViewModel> CreateModelAsync(string ipAddressInput = null, string ipNetworkInput = null)
		{
			var model = new HomeViewModel
			{
				IpAddressInput = ipAddressInput,
				IpNetworkInput = ipNetworkInput
			};

			return await Task.FromResult(model);
		}

		public virtual async Task<IActionResult> Index()
		{
			return await Task.FromResult(this.View(await this.CreateModelAsync()));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Index(string ipAddressInput, string ipNetworkInput)
		{
			var model = await this.CreateModelAsync(ipAddressInput, ipNetworkInput);

			if(!string.IsNullOrEmpty(ipAddressInput))
			{
				if(IPAddress.TryParse(ipAddressInput, out var ipAddress))
					model.IpAddress = ipAddress;
				else
					this.ModelState.AddModelError(nameof(HomeViewModel.IpAddressInput), "\"IP-address\" is invalid.");
			}

			if(!string.IsNullOrEmpty(ipNetworkInput))
			{
				if(IPNetwork.TryParse(ipNetworkInput, out var ipNetwork))
					model.IpNetwork = ipNetwork;
				else
					this.ModelState.AddModelError(nameof(HomeViewModel.IpNetworkInput), "\"IP-network\" is invalid.");
			}

			return await Task.FromResult(this.View(model));
		}

		#endregion

		//protected internal virtual bool TryParseToIpNetwork(string value, out IPNetwork ipNetwork)
		//{
		//	ipNetwork = null;

		//	if(string.IsNullOrWhiteSpace(value))
		//		return false;

		//	var parts = value.Split('/', 2);

		//	if(parts.Length != 2)
		//		return false;

		//	if(!IPAddress.TryParse(parts[0], out var prefix))
		//		return false;

		//	if(!int.TryParse(parts[1], out var prefixLength))
		//		return false;

		//	try
		//	{
		//		ipNetwork = new IPNetwork(prefix, prefixLength);
		//	}
		//	catch
		//	{
		//		ipNetwork = null;
		//		return false;
		//	}

		//	return true;
		//}
	}
}