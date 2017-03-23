using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using CableMan.Model;
using CableMan.View;
//using Java.Lang;
using Plugin.Messaging;

namespace CableMan.ViewModel
{
    class PageMenuViewModel : BaseViewModel
    {
        private ListView _listMenu;
        private string _name;
        private string _user; 
        private string _phone; 
        public PageMenuViewModel(ListView listMenu)
        {            
            Name  = Initial.User.FULLNAME;
            User  = Initial.User.USERNAME;
            Phone = Initial.User.PHONE;

            ObservableCollection <DataMenu> dataAPIMenu = new ObservableCollection<DataMenu>();
            _listMenu = listMenu;
            _listMenu.ItemsSource = dataAPIMenu;
            _listMenu.SeparatorVisibility = SeparatorVisibility.None;
            dataAPIMenu.Add(new DataMenu{id = 0, imgLink = "TramVTGanDay.png",  headerMenu= "        Trạm VT gần đây" });
            dataAPIMenu.Add(new DataMenu{id = 1, imgLink = "KetCuoiGanDay.png", headerMenu= "        Kết cuối gần đây" });
            dataAPIMenu.Add(new DataMenu{id = 2, imgLink = "World.png",         headerMenu= "        Dịch vụ khả dụng" });
            dataAPIMenu.Add(new DataMenu{id = 3, imgLink = "Finder.png",        headerMenu= "        Tra cứu thuê bao" });            
            dataAPIMenu.Add(new DataMenu { id = 4, imgLink = "SMS.png",         headerMenu= "        Gửi SMS tọa độ" });
            dataAPIMenu.Add(new DataMenu { id = 5, imgLink = "Back_arrow.png", headerMenu = "        Đăng xuất" });
            dataAPIMenu.Add(new DataMenu { id = 6, imgLink = "App_Menu.png",    headerMenu= "        Test menu" });
            _listMenu.ItemTapped += ListMenuOnItemTapped;

            
        }

        private async void ListMenuOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            DataMenu data = (DataMenu)itemTappedEventArgs.Item;            


            switch (data.id)
            {
                case 0:
                    await App.NavigateMasterDetail(new PageTramVTGanDay());                 
                    break;
                case 1:
                    await App.NavigateMasterDetail(new PageKetCuoiGanDay());
                    break;
                case 2:
                    await App.NavigateMasterDetail(new PageDichVuKhaDung());
                    break;
                case 3:
                    await App.NavigateMasterDetail(new PageTraCuuThueBao());
                    break;
                case 4:
                    var smsMessenger = MessagingPlugin.SmsMessenger;
                    if (smsMessenger.CanSendSms)
                        smsMessenger.SendSms("+84", "Bạn đang ở địa chỉ: "+Initial.PositionName+". Có tọa độ (Lat,Long)=("
                            + Initial.PositionNow.Latitude+","+Initial.PositionNow.Longitude+")");
                    else
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Thiết bị của bạn không hỗ trợ thực hiện gửi SMS", "Ok");

                    break;
                case 5:
                    var answer = await Application.Current.MainPage.DisplayAlert("Thông báo", "Bạn có chắc đăng xuất không?", "Yes", "No");
                    if (answer)
                    {
                        App.Current.MainPage = new PageLogin();
                    }
                    break;
                    
                case 6:
                    //var PhoneCallTask = MessagingPlugin.PhoneDialer;
                    //if (PhoneCallTask.CanMakePhoneCall)
                    //    PhoneCallTask.MakePhoneCall("01234445079");
                    //else
                    //    await Application.Current.MainPage.DisplayAlert("Thông báo", "Thiết bị của bạn không hỗ trợ thực hiện cuộc gọi", "Ok");
                    await App.NavigateMasterDetail(new Pagetest());
                    break;
                default:
                    
                    break;
            }
        }
        public string User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    //Thaydoi();
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    //Thaydoi();
                    OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    //Thaydoi();
                    OnPropertyChanged();
                }
            }
        }
    }
}
