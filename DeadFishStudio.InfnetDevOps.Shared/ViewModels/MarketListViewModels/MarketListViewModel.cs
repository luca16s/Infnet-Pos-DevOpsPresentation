using System;
using System.ComponentModel.DataAnnotations;

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
        [Editable(true)]
        public DateTime DataDeCriacao { get; set; }

        [Display(Name = "Data de Modificacao")]
        [DataType(DataType.Date)]
        [Editable(true)]
        public DateTime DataDeModificacao { get; set; }

        public ItemViewModel<MarketListProductViewModel> ItemViewModels { get; set; }
    }
}