using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using WalletAnonTx.Fluent.CrashReport.ViewModels;
using WalletAnonTx.Models;
using WalletAnonTx.Fluent.CrashReport.Views;

namespace WalletAnonTx.Fluent.CrashReport;

public class CrashReportApp : Application
{
	private readonly SerializableException? _serializableException;

	public CrashReportApp()
	{
		Name = "AnonTx Wallet Crash Report";
	}

	public CrashReportApp(SerializableException exception) : this()
	{
		_serializableException = exception;
	}

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _serializableException is { })
		{
			desktop.MainWindow = new CrashReportWindow
			{
				DataContext = new CrashReportWindowViewModel(_serializableException)
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
