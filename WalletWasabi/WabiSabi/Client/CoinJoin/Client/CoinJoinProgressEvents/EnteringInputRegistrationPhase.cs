using WalletAnonTx.WabiSabi.Models;

namespace WalletAnonTx.WabiSabi.Client.CoinJoinProgressEvents;

public class EnteringInputRegistrationPhase : RoundStateChanged
{
	public EnteringInputRegistrationPhase(RoundState roundState, DateTimeOffset timeoutAt) : base(roundState, timeoutAt)
	{
	}
}
