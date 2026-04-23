using Newtonsoft.Json;

namespace WalletAnonTx.WebClients.CoinGecko;

public class CoinGeckoExchangeRate
{
	[JsonProperty(PropertyName = "current_price")]
	public decimal Rate { get; set; }
}
