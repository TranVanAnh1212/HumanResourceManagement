﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLNhanSuEntities : DbContext
    {
        public QLNhanSuEntities()
            : base("metadata=res://*/EF.QLNhanSuDBContext.csdl|res://*/EF.QLNhanSuDBContext.ssdl|res://*/EF.QLNhanSuDBContext.msl;provider=System.Data.SqlClient;provider connection string=\"data source=.\\sqlexpress;initial catalog=QLNhanSu;persist security info=True;user id=sa;password=12062003;multipleactiveresultsets=True;application name=EntityFramework\"")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BacLuong> BacLuong { get; set; }
        public virtual DbSet<BaoCaoDangNhap> BaoCaoDangNhap { get; set; }
        public virtual DbSet<ChamCong> ChamCong { get; set; }
        public virtual DbSet<Chitiet_Quyen> Chitiet_Quyen { get; set; }
        public virtual DbSet<ChiTietQuyen_Quyen> ChiTietQuyen_Quyen { get; set; }
        public virtual DbSet<ChucVu> ChucVu { get; set; }
        public virtual DbSet<ChuyenCongTac> ChuyenCongTac { get; set; }
        public virtual DbSet<ChuyenCongTac_NhanVien> ChuyenCongTac_NhanVien { get; set; }
        public virtual DbSet<ChuyenMon> ChuyenMon { get; set; }
        public virtual DbSet<DanToc> DanToc { get; set; }
        public virtual DbSet<HopDong> HopDong { get; set; }
        public virtual DbSet<HoSo> HoSo { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<Quyen> Quyen { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<TonGiao> TonGiao { get; set; }
        public virtual DbSet<TrinhDo> TrinhDo { get; set; }
    }
}
