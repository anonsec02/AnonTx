using NBitcoin;
using WalletAnonTx.Blockchain.Analysis.FeesEstimation;
using WalletAnonTx.Blockchain.Keys;
using WalletAnonTx.Blockchain.TransactionProcessing;
using WalletAnonTx.Models;
using WalletAnonTx.Services;
using WalletAnonTx.Stores;
using WalletAnonTx.Wallets.FilterProcessor;

namespace WalletAnonTx.Wallets;

/// <summary>
/// Class to create <see cref="Wallet"/> instances.
/// </summary>
public record WalletFactory(
	string DataDir,
	Network Network,
	BitcoinStore BitcoinStore,
	AnonTxSynchronizer AnonTxSynchronizer,
	ServiceConfiguration ServiceConfiguration,
	HybridFeeProvider FeeProvider,
	BlockDownloadService BlockDownloadService,
    UnconfirmedTransactionChainProvider UnconfirmedTransactionChainProvider)
{
	public Wallet Create(KeyManager keyManager)
	{
		TransactionProcessor transactionProcessor = new(BitcoinStore.TransactionStore, BitcoinStore.MempoolService, keyManager, ServiceConfiguration.DustThreshold);
		WalletFilterProcessor walletFilterProcessor = new(keyManager, BitcoinStore, transactionProcessor, BlockDownloadService);

		return new(DataDir, Network, keyManager, BitcoinStore, AnonTxSynchronizer, ServiceConfiguration, FeeProvider, transactionProcessor, walletFilterProcessor, UnconfirmedTransactionChainProvider);
	}

	public Wallet CreateAndInitialize(KeyManager keyManager)
	{
		Wallet wallet = Create(keyManager);
		wallet.Initialize();

		return wallet;
	}
}
