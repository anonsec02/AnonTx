using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WalletAnonTx.Crypto;

namespace WalletAnonTx.Helpers;

public class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
{
	public bool Equals([AllowNull] byte[] x, [AllowNull] byte[] y) => ByteHelpers.CompareFastUnsafe(x, y);

	public int GetHashCode([DisallowNull] byte[] obj) => HashHelpers.ComputeHashCode(obj);
}
