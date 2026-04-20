using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Xaml.Interactions.Custom;
using WalletAnonTx.Fluent.Helpers;

namespace WalletAnonTx.Fluent.Behaviors;

public class RegisterNotificationHostBehavior : AttachedToVisualTreeBehavior<Visual>
{
	protected override void OnAttachedToVisualTree(CompositeDisposable disposable)
	{
		if (AssociatedObject is null)
		{
			return;
		}

		NotificationHelpers.SetNotificationManager(AssociatedObject);
	}
}
