using System;
using Microsoft.AspNetCore.Mvc;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels
{
    public class BaseViewModel
    {
        [HiddenInput(DisplayValue = false)] public Guid Id { get; set; }
    }
}