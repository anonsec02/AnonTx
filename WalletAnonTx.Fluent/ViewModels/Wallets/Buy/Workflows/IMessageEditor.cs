using WalletAnonTx.BuyAnything;

namespace WalletAnonTx.Fluent.ViewModels.Wallets.Buy.Workflows;

public interface IMessageEditor
{
	bool IsEditable(ChatMessage chatMessage);

	IWorkflowStep? Get(ChatMessage chatMessage);
}
