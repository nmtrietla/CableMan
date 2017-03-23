using CableMan.ViewModel;
using Xamarin.Forms;

namespace CableMan.View
{
    public partial class PageDichVuKhaDung : ContentPage
    {
        public PageDichVuKhaDung()
        {
            InitializeComponent();
            BindingContext = new PageDichVuKhaDungViewModel(DichVuCungCap, Name, Phone);
        }
    }
}
