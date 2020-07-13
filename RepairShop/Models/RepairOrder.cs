using RepairShop.View_Models;
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

        public Repairman Repairman { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public RepairOrderStatus Status { get; set; }

        [DisplayName("Repair Description")]
        public string RepairDescription { get; set; }

        public List<AvailablePart> Parts { get; set; }
    }
}