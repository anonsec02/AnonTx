using WalletAnonTx.Tor.Socks5.Models.Bases;
using WalletAnonTx.Tor.Socks5.Models.Fields.ByteArrayFields;

namespace WalletAnonTx.Tor.Socks5.Models.Fields.OctetFields;

public class NMethodsField : OctetSerializableBase
{
	public NMethodsField(byte value)
	{
		ByteValue = value;
	}

	public NMethodsField(MethodsField methods)
	{
		ByteValue = (byte)methods.ToBytes().Length;
	}

	public int Value => ByteValue;
}
