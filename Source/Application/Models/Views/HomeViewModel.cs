using System.Net;

namespace Application.Models.Views
{
	public class HomeViewModel
	{
		#region Properties

		public virtual IPAddress IpAddress { get; set; }
		public virtual string IpAddressInput { get; set; }
		public virtual IPNetwork IpNetwork { get; set; }
		public virtual string IpNetworkInput { get; set; }

		#endregion
	}
}