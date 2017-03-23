using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CableMan.Model;
using CableMan.View;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CableMan.ViewModel
{
    class PageTraCuuThueBaoViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private ListView _doKiem;
        private ListView _traCuuThueBao;
        private Entry _maThueBao;
        private Entry _tenThueBao;
        private Entry _nhapDiaChi;
        private Entry _maThueBaoSuyHao;
        private Entry _locThongTinSuyHao;
        private Entry _locThongTin;
        private Image _imgFind;
        private Image _imgFindSuyHao;
        private String _textLocThongTin;
        private String _textLocThongTinSuyHao;
        private List<DataAPIDoKiem> tempDataGetDoKiem = new List<DataAPIDoKiem>();
        private List<DataAPITraCuuNangCao> tempDataGetTraCuuNangCao = new List<DataAPITraCuuNangCao>();
        
        //private List<DataAPIDoKiem> tempDataGetDoKiem;

        public PageTraCuuThueBaoViewModel(ListView traCuuThueBao, ListView doKiem, Entry maThueBao, Entry tenThueBao, Entry nhapDiaChi, Entry locThongTin, Image imgFind,
            Entry maThueBaoSuyHao, Entry locThongTinSuyHao, Image imgFindSuyHao)
        {

            _traCuuThueBao = traCuuThueBao;
            //GetDataTraCuuNangCao();

            _doKiem = doKiem;
            //GetDataDoKiem();
            double Width = App.Current.MainPage.Width - 53;
            //Tra cứu nâng cao
            _maThueBao = maThueBao; _maThueBao.WidthRequest = Width / 3;
            _tenThueBao = tenThueBao; _tenThueBao.WidthRequest = Width / 3;
            _nhapDiaChi = nhapDiaChi; _nhapDiaChi.WidthRequest = Width / 3;
            _locThongTin = locThongTin;
            _imgFind = imgFind; _imgFind.WidthRequest = 32;

            //Tra cứu suy hao
            _locThongTinSuyHao = locThongTinSuyHao; _locThongTinSuyHao.WidthRequest = Width + 13;
            _maThueBaoSuyHao = maThueBaoSuyHao;
            _imgFindSuyHao = imgFindSuyHao; _imgFindSuyHao.WidthRequest = 32;

            //img click nút find của tìm kím nâng cao
            //Creating TapGestureRecognizers  
            var tapImage = new TapGestureRecognizer();
            //Binding events  
            tapImage.Tapped += TapImage_Tapped;
            //Associating tap events to the image buttons  
            _imgFind.GestureRecognizers.Add(tapImage);
            //img  hết click nút find của tìm kím nâng cao


            //img click nút find của tìm kím nâng cao
            //Creating TapGestureRecognizers  
            var tapImageSuyHao = new TapGestureRecognizer();
            //Binding events  
            tapImageSuyHao.Tapped += TapImageSuyHao_Tapped;
            //Associating tap events to the image buttons  
            _imgFindSuyHao.GestureRecognizers.Add(tapImageSuyHao);
            //img  hết click nút find của tìm kím nâng cao
        }
        //sự kiện img click của tìm kím suy hao
        public async void TapImageSuyHao_Tapped(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading...");
            try
            {
                var myClient = new HttpClient();
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("NguoiDungId",Initial.NguoiDungId),//Initial.NguoiDungId
                        new KeyValuePair<string, string>("Token", Initial.User.TOKEN),//odp code
                        new KeyValuePair<string, string>("phoneKey", "123456"),
                        new KeyValuePair<string, string>("Account", _maThueBaoSuyHao.Text)
                });

                var request = await myClient.PostAsync(Conf.urlDoKiem, content);
                string json = await request.Content.ReadAsStringAsync();
                if (json.Equals("")) await Application.Current.MainPage.DisplayAlert("Thông báo", "Không có account '"+ _maThueBaoSuyHao.Text + "' trong hệ thống. \nVui lòng kiểm tra lại!", "Ok");
                Initial.dataGetDoKiem = JsonConvert.DeserializeObject<ObservableCollection<DataAPIDoKiem>>(json);

                //Initial.dataGetDoKiem = JsonConvert.DeserializeObject<List<DataAPIDoKiem>>(json);
                foreach (var item in Initial.dataGetDoKiem)
                {
                    tempDataGetDoKiem.Add(item);
                }
                //tempDataGetDoKiem = Initial.dataGetDoKiem;
                //_doKiem.ItemsSource = tempDataGetDoKiem;
                _doKiem.ItemsSource = Initial.dataGetDoKiem;// gán Source
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                throw;
            }

        }
        //sự kiện img click của tìm kím nâng cao
        public async void TapImage_Tapped(object sender, EventArgs e)
        {            
                try
                {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                var myClient = new HttpClient();
                    // var request = await myClient.GetAsync(Conf.urlTraCuuNangCao);                
                    var request = await myClient.GetAsync(Conf.urlTraCuuNangCao + "?pPersionid=" + Initial.User.NguoiDungId + "&pMaTB=" + _maThueBao.Text + "&pTenTB=" + _tenThueBao.Text + "&pDiaChi=" + _nhapDiaChi.Text + "&pPhoneKey=123456");
                    string json = await request.Content.ReadAsStringAsync();
                    Initial.dataGetTraCuuNangCao = JsonConvert.DeserializeObject<ObservableCollection<DataAPITraCuuNangCao>>(json);

                    _traCuuThueBao.ItemsSource = Initial.dataGetTraCuuNangCao;// gán Source

                foreach (var item in Initial.dataGetTraCuuNangCao)
                {
                    tempDataGetTraCuuNangCao.Add(item);
                }
                
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
                catch (Exception)
                {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
                    throw;
                }            
        }
        public string TextLocThongTin
        {
            get { return _textLocThongTin; }
            set
            {
                if (_textLocThongTin != value)
                {
                    _textLocThongTin = value;
                    Thaydoi();
                    OnPropertyChanged();
                }
            }
        }
        private void Thaydoi()
        {
            Initial.dataGetTraCuuNangCao.Clear();
            foreach (var item in tempDataGetTraCuuNangCao)
            {
                Initial.dataGetTraCuuNangCao.Add(item);
            }
            if (TextLocThongTin.Equals(null))
            { }            
            else
            {
                foreach (var item in tempDataGetTraCuuNangCao)
                {
                    if (item.TenDVVT.Equals(null))  item.TenDVVT = "";
                    if (item.NgayHT.Equals(null))   item.NgayHT = "";
                    if (item.TenTB.Equals(null))    item.TenTB = "";
                    if (item.MaTB.Equals(null))     item.MaTB = "";               
                    if (item.DaiVT.Equals(null))    item.DaiVT = "";
                    if (item.TramVT.Equals(null))   item.TramVT = "";
                    if (item.DiaChi.Equals(null))   item.DiaChi = "";
                    if (item.LienHe.Equals(null))   item.LienHe = "";
                    if (item.GhiChu.Equals(null))   item.GhiChu = "";


                    if (!item.TenDVVT.Contains(TextLocThongTin)&& !item.NgayHT.Contains(TextLocThongTin)
                        && !item.TenTB.Contains(TextLocThongTin) && !item.MaTB.Contains(TextLocThongTin)
                            /*&& !item.NgayHT.Contains(TextLocThongTin) */&& !item.DaiVT.Contains(TextLocThongTin)
                                && !item.TramVT.Contains(TextLocThongTin) && !item.DiaChi.Contains(TextLocThongTin)
                                    && !item.LienHe.Contains(TextLocThongTin) && !item.GhiChu.Contains(TextLocThongTin)
                        )
                    {
                        Initial.dataGetTraCuuNangCao.Remove(item);
                    }                   
                      
                }
            }              
        }
        public string TextLocThongTinSuyHao
        {
            get { return _textLocThongTinSuyHao; }
            set
            {
                if (_textLocThongTinSuyHao != value)
                {
                    _textLocThongTinSuyHao = value;
                    ThaydoiSuyHao();
                    OnPropertyChanged();
                }
            }
        }
        private void ThaydoiSuyHao()
        {
            Initial.dataGetDoKiem.Clear();
            foreach (var item in tempDataGetDoKiem)
            {
                Initial.dataGetDoKiem.Add(item);
            }
            if (TextLocThongTinSuyHao.Equals(null))
            { }
            else
            {
                foreach (var item in tempDataGetDoKiem)
                {
                    if (!(item.Name.Contains(TextLocThongTinSuyHao)) || (item.Value.Contains(TextLocThongTinSuyHao)))
                        Initial.dataGetDoKiem.Remove(item);
                }
            }
        }
        ////lay dữ liệu web GetDataDoKiem
        //public async void GetDataTraCuuNangCao()
        //{
        //    try
        //    {
        //        var myClient = new HttpClient();
        //        // var request = await myClient.GetAsync(Conf.urlTraCuuNangCao);                
        //        var request = await myClient.GetAsync(Conf.urlTraCuuNangCao+"?pPersionid="+Initial.User.NguoiDungId+ "&pMaTB="+ _maThueBao.Text+"&pTenTB="+_tenThueBao.Text+ "&pDiaChi="+ _nhapDiaChi.Text + "&pPhoneKey=123456");
        //        string json = await request.Content.ReadAsStringAsync();
        //        Initial.dataGetTraCuuNangCao = JsonConvert.DeserializeObject<ObservableCollection<DataAPITraCuuNangCao>>(json);

        //        _traCuuThueBao.ItemsSource = Initial.dataGetTraCuuNangCao;// gán Source
        //    }
        //    catch (Exception)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
        //        throw;
        //    }

        //GetDataDoKiem từ web api
        //public async void GetDataDoKiem()
        //{
        //    try
        //    {
        //        var myClient = new HttpClient();
        //        HttpContent content = new FormUrlEncodedContent(new[]
        //        {
        //                new KeyValuePair<string, string>("NguoiDungId",Initial.NguoiDungId),//Initial.NguoiDungId
        //                new KeyValuePair<string, string>("Token", Initial.User.TOKEN),//odp code
        //                new KeyValuePair<string, string>("phoneKey", "123456"),
        //                new KeyValuePair<string, string>("Account", "fibercothu889467")
        //        });
        //        var request = await myClient.PostAsync(Conf.urlDoKiem, content);
        //        string json = await request.Content.ReadAsStringAsync();
        //        Initial.dataGetDoKiem = JsonConvert.DeserializeObject<ObservableCollection<DataAPIDoKiem>>(json);

        //        _doKiem.ItemsSource = Initial.dataGetDoKiem;// gán Source
        //    }
        //    catch (Exception)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Thông báo", "Kiểm tra lại kết nối mạng!", "Ok");
        //        throw;
        //    }
        //}
    }
}
