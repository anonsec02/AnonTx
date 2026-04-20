using WalletAnonTx.Backend.Models.Responses;

namespace WalletAnonTx.WabiSabi.Client;

public interface IAnonTxBackendStatusProvider
{
	SynchronizeResponse? LastResponse { get; }
}
