using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CableMan.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CableMan.View
{
    public partial class PageTraCuuThueBao : TabbedPage
    {
        public PageTraCuuThueBao()
        {
            InitializeComponent();
            BindingContext = new PageTraCuuThueBaoViewModel(TraCuuThueBao, DoKiem, MaThueBao, TenThueBao, NhapDiaChi, LocThongTin, ImgFind, MaThueBaoSuyHao, LocThongTinSuyHao, ImgFindSuyHao);
        }
    }
}
