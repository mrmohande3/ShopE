using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShopE.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommodityItem : ContentView
    {
        public CommodityItem()
        {
            InitializeComponent();
        }
        public CommodityItem(Commodity commodity)
        {
            InitializeComponent();
            BindingContext = commodity;
        }

        private void BuyProduct_OnTapped(object sender, EventArgs e)
        {
            var dialog = new PaymentDialog(this.BindingContext as Commodity);
            PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = dialog,
                CloseWhenBackgroundIsClicked = true
            });
        }
    }
}