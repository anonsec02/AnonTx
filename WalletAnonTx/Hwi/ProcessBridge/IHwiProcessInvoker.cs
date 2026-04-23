using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WalletAnonTx.Hwi.ProcessBridge;

public interface IHwiProcessInvoker
{
	Task<(string response, int exitCode)> SendCommandAsync(string arguments, bool openConsole, CancellationToken cancel, Action<StreamWriter>? standardInputWriter = null);
}
