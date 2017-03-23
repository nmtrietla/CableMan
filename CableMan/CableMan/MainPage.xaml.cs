using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CableMan
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {            
            InitializeComponent();
            this.Master = new View.PageMenu();
            this.Detail = new NavigationPage(new View.PageViewMap());
            App.MasterDeltail = this;
        }
    }
}
