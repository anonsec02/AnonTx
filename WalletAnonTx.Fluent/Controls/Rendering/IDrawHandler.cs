using Avalonia;
using Avalonia.Skia;

namespace WalletAnonTx.Fluent.Controls.Rendering;

internal interface IDrawHandler : IDisposable
{
	void Draw(ISkiaSharpApiLease skia, Rect bounds);
}
