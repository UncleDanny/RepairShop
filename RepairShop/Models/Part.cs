using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
    public class Part
    {
        public int ID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}