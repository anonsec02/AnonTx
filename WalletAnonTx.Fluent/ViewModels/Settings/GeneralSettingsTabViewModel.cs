using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using WalletAnonTx.Fluent.Models;
using WalletAnonTx.Logging;
using System.Windows.Input;
using WalletAnonTx.Fluent.Infrastructure;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.ViewModels.Navigation;
using WalletAnonTx.Models;

namespace WalletAnonTx.Fluent.ViewModels.Settings;

[AppLifetime]
[NavigationMetaData(
	Title = "General",
	Caption = "Manage general settings",
	Order = 0,
	Category = "Settings",
	Keywords = new[]
	{
			"Settings", "General", "Bitcoin", "Dark", "Mode", "Run", "AnonTx", "Computer", "System", "Start", "Background", "Close",
			"Auto", "Copy", "Paste", "Addresses", "Custom", "Change", "Address", "Fee", "Display", "Format", "BTC", "sats"
	},
	IconName = "settings_general_regular")]
public partial class GeneralSettingsTabViewModel : RoutableViewModel
{
	[AutoNotify] private bool _runOnSystemStartup;

	public GeneralSettingsTabViewModel(IApplicationSettings settings)
	{
		Settings = settings;
		_runOnSystemStartup = settings.RunOnSystemStartup;

		StartupCommand = ReactiveCommand.Create(async () =>
		{
			try
			{
				settings.RunOnSystemStartup = RunOnSystemStartup;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex);
				RunOnSystemStartup = !RunOnSystemStartup;
				await ShowErrorAsync(Title, "Couldn't save your change, please see the logs for further information.", "Error occurred.");
			}
		});
	}

	public bool IsReadOnly => Settings.IsOverridden;

	public IApplicationSettings Settings { get; }

	public ICommand StartupCommand { get; }

	public IEnumerable<FeeDisplayUnit> FeeDisplayUnits =>
		Enum.GetValues(typeof(FeeDisplayUnit)).Cast<FeeDisplayUnit>();

	public IEnumerable<TorMode> TorModes =>
		Enum.GetValues(typeof(TorMode)).Cast<TorMode>();
}
