using System.Windows.Input;
using WalletAnonTx.Fluent.ViewModels.Navigation;

namespace WalletAnonTx.Fluent.ViewModels;

public abstract class TriggerCommandViewModel : RoutableViewModel
{
	public abstract ICommand TargetCommand { get; }
}
