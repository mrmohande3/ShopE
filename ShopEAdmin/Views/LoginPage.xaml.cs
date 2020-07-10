using System;
using RestSharp;
using ShopEAdmin.Models;
using ShopEAdmin.ViewModels;
using Xamarin.Forms;

namespace ShopEAdmin.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            RestRequest request  = new RestRequest($"{App.BaseAddress}/api/Store/Login", Method.POST);
            request.AddQueryParameter("username",userNameEntry.Text);
            request.AddQueryParameter("password",passwordEntry.Text);
            var res = await client.ExecutePostTaskAsync(request);
            if (res.IsSuccessful)
            {
                App.CurrentStore = Newtonsoft.Json.JsonConvert.DeserializeObject<Store>(res.Content);
                (BindingContext as LoginPageViewModel)?.LoginPage();
            }
            else
            {
                await DisplayAlert("خطا", res.Content , "بستن");
            }
        }

        private void SugnUpButton_OnClicked(object sender, EventArgs e)
        {
            (BindingContext as LoginPageViewModel)?.SignUp();
        }
    }
}
