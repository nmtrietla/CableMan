using System.Collections.Generic;
using System.Net.Http;
using CableMan.Model;
using CableMan.View;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Linq;

namespace CableMan.ViewModel
{
    class PageLoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _maintext;


        private ActivityIndicator _activityIndicator;
        public Command LoginCommand { get; set; }

        public PageLoginViewModel(ActivityIndicator activityIndicatorLoading)
        {
            LoginCommand = new Command(LoadCommandLogin);
            _activityIndicator = activityIndicatorLoading;
            Username = "Trietnm.ctv";
            Password = "Mt060892";
        }

        public async void GetTramVtData()
        {
            try
            {
                var myClient = new HttpClient();
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("NguoiDungId", Initial.User.NguoiDungId),
                    new KeyValuePair<string, string>("Token", Initial.User.TOKEN),
                    //new KeyValuePair<string, string>("ToaDoLat", "10.539381"),
                    //new KeyValuePair<string, string>("ToaDoLong", "106.414153")
                    new KeyValuePair<string, string>("ToaDoLat", Initial.PositionNow.Latitude.ToString()),
                    new KeyValuePair<string, string>("ToaDoLong", Initial.PositionNow.Longitude.ToString())
                });
                var request = await myClient.PostAsync(Conf.urlGetTramVT, content);
                string json = await request.Content.ReadAsStringAsync();
                Initial.dataGetTramVienThong = JsonConvert.DeserializeObject<List<DataAPIGetTramVT>>(json);
                //Covert List => Observablecollection (Initial.dataGetTramVienThong)
                //foreach (var item in des)
                //    Initial.dataGetTramVienThong.Add(item);
            }
            catch
            {
                //await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }
        }
        public async void Check()
        {
            bool checkDone = await CheckTurnOnLocation();
            if (checkDone)//kiểm tra return coi có đọc được tọa độ không
                GetData();
        }
        //CheckTurnOnLocation
        public async Task<bool> CheckTurnOnLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                //Task<CrossGeolocator> position = await locator.GetPositionAsync(20000);
                var position = await locator.GetPositionAsync(10000);
                Initial.PositionNow = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);


                Geocoder _geoCoder = new Geocoder();
                var possibleAddresses = await _geoCoder.GetAddressesForPositionAsync(Initial.PositionNow);
                // foreach (var address in possibleAddresses)
                Initial.PositionName = possibleAddresses.ToList()[0].Replace('\n', ',');
                // LocationText += possibleAddresses.GetEnumerator() + "\n";

                return true;
            }
            catch
            {
                PushNotication("Bạn phải bật 'Vị Trí' để có thể tiếp tục");
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                return false;
            }
            //GetData();

        }
        public void LoadCommandLogin()
        {
            //_activityIndicator.IsRunning = true;
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Checking...");
            //CheckTurnOnLocation();
            Check();

        }
        //PushNotication
        public async void PushNotication(string me)
        {
            await Application.Current.MainPage.DisplayAlert("Thông báo", me, "Ok");
        }
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    //Thaydoi();
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MainText
        {
            get { return _maintext; }
            set
            {
                if (_maintext != value)
                {
                    _maintext = value;
                    OnPropertyChanged();
                }
            }
        }
        //Lấy dữ liệu login user về -> Initial.NguoiDungId
        public async void GetData()
        {
            try
            {
                var myClient = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("username", Username),
                        new KeyValuePair<string, string>("password", Password),
                        new KeyValuePair<string, string>("phonekey", "123456")
                });
                var request = await myClient.PostAsync(Conf.urlLogin, content);
                string json = await request.Content.ReadAsStringAsync();
                var des = JsonConvert.DeserializeObject<string>(json);
                Initial.NguoiDungId = JsonConvert.DeserializeObject<string>(json);
                if (Initial.NguoiDungId.Equals("0"))
                {
                    //_activityIndicator.IsRunning = false;
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    await Application.Current.MainPage.DisplayAlert("Thông báo", "Sai thông tin đăng nhập", "OK");
                }
                else
                {
                    Application.Current.MainPage = new PageODP();
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            }
            catch
            {
                //_activityIndicator.IsRunning = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
            }



        }
    }
}