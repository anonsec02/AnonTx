using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WalletAnonTx.Fluent.Views.Dialogs;
public partial class AdvancedCoinjoinOptionsView : UserControl
{
	public AdvancedCoinjoinOptionsView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}
