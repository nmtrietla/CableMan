using CableMan.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CableMan.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageViewMap : ContentPage
    {
        public PageViewMap()
        {
            InitializeComponent();
            BindingContext = new PageViewMapViewModel(MyMap);
        }
        //private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        //{
        //    var zoomLevel = e.NewValue; // between 1 and 18
        //    var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
        //    MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        //}

    }
}
