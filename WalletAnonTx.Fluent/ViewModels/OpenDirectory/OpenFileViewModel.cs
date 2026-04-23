using System.Windows.Input;
using ReactiveUI;
using WalletAnonTx.Fluent.Extensions;
using WalletAnonTx.Fluent.Models.UI;

namespace WalletAnonTx.Fluent.ViewModels.OpenDirectory;

public abstract class OpenFileViewModel : TriggerCommandViewModel
{
	public OpenFileViewModel(UiContext uiContext)
	{
		UiContext = uiContext;
	}

	public abstract string FilePath { get; }

	public override ICommand TargetCommand =>
		ReactiveCommand.CreateFromTask(async () =>
		{
			try
			{
				await UiContext.FileSystem.OpenFileInTextEditorAsync(FilePath);
			}
			catch (Exception ex)
			{
				await ShowErrorAsync("Open", ex.ToUserFriendlyString(), "AnonTx was unable to open the file");
			}
		});
}
