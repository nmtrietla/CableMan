using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CableMan
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDeltail { get; set; }

        public async static Task NavigateMasterDetail(Page page)
        {
            App.MasterDeltail.IsPresented = false;
            await App.MasterDeltail.Detail.Navigation.PushAsync(page);
        }
        public App()
        {
            InitializeComponent();

            //MainPage = new CableMan.MainPage();
            MainPage = new CableMan.View.PageLogin();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
