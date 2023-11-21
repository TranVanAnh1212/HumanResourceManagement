using HRMana.Main.ViewModel;
using HRMana.Model.DAO;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.View.Personnel
{
    /// <summary>
    /// Interaction logic for PersonnelDetailsWindow.xaml
    /// </summary>
    public partial class PersonnelDetailsWindow : Window
    {
        public PersonnelDetailsWindow()
        {
            InitializeComponent();
        }

        public PersonnelDetailsWindow(int maNhanVien)
        {
            InitializeComponent();
            this.DataContext = new PersonnelDetailsViewModel(maNhanVien);
        }

        private void txt_LuongOffer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 0)
            {
                double value = 0;
                double.TryParse(txt_LuongOffer.Text, out value);
                tb.Text = value.ToString("N0");
                tb.CaretIndex = tb.Text.Length;
            }
        }

        //private void Get_NhanVien_ByMaNhanVien()
        //{
        //    if (MaNhanVien != 0)
        //    {
        //        var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);

        //        if (nv != null)
        //        {
        //            HoTen = nv.tenNhanVien;
        //            GioiTinh = nv.gioiTinh;
        //            NgaySinh = nv.ngaySinh;
        //            NoiSinh = nv.noiSinh;
        //            Cccd = nv.CCCD;
        //            DienThoai = nv.dienThoai;
        //            NoiOHienTai = nv.noiOHienTai;
        //            QueQuan = nv.queQuan;
        //            TinhTrangGiaDinh = nv.giaDinh;
        //            EmailCaNhan = nv.emailCaNhan;
        //            EmailNoiBo = nv.emailNoiBo;
        //            CoSoLamViec = nv.coSoLamViec;
        //            LoaiHinhLamViec = nv.loaiHinhLamViec;
        //            LuongOffer = Convert.ToDouble(nv.luongOffer);
        //            MaHoSo = nv.maHoSo;
        //            MaChucVu = nv.maChucVu;
        //            TenChucVu = nv.ChucVu.tenChucVu;
        //            MaPhong = nv.maPhong;
        //            TenPhong = nv.PhongBan.tenPhong;
        //            MaTrinhDo = nv.maTrinhDo;
        //            TenTrinhDo = nv.TrinhDo.tenTrinhDo;
        //            MaDanToc = nv.maDanToc;
        //            TenDanToc = nv.DanToc.tenDanToc;
        //            MaTonGiao = nv.maTonGiao;
        //            TenTonGiao = nv.TonGiao.tenTonGiao;
        //            MaChuyenMon = nv.maChuyenMon;
        //            TenChuyenMon = nv.ChuyenMon.tenChuyenMon;
        //            MaHopDong = nv.maHopDong;
        //            SoHopDong = nv.HopDong.soHopDong;
        //            AnhThe = AppDomain.CurrentDomain.BaseDirectory + "NhanVien_Image/" + nv.anhThe;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Lỗi");
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private int _maNhanVien;
        //private string _hoTen;
        //private string _gioiTinh;
        //private DateTime _ngaySinh;
        //private string _noiSinh;
        //private string _cccd;
        //private string _dienThoai;
        //private string _noiOHienTai;
        //private string _QueQuan;
        //private string _tinhTrangGiaDinh;
        //private string _emailCaNhan;
        //private string _emailNoiBo;
        //private string _coSoLamViec;
        //private string _loaiHinhLamViec;
        //private double _luongOffer;
        //private int _maHoSo;
        //private int _maChucVu;
        //private string _tenChucVu;
        //private int _maPhong;
        //private string _tenPhong;
        //private int _maTrinhDo;
        //private string _tenTrinhDo;
        //private int _maDanToc;
        //private string _tenDanToc;
        //private int _maTonGiao;
        //private string _tenTonGiao;
        //private int _maChuyenMon;
        //private string _tenChuyenMon;
        //private int _maHopDong;
        //private string _soHopDong;
        //private string _anhThe;

        //public ICommand LoadWindowCommand { get; set; }

        //public int MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        //public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        //public string GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }
        //public DateTime NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
        //public string NoiSinh { get => _noiSinh; set { _noiSinh = value; OnPropertyChanged(); } }
        //public string Cccd { get => _cccd; set { _cccd = value; OnPropertyChanged(); } }
        //public string DienThoai { get => _dienThoai; set { _dienThoai = value; OnPropertyChanged(); } }
        //public string NoiOHienTai { get => _noiOHienTai; set { _noiOHienTai = value; OnPropertyChanged(); } }
        //public string QueQuan { get => _QueQuan; set { _QueQuan = value; OnPropertyChanged(); } }
        //public string TinhTrangGiaDinh { get => _tinhTrangGiaDinh; set { _tinhTrangGiaDinh = value; OnPropertyChanged(); } }
        //public string EmailCaNhan { get => _emailCaNhan; set { _emailCaNhan = value; OnPropertyChanged(); } }
        //public string EmailNoiBo { get => _emailNoiBo; set { _emailNoiBo = value; OnPropertyChanged(); } }
        //public string CoSoLamViec { get => _coSoLamViec; set { _coSoLamViec = value; OnPropertyChanged(); } }
        //public string LoaiHinhLamViec { get => _loaiHinhLamViec; set { _loaiHinhLamViec = value; OnPropertyChanged(); } }
        //public double LuongOffer { get => _luongOffer; set { _luongOffer = value; OnPropertyChanged(); } }
        //public int MaHoSo { get => _maHoSo; set { _maHoSo = value; OnPropertyChanged(); } }
        //public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        //public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        //public int MaPhong { get => _maPhong; set { _maPhong = value; OnPropertyChanged(); } }
        //public string TenPhong { get => _tenPhong; set { _tenPhong = value; OnPropertyChanged(); } }
        //public int MaTrinhDo { get => _maTrinhDo; set { _maTrinhDo = value; OnPropertyChanged(); } }
        //public string TenTrinhDo { get => _tenTrinhDo; set { _tenTrinhDo = value; OnPropertyChanged(); } }
        //public int MaDanToc { get => _maDanToc; set { _maDanToc = value; OnPropertyChanged(); } }
        //public string TenDanToc { get => _tenDanToc; set { _tenDanToc = value; OnPropertyChanged(); } }
        //public int MaTonGiao { get => _maTonGiao; set { _maTonGiao = value; OnPropertyChanged(); } }
        //public string TenTonGiao { get => _tenTonGiao; set { _tenTonGiao = value; OnPropertyChanged(); } }
        //public int MaChuyenMon { get => _maChuyenMon; set { _maChuyenMon = value; OnPropertyChanged(); } }
        //public string TenChuyenMon { get => _tenChuyenMon; set { _tenChuyenMon = value; OnPropertyChanged(); } }
        //public int MaHopDong { get => _maHopDong; set { _maHopDong = value; OnPropertyChanged(); } }
        //public string SoHopDong { get => _soHopDong; set { _soHopDong = value; OnPropertyChanged(); } }
        //public string AnhThe { get => _anhThe; set { _anhThe = value; OnPropertyChanged(); } }
    }
}
