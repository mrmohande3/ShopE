using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeccussDialogBox : ContentView
    {
        public SeccussDialogBox()
        {
            InitializeComponent();
            Timer timer = new Timer(3000);
            timer.Start();
            timer.Elapsed += ((sender, e) =>
            {
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    PopupNavigation.Instance.PopAllAsync();
                timer.Close();
            });
        }
    }
}