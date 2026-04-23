#pragma warning disable IDE0130 // Namespace does not match folder structure (see https://github.com/zkSNACKs/WalletAnonTx/pull/10576#issuecomment-1552750543)

namespace WalletAnonTx.Fluent;

public interface INavBarToggle : INavBarItem
{
	void Toggle();
}
