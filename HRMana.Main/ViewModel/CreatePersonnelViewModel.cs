using HRMana.Common;
using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Main.View.Personnel;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class CreatePersonnelViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Khai báo biến
        private string _maNhanVien;
        private string _hoTen;
        private string _gioiTinh;
        private bool _Nam_Checked;
        private bool _Nu_Checked;
        private string _ngaySinh;
        private string _cccd;
        private string _dienThoai;
        private string _noiOHienTai;
        private string _QueQuan;
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

        public ICommand LoadWindowCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CreateNewCommand { get; set; }
        public ICommand ChooseImageCommand { get; set; }

        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
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
        public string Cccd { get => _cccd; set { _cccd = value; OnPropertyChanged(); } }
        public string DienThoai { get => _dienThoai; set { _dienThoai = value; OnPropertyChanged(); } }
        public string NoiOHienTai
        {
            get => _noiOHienTai;
            set
            {
                // Lỗi này chỉ hiển thị lên giao diện khi có Material design library

                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ArgumentException("Nơi ở hiện tại không được bỏ trống");
                //}

                _noiOHienTai = value; 
                OnPropertyChanged();

                
            }
        }
        public string QueQuan { get => _QueQuan; set { _QueQuan = value; OnPropertyChanged(); } }
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

        #region Bắt lỗi
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                var res = "";

                switch(columnName)
                {
                    case "HoTen":
                        if (string.IsNullOrEmpty(HoTen))
                        {
                            res = "Họ tên nhân viên không được bỏ trống";
                        }
                        break;
                    case "NoiOHienTai":
                        if (string.IsNullOrEmpty(NoiOHienTai))
                        {
                            res = "Nơi ở hiện tại không được bỏ trống";
                        }
                        break;
                    case "Cccd":
                        if (string.IsNullOrEmpty(Cccd))
                        {
                            res = "CCCD không được bỏ trống";
                        }

                        if (!string.IsNullOrEmpty(Cccd) && !StringHelper.CheckStringContainsLetter(Cccd))
                        {
                            res = "CCCD không thể chứa ký tự số.";
                        }
                        break;
                    case "QueQuan":
                        if (string.IsNullOrEmpty(QueQuan))
                        {
                            res = "Quê quán không được bỏ trống";
                        }
                        break;
                    case "NgaySinh":
                        if (NgaySinh == null )
                        {
                            res = "Ngày sinh không được bỏ trống";
                        }

                        if (!StringHelper.IsValidDate(NgaySinh, "dd/MM/yyyy"))
                        {
                            res = "Ngày sinh không đúng định dạng";
                        }

                        if (DateTime.TryParse(NgaySinh, out var ns) && DateTime.Now.Year - ns.Year < 18)
                        {
                            res = "Nhân viên phải có số tuổi lớn hơn 18.";
                        }

                        if (DateTime.TryParse(NgaySinh, out var checkTuoi) && DateTime.Now < checkTuoi)
                        {
                            res = "Ngày tháng năm sinh phải bé hơn ngày táng hiện tại.";
                        }
                        break;
                    case "SelectedTonGiao":
                        if (SelectedTonGiao == null)
                        {
                            res = "Tôn giáo chưa được chọn";
                        }
                        break;
                    case "SelectedDanToc":
                        if (SelectedDanToc == null)
                        {
                            res = "Dân tộc chưa được chọn";
                        }
                        break;
                    case "SelectedTrinhDo":
                        if (SelectedTrinhDo == null)
                        {
                            res = "Trình độ học vấn chưa được chọn";
                        }
                        break;
                    case "SelectedChuyenMon":
                        if (SelectedChuyenMon == null)
                        {
                            res = "Chuyên môn chưa được chọn";
                        }
                        break;
                    case "SelectedChucVu":
                        if (SelectedChucVu == null)
                        {
                            res = "Chức vụ chưa được chọn";
                        }
                        break;
                    case "SelectedPhongBan":
                        if (SelectedPhongBan == null)
                        {
                            res = "Phòng ban chưa được chọn";
                        }
                        break;

                }

                return res;
            }
        }
        #endregion

        #endregion

        public CreatePersonnelViewModel()
        {
            Nam_Checked = true;

            Initialized();
        }

        private void Initialized()
        {
            LoadWindowCommand = new RelayCommand<Page>(
                (param) => true,
                (param) =>
                {
                    GetList_ChucVu();
                    GetList_PhongBan();
                    GetList_TrinhDo();
                    GetList_DanToc();
                    GetList_TonGiao();
                    GetList_ChuyenMon();
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    EmptyField();
                }
                );

            CreateNewCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (string.IsNullOrEmpty(NoiOHienTai))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(Cccd))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(NoiOHienTai))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(QueQuan))
                    {
                        return false;
                    }

                    if (NgaySinh == null)
                    {
                        return false;
                    }

                    if (!StringHelper.IsValidDate(NgaySinh, "dd/MM/yyyy"))
                    {
                        return false;
                    }

                    if (DateTime.Now.Year - Convert.ToDateTime(NgaySinh).Year < 18)
                    {
                        return false;
                    }

                    if (DateTime.Now < Convert.ToDateTime(NgaySinh))
                    {
                        return false;
                    }

                    if (SelectedTrinhDo == null)
                    {
                        return false;
                    }

                    if (SelectedPhongBan == null)
                    {
                        return false;
                    }

                    if (SelectedTonGiao == null)
                    {
                        return false;
                    }

                    if (SelectedChuyenMon == null)
                    {
                        return false; 
                    }

                    if (SelectedDanToc == null)
                    {
                        return false;
                    }

                    if (SelectedChucVu == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    CreateNewPersonnel();
                }
                );

            ChooseImageCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    ChooseImage();
                }
                );
        }

        private void CreateNewPersonnel()
        {
            try
            {
                if (Nam_Checked == false && Nu_Checked == false)
                {
                    ShowNotification("Giới tính không được để trống.", "#FFFF5858");
                    return;
                }

                GioiTinh = Nam_Checked ? "Nam" : "Nữ";
                GioiTinh = Nu_Checked ? "Nữ" : "Nam";

                if (string.IsNullOrEmpty(AnhThe))
                {
                    AnhThe = "DefaultAvatar.jpg";
                }
                else
                {
                    AnhThe = Path.GetFileName(AnhThe);
                }

                NhanVien nv = new NhanVien()
                {
                    tenNhanVien = HoTen.Trim(),
                    gioiTinh = GioiTinh.Trim(),
                    ngaySinh = Convert.ToDateTime(NgaySinh),
                    CCCD = Cccd.Trim(),
                    dienThoai = DienThoai,
                    noiOHienTai = NoiOHienTai.Trim(),
                    queQuan = QueQuan.Trim(),
                    anhThe = AnhThe.Trim(),
                    maHoSo = MaHoSo,
                    maHopDong = MaHopDong,
                    maChucVu = MaChucVu,
                    maPhong = MaPhong,
                    maTrinhDo = MaTrinhDo,
                    maDanToc = MaDanToc,
                    maTonGiao = MaTonGiao,
                    maChuyenMon = MaChuyenMon,
                };

                var result = new NhanVienDAO().CreateNew_NhanVien(nv);

                if (!string.IsNullOrEmpty(result))
                {
                    ShowNotification("Thêm thành công.", "#FF58FF7B");
                    EmptyField();
                }
                else
                {
                    ShowNotification("Thêm mới không thành công.", "#FFFF5858");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ShowNotification(string message, string fill)
        {
            Message = message;
            Fill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }

        private void ChooseImage()
        {
            AnhThe = FeatureHelper.ChooseImage();
        }

        private void EmptyField()
        {
            SelectedChucVu = null;
            SelectedChuyenMon = null;
            SelectedDanToc = null;
            SelectedPhongBan = null;
            SelectedTonGiao = null;
            SelectedTrinhDo = null;

            MaNhanVien = string.Empty;
            HoTen = string.Empty;
            GioiTinh = string.Empty;
            NgaySinh = string.Empty;
            Cccd = string.Empty;
            DienThoai = string.Empty;
            NoiOHienTai = string.Empty;
            QueQuan = string.Empty;
            MaHoSo = 0;
            MaChucVu = 0;
            TenChucVu = string.Empty;
            MaPhong = 0;
            TenPhong = string.Empty;
            MaTrinhDo = 0;
            TenTrinhDo = string.Empty;
            MaDanToc = 0;
            TenDanToc = string.Empty;
            MaTonGiao = 0;
            TenTonGiao = string.Empty;
            MaChuyenMon = 0;
            TenChuyenMon = string.Empty;
            MaHopDong = 0;
            SoHopDong = string.Empty;
            AnhThe = string.Empty;
        }

        private void GetList_TrinhDo()
        {
            try
            {
                var result = new TrinhDoDAO().GetList_TrinhDo();

                ListTrinhDo = new ObservableCollection<TrinhDo>(result);
            }
            catch (Exception ex) { }
        }

        private void GetList_ChuyenMon()
        {
            try
            {
                var result = new ChuyenMonDAO().GetListChuyenMon();

                ListChuyenMon = new ObservableCollection<ChuyenMon>(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void GetList_PhongBan()
        {
            try
            {
                var result = new PhongBanDAO().GetList_PhongBan();

                ListPhongBan = new ObservableCollection<PhongBan>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_ChucVu()
        {
            try
            {
                var result = new ChucVuDAO().GetListChucVu();

                ListChucVu = new ObservableCollection<ChucVu>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_DanToc()
        {
            try
            {
                var result = new DanTocDAO().GetList_DanToc();

                ListDanToc = new ObservableCollection<DanToc>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_TonGiao()
        {
            try
            {
                var result = new TonGiaoDAO().GetList_TonGiao();

                ListTonGiao = new ObservableCollection<TonGiao>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
