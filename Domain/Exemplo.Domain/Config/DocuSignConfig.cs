using System;
namespace Exemplo.Domain.Config
{
	public class DocuSignConfig
	{
		public string AccountUri { get; set; }
		public string BaseUri { get; set; }
		public string AccountId { get; set; }
		public string ClientId { get; set; }
		public string SecretKey { get; set; }

		public string ResponseType { get; set; }
		public string Scope { get; set; }
		public string RedirectUri { get; set; }
	}
}



