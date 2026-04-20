using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WalletAnonTx.Fluent.Views.CoinJoinProfiles;

public class CoinJoinProfileControl : UserControl
{
	public CoinJoinProfileControl()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
