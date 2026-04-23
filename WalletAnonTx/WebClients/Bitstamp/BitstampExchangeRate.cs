using Newtonsoft.Json;

namespace WalletAnonTx.WebClients.Bitstamp;

public class BitstampExchangeRate
{
	[JsonProperty(PropertyName = "bid")]
	public decimal Rate { get; set; }
}
