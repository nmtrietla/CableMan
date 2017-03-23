//////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2017 VNPT Long An                                                  //
// All rights reserved.                                                             //
// Create Date :24-02-2017                                                          //
// Modify Date :                                                                    //
// Create By Nguyễn Minh Triết.                                                     //
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using CableMan.Model;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Plugin.Messaging;

namespace CableMan.ViewModel
{

    class PageViewMapViewModel : BaseViewModel
    {
        private Map _myMap;
        private List<Pin> pins = new List<Pin>();
        private int indexTramSelect;
        private int indexSelect;
        private string _locationText;

        public PageViewMapViewModel(Map myMap)
        {
            _myMap = myMap;
            LocationText = "Đc: " + Initial.PositionName;
            var pin = new Pin()
            {
                Position = Initial.PositionNow,
                Label = "Bạn đang ở đây!"
            };
            _myMap.MoveToRegion(MapSpan.FromCenterAndRadius(Initial.PositionNow, Distance.FromMiles(1)));
            _myMap.Pins.Add(pin);
            //GetNameLocation(Initial.PositionNow);
            //LoadLocation();
            //_myMap.MoveToRegion(MapSpan.FromCenterAndRadius(Initial.PositionNow, Distance.FromKilometers(1)));            

            LoadCheckLocationCommand();
            GetDataDichVuKhaDung();
            //GetTramVtData();
        }        
        public void LoadCheckLocationCommand()
        {
            foreach (var intemofTramVienThong in Initial.dataGetTramVienThong)
            {
                var posPoint = new Position(Convert.ToDouble(intemofTramVienThong.LAT, CultureInfo.InvariantCulture), Convert.ToDouble(intemofTramVienThong.LONG, CultureInfo.InvariantCulture));
                var pinTram = new Pin()
                {
                    Position = posPoint,
                    Label = intemofTramVienThong.TramVTNAME + " " + intemofTramVienThong.KC.Split('.')[0] + " mét",
                    //Address = intemofTramVienThong.KC, //+ " mét" + "; Mega: " + intemofTramVienThong.ADSL + "; Fiber: " + intemofTramVienThong.FIBER + "; Mytv: " + intemofTramVienThong.MyTV,
                    Type = PinType.Place
                };                
                pinTram.Clicked += PinTramOnClicked;
                _myMap.Pins.Add(pinTram);
                _myMap.MoveToRegion(MapSpan.FromCenterAndRadius(Initial.PositionNow, Distance.FromKilometers(1)));

                ///

            }
            

        }

        private void PinTramOnClicked(object sender, EventArgs eventArgs)
        {
            _myMap.Pins.Clear();
            pins.Clear();
            foreach (var intemofTramVienThong in Initial.dataGetTramVienThong)
            {
                //if (Device.OS == TargetPlatform.Android)
                //    _myMap.MoveToRegion(MapSpan.FromCenterAndRadius(_myMap.CustomPins[0].Pin.Position, Distance.FromMiles(55.0)));
                //if (Device.OS == TargetPlatform.iOS)
                var posPoint = new Position(Convert.ToDouble(intemofTramVienThong.LAT, CultureInfo.InvariantCulture), Convert.ToDouble(intemofTramVienThong.LONG, CultureInfo.InvariantCulture));
                var pinTram = new Pin()
                {
                    Position = posPoint,
                    Label = intemofTramVienThong.TramVTNAME + " "+intemofTramVienThong.KC.Split('.')[0] + " mét",
                    //Address = intemofTramVienThong.KC, /* + " mét"+ "; Mega: " + intemofTramVienThong.ADSL + "; Fiber: " + intemofTramVienThong.FIBER + "; Mytv: " + intemofTramVienThong.MyTV,*/
                    Type = PinType.Place
                };
                pinTram.Clicked += PinTramOnClicked;
                _myMap.Pins.Add(pinTram);
            }

            int index = pins.IndexOf((Pin)sender);
            indexTramSelect = _myMap.Pins.IndexOf((Pin)sender);

            Acr.UserDialogs.UserDialogs.Instance.Toast("KC: " + Initial.dataGetTramVienThong[indexTramSelect].KC.Split('.')[0] + " mét\nMega:"
                + Initial.dataGetTramVienThong[indexTramSelect].ADSL + " Fiber:" + Initial.dataGetTramVienThong[indexTramSelect].FIBER
                + " MYTV:" + Initial.dataGetTramVienThong[indexTramSelect].MyTV, new TimeSpan(0, 0, 10));
            foreach (var item in Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi)
            {
                var posPoint = new Position(Convert.ToDouble(item.LAT, CultureInfo.InvariantCulture), Convert.ToDouble(item.LONG, CultureInfo.InvariantCulture));
                var pin = new Pin()
                {
                    Position = posPoint,
                    Label = item.CABI_NAME,
                    Address = item.CABI_ADDR,
                    Type = PinType.Place
                };
                pins.Add(pin);
                pin.Clicked += PinOnClicked;
                _myMap.Pins.Add(pin);
            }
        }

        //pim con nho
        private async void PinOnClicked(object sender, EventArgs eventArgs)
        {                        indexSelect = pins.IndexOf((Pin)sender);
           
            string hoten = Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi[indexSelect].HoTen;
            string sdt = Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi[indexSelect].SoDienThoai;

            Acr.UserDialogs.UserDialogs.Instance.Toast("ĐC: "+Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi[indexSelect].CABI_ADDR
                + "\nKC: "+ Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi[indexSelect].KC.Split('.')[0] 
                + " mét - Dung lượng: "+ Initial.dataGetTramVienThong[indexTramSelect].DanhSachKetCuoi[indexSelect].SoThueBao, new TimeSpan(0, 0, 10));

            var answer = await Application.Current.MainPage.DisplayAlert("Hỏi ý kiến kỹ thuật", "Anh " + hoten + "\n SĐT: " + sdt + "\n Bạn có muốn gọi không?", "Yes", "No");
            if (answer)
            {
                var PhoneCallTask = MessagingPlugin.PhoneDialer;
                if (PhoneCallTask.CanMakePhoneCall)
                    PhoneCallTask.MakePhoneCall(sdt);
                else
                    await Application.Current.MainPage.DisplayAlert("Thông báo", "Thiết bị của bạn không hỗ trợ thực hiện cuộc gọi", "Ok");                
            }
        }
        public async void GetTramVtData()
        {
            try
            {
                var myClient = new HttpClient();
                var point = new Position(Convert.ToDouble(Initial.PositionNow.Latitude, CultureInfo.InvariantCulture), Convert.ToDouble(Initial.PositionNow.Longitude, CultureInfo.InvariantCulture));

                var content = new FormUrlEncodedContent(new[]
                {
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
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }
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

            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }
        }
        //Dùng để blinding đọc địa chỉ hiện tại
        public string LocationText
        {
            get { return _locationText; }
            set
            {
                if (_locationText != value)
                {
                    _locationText = value;
                    OnPropertyChanged();
                }
            }
        }
    }

}
