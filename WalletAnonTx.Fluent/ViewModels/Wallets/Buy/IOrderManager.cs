using System.Threading.Tasks;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Buy;

public interface IOrderManager
{
	Task RemoveOrderAsync(int id);

	Task OnError(Exception ex);
}
