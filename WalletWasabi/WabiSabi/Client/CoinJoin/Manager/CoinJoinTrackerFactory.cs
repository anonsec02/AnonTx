using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NBitcoin;
using WalletAnonTx.Blockchain.TransactionOutputs;
using WalletAnonTx.WabiSabi.Client.CoinJoin.Client;
using WalletAnonTx.WabiSabi.Client.RoundStateAwaiters;
using WalletAnonTx.Wallets;
using WalletAnonTx.WebClients.AnonTx;

namespace WalletAnonTx.WabiSabi.Client;

public class CoinJoinTrackerFactory
{
	public CoinJoinTrackerFactory(
		IAnonTxHttpClientFactory httpClientFactory,
		RoundStateUpdater roundStatusUpdater,
		CoinJoinConfiguration coinJoinConfiguration,
		CancellationToken cancellationToken)
	{
		HttpClientFactory = httpClientFactory;
		RoundStatusUpdater = roundStatusUpdater;
		CoinJoinConfiguration = coinJoinConfiguration;
		CancellationToken = cancellationToken;
		LiquidityClueProvider = new LiquidityClueProvider();
	}

	private IAnonTxHttpClientFactory HttpClientFactory { get; }
	private RoundStateUpdater RoundStatusUpdater { get; }
	private CoinJoinConfiguration CoinJoinConfiguration { get; }
	private CancellationToken CancellationToken { get; }
	private LiquidityClueProvider LiquidityClueProvider { get; }

	public async Task<CoinJoinTracker> CreateAndStartAsync(IWallet wallet, IWallet? outputWallet, Func<Task<IEnumerable<SmartCoin>>> coinCandidatesFunc, bool stopWhenAllMixed, bool overridePlebStop)
	{
		await LiquidityClueProvider.InitLiquidityClueAsync(wallet).ConfigureAwait(false);

		if (wallet.KeyChain is null)
		{
			throw new NotSupportedException("Wallet has no key chain.");
		}

		// The only use-case when we set consolidation mode to true, when we are mixing to another wallet.
		wallet.ConsolidationMode = outputWallet is not null && outputWallet.WalletId != wallet.WalletId;

		var coinSelector = CoinJoinCoinSelector.FromWallet(wallet);
		var coinJoinClient = new CoinJoinClient(
			HttpClientFactory,
			wallet.KeyChain,
			outputWallet != null ? outputWallet.OutputProvider : wallet.OutputProvider,
			RoundStatusUpdater,
			coinSelector,
			CoinJoinConfiguration,
			LiquidityClueProvider,
			feeRateMedianTimeFrame: wallet.FeeRateMedianTimeFrame,
			skipFactors: wallet.CoinjoinSkipFactors,
			doNotRegisterInLastMinuteTimeLimit: TimeSpan.FromMinutes(1));

		return new CoinJoinTracker(wallet, coinJoinClient, coinCandidatesFunc, stopWhenAllMixed, overridePlebStop, outputWallet, CancellationToken);
	}
}
