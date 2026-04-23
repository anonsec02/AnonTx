using NBitcoin;
using WabiSabi.CredentialRequesting;

namespace WalletAnonTx.WabiSabi.Models;

public record OutputRegistrationRequest(
	uint256 RoundId,
	Script Script,
	RealCredentialsRequest AmountCredentialRequests,
	RealCredentialsRequest VsizeCredentialRequests
);
