using System.Collections.Generic;
using NBitcoin;
using WalletAnonTx.Blockchain.Keys;
using WalletAnonTx.Crypto;

namespace WalletAnonTx.WabiSabi.Client;

public interface IKeyChain
{
	OwnershipProof GetOwnershipProof(IDestination destination, CoinJoinInputCommitmentData committedData);

	Transaction Sign(Transaction transaction, Coin coin, PrecomputedTransactionData precomputeTransactionData);
}
