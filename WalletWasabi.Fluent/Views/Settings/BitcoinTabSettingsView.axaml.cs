using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WalletAnonTx.Fluent.Views.Settings;

public class BitcoinTabSettingsView : UserControl
{
	public BitcoinTabSettingsView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
