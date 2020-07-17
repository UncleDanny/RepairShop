using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace RepairShop.Models
{
    public class AvailablePart
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

        public bool isActive { get; set; } = true;

        [ScriptIgnore]
        public RepairOrder RepairOrder { get; set; }
    }
}