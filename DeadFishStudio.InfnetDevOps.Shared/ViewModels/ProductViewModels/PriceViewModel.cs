using System.ComponentModel.DataAnnotations;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels
{
    public class PriceViewModel
    {
        [Required] [Display(Name = "Moeda")] public string Currency { get; set; }

        [Required] [Display(Name = "Valor")] public decimal Amount { get; set; }
    }
}