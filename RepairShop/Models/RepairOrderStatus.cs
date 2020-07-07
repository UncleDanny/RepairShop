using System.ComponentModel.DataAnnotations;

namespace RepairShop.Models
{
    public enum RepairOrderStatus
    {
        [Display(Name = "Awaiting")]
        Awaiting,

        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Awaiting Parts")]
        AwaitingParts,

        [Display(Name = "Done")]
        Done,
    }
}