using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
    public class RepairOrder
    {
        public int ID { get; set; }

        public Customer Customer { get; set; }

        [DataType(DataType.Date), DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date), DisplayName("End Date")]
        public DateTime EndDate { get; set; }
       
        public RepairOrderStatus Status { get; set; }

        public List<AvailablePart> AvailableParts { get; set; }
    }
}