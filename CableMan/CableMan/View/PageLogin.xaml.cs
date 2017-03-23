using CableMan.ViewModel;
using Xamarin.Forms;

namespace CableMan.View
{
    public partial class PageLogin : ContentPage
    {
        public PageLogin()
        {
            InitializeComponent();
            //BindingContext = new PageLoginViewModel();
            BindingContext = new PageLoginViewModel(ActivityIndicatorLoading);
        }
    }
}
