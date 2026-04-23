using WalletAnonTx.Blockchain.TransactionBuilding;

namespace WalletAnonTx.Fluent.Models.Wallets;

public record CancellingTransaction(
	TransactionModel TargetTransaction,
	BuildTransactionResult CancelTransaction,
	Amount Fee);
