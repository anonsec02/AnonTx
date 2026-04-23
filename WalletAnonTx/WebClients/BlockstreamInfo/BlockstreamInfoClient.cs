using NBitcoin;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WalletAnonTx.Blockchain.Analysis.FeesEstimation;
using WalletAnonTx.Tor.Http;
using WalletAnonTx.Tor.Http.Extensions;
using WalletAnonTx.Tor.Socks5.Pool.Circuits;
using WalletAnonTx.WebClients.AnonTx;

namespace WalletAnonTx.WebClients.BlockstreamInfo;

public class BlockstreamInfoClient
{
	public BlockstreamInfoClient(Network network, AnonTxHttpClientFactory httpClientFactory)
	{
		string uriString;

		if (httpClientFactory.IsTorEnabled)
		{
			uriString = network == Network.TestNet
				? "http://explorerzydxu5ecjrkwceayqybizmpjjznk5izmitf2modhcusuqlid.onion/testnet"
				: "http://explorerzydxu5ecjrkwceayqybizmpjjznk5izmitf2modhcusuqlid.onion";
		}
		else
		{
			uriString = network == Network.TestNet
				? "https://blockstream.info/testnet"
				: "https://blockstream.info";
		}

		HttpClient = httpClientFactory.NewHttpClient(() => new Uri(uriString), Mode.DefaultCircuit);
	}

	private IHttpClient HttpClient { get; }

	public async Task<AllFeeEstimate> GetFeeEstimatesAsync(CancellationToken cancel)
	{
		using HttpResponseMessage response = await HttpClient.SendAsync(HttpMethod.Get, "api/fee-estimates", null, cancel).ConfigureAwait(false);

		if (!response.IsSuccessStatusCode)
		{
			await response.ThrowRequestExceptionFromContentAsync(cancel).ConfigureAwait(false);
		}

		var responseString = await response.Content.ReadAsStringAsync(cancel).ConfigureAwait(false);
		var parsed = JsonDocument.Parse(responseString).RootElement;
		var myDic = new Dictionary<int, int>();
		foreach (var elem in parsed.EnumerateObject())
		{
			myDic.Add(int.Parse(elem.Name), (int)Math.Ceiling(elem.Value.GetDouble()));
		}

		return new AllFeeEstimate(myDic);
	}
}
