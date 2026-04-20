using System.Threading.Tasks;

namespace WalletAnonTx.Fluent.ViewModels.SearchBar.SearchItems;

public interface IActionableItem : ISearchItem
{
	Func<Task> Activate { get; }
}
