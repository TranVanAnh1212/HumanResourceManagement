using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Common.Commons
{
    public static class CommonConstant
    {
        public const string TrangThaiTaiKhoan_Khoa = "Đã khóa";
        public const string TrangThaiTaiKhoan_HoatDong = "Đang hoạt động";
        public static BaoCaoDangNhap baoCaoDN { get; set; }
        public static TaiKhoan taiKhoanDN { get; set; }
    }
}
