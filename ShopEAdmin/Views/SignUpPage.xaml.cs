using System;
using RestSharp;
using ShopEAdmin.Models;
using ShopEAdmin.ViewModels;
using Xamarin.Forms;

namespace ShopEAdmin.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            (BindingContext as SignUpPageViewModel)?.LoginPage();
        }

        private async void SugnUpButton_OnClicked(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{App.BaseAddress}/api/Store", Method.POST);
            var store = new Store
            {
                Name = nameEntry.Text,
                Password = passwordEntry.Text,
                PhoneNumber = phoneEntry.Text,
                UserName = userNameEntry.Text
            };
            request.AddJsonBody(store);
            var res = await client.ExecutePostAsync(request);
            if (res.IsSuccessful)
            {
                await DisplayAlert("موفقیت امیز :)", "ثبت نام شما موفقیت امیز بود می توانید وارد شوید", "بستن");
                (BindingContext as SignUpPageViewModel)?.LoginPage();
            }
            else
            {
                await DisplayAlert("خطا", res.Content, "بستن");
            }
        }
    }
}
