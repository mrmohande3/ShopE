using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using ShopE.Models;
using ShopE.Views;

namespace ShopE.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public SearchPageViewModel(INavigationService navigationService) :base(navigationService)
        {
        }

        public void BackButtonClicked()
        {
            NavigationService.GoBackAsync();
        }
    }
}
