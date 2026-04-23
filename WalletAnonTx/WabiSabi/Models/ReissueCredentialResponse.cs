using WabiSabi.CredentialRequesting;

namespace WalletAnonTx.WabiSabi.Models;

public record ReissueCredentialResponse(
	CredentialsResponse RealAmountCredentials,
	CredentialsResponse RealVsizeCredentials,
	CredentialsResponse ZeroAmountCredentials,
	CredentialsResponse ZeroVsizeCredentials
);
