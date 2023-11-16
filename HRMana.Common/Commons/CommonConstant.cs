﻿using HRMana.Model.EF;
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
        public static List<Chitiet_Quyen> DsChiTietQuyen { get; set; }
        public static BaoCaoDangNhap baoCaoDN { get; set; }
        public static TaiKhoan taiKhoanDN { get; set; }
        public static bool Permission_ADD {  get; set; }    
        public static bool Permission_EDIT {  get; set; }    
        public static bool Permission_DEL {  get; set; }    
        public static bool Permission_VIEW {  get; set; }
        public static bool Permission_MUSER {  get; set; }
    }
}
