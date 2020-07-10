using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using RestSharp;
using ShopEAdmin.Models;
using ShopEAdmin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopEAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductDialog : ContentView
    {
        public string fileString;
        public event EventHandler<bool> Requested;
        public AddProductDialog()
        {
            InitializeComponent();
            fileString = null;
        }

        private async void AddProduct_OnTapped(object sender, EventArgs e)
        {
            Commodity commodity = new Commodity
            {
                Count = int.Parse(countEntry.Text.PersianToEnglish()),
                StoreId = App.CurrentStore.StoreId,
                Name = nameEntry.Text,
                Price = int.Parse(priceEntry.Text.PersianToEnglish()),
                Photo = fileString
            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{App.BaseAddress}/api/Commodities", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(commodity);

            var res = await client.ExecutePostTaskAsync(request);
            if (res.IsSuccessful)
            {
                Requested?.Invoke(this, true);
            }
            else
                Requested?.Invoke(this, false);
        }

        private async void AddPhotoButton_OnClicked(object sender, EventArgs e)
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();
            fileString = Convert.ToBase64String(fileData.DataArray);
            image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fileString)));
        }
    }
}