using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace CableMan.Model
{
    public static class Initial
    {
        public static string NguoiDungId = "";
        public static DataAPILogin User = new DataAPILogin();
        //public static ObservableCollection<DataAPIGetTramVT> dataGetTramVienThong
        public static List<DataAPIGetTramVT> dataGetTramVienThong;
        public static ObservableCollection<DataAPIDichVuKhaDung> dataGetDichVuKhaDung;
        public static ObservableCollection<DataAPIDoKiem> dataGetDoKiem; //chuyen qua List
        //public static List<DataAPIDoKiem> dataGetDoKiem; //chuyen qua List
        public static ObservableCollection<DataAPITraCuuNangCao> dataGetTraCuuNangCao;
        public static bool checkDone = false;
        public static Position PositionNow = new Position(10.539311, 106.414953);
        public static string PositionName = "";

    }
}
