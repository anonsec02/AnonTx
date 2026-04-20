using NBitcoin;

namespace WalletAnonTx.Affiliation;

public record AffiliateInput(OutPoint Prevout, Script ScriptPubKey, Money Amount, string AffiliationId, bool IsNoFee);
