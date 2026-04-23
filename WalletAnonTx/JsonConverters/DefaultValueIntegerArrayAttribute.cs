using System.ComponentModel;

namespace WalletAnonTx.JsonConverters;

public class DefaultValueIntegerArrayAttribute : DefaultValueAttribute
{
	public DefaultValueIntegerArrayAttribute(string json) : base(IntegerArrayJsonConverter.Parse(json))
	{
	}
}
