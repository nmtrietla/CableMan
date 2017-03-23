using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CableMan.Model;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CableMan.ViewModel
{
    class PageKetCuoiGanDayViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private ListView _listViewKetCuoiKhaDungCopper;
        private ListView _listViewKetCuoiKhaDungFiber;
        //private ListView _listViewKetCuoiKhaDungCapDong;
        //public PageKetCuoiGanDayViewModel(INavigation navigation)
        public PageKetCuoiGanDayViewModel(ListView listViewKetCuoiKhaDungCopper, ListView listViewKetCuoiKhaDungFiber)
        {
            ObservableCollection<DanhSachKetCuoi> fiber = new ObservableCollection<DanhSachKetCuoi>();
            ObservableCollection<DanhSachKetCuoi> copper = new ObservableCollection<DanhSachKetCuoi>();

            foreach (var item in Initial.dataGetTramVienThong[0].DanhSachKetCuoi)
            {
                string temp = "";
                if (item.SPLITTER != "")
                    temp = "(" + item.SPLITTER + ") ";
                if (item.FType == "0")
                    copper.Add(new DanhSachKetCuoi()
                    {
                        CABI_NAME = item.CABI_NAME, //1A
                        KC = temp + item.KC + " mét", //1B
                        CABI_ADDR = item.CABI_ADDR,
                        DungLuong = item.DungLuong, //2B
                        DLCapDen = Initial.dataGetTramVienThong[0].TramVTNAME + " Cáp đến:" + item.DLCapDen + " đôi", //3A
                        SoThueBao = item.SoThueBao + " tb", //3B
                        FType = "copper50x50.jpg",
                        HoTen = item.HoTen,
                        SoDienThoai = item.SoDienThoai
                    });

                else
                {
                    fiber.Add(new DanhSachKetCuoi()
                    {
                        CABI_NAME = item.CABI_NAME, //1A
                        KC = temp + item.KC + " mét", //1B
                        CABI_ADDR = item.CABI_ADDR,
                        DungLuong = item.DungLuong, //2B
                        DLCapDen = Initial.dataGetTramVienThong[0].TramVTNAME + " Cáp đến:" + item.DLCapDen + " đôi", //3A
                        SoThueBao = item.SoThueBao + " tb", //3B
                        FType = "fiber50x50.jpg",
                        HoTen = item.HoTen,
                        SoDienThoai = item.SoDienThoai
                    });
                }
            }
            _listViewKetCuoiKhaDungCopper = listViewKetCuoiKhaDungCopper;
            _listViewKetCuoiKhaDungCopper.ItemsSource = copper;
            _listViewKetCuoiKhaDungCopper.ItemTapped += ListViewKetCuoiKhaDungCopperOnItemTapped;

            _listViewKetCuoiKhaDungFiber = listViewKetCuoiKhaDungFiber;
            _listViewKetCuoiKhaDungFiber.ItemsSource = fiber;
            _listViewKetCuoiKhaDungFiber.ItemTapped += ListViewKetCuoiKhaDungFiberOnItemTapped;
        }

        private async void ListViewKetCuoiKhaDungFiberOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            DanhSachKetCuoi data = (DanhSachKetCuoi)itemTappedEventArgs.Item;
            var answer = await Application.Current.MainPage.DisplayAlert("Hỏi ý kiến kỹ thuật", "Anh" + data.HoTen + "\n SĐT: " + data.SoDienThoai + "\n Bạn có muốn gọi không?", "Yes", "No");
            if (answer)
            {
                Device.OpenUri(new Uri("tel:" + data.SoDienThoai));
            }
        }


        //ListViewKetCuoiKhaDungCopperOnItemTapped
        private async void ListViewKetCuoiKhaDungCopperOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            DanhSachKetCuoi data = (DanhSachKetCuoi)itemTappedEventArgs.Item;
            var answer = await Application.Current.MainPage.DisplayAlert("Hỏi ý kiến kỹ thuật", "Anh"+ data.HoTen +"\n SĐT: "+data.SoDienThoai + "\n Bạn có muốn gọi không?", "Yes", "No");
            if (answer)
            {
                Device.OpenUri(new Uri("tel:"+ data.SoDienThoai));
            }
        }
    }
}
