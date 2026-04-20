using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Xaml.Interactions.Custom;

namespace WalletAnonTx.Fluent.Behaviors;

internal class TextBoxSelectAllTextBehavior : AttachedToVisualTreeBehavior<TextBox>
{
	protected override void OnAttachedToVisualTree(CompositeDisposable disposable)
	{
		AssociatedObject?.SelectAll();
	}
}
