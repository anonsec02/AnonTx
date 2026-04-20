using System.Net.Http.Headers;

namespace WalletAnonTx.Tor.Http.Models;

public record HttpResponseContentHeaders(
	HttpResponseHeaders ResponseHeaders,
	HttpContentHeaders ContentHeaders);
