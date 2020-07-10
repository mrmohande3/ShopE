using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using ShopE.Models;
using ShopE.Views;

namespace ShopE.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public void SearchClicked()
        {
            NavigationService.NavigateAsync("SearchPage");
        }

    }
}
