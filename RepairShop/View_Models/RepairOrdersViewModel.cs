using RepairShop.Models;
using System.Collections.Generic;

namespace RepairShop.View_Models
{
    public class RepairOrdersViewModel
    {
        public RepairOrder RepairOrder { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Repairman> Repairmen { get; set; }
    }
}