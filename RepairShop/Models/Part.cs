using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairShop.Models
{
    public class Part
    {
        public int ID { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }
    }
}