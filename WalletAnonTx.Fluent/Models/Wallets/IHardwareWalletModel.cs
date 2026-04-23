using System.Threading.Tasks;

namespace WalletAnonTx.Fluent.Models.Wallets;

public interface IHardwareWalletModel : IWalletModel
{
	Task<bool> AuthorizeTransactionAsync(TransactionAuthorizationInfo transactionAuthorizationInfo);
}
