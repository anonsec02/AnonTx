using System.Net.Http.Headers;

namespace WalletAnonTx.Tor.Http.Models;

public record HttpRequestContentHeaders(
	HttpRequestHeaders RequestHeaders,
	HttpContentHeaders ContentHeaders);
