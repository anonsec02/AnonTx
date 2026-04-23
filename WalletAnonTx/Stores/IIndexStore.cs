using System.Threading;
using System.Threading.Tasks;
using WalletAnonTx.Backend.Models;

namespace WalletAnonTx.Stores;

public interface IIndexStore
{
	Task<FilterModel[]> FetchBatchAsync(uint fromHeight, int batchSize, CancellationToken cancellationToken);
}
