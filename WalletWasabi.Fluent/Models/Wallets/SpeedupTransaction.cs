using WalletAnonTx.Blockchain.TransactionBuilding;
using WalletAnonTx.Blockchain.Transactions;

namespace WalletAnonTx.Fluent.Models.Wallets;

public record SpeedupTransaction(
	SmartTransaction TargetTransaction,
	BuildTransactionResult BoostingTransaction,
	bool AreWePayingTheFee,
	Amount Fee
	);
