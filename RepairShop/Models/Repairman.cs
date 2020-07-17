using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
	public class Repairman : Person
	{
		[Required]
		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Wage { get; set; }
	}
}