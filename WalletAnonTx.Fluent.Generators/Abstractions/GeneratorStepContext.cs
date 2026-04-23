using Microsoft.CodeAnalysis;

namespace WalletAnonTx.Fluent.Generators.Abstractions;

internal record GeneratorStepContext(GeneratorExecutionContext Context, Compilation Compilation);
