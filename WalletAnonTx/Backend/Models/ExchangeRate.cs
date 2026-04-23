using System.ComponentModel.DataAnnotations;

namespace WalletAnonTx.Backend.Models;

public class ExchangeRate
{
	[Required]
	public string Ticker { get; set; } = "";

	[Required]
	public decimal Rate { get; set; }
}
