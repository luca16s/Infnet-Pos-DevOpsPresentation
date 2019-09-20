using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        [Required] [Display(Name = "Nome:")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Required] [Display(Name = "Preco:")]
        public List<PriceViewModel> Prices { get; set; }
    }
}