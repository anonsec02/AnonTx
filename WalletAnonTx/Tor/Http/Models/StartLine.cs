using System.Linq;
using WalletAnonTx.Helpers;
using static WalletAnonTx.Tor.Http.Constants;

namespace WalletAnonTx.Tor.Http.Models;

public abstract class StartLine
{
	protected StartLine(HttpProtocol protocol)
	{
		Protocol = protocol;
	}

	public HttpProtocol Protocol { get; }

	public static string[] GetParts(string startLineString)
	{
		var trimmed = Guard.NotNullOrEmptyOrWhitespace(nameof(startLineString), startLineString, trim: true);
		return trimmed.Split(SP, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
	}
}
