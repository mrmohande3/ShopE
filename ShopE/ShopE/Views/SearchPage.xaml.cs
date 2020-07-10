using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RestSharp;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShopE.Models;
using ShopE.ViewModels;
using Xamarin.Forms;

namespace ShopE.Views
{
    public partial class SearchPage : ContentPage
    {
        private Button pastSelected;
        private ObservableCollection<Commodity> Commodities;
        private ObservableCollection<Store> Stores;
        public SearchPage()
        {
            InitializeComponent();
            init();
            pastSelected = null;
            Commodities = new ObservableCollection<Commodity>();
            Stores=new ObservableCollection<Store>();
            mainList.BindingContext = Commodities;
            storesList.ItemsSource = Stores;
        }

        private async void init()
        {
            RestSharp.RestRequest request = new RestRequest($"{App.BaseAddress}/api/Store", Method.GET);
            RestClient client = new RestClient();
            var res = await client.ExecuteGetTaskAsync(request);
            if (res.IsSuccessful)
            {
                var stores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Store>>(res.Content);
                stores.ForEach(s=>{Stores.Add(s);});
            }
            else
            {
                await DisplayActionSheet("خطا", res.Content, "بستن");
            }
        }

        private void BackButton_OnClicked(object sender, EventArgs e)
        {
            (BindingContext as SearchPageViewModel).BackButtonClicked();
        }

        private async void SearcheButton_OnClicked(object sender, EventArgs e)
        {
            RestSharp.RestRequest request = new RestRequest($"{App.BaseAddress}/api/Commodities/Search/{searchEntry.Text}", Method.GET);
            RestClient client = new RestClient();
            var res = await client.ExecuteGetTaskAsync(request);
            Commodities.Clear();
            if (res.IsSuccessful)
            {
                var commiditys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Commodity>>(res.Content);
                commiditys.ForEach(x =>
                {
                    x.Photo = "http://" + x.Photo;
                    Commodities.Add(x);
                });
            }
            else
            {
                await DisplayActionSheet("خطا", res.Content, "بستن");
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (pastSelected != null)
            {
                pastSelected.BackgroundColor = Color.White;
                pastSelected.TextColor = (Color) Application.Current.Resources["Primary"];
            }
            var store = ((Button) sender).BindingContext as Store;
            ((Button)sender).BackgroundColor = (Color)Application.Current.Resources["Primary"];
            ((Button)sender).TextColor = Color.White;
            pastSelected = (Button) sender;
            RestSharp.RestRequest request = new RestRequest($"{App.BaseAddress}/api/Store/Commodities/{store.StoreId}", Method.GET);
            RestClient client = new RestClient();
            var res = await client.ExecuteGetTaskAsync(request);
            Commodities.Clear();
            if (res.IsSuccessful)
            {
                var commiditys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Commodity>>(res.Content);
                commiditys.ForEach(x =>
                {
                    x.Photo = "http://" + x.Photo;
                    Commodities.Add(x);
                });
            }
            else
            {
                await DisplayActionSheet("خطا", res.Content, "بستن");
            }
        }
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopupPage
            {
                Content = new CommodityItem((sender as Grid).BindingContext as Commodity),
                CloseWhenBackgroundIsClicked = true
            });
        }
    }
}
