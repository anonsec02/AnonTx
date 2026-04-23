using System.Collections.Generic;
using System.Threading.Tasks;
using WalletAnonTx.Backend.Models;
using WalletAnonTx.Interfaces;
using WalletAnonTx.Logging;
using WalletAnonTx.WebClients.BlockchainInfo;
using WalletAnonTx.WebClients.Coinbase;
using WalletAnonTx.WebClients.Bitstamp;
using WalletAnonTx.WebClients.CoinGecko;
using WalletAnonTx.WebClients.Gemini;
using System.Linq;
using System.Threading;
using WalletAnonTx.WebClients.Coingate;

namespace WalletAnonTx.WebClients;

public class ExchangeRateProvider : IExchangeRateProvider
{
	private readonly IExchangeRateProvider[] _exchangeRateProviders =
	{
		new BlockchainInfoExchangeRateProvider(),
		new BitstampExchangeRateProvider(),
		new CoinGeckoExchangeRateProvider(),
		new CoinbaseExchangeRateProvider(),
		new GeminiExchangeRateProvider(),
		new CoingateExchangeRateProvider()
	};

	public async Task<IEnumerable<ExchangeRate>> GetExchangeRateAsync(CancellationToken cancellationToken)
	{
		foreach (var provider in _exchangeRateProviders)
		{
			try
			{
				return await provider.GetExchangeRateAsync(cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				// Ignore it and try with the next one
				Logger.LogTrace(ex);
			}
		}
		return Enumerable.Empty<ExchangeRate>();
	}
}
