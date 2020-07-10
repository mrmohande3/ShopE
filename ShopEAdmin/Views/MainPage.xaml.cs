using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShopEAdmin.Models;
using ShopEAdmin.ViewModels;
using Xamarin.Forms;

namespace ShopEAdmin.Views
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Commodity> commodities;
        private List<Payment> Payments;
        public MainPage()
        {
            try
            {
                InitializeComponent();
                commodities = new ObservableCollection<Commodity>();
                mainList.BindingContext = commodities;
                Init();
            }
            catch (Exception e)
            {
                DisplayAlert("خطا", e.Message, "بستن");
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        private async void Init()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{App.BaseAddress}/api/Store/{App.CurrentStore.StoreId}", Method.GET);
            var res = await client.ExecuteTaskAsync(request);
            if (res.IsSuccessful)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<MainPageModel>(res.Content);
                paymentCount.Text = model.PaymentCount.ToString();
                paymentTotal.Text = model.PaymentTotal + "T";
                Payments = model.Payments;
                model.Commodities.ForEach(x =>
                {
                    x.Photo = $"{App.BaseAddress}{x.Photo}";
                    commodities.Add(x);
                });
            }
            else
            {
                await DisplayAlert("خطا", res.Content, "بستن");
            }
        }

        private void AddProduct_OnTapped(object sender, EventArgs e)
        {
            var cp = new AddProductDialog();
            cp.Requested += ((s, ee) =>
            {
                PopupNavigation.PopAllAsync();
                Init();
            });
            PopupNavigation.PushAsync(new PopupPage
            {
                Content = cp,
                CloseWhenBackgroundIsClicked = true
            });
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = new PaymentList(Payments),
                CloseWhenBackgroundIsClicked = true
            });
        }
    }
}