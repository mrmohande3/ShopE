using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEAdmin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopEAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentList : ContentView
    {
        public PaymentList()
        {
            InitializeComponent();
        }
        public PaymentList(List<Payment> payments)
        {
            BindingContext = payments;
            InitializeComponent();
        }
    }
}