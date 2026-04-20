using System;
using System.Linq;
using System.Threading.Tasks;
using WalletAnonTx.Logging;

namespace WalletAnonTx.Daemon;

public enum ExitCode
{
	Ok,
	FailedAlreadyRunningSignaled,
	FailedAlreadyRunningError,
}

public record AnonTxAppBuilder(string AppName, string[] Arguments)
{
	internal bool MustCheckSingleInstance { get; init; }
	internal EventHandler<Exception>? UnhandledExceptionEventHandler { get; init; }
	internal EventHandler<AggregateException>? UnobservedTaskExceptionsEventHandler { get; init; }
	internal Action Terminate { get; init; } = () => { };

	public AnonTxAppBuilder EnsureSingleInstance(bool ensure = true) =>
		this with { MustCheckSingleInstance = ensure };

	public AnonTxAppBuilder OnUnhandledExceptions(EventHandler<Exception> handler) =>
		this with { UnhandledExceptionEventHandler = handler };

	public AnonTxAppBuilder OnUnobservedTaskExceptions(EventHandler<AggregateException> handler) =>
		this with { UnobservedTaskExceptionsEventHandler = handler };

	public AnonTxAppBuilder OnTermination(Action action) =>
		this with { Terminate = action };
	public AnonTxApplication Build() =>
		new(this);

	public static AnonTxAppBuilder Create(string appName, string[] args) =>
		new(appName, args);
}

public static class AnonTxAppExtensions
{
	public static async Task<ExitCode> RunAsConsoleAsync(this AnonTxApplication app)
	{
		void ProcessCommands()
		{
			var arguments = app.AppConfig.Arguments;
			var walletNames = ArgumentHelpers
				.GetValues("wallet", arguments)
				.Distinct();

			foreach (var walletName in walletNames)
			{
				try
				{
					var wallet = app.Global!.WalletManager.GetWalletByName(walletName);
					app.Global!.WalletManager.StartWalletAsync(wallet).ConfigureAwait(false);
				}
				catch (InvalidOperationException)
				{
					Logger.LogWarning($"Wallet '{walletName}' was not found. Ignoring...");
				}
			}
		}

		return await app.RunAsync(
			async () =>
			{
				try
				{
					await app.Global!.InitializeNoWalletAsync(initializeSleepInhibitor: false, app.TerminateService, app.TerminateService.CancellationToken).ConfigureAwait(false);
				}
				catch (OperationCanceledException) when (app.TerminateService.CancellationToken.IsCancellationRequested)
				{
					Logger.LogInfo("User requested the application to stop. Stopping.");
				}

				if (!app.TerminateService.CancellationToken.IsCancellationRequested)
				{
					ProcessCommands();
					await app.TerminateService.ForcefulTerminationRequestedTask.ConfigureAwait(false);
				}
			}).ConfigureAwait(false);
	}
}
