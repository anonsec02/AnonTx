using NBitcoin;
using WabiSabi.CredentialRequesting;
using WalletAnonTx.Crypto;

namespace WalletAnonTx.WabiSabi.Models;

public record InputRegistrationRequest(
	uint256 RoundId,
	OutPoint Input,
	OwnershipProof OwnershipProof,
	ZeroCredentialsRequest ZeroAmountCredentialRequests,
	ZeroCredentialsRequest ZeroVsizeCredentialRequests
);
