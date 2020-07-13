using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
    public class Part
    {
        public int ID { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
    }
}