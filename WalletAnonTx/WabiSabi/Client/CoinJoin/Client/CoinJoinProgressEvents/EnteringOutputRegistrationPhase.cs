using WalletAnonTx.WabiSabi.Models;

namespace WalletAnonTx.WabiSabi.Client.CoinJoinProgressEvents;

public class EnteringOutputRegistrationPhase : RoundStateChanged
{
	public EnteringOutputRegistrationPhase(RoundState roundState, DateTimeOffset timeoutAt) : base(roundState, timeoutAt)
	{
	}
}
