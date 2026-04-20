using NBitcoin;
using System.ComponentModel;
using WalletAnonTx.WabiSabi.Models;

namespace WalletAnonTx.JsonConverters;

public class DefaultValueCoordinationFeeRateAttribute : DefaultValueAttribute
{
	public DefaultValueCoordinationFeeRateAttribute(double feeRate, double plebsDontPayThreshold)
		: base(new CoordinationFeeRate((decimal)feeRate, Money.Coins((decimal)plebsDontPayThreshold)))
	{
	}
}
