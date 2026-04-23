using WalletAnonTx.WabiSabi.Models;

namespace WalletAnonTx.WabiSabi.Client.CoinJoinProgressEvents;

public class EnteringConnectionConfirmationPhase : RoundStateChanged
{
	public EnteringConnectionConfirmationPhase(RoundState roundState, DateTimeOffset timeoutAt) : base(roundState, timeoutAt)
	{
	}
}
