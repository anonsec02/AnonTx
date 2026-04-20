using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WalletAnonTx.Fluent.Views.Dialogs;

public class NewWalletAdvancedOptionsDialogView : UserControl
{
	public NewWalletAdvancedOptionsDialogView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
