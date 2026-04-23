using Newtonsoft.Json;
using System.Collections.Generic;
using WalletAnonTx.JsonConverters;
using WalletAnonTx.JsonConverters.Bitcoin;
using WalletAnonTx.JsonConverters.Collections;
using WalletAnonTx.JsonConverters.Timing;
using WalletAnonTx.WabiSabi.Crypto.Serialization;

namespace WalletAnonTx.WabiSabi.Models.Serialization;

public class JsonSerializationOptions
{
	private static readonly JsonSerializerSettings CurrentSettings = new()
	{
		Converters = new List<JsonConverter>()
			{
				new ScalarJsonConverter(),
				new GroupElementJsonConverter(),
				new OutPointJsonConverter(),
				new WitScriptJsonConverter(),
				new ScriptJsonConverter(),
				new OwnershipProofJsonConverter(),
				new NetworkJsonConverter(),
				new FeeRateJsonConverter(),
				new MoneySatoshiJsonConverter(),
				new Uint256JsonConverter(),
				new HashSetUint256JsonConverter(),
				new MultipartyTransactionStateJsonConverter(),
				new ExceptionDataJsonConverter(),
				new ExtPubKeyJsonConverter(),
				new TimeSpanJsonConverter(),
				new CoinJsonConverter(),
				new CoinJoinEventJsonConverter(),
				new GroupElementVectorJsonConverter(),
				new ScalarVectorJsonConverter(),
				new IssuanceRequestJsonConverter(),
				new CredentialPresentationJsonConverter(),
				new ProofJsonConverter(),
				new MacJsonConverter()
			}
	};

	public static readonly JsonSerializationOptions Default = new();

	private JsonSerializationOptions()
	{
	}

	public JsonSerializerSettings Settings => CurrentSettings;
}
