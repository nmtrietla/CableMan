using CableMan.ViewModel;
using Xamarin.Forms;

namespace CableMan.View
{
    public partial class PageKetCuoiGanDay : TabbedPage
    {
        public PageKetCuoiGanDay()
        {
            InitializeComponent();
            BindingContext = new PageKetCuoiGanDayViewModel(ListViewKetCuoiKhaDungCapDong, ListViewKetCuoiKhaDungCapQuang);
            //BindingContext = new PageKetCuoiGanDayViewModel(Navigation);ListViewKetCuoiKhaDungFiber
        }
    }
}
