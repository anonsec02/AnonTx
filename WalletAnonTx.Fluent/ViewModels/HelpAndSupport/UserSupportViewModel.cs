using System.Windows.Input;
using ReactiveUI;

namespace WalletAnonTx.Fluent.ViewModels.HelpAndSupport;

[NavigationMetaData(
	Title = "User Support",
	Caption = "Open AnonTx's user support website",
	Order = 0,
	Category = "Help & Support",
	Keywords = new[]
	{
		"User", "Support", "Website"
	},
	IconName = "person_support_regular")]
public partial class UserSupportViewModel : TriggerCommandViewModel
{
	private UserSupportViewModel()
	{
		TargetCommand = ReactiveCommand.CreateFromTask(async () => await UiContext.FileSystem.OpenBrowserAsync(AboutViewModel.UserSupportLink));
	}

	public override ICommand TargetCommand { get; }
}
