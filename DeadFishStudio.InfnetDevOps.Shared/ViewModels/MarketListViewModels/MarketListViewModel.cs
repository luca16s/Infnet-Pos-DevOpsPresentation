using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels
{
    public class MarketListViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Necessario informar nome da lista.")]
        [Display(Name = "Nome")]
        [MaxLength(250, ErrorMessage = "Nome não pode ser maior que 250 caracteres")]
        [Editable(true)]
        public string Name { get; set; }

        [Display(Name = "Data de Criacao")]
        [DataType(DataType.Date)]
        [Editable(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeCriacao { get; set; }

        [Display(Name = "Data de Modificacao")]
        [DataType(DataType.Date)]
        [Editable(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeModificacao { get; set; }

        public List<MarketListProductViewModel> ItemViewModels { get; set; }
    }
}