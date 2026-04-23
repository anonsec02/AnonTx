using System.Collections.Generic;
using System.Threading.Tasks;
using WalletAnonTx.Wallets;

namespace WalletAnonTx.WabiSabi.Client;

public interface IWalletProvider
{
	Task<IEnumerable<IWallet>> GetWalletsAsync();
}
