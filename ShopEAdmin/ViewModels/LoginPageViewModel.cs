using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace ShopEAdmin.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService):base(navigationService)
        {

        }

        public void LoginPage()
        {
            NavigationService.NavigateAsync("MainPage");
        }

        public void SignUp()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }
    }
}
