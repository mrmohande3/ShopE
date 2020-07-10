using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShopE.Models;
using ShopE.ViewModels;
using Xamarin.Forms;

namespace ShopE.Views
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Commodity> Commodities;
        public MainPage()
        {
            InitializeComponent();
            Commodities=new ObservableCollection<Commodity>();
            SynceInfos();
            mainList.BindingContext = Commodities;
        }

        private async void  SynceInfos()
        {
            RestSharp.RestRequest request = new RestRequest($"{App.BaseAddress}/api/Commodities",Method.GET);
            RestClient client = new RestClient();
            var res = await client.ExecuteGetTaskAsync(request);
            if (res.IsSuccessful)
            {
                var commiditys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Commodity>>(res.Content);
                commiditys.ForEach(x =>
                {
                    x.Photo = $"{App.BaseAddress}{x.Photo}";
                    Commodities.Add(x);
                });
            }
            else
            {
                await DisplayActionSheet("خطا", res.Content, "بستن");
            }
        }

        private void SearchButton_OnClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.SearchClicked();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = new CommodityItem((sender as Grid).BindingContext as Commodity),
                CloseWhenBackgroundIsClicked = true
            });
        }
    }
}