using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableMan.Model
{
    public class DataAPIGetTramVT
    {
        public string TramVTID { get; set; }
        public string TramVTNAME { get; set; }
        public string ADSL { get; set; }
        public string FIBER { get; set; }
        public string MyTV { get; set; }
        public string LAT { get; set; }
        public string LONG { get; set; }
        public string KC { get; set; }
        public List<DanhSachKetCuoi> DanhSachKetCuoi { get; set; }
    }
}
