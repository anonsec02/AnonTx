using System.Threading;
using System.Threading.Tasks;
using NBitcoin;
using WalletAnonTx.BitcoinCore.Rpc;
using WalletAnonTx.Logging;

namespace WalletAnonTx.Wallets;

public class RpcBlockProvider : IBlockProvider
{
	public RpcBlockProvider(IRPCClient rpcClient)
	{
		RpcClient = rpcClient;
	}

	private IRPCClient RpcClient { get; }

	public async Task<Block?> TryGetBlockAsync(uint256 hash, CancellationToken cancellationToken)
	{
		try
		{
			return await RpcClient.GetBlockAsync(hash, cancellationToken).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Logger.LogDebug(ex);
			return null;
		}
	}
}
