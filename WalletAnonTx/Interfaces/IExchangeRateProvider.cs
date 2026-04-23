using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WalletAnonTx.Backend.Models;

namespace WalletAnonTx.Interfaces;

public interface IExchangeRateProvider
{
	Task<IEnumerable<ExchangeRate>> GetExchangeRateAsync(CancellationToken cancellationToken);
}
