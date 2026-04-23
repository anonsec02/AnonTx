using WalletAnonTx.WabiSabi.Backend.Models;

namespace WalletAnonTx.WabiSabi.Models;

public record Error(
	string Type,
	string ErrorCode,
	string Description,
	ExceptionData ExceptionData
);
