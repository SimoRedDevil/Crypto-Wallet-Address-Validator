using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Crypto_Wallet_Address_Validator
{
    class RapidAPI
    {
        public static string ValidateAddress(string address, string coin)
        {
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://crypto-wallet-address-validator.p.rapidapi.com/validate/{address}?currency={coin}&network=prod"),
				Headers =
	{
		{ "X-RapidAPI-Key", "c111b8ee68msh27105aa2fe988ebp1f7bd1jsn0a971cf084d2" },
		{ "X-RapidAPI-Host", "crypto-wallet-address-validator.p.rapidapi.com" },
	},
			};
			using (var response = client.SendAsync(request).Result)
			{
				response.EnsureSuccessStatusCode();
				var body = response.Content.ReadAsStringAsync().Result;
				return body;
			}
		}
    }
}
