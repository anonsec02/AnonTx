using NBitcoin.RPC;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WalletAnonTx.Backend.Models;
using WalletAnonTx.Backend.Models.Responses;
using WalletAnonTx.Bases;
using WalletAnonTx.Blockchain.Analysis.FeesEstimation;
using WalletAnonTx.Blockchain.BlockFilters;
using WalletAnonTx.Blockchain.Blocks;
using WalletAnonTx.Models;
using WalletAnonTx.Stores;
using WalletAnonTx.Tor.Socks5.Exceptions;
using WalletAnonTx.WabiSabi.Client;
using WalletAnonTx.WebClients.AnonTx;

namespace WalletAnonTx.Services;

public class AnonTxSynchronizer : PeriodicRunner, INotifyPropertyChanged, IThirdPartyFeeProvider, IAnonTxBackendStatusProvider
{
	private decimal _usdExchangeRate;

	private TorStatus _torStatus;

	private BackendStatus _backendStatus;
	private bool _backendNotCompatible;

	public AnonTxSynchronizer(TimeSpan period, int maxFiltersToSync, BitcoinStore bitcoinStore, AnonTxHttpClientFactory httpClientFactory) : base(period)
	{
		MaxFiltersToSync = maxFiltersToSync;

		LastResponse = null;
		SmartHeaderChain = bitcoinStore.SmartHeaderChain;
		FilterProcessor = new FilterProcessor(bitcoinStore);
		HttpClientFactory = httpClientFactory;
		AnonTxClient = httpClientFactory.SharedAnonTxClient;
	}

	#region EventsPropertiesMembers

	public event EventHandler<bool>? SynchronizeRequestFinished;

	public event EventHandler<SynchronizeResponse>? ResponseArrived;

	public event EventHandler<AllFeeEstimate>? AllFeeEstimateArrived;

	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>Task completion source that is completed once a first synchronization request succeeds or fails.</summary>
	public TaskCompletionSource<bool> InitialRequestTcs { get; } = new();

	public SynchronizeResponse? LastResponse { get; private set; }
	public AnonTxHttpClientFactory HttpClientFactory { get; }
	private AnonTxClient AnonTxClient { get; }

	/// <summary>Gets the Bitcoin price in USD.</summary>
	public decimal UsdExchangeRate
	{
		get => _usdExchangeRate;
		private set => RaiseAndSetIfChanged(ref _usdExchangeRate, value);
	}

	public TorStatus TorStatus
	{
		get => _torStatus;
		private set => RaiseAndSetIfChanged(ref _torStatus, value);
	}

	public BackendStatus BackendStatus
	{
		get => _backendStatus;
		private set
		{
			if (RaiseAndSetIfChanged(ref _backendStatus, value))
			{
				BackendStatusChangedAt = DateTimeOffset.UtcNow;
			}
		}
	}

	public bool BackendNotCompatible
	{
		get => _backendNotCompatible;
		private set => RaiseAndSetIfChanged(ref _backendNotCompatible, value);
	}

	private DateTimeOffset BackendStatusChangedAt { get; set; } = DateTimeOffset.UtcNow;
	public TimeSpan BackendStatusChangedSince => DateTimeOffset.UtcNow - BackendStatusChangedAt;
	private int MaxFiltersToSync { get; }
	private SmartHeaderChain SmartHeaderChain { get; }
	private FilterProcessor FilterProcessor { get; }

	public AllFeeEstimate? LastAllFeeEstimate => LastResponse?.AllFeeEstimate;

	public bool InError => BackendStatus != BackendStatus.Connected;

	#endregion EventsPropertiesMembers

	protected override async Task ActionAsync(CancellationToken cancel)
	{
		try
		{
			SynchronizeResponse response;

			ushort lastUsedApiVersion = AnonTxClient.ApiVersion;
			try
			{
				if (SmartHeaderChain.TipHash is null)
				{
					return;
				}

				response = await AnonTxClient
					.GetSynchronizeAsync(SmartHeaderChain.TipHash, MaxFiltersToSync, EstimateSmartFeeMode.Conservative, cancel)
					.ConfigureAwait(false);

				// NOT GenSocksServErr
				BackendStatus = BackendStatus.Connected;
				BackendNotCompatible = false;
				TorStatus = TorStatus.Running;
				OnSynchronizeRequestFinished();
			}
			catch (HttpRequestException ex) when (ex.InnerException is TorException innerEx)
			{
				TorStatus = innerEx is TorConnectionException ? TorStatus.NotRunning : TorStatus.Running;
				BackendStatus = BackendStatus.NotConnected;
				OnSynchronizeRequestFinished();
				throw;
			}
			catch (HttpRequestException ex) when (ex.Message.Contains("Not Found"))
			{
				TorStatus = TorStatus.Running;
				BackendStatus = BackendStatus.NotConnected;

				// Backend API version might be updated meanwhile. Trying to update the versions.
				bool backendCompatible;
				try
				{
					backendCompatible = await AnonTxClient.CheckUpdatesAsync(cancel).ConfigureAwait(false);
				}
				catch (HttpRequestException) when (ex.Message.Contains("Not Found"))
				{
					// Backend is online but the endpoint for versions doesn't exist -> backend is not compatible.
					BackendNotCompatible = true;
					return;
				}

				// If the backend is compatible and the Api version updated then we just used the wrong API.
				if (backendCompatible && lastUsedApiVersion != AnonTxClient.ApiVersion)
				{
					// Next request will be fine, do not throw exception.
					TriggerRound();
					return;
				}

				BackendNotCompatible = !backendCompatible;
				throw;
			}
			catch (Exception)
			{
				TorStatus = TorStatus.Running;
				BackendStatus = BackendStatus.NotConnected;
				OnSynchronizeRequestFinished();
				throw;
			}

			// If it's not fully synced or reorg happened.
			if (response.Filters.Count() == MaxFiltersToSync || response.FiltersResponseState == FiltersResponseState.BestKnownHashNotFound)
			{
				TriggerRound();
			}

			ExchangeRate? exchangeRate = response.ExchangeRates.FirstOrDefault();
			if (exchangeRate is { Rate: > 0 })
			{
				UsdExchangeRate = exchangeRate.Rate;
			}

			await FilterProcessor.ProcessAsync((uint)response.BestHeight, response.FiltersResponseState, response.Filters).ConfigureAwait(false);

			LastResponse = response;
			ResponseArrived?.Invoke(this, response);
			if (response.AllFeeEstimate is { } allFeeEstimate)
			{
				AllFeeEstimateArrived?.Invoke(this, allFeeEstimate);
			}
		}
		catch (HttpRequestException)
		{
			await Task.Delay(3000, cancel).ConfigureAwait(false); // Retry sooner in case of connection error.
			TriggerRound();
			throw;
		}
	}

	private void OnSynchronizeRequestFinished()
	{
		var isBackendConnected = BackendStatus is BackendStatus.Connected;

		// One time trigger for the UI about the first request.
		InitialRequestTcs.TrySetResult(isBackendConnected);

		SynchronizeRequestFinished?.Invoke(this, isBackendConnected);
	}

	protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value))
		{
			return false;
		}

		field = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}
}
