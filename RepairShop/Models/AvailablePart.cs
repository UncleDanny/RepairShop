using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace RepairShop.Models
{
    public class AvailablePart
    {
        public int ID { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [ScriptIgnore]
        public RepairOrder RepairOrder { get; set; }
    }
}