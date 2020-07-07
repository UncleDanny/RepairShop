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

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayName("Start Date"), Required]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayName("End Date"), Required]
        public DateTime EndDate { get; set; }

        public RepairOrderStatus Status { get; set; }

        public List<Part> Parts { get; set; }
    }
}