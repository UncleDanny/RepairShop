using RepairShop.Models;
using System.Collections.Generic;

namespace RepairShop.View_Models
{
    public class PartsViewModel
    {
        public List<Part> Parts { get; set; }

        public List<AvailablePart> AvailableParts { get; set; }
    }
}