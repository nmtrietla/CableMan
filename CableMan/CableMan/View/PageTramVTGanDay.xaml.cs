using System.Collections.Generic;
using CableMan.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CableMan.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTramVTGanDay : ContentPage
    {
        public PageTramVTGanDay()
        {
            InitializeComponent();
            BindingContext = new PageTramVTGanDayViewModel(ListViewTramVtGanDay);
        }
    }
}
