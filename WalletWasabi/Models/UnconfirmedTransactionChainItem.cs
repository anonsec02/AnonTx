using System.Collections.Generic;
using NBitcoin;
using Newtonsoft.Json;
using WalletAnonTx.JsonConverters;
using WalletAnonTx.JsonConverters.Bitcoin;
using WalletAnonTx.JsonConverters.Collections;

namespace WalletAnonTx.Models;
public record UnconfirmedTransactionChainItem(
	[JsonProperty]
	[JsonConverter(typeof(Uint256JsonConverter))]
	uint256 TxId,
	[JsonProperty]
	int Size,
	[JsonProperty]
	[JsonConverter(typeof(MoneySatoshiJsonConverter))]
	Money Fee,
	[JsonProperty]
	[JsonConverter(typeof(HashSetUint256JsonConverter))]
	HashSet<uint256> Parents,
	[JsonProperty]
	[JsonConverter(typeof(HashSetUint256JsonConverter))]
	HashSet<uint256> Children);
