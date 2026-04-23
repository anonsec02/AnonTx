using System.ComponentModel;

namespace WalletAnonTx.JsonConverters.Timing;

public class DefaultValueTimeSpanAttribute : DefaultValueAttribute
{
	public DefaultValueTimeSpanAttribute(string json) : base(TimeSpanJsonConverter.Parse(json))
	{
	}
}
