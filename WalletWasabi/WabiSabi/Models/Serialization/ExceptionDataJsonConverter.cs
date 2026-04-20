using WalletAnonTx.WabiSabi.Backend.Models;

namespace WalletAnonTx.WabiSabi.Models.Serialization;

public class ExceptionDataJsonConverter : GenericInterfaceJsonConverter<ExceptionData>
{
	public ExceptionDataJsonConverter() : base(new[] { typeof(InputBannedExceptionData), typeof(EmptyExceptionData), typeof(WrongPhaseExceptionData) })
	{
	}
}
