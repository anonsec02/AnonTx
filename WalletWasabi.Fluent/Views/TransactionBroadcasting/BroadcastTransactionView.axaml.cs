using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WalletAnonTx.Fluent.Views.TransactionBroadcasting;

public class BroadcastTransactionView : UserControl
{
	public BroadcastTransactionView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
