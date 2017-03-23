using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CableMan.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static CableMan.ViewModel.PageTramVTGanDayViewModel;

namespace CableMan.ViewModel
{
    class PageTramVTGanDayViewModel
    {
        private ListView _listViewTramVtGanDay;
        private ObservableCollection<GroupedVeggieModel> grouped { get; set; }
        ObservableCollection<DanhSachKetCuoi> _tramVtGanDay = new ObservableCollection<DanhSachKetCuoi>();
        public PageTramVTGanDayViewModel(ListView listViewTramVtGanDay)
        {
            grouped = new ObservableCollection<GroupedVeggieModel>();

            _listViewTramVtGanDay = listViewTramVtGanDay;
            //_listViewTramVtGanDay.ItemsSource = _tramVtGanDay;
            _listViewTramVtGanDay.ItemsSource = grouped;
            _listViewTramVtGanDay.ItemTapped += ListViewTramVtGanDayOnItemTapped;
            

            foreach (var intemofTramVienThong in Initial.dataGetTramVienThong)
            {
                var groupName = new GroupedVeggieModel() { LongName = intemofTramVienThong.TramVTNAME, ShortName = "z" };
                foreach (var item in intemofTramVienThong.DanhSachKetCuoi)
                {
                    string tempSplitter = "";
                    string tempFType = "";
                    if (item.SPLITTER != "")
                        tempSplitter = "(" + item.SPLITTER + ") ";
                    tempFType = item.FType == "0" ? "copper50x50.jpg" : "fiber50x50.jpg";
                    groupName.Add(new DanhSachKetCuoi()
                    {
                        CABI_NAME = item.CABI_NAME, //1A
                        KC = tempSplitter + item.KC + " mét", //1B
                        CABI_ADDR = item.CABI_ADDR,
                        DungLuong = item.DungLuong, //2B
                        DLCapDen = intemofTramVienThong.TramVTNAME + " Cáp đến:" + item.DLCapDen + " đôi", //3A
                        SoThueBao = item.SoThueBao + " tb", //3B
                        FType = tempFType,
                        HoTen = item.HoTen,
                        SoDienThoai = item.SoDienThoai
                    });
                }
                grouped.Add(groupName);
            }            
         
        }

        private async void ListViewTramVtGanDayOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            DanhSachKetCuoi data = (DanhSachKetCuoi)itemTappedEventArgs.Item;
            var answer = await Application.Current.MainPage.DisplayAlert("Hỏi ý kiến kỹ thuật", "Anh" + data.HoTen + "\n SĐT: " + data.SoDienThoai + "\n Bạn có muốn gọi không?", "Yes", "No");
            if (answer)
            {
                Device.OpenUri(new Uri("tel:" + data.SoDienThoai));
            }
        }
        public class GroupedVeggieModel : ObservableCollection<DanhSachKetCuoi>
        {
            public string LongName { get; set; }
            public string ShortName { get; set; }
        }
    }
}
