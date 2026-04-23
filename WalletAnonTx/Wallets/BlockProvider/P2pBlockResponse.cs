using NBitcoin;

namespace WalletAnonTx.Wallets.BlockProvider;

public record P2pBlockResponse(Block? Block, ISourceData SourceData);
