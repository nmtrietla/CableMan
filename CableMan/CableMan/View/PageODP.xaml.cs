using CableMan.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CableMan.View
{
    public partial class PageODP : ContentPage
    {
        public PageODP()
        {
            InitializeComponent();
            BindingContext = new PageODPViewModel(ButtonODP, EntryODP);
        }
    }
}
