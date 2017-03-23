using System;
using System.Collections.Generic;
using System.Net.Http;
using CableMan.Model;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Globalization;
using System.Threading.Tasks;

namespace CableMan.ViewModel
{
    class PageODPViewModel : BaseViewModel
    {
        private Button _buttonODP;
        private Entry _entryODP;
        public PageODPViewModel(Button ButtonODP, Entry EntryODP)
        {
            //_navigation = navigation;
            _buttonODP = ButtonODP;
            _buttonODP.Clicked += OnButtonODPClicked;
            _entryODP = EntryODP;
        }

        private void OnButtonODPClicked(object sender, EventArgs e)
        {

            GetDataODP();

        }
        public async void GetDataODP()
        {
            try
            {
                //_activityIndicatorLoading.IsRunning = true;
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Checking...");

                var myClient = new HttpClient();
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("NguoiDungId", Initial.NguoiDungId),
                        new KeyValuePair<string, string>("ODPCode", _entryODP.Text),
                        new KeyValuePair<string, string>("phoneKey", "123456")
                });
                var request = await myClient.PostAsync(Conf.urlODP, content);
                string json = await request.Content.ReadAsStringAsync();
                //var des = JsonConvert.DeserializeObject<string>(json);
                DataAPILogin getUser = JsonConvert.DeserializeObject<DataAPILogin>(json);
                Initial.User = getUser;
                Initial.User.NguoiDungId = Initial.NguoiDungId;


                if (!getUser.SUCCESS.Equals("1"))
                {
                    //_activityIndicatorLoading.IsRunning = false;
                    

                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    await Application.Current.MainPage.DisplayAlert("Thông báo", "Nhập ODP chưa chính xác!", "OK");
                }
                else
                {
                    // Initial.User.TOKEN = _entryODP.Text;
                    //Application.Current.MainPage = new NavigationPage(new PageMain());
                    Acr.UserDialogs.UserDialogs.Instance.Loading("Loading...");
                    bool checkDone = await GetTramVtData();
                    Application.Current.MainPage = new MainPage();// thêm vào để tạo hiệu ứng
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                //_activityIndicatorLoading.IsRunning = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }
        }
        public async Task<bool> GetTramVtData()
        {
            try
            {
                var myClient = new HttpClient();
                var point = new Position(Convert.ToDouble(Initial.PositionNow.Latitude, CultureInfo.InvariantCulture), Convert.ToDouble(Initial.PositionNow.Longitude, CultureInfo.InvariantCulture));

                var content = new FormUrlEncodedContent(new[]
                {
                    //new KeyValuePair<string, string>("NguoiDungId", Initial.User.NguoiDungId),
                    //new KeyValuePair<string, string>("Token", Initial.User.TOKEN),
                    //new KeyValuePair<string, string>("ToaDoLat", Initial.PositionNow.Latitude.ToString().Replace(",",".")),
                    //new KeyValuePair<string, string>("ToaDoLong", Initial.PositionNow.Longitude.ToString().Replace(",","."))  
                    new KeyValuePair<string, string>("NguoiDungId", Initial.User.NguoiDungId),
                    new KeyValuePair<string, string>("Token", Initial.User.TOKEN),
                    new KeyValuePair<string, string>("ToaDoLat", Initial.PositionNow.Latitude.ToString().Replace(",",".")),
                    new KeyValuePair<string, string>("ToaDoLong", Initial.PositionNow.Longitude.ToString().Replace(",","."))     

                   // 10.539381,
                   // 106.414153
                });
                var request = await myClient.PostAsync(Conf.urlGetTramVT, content);
                string json = await request.Content.ReadAsStringAsync();
                Initial.dataGetTramVienThong = JsonConvert.DeserializeObject<List<DataAPIGetTramVT>>(json);
                return true;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                return false;
                throw;
            }
        }

    }
}
