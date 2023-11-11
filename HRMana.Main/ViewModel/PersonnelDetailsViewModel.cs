using HRMana.Main.View.Dialog;
using HRMana.Main.View.Personnel;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class PersonnelDetailsViewModel : BaseViewModel
    {
        #region Khai báo biến

        private int _maNhanVien;
        private string _hoTen;
        private string _gioiTinh;
        private bool _Nam_Checked;
        private bool _Nu_Checked;
        private string _ngaySinh;
        private string _noiSinh;
        private string _cccd;
        private string _dienThoai;
        private string _noiOHienTai;
        private string _QueQuan;
        private string _tinhTrangGiaDinh;
        private string _emailCaNhan;
        private string _emailNoiBo;
        private string _coSoLamViec;
        private string _loaiHinhLamViec;
        private string _luongOffer;
        private int _maHoSo;
        private int _maChucVu;
        private string _tenChucVu;
        private ObservableCollection<ChucVu> _listChucVu;
        private ChucVu _selectedChucVu;
        private int _maPhong;
        private string _tenPhong;
        private ObservableCollection<PhongBan> _listPhongBan;
        private PhongBan _selectedPhongBan;
        private int _maTrinhDo;
        private string _tenTrinhDo;
        private ObservableCollection<TrinhDo> _listTrinhDo;
        private TrinhDo _selectedTrinhDo;
        private int _maDanToc;
        private string _tenDanToc;
        private ObservableCollection<DanToc> _listDanToc;
        private DanToc _selectedDanToc;
        private int _maTonGiao;
        private string _tenTonGiao;
        private ObservableCollection<TonGiao> _listTonGiao;
        private TonGiao _selectedTonGiao;
        private int _maChuyenMon;
        private string _tenChuyenMon;
        private ObservableCollection<ChuyenMon> _listChuyenMon;
        private ChuyenMon _selectedChuyenMon;
        private int _maHopDong;
        private string _soHopDong;
        private string _anhThe;

        private string _message;
        private string _fill;

        public ICommand Update_NhanVienCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand Delete_NhanVienCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand ChooseImageCommand { get; set; }

        public int MaNhanVien
        {
            get => _maNhanVien;
            set
            {
                _maNhanVien = value;
                //OnIDChanged(value);
                OnPropertyChanged();
            }
        }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public string GioiTinh
        {
            get => _gioiTinh;
            set
            {
                _gioiTinh = value;
                OnPropertyChanged();

                //if (Nam_Checked) 
                //    GioiTinh = "Nam";

                //if (Nu_Checked) 
                //    GioiTinh = "Nữ";
            }
        }
        public string NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
        public string NoiSinh { get => _noiSinh; set { _noiSinh = value; OnPropertyChanged(); } }
        public string Cccd { get => _cccd; set { _cccd = value; OnPropertyChanged(); } }
        public string DienThoai { get => _dienThoai; set { _dienThoai = value; OnPropertyChanged(); } }
        public string NoiOHienTai { get => _noiOHienTai; set { _noiOHienTai = value; OnPropertyChanged(); } }
        public string QueQuan { get => _QueQuan; set { _QueQuan = value; OnPropertyChanged(); } }
        public string TinhTrangGiaDinh { get => _tinhTrangGiaDinh; set { _tinhTrangGiaDinh = value; OnPropertyChanged(); } }
        public string EmailCaNhan { get => _emailCaNhan; set { _emailCaNhan = value; OnPropertyChanged(); } }
        public string EmailNoiBo { get => _emailNoiBo; set { _emailNoiBo = value; OnPropertyChanged(); } }
        public string CoSoLamViec { get => _coSoLamViec; set { _coSoLamViec = value; OnPropertyChanged(); } }
        public string LoaiHinhLamViec { get => _loaiHinhLamViec; set { _loaiHinhLamViec = value; OnPropertyChanged(); } }
        public string LuongOffer { get => _luongOffer; set { _luongOffer = value; OnPropertyChanged(); } }
        public int MaHoSo { get => _maHoSo; set { _maHoSo = value; OnPropertyChanged(); } }
        public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        public int MaPhong { get => _maPhong; set { _maPhong = value; OnPropertyChanged(); } }
        public string TenPhong { get => _tenPhong; set { _tenPhong = value; OnPropertyChanged(); } }
        public int MaTrinhDo { get => _maTrinhDo; set { _maTrinhDo = value; OnPropertyChanged(); } }
        public string TenTrinhDo { get => _tenTrinhDo; set { _tenTrinhDo = value; OnPropertyChanged(); } }
        public int MaDanToc { get => _maDanToc; set { _maDanToc = value; OnPropertyChanged(); } }
        public string TenDanToc { get => _tenDanToc; set { _tenDanToc = value; OnPropertyChanged(); } }
        public int MaTonGiao { get => _maTonGiao; set { _maTonGiao = value; OnPropertyChanged(); } }
        public string TenTonGiao { get => _tenTonGiao; set { _tenTonGiao = value; OnPropertyChanged(); } }
        public int MaChuyenMon { get => _maChuyenMon; set { _maChuyenMon = value; OnPropertyChanged(); } }
        public string TenChuyenMon { get => _tenChuyenMon; set { _tenChuyenMon = value; OnPropertyChanged(); } }
        public int MaHopDong { get => _maHopDong; set { _maHopDong = value; OnPropertyChanged(); } }
        public string SoHopDong { get => _soHopDong; set { _soHopDong = value; OnPropertyChanged(); } }
        public string AnhThe { get => _anhThe; set { _anhThe = value; OnPropertyChanged(); } }

        public ObservableCollection<ChucVu> ListChucVu { get => _listChucVu; set { _listChucVu = value; OnPropertyChanged(); } }
        public ChucVu SelectedChucVu
        {
            get => _selectedChucVu;
            set
            {
                _selectedChucVu = value;
                OnPropertyChanged();

                if (SelectedChucVu != null)
                {
                    MaChucVu = SelectedChucVu.maChucVu;
                    TenChucVu = SelectedChucVu.tenChucVu;
                }
            }
        }
        public ObservableCollection<PhongBan> ListPhongBan { get => _listPhongBan; set { _listPhongBan = value; OnPropertyChanged(); } }
        public PhongBan SelectedPhongBan
        {
            get => _selectedPhongBan;
            set
            {
                _selectedPhongBan = value;
                OnPropertyChanged();

                if (_selectedPhongBan != null)
                {
                    MaPhong = SelectedPhongBan.maPhong;
                    TenPhong = SelectedPhongBan.tenPhong;
                }
            }
        }
        public ObservableCollection<TrinhDo> ListTrinhDo { get => _listTrinhDo; set { _listTrinhDo = value; OnPropertyChanged(); } }
        public TrinhDo SelectedTrinhDo
        {
            get => _selectedTrinhDo;
            set
            {
                _selectedTrinhDo = value;
                OnPropertyChanged();

                if (SelectedTrinhDo != null)
                {
                    MaTrinhDo = SelectedTrinhDo.maTrinhDo;
                    TenTrinhDo = SelectedTrinhDo.tenTrinhDo;
                }
            }
        }
        public ObservableCollection<DanToc> ListDanToc { get => _listDanToc; set { _listDanToc = value; OnPropertyChanged(); } }
        public DanToc SelectedDanToc
        {
            get => _selectedDanToc;
            set
            {
                _selectedDanToc = value;
                OnPropertyChanged();

                if (_selectedDanToc != null)
                {
                    MaDanToc = SelectedDanToc.maDanToc;
                    TenDanToc = SelectedDanToc.tenDanToc;
                }
            }
        }
        public ObservableCollection<TonGiao> ListTonGiao { get => _listTonGiao; set { _listTonGiao = value; OnPropertyChanged(); } }
        public TonGiao SelectedTonGiao
        {
            get => _selectedTonGiao;
            set
            {
                _selectedTonGiao = value;
                OnPropertyChanged();

                if (SelectedTonGiao != null)
                {
                    MaTonGiao = SelectedTonGiao.maTonGiao;
                    TenTonGiao = SelectedTonGiao.tenTonGiao;
                }
            }
        }
        public ObservableCollection<ChuyenMon> ListChuyenMon { get => _listChuyenMon; set { _listChuyenMon = value; OnPropertyChanged(); } }
        public ChuyenMon SelectedChuyenMon
        {
            get => _selectedChuyenMon;
            set
            {
                _selectedChuyenMon = value;
                OnPropertyChanged();

                if (SelectedChuyenMon != null)
                {
                    MaChuyenMon = SelectedChuyenMon.maChuyenMon;
                    TenChuyenMon = SelectedChuyenMon.tenChuyenMon;
                }
            }
        }

        public bool Nam_Checked { get => _Nam_Checked; set { _Nam_Checked = value; OnPropertyChanged(); } }
        public bool Nu_Checked { get => _Nu_Checked; set { _Nu_Checked = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }

        #endregion


        public PersonnelDetailsViewModel(int maNhanVien)
        {
            MaNhanVien = maNhanVien;
            Initialize();
        }

        public PersonnelDetailsViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {

            LoadWindowCommand = new RelayCommand<Window>(
                (param) => true,
                (param) =>
                {
                    Get_NhanVien_ByMaNhanVien();
                }
                );

            ExitCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    if (activeWindow != null)
                    {
                        activeWindow.Close();
                    }
                }
                );

            Delete_NhanVienCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    DialogWindow dialogWindow = new DialogWindow();
                    dialogWindow.DialogMessage = "Bạn có chắc muốn xóa nhân viên này";
                    if (true == dialogWindow.ShowDialog())
                    {

                    }
                }
                );

            Update_NhanVienCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    DialogWindow dialogWindow = new DialogWindow();
                    dialogWindow.DialogMessage = "Bạn có chắc muốn cập nhật thông tin nhân viên này";
                    if (true == dialogWindow.ShowDialog())
                    {

                    }
                }
                );

            SaveCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {

                }
                );
        }

        public void SetData(int data)
        {
            MaNhanVien = data;
        }

        private void Get_NhanVien_ByMaNhanVien()
        {
            if (MaNhanVien != 0)
            {
                var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);

                if (nv != null)
                {
                    HoTen = nv.tenNhanVien;
                    GioiTinh = nv.gioiTinh;
                    NgaySinh = nv.ngaySinh.ToString("dd/MM/yyyy");
                    NoiSinh = nv.noiSinh;
                    Cccd = nv.CCCD;
                    DienThoai = nv.dienThoai;
                    NoiOHienTai = nv.noiOHienTai;
                    QueQuan = nv.queQuan;
                    TinhTrangGiaDinh = nv.giaDinh;
                    EmailCaNhan = nv.emailCaNhan;
                    EmailNoiBo = nv.emailNoiBo;
                    CoSoLamViec = nv.coSoLamViec;
                    LoaiHinhLamViec = nv.loaiHinhLamViec;
                    LuongOffer = nv.luongOffer.ToString();
                    MaHoSo = nv.maHoSo;
                    MaChucVu = nv.maChucVu;
                    TenChucVu = nv.ChucVu.tenChucVu;
                    MaPhong = nv.maPhong;
                    TenPhong = nv.PhongBan.tenPhong;
                    MaTrinhDo = nv.maTrinhDo;
                    TenTrinhDo = nv.TrinhDo.tenTrinhDo;
                    MaDanToc = nv.maDanToc;
                    TenDanToc = nv.DanToc.tenDanToc;
                    MaTonGiao = nv.maTonGiao;
                    TenTonGiao = nv.TonGiao.tenTonGiao;
                    MaChuyenMon = nv.maChuyenMon;
                    TenChuyenMon = nv.ChuyenMon.tenChuyenMon;
                    MaHopDong = nv.maHopDong;
                    SoHopDong = nv.HopDong.soHopDong;
                    AnhThe = AppDomain.CurrentDomain.BaseDirectory + "NhanVien_Image/" + nv.anhThe;
                }
            }
            else
            {
                MessageBox.Show("Lấy thông tin nhân viên lỗi");
            }
        }
    }
}
