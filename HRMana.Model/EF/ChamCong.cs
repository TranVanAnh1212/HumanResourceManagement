//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMana.Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChamCong
    {
        public int maChamCong { get; set; }
        public string maNhanVien { get; set; }
        public int thang { get; set; }
        public int nam { get; set; }
        public decimal heSoLuong { get; set; }
        public Nullable<int> SoNgayCong { get; set; }
        public Nullable<decimal> ungTruocLuong { get; set; }
        public Nullable<decimal> conLai { get; set; }
        public Nullable<int> nghiPhep { get; set; }
        public Nullable<int> soGioTangCa { get; set; }
        public Nullable<decimal> luongTangCa { get; set; }
        public Nullable<decimal> phuCapCongViec { get; set; }
        public Nullable<decimal> tongNhan { get; set; }
    
        public virtual BacLuong BacLuong { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
