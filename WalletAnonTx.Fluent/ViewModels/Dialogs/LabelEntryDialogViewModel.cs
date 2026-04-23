using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using WalletAnonTx.Blockchain.Analysis.Clustering;
using WalletAnonTx.Fluent.Extensions;
using WalletAnonTx.Fluent.Models.Wallets;
using WalletAnonTx.Fluent.ViewModels.Dialogs.Base;
using WalletAnonTx.Fluent.ViewModels.Wallets.Labels;

namespace WalletAnonTx.Fluent.ViewModels.Dialogs;

[NavigationMetaData(Title = "Recipient", NavigationTarget = NavigationTarget.CompactDialogScreen)]
public partial class LabelEntryDialogViewModel : DialogViewModelBase<LabelsArray?>
{
	private readonly IWalletModel _wallet;

	public LabelEntryDialogViewModel(IWalletModel wallet, LabelsArray labels)
	{
		_wallet = wallet;

		SuggestionLabels = new SuggestionLabelsViewModel(wallet, Intent.Send, 3)
		{
			Labels = { labels.AsEnumerable() }
		};

		SetupCancel(enableCancel: true, enableCancelOnEscape: true, enableCancelOnPressed: true);

		var nextCommandCanExecute =
			Observable
				.Merge(SuggestionLabels.WhenAnyValue(x => x.Labels.Count).ToSignal())
				.Merge(SuggestionLabels.WhenAnyValue(x => x.IsCurrentTextValid).ToSignal())
				.Select(_ => SuggestionLabels.Labels.Any() || SuggestionLabels.IsCurrentTextValid);

		NextCommand = ReactiveCommand.Create(OnNext, nextCommandCanExecute);
	}

	public SuggestionLabelsViewModel SuggestionLabels { get; }

	private void OnNext()
	{
		SuggestionLabels.ForceAdd = true;
		Close(DialogResultKind.Normal, new LabelsArray(SuggestionLabels.Labels.ToArray()));
	}

	protected override void OnNavigatedTo(bool isInHistory, CompositeDisposable disposables)
	{
		base.OnNavigatedTo(isInHistory, disposables);

		// TODO: why are we using this here and turning it into a Signal, instead of an event like _wallet.TransactionProcessed?
		_wallet.Coins.List
			.Connect()
			.ToSignal()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Subscribe(_ => SuggestionLabels.UpdateLabels())
			.DisposeWith(disposables);

		SuggestionLabels.Activate(disposables);
	}
}
