using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CableMan.Model;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace CableMan.ViewModel
{
    class PageDichVuKhaDungViewModel : BaseViewModel
    {
        private ListView _dichVuCungCap;
        public PageDichVuKhaDungViewModel(ListView dichVuCungCap, Label name, Label phone)
        {
            //GetDataDichVuKhaDung();
            //_navigation = navigation;

            name.Text = Initial.dataGetTramVienThong[0].DanhSachKetCuoi[0].HoTen;
            phone.Text = Initial.dataGetTramVienThong[0].DanhSachKetCuoi[0].SoDienThoai;
            _dichVuCungCap = dichVuCungCap;
            _dichVuCungCap.ItemsSource = Initial.dataGetDichVuKhaDung;
        }
        public async void GetDataDichVuKhaDung()
        {
            try
            {
                var myClient = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("NguoiDungId", Initial.User.NguoiDungId),
                    new KeyValuePair<string, string>("Token", Initial.User.TOKEN),
                    new KeyValuePair<string, string>("PhoneKey", "123456")
                });
                var request = await myClient.PostAsync(Conf.urlGetDichVuKhaDung, content);
                string json = await request.Content.ReadAsStringAsync();
                Initial.dataGetDichVuKhaDung = JsonConvert.DeserializeObject<ObservableCollection<DataAPIDichVuKhaDung>>(json);
                Initial.checkDone = true;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }
        }
    }
}
