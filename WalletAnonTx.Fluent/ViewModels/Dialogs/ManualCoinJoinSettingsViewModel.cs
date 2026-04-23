using System.Linq;
using WalletAnonTx.Fluent.ViewModels.CoinJoinProfiles;
using WalletAnonTx.Helpers;
using WalletAnonTx.Models;

namespace WalletAnonTx.Fluent.ViewModels.Dialogs;

public partial class ManualCoinJoinSettingsViewModel : ViewModelBase
{
	[AutoNotify] private bool _redCoinIsolation;
	[AutoNotify] private CoinjoinSkipFactors _skipFactors;
	[AutoNotify] private int _anonScoreTarget;
	[AutoNotify] private TimeFrameItem[] _timeFrames;
	[AutoNotify] private TimeFrameItem _selectedTimeFrame;

	public ManualCoinJoinSettingsViewModel(CoinJoinProfileViewModelBase current)
	{
		_redCoinIsolation = current.RedCoinIsolation;
		_skipFactors = current.SkipFactors;

		_anonScoreTarget = current.AnonScoreTarget;

		_timeFrames = new[]
		{
			new TimeFrameItem("Hours", TimeSpan.Zero),
			new TimeFrameItem("Days", TimeSpan.FromHours(Constants.CoinJoinFeeRateMedianTimeFrames[0])),
			new TimeFrameItem("Weeks", TimeSpan.FromHours(Constants.CoinJoinFeeRateMedianTimeFrames[1])),
			new TimeFrameItem("Months", TimeSpan.FromHours(Constants.CoinJoinFeeRateMedianTimeFrames[2]))
		};

		_selectedTimeFrame = _timeFrames.FirstOrDefault(tf => tf.TimeFrame == TimeSpan.FromHours(current.FeeRateMedianTimeFrameHours)) ?? _timeFrames.First();
	}

	public record TimeFrameItem(string Name, TimeSpan TimeFrame)
	{
		public override string ToString()
		{
			return Name;
		}
	}
}
