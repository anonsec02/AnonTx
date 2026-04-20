using DynamicData;
using WalletAnonTx.Fluent.ViewModels.SearchBar.Patterns;
using WalletAnonTx.Fluent.ViewModels.SearchBar.SearchItems;

namespace WalletAnonTx.Fluent.ViewModels.SearchBar.Sources;

public interface ISearchSource
{
	IObservable<IChangeSet<ISearchItem, ComposedKey>> Changes { get; }
}
