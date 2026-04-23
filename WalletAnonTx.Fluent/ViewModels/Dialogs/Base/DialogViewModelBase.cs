using WalletAnonTx.Fluent.ViewModels.Navigation;

namespace WalletAnonTx.Fluent.ViewModels.Dialogs.Base;

/// <summary>
/// CommonBase class.
/// </summary>
public abstract partial class DialogViewModelBase : RoutableViewModel
{
	[AutoNotify] private bool _isDialogOpen;
}
