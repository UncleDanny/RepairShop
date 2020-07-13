using RepairShop.Models;
using System.Collections.Generic;

namespace RepairShop.View_Models
{
    public class RepairOrderPartsViewModel
    {
        public RepairOrder RepairOrder { get; set; }

        public List<AvailablePart> AvailableParts { get; set; }
    }
}