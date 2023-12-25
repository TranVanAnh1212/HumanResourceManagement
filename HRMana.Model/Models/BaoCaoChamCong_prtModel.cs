using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.Models
{
    public class BaoCaoChamCong_prtModel
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public decimal HeSoLuong { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal TongNhan { get; set; }
        public Nullable<int> SoNgayCong { get; set; }
        public Nullable<decimal> UngTruocLuong { get; set; }
        public Nullable<decimal> ConLai { get; set; }
        public Nullable<int> NghiPhep { get; set; }
        public Nullable<int> SoGioTangCa { get; set; }
        public Nullable<decimal> LuongTangCa { get; set; }
        public Nullable<decimal> PhuCapCongViec { get; set; }
    }
}
