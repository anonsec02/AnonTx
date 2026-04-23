using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using WalletAnonTx.Fluent.ViewModels.SearchBar.Patterns;
using WalletAnonTx.Fluent.ViewModels.SearchBar.SearchItems;

namespace WalletAnonTx.Fluent.ViewModels.SearchBar.Sources;

public class CompositeSearchSource : ISearchSource
{
	private readonly ISearchSource[] _sources;

	public CompositeSearchSource(params ISearchSource[] sources)
	{
		_sources = sources;
	}

	public IObservable<IChangeSet<ISearchItem, ComposedKey>> Changes => _sources.Select(r => r.Changes).Merge();
}
