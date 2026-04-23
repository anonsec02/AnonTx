using Avalonia;
using Avalonia.Controls.Primitives;
using WalletAnonTx.Fluent.Models.Wallets;

namespace WalletAnonTx.Fluent.Controls;

public class AmountControl : TemplatedControl
{
	public static readonly StyledProperty<Amount> AmountProperty = AvaloniaProperty.Register<AmountControl, Amount>(nameof(Amount));

	public Amount Amount
	{
		get => GetValue(AmountProperty);
		set => SetValue(AmountProperty, value);
	}
}
