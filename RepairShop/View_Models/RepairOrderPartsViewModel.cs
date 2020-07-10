using RepairShop.Models;
using System.Collections.Generic;

namespace RepairShop.View_Models
{
    public class RepairOrderPartsViewModel
    {
        public RepairOrder RepairOrder { get; set; }

        public List<Part> Parts { get; set; }

        public int[] OrderParts { get; set; }

        public int[] DbParts { get; set; }
    }
}