using Avalonia;

namespace WalletAnonTx.Fluent.Controls.Rendering;

internal record struct DrawPayload(
	HandlerCommand HandlerCommand,
	IDrawHandler? Handler = null,
	Rect Bounds = default);
