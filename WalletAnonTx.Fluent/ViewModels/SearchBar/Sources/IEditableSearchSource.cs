using WalletAnonTx.Fluent.ViewModels.SearchBar.SearchItems;

namespace WalletAnonTx.Fluent.ViewModels.SearchBar.Sources;

public interface IEditableSearchSource : ISearchSource
{
	void Remove(params ISearchItem[] searchItems);
	void Add(params ISearchItem[] searchItems);
	void SetQueries(IObservable<string> queries);
}
