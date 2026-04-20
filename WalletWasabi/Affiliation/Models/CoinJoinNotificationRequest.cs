using WalletAnonTx.Affiliation.Models.CoinJoinNotification;

namespace WalletAnonTx.Affiliation.Models;

public record CoinJoinNotificationRequest(Body Body, byte[] Signature);
