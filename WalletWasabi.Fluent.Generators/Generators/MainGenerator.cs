using Microsoft.CodeAnalysis;
using WalletAnonTx.Fluent.Generators.Abstractions;

namespace WalletAnonTx.Fluent.Generators.Generators;

[Generator]
internal class MainGenerator : CombinedGenerator
{
	public MainGenerator()
	{
		AddStaticFileGenerator<AutoInterfaceAttributeGenerator>();
		AddStaticFileGenerator<AutoNotifyAttributeGenerator>();
		Add<AutoNotifyGenerator>();
		Add<AutoInterfaceGenerator>();
		Add<UiContextConstructorGenerator>();
		Add<FluentNavigationGenerator>();
	}
}
