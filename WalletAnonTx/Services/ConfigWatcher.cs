using System.Threading;
using System.Threading.Tasks;
using WalletAnonTx.Bases;
using WalletAnonTx.WabiSabi.Backend;

namespace WalletAnonTx.Services;

public class ConfigWatcher : PeriodicRunner
{
	public ConfigWatcher(TimeSpan period, WabiSabiConfig config, Action executeWhenChanged) : base(period)
	{
		Config = config;
		ExecuteWhenChanged = executeWhenChanged;
		config.AssertFilePathSet();
	}

	private WabiSabiConfig Config { get; }
	private Action ExecuteWhenChanged { get; }

	protected override Task ActionAsync(CancellationToken cancel)
	{
		if (ConfigManager.CheckFileChange(Config.FilePath, Config))
		{
			cancel.ThrowIfCancellationRequested();
			Config.LoadFile(createIfMissing: true);

			ExecuteWhenChanged();
		}

		return Task.CompletedTask;
	}
}
