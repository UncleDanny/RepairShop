using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
    public enum RepairOrderStatus
    {
        [Display(Name = "Awaiting Parts")]
        AwaitingParts,
        Awaiting,
        Done,
        Pending,
    }
}