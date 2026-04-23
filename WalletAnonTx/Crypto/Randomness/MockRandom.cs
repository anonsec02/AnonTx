using System.Collections.Generic;
using System.Linq;
using WabiSabi.Crypto.Randomness;
using WalletAnonTx.Extensions;

namespace WalletAnonTx.Crypto.Randomness;

public class MockRandom : AnonTxRandom
{
	public List<byte[]> GetBytesResults { get; } = new List<byte[]>();

	public override void GetBytes(Span<byte> output)
	{
		var first = GetBytesResults.First();
		GetBytesResults.RemoveFirst();
		first.AsSpan().CopyTo(output);
	}

	public override void GetBytes(byte[] output)
	{
		throw new NotImplementedException();
	}

	public override int GetInt(int fromInclusive, int toExclusive)
	{
		throw new NotImplementedException();
	}
}
