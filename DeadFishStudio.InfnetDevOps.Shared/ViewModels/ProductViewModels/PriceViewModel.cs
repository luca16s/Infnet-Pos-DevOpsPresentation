using System;
using System.ComponentModel.DataAnnotations;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels
{
    public class PriceViewModel
    {
        [Required] [Display(Name = "Moeda")] public string Currency { get; set; }

        [Required] [Display(Name = "Valor")] public decimal Amount { get; set; }

        [Required] [Display(Name = "Data de Criação")] [DataType(DataType.Date)] [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] public DateTime CreateDate { get; set; }

        [Required] [Display(Name = "Ativo")] public bool IsActive { get; set; }
    }
}