using System.Threading.Tasks;
using WalletAnonTx.Fluent.Helpers;
using WalletAnonTx.Helpers;

namespace WalletAnonTx.Fluent.Models.FileSystem;

public class FileSystemModel : IFileSystem
{
	public Task OpenFileInTextEditorAsync(string filePath)
	{
		return FileHelpers.OpenFileInTextEditorAsync(filePath);
	}

	public void OpenFolderInFileExplorer(string dirPath)
	{
		IoHelpers.OpenFolderInFileExplorer(dirPath);
	}

	public Task OpenBrowserAsync(string url)
	{
		return IoHelpers.OpenBrowserAsync(url);
	}
}
