using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShopE.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentDialog : ContentView
    {
        public PaymentDialog()
        {
            InitializeComponent();
        }
        public PaymentDialog(Commodity commodity)
        {
            BindingContext = commodity;
            InitializeComponent();
        }

        private async void SubmitButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nameEntry.Text))
                    throw new Exception("لطفا نام خود را وارد کنید");
                if (string.IsNullOrEmpty(phoenEntry.Text))
                    throw new Exception("لطفا شماره تلفن خود را وارد کنید");

                RestSharp.RestRequest request = new RestRequest($"{App.BaseAddress}/api/Commodities/Payment", Method.POST);
                request.AddJsonBody(new Payment
                {
                    PaymentId = 0,
                    CommodityId = (BindingContext as Commodity).Id,
                    CustomerName = nameEntry.Text,
                    CustomerPhoneNumber = phoenEntry.Text,
                    TotalPrice = (long) (BindingContext as Commodity).PriceWOff
                });
                request.AddHeader("Accept", "application/json");
                RestClient client = new RestClient();
                var res = await client.ExecutePostAsync(request);
                if (res.IsSuccessful)
                {
                    PopupNavigation.Instance.PopAsync();
                    PopupNavigation.Instance.PushAsync(new PopupPage
                    {
                        Content = new SeccussDialogBox(),
                        CloseWhenBackgroundIsClicked = true
                    });
                }
                else
                {

                }
            }
            catch (Exception exception)
            {

            }

        }
    }
}