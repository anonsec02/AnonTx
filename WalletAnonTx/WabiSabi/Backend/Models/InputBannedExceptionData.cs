namespace WalletAnonTx.WabiSabi.Backend.Models;

public record InputBannedExceptionData(DateTimeOffset BannedUntil) : ExceptionData;
