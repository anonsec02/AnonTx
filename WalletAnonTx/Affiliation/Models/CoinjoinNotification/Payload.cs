using Newtonsoft.Json;
using WalletAnonTx.Affiliation.Serialization;
using System.Text;

namespace WalletAnonTx.Affiliation.Models.CoinJoinNotification;

public record Payload(Header Header, Body Body)
{
	public byte[] GetCanonicalSerialization() =>
		Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this, CanonicalJsonSerializationOptions.Settings));
}
