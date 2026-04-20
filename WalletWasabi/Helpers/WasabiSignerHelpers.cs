using NBitcoin.Crypto;
using NBitcoin;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Linq;

namespace WalletAnonTx.Helpers;

public class AnonTxSignerHelpers
{
	public static async Task SignSha256SumsFileAsync(string sha256SumsAscFilePath, Key anontxPrivateKey)
	{
		var computedHash = await GetShaComputedBytesOfFileAsync(sha256SumsAscFilePath).ConfigureAwait(false);

		ECDSASignature signature = anontxPrivateKey.Sign(new uint256(computedHash));

		string base64Signature = Convert.ToBase64String(signature.ToDER());
		var anontxSignatureFilePath = Path.ChangeExtension(sha256SumsAscFilePath, "anontxsig");

		await File.WriteAllTextAsync(anontxSignatureFilePath, base64Signature).ConfigureAwait(false);
	}

	public static async Task VerifySha256SumsFileAsync(string sha256SumsAscFilePath)
	{
		// Read the content file
		byte[] hash = await GetShaComputedBytesOfFileAsync(sha256SumsAscFilePath).ConfigureAwait(false);

		// Read the signature file
		var anontxSignatureFilePath = Path.ChangeExtension(sha256SumsAscFilePath, "anontxsig");
		string signatureText = await File.ReadAllTextAsync(anontxSignatureFilePath).ConfigureAwait(false);
		byte[] signatureBytes = Convert.FromBase64String(signatureText);

		VerifySha256Sum(hash, signatureBytes);
	}

	public static void VerifySha256Sum(byte[] sha256Hash, byte[] signatureBytes)
	{
		ECDSASignature anontxSignature = ECDSASignature.FromDER(signatureBytes);

		PubKey pubKey = new(Constants.AnonTxPubKey);

		if (!pubKey.Verify(new uint256(sha256Hash), anontxSignature))
		{
			throw new InvalidOperationException("Invalid anontx signature.");
		}
	}

	public static async Task GeneratePrivateAndPublicKeyToFileAsync(string anontxPrivateKeyFilePath, string anontxPublicKeyFilePath)
	{
		if (File.Exists(anontxPrivateKeyFilePath))
		{
			throw new ArgumentException("Private key file already exists.");
		}

		IoHelpers.EnsureContainingDirectoryExists(anontxPrivateKeyFilePath);

		using Key key = new();
		await File.WriteAllTextAsync(anontxPrivateKeyFilePath, key.ToString(Network.Main)).ConfigureAwait(false);
		await File.WriteAllTextAsync(anontxPublicKeyFilePath, key.PubKey.ToString()).ConfigureAwait(false);
	}

	public static async Task<Key> GetPrivateKeyFromFileAsync(string anontxPrivateKeyFilePath)
	{
		string keyFileContent = await File.ReadAllTextAsync(anontxPrivateKeyFilePath).ConfigureAwait(false);
		BitcoinSecret secret = new(keyFileContent, Network.Main);
		return secret.PrivateKey;
	}

	public static async Task VerifyInstallerFileHashesAsync(string[] finalFiles, string sha256SumsFilePath)
	{
		string[] lines = await File.ReadAllLinesAsync(sha256SumsFilePath).ConfigureAwait(false);
		var hashWithFileNameLines = lines.Where(line => line.Contains("AnonTx-"));

		foreach (var installerFilePath in finalFiles)
		{
			string installerName = Path.GetFileName(installerFilePath);
			string installerExpectedHash = hashWithFileNameLines.Single(line => line.Contains(installerName)).Split(" ")[0];

			var bytes = await GetShaComputedBytesOfFileAsync(installerFilePath).ConfigureAwait(false);
			string installerRealHash = Convert.ToHexString(bytes).ToLower();

			if (installerExpectedHash != installerRealHash)
			{
				throw new InvalidOperationException("Installer file's hash doesn't match expected hash.");
			}
		}
	}

	/// <summary>
	/// This function returns a SHA256 computed byte array of a file on the provided file path.
	/// </summary>
	/// <exception cref="FileNotFoundException"></exception>
	public static async Task<byte[]> GetShaComputedBytesOfFileAsync(string filePath, CancellationToken cancellationToken = default)
	{
		byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
		byte[] computedHash = SHA256.HashData(bytes);
		return computedHash;
	}
}
