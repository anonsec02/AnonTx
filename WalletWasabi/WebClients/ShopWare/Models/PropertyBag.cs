using System.Collections.Generic;

namespace WalletAnonTx.WebClients.ShopWare.Models;

public class PropertyBag : Dictionary<string, object>
{
	public static readonly PropertyBag Empty = new();
}
