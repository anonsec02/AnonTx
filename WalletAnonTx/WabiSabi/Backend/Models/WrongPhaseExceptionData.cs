using WalletAnonTx.WabiSabi.Backend.Rounds;

namespace WalletAnonTx.WabiSabi.Backend.Models;

public record WrongPhaseExceptionData(Phase CurrentPhase) : ExceptionData;
