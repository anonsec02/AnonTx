#pragma warning disable IDE0130 // Namespace does not match folder structure (see https://github.com/zkSNACKs/WalletAnonTx/pull/10576#issuecomment-1552750543)

using System.Threading.Tasks;

namespace WalletAnonTx.Fluent;

public interface INavBarButton : INavBarItem
{
	Task Activate();
}
