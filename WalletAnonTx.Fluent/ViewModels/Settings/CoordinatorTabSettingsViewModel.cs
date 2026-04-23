using System.Globalization;
using ReactiveUI;
using WalletAnonTx.Fluent.Infrastructure;
using WalletAnonTx.Fluent.Models.UI;
using WalletAnonTx.Fluent.Validation;
using WalletAnonTx.Fluent.ViewModels.Navigation;
using WalletAnonTx.Models;

namespace WalletAnonTx.Fluent.ViewModels.Settings;

[AppLifetime]
[NavigationMetaData(
	Title = "Coordinator",
	Caption = "Manage Coordinator settings",
	Order = 2,
	Category = "Settings",
	Keywords =
	[
		"Settings", "Bitcoin", "BTC", "Coordinator", "Coordination", "Fee", "Coinjoin", "Rate", "Mining"
	],
	IconName = "settings_bitcoin_regular")]
public partial class CoordinatorTabSettingsViewModel : RoutableViewModel
{
	[AutoNotify] private string _coordinatorUri;
	[AutoNotify] private string _maxCoordinationFeeRate;
	[AutoNotify] private string _maxCoinJoinMiningFeeRate;

	public CoordinatorTabSettingsViewModel(IApplicationSettings settings)
	{
		Settings = settings;

		this.ValidateProperty(x => x.CoordinatorUri, ValidateCoordinatorUri);
		this.ValidateProperty(x => x.MaxCoordinationFeeRate, ValidateMaxCoordinationFeeRate);
		this.ValidateProperty(x => x.MaxCoinJoinMiningFeeRate, ValidateMaxCoinJoinMiningFeeRate);


		_coordinatorUri = settings.CoordinatorUri;
		_maxCoordinationFeeRate = settings.MaxCoordinationFeeRate;
		_maxCoinJoinMiningFeeRate = settings.MaxCoinJoinMiningFeeRate;

		this.WhenAnyValue(x => x.Settings.CoordinatorUri)
			.Subscribe(x => CoordinatorUri = x);
	}

	public bool IsReadOnly => Settings.IsOverridden;

	public IApplicationSettings Settings { get; }

	private void ValidateCoordinatorUri(IValidationErrors errors)
	{
		var coordinatorUri = CoordinatorUri;

		if (string.IsNullOrEmpty(coordinatorUri))
		{
			return;
		}

		if (!Uri.TryCreate(coordinatorUri, UriKind.Absolute, out _))
		{
			errors.Add(ErrorSeverity.Error, "Invalid URI.");
			return;
		}

		Settings.CoordinatorUri = coordinatorUri;
	}

	private void ValidateMaxCoordinationFeeRate(IValidationErrors errors)
	{
		var maxCoordinationFeeRate = MaxCoordinationFeeRate;

		if (string.IsNullOrEmpty(maxCoordinationFeeRate))
		{
			return;
		}

		if (!decimal.TryParse(maxCoordinationFeeRate, out var maxCoordinationFeeRateDecimal))
		{
			errors.Add(ErrorSeverity.Error, "Invalid number.");
			return;
		}

		if (maxCoordinationFeeRateDecimal < 0)
		{
			errors.Add(ErrorSeverity.Error, "Cannot be lower than 0.0%");
			return;
		}

		if (maxCoordinationFeeRateDecimal > 1)
		{
			errors.Add(ErrorSeverity.Error, "Absolute maximum coordination fee rate is 1%");
			return;
		}

		Settings.MaxCoordinationFeeRate = maxCoordinationFeeRateDecimal.ToString(CultureInfo.InvariantCulture);
	}

	private void ValidateMaxCoinJoinMiningFeeRate(IValidationErrors errors)
	{
		var maxCoinJoinMiningFeeRate = MaxCoinJoinMiningFeeRate;

		if (string.IsNullOrEmpty(maxCoinJoinMiningFeeRate))
		{
			return;
		}

		if (!decimal.TryParse(maxCoinJoinMiningFeeRate, out var maxCoinJoinMiningFeeRateDecimal))
		{
			errors.Add(ErrorSeverity.Error, "Invalid number.");
			return;
		}

		if (maxCoinJoinMiningFeeRateDecimal < 1)
		{
			errors.Add(ErrorSeverity.Error, "Mining fee rate must be at least 1 sat/vb");
			return;
		}

		Settings.MaxCoinJoinMiningFeeRate = maxCoinJoinMiningFeeRateDecimal.ToString(CultureInfo.InvariantCulture);
	}
}
