using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using WalletAnonTx.Fluent.Helpers;
using WalletAnonTx.Fluent.Infrastructure;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.Models.Wallets;
using WalletAnonTx.Helpers;

namespace WalletAnonTx.Fluent.ViewModels.StatusIcon;

[AppLifetime]
public partial class StatusIconViewModel : ViewModelBase
{
	[AutoNotify] private string? _versionText;

	public StatusIconViewModel(UiContext uiContext)
	{
		UiContext = uiContext;
		HealthMonitor = uiContext.HealthMonitor;

		ManualUpdateCommand = ReactiveCommand.CreateFromTask(() => UiContext.FileSystem.OpenBrowserAsync("https://anontxwallet.io/#download"));
		UpdateCommand = ReactiveCommand.Create(
			() =>
			{
				UiContext.ApplicationSettings.DoUpdateOnClose = true;
				AppLifetimeHelper.Shutdown();
			});

		AskMeLaterCommand = ReactiveCommand.Create(() => HealthMonitor.CheckForUpdates = false);

		OpenTorStatusSiteCommand = ReactiveCommand.CreateFromTask(() => UiContext.FileSystem.OpenBrowserAsync("https://status.torproject.org"));

		this.WhenAnyValue(
				x => x.HealthMonitor.UpdateAvailable,
				x => x.HealthMonitor.IsReadyToInstall,
				x => x.HealthMonitor.ClientVersion,
				(updateAvailable, isReadyToInstall, clientVersion) =>
					(updateAvailable || isReadyToInstall) && clientVersion != null)
			.Select(_ => GetVersionText())
			.BindTo(this, x => x.VersionText);
	}

	public IHealthMonitor HealthMonitor { get; }

	public ICommand OpenTorStatusSiteCommand { get; }

	public ICommand UpdateCommand { get; }

	public ICommand ManualUpdateCommand { get; }

	public ICommand AskMeLaterCommand { get; }

	public string BitcoinCoreName => Constants.BuiltinBitcoinNodeName;

	private string GetVersionText()
	{
		if (HealthMonitor.IsReadyToInstall)
		{
			return $"Version {HealthMonitor.ClientVersion} is now ready to install";
		}
		else if (HealthMonitor.UpdateAvailable)
		{
			return $"Version {HealthMonitor.ClientVersion} is now available";
		}

		return string.Empty;
	}
}
