using HRMana.Main.View.Dialog;
using HRMana.Main.View.Personnel;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using HRMana.Common.Commons;
using System.ComponentModel;
using Microsoft.Win32;
using HRMana.Common;

namespace HRMana.Main.ViewModel
{
    public class PersonnelDetailsViewModel : BaseViewModel, IDataErrorInfo
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

        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;

        public ICommand Update_NhanVienCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand Delete_NhanVienCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand ChooseImageCommand { get; set; }

        public string MaNhanVien
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
            }
        }
        public string NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
        public string Cccd { get => _cccd; set { _cccd = value; OnPropertyChanged(); } }
        public string DienThoai { get => _dienThoai; set { _dienThoai = value; OnPropertyChanged(); } }
        public string NoiOHienTai { get => _noiOHienTai; set { _noiOHienTai = value; OnPropertyChanged(); } }
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

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                var res = "";

                switch (columnName)
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
                        if (NgaySinh == null)
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


        public PersonnelDetailsViewModel(string maNhanVien)
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
                async (param) =>
                {
                    Task loading_Task = new Task(() =>
                    {
                        var permissions = new Dictionary<string, string>
                            {
                                { "VIEW", CommonConstant.Visibility_Visible },
                                { "ADD", CommonConstant.Visibility_Collapsed },
                                { "EDIT", CommonConstant.Visibility_Collapsed },
                                { "DEL", CommonConstant.Visibility_Collapsed },
                            };
                        var checkPermission = CommonConstant.DsQuyenCuaTKDN;
                        foreach (var i in checkPermission)
                        {
                            if (permissions.ContainsKey(i.Chitiet_Quyen.mahanhDong))
                            {
                                permissions[i.Chitiet_Quyen.mahanhDong] = CommonConstant.Visibility_Visible;
                            }
                        }

                        // Gán giá trị từ dictionary vào các biến
                        Permission_ADD = permissions["ADD"];
                        Permission_EDIT = permissions["EDIT"];
                        Permission_DEL = permissions["DEL"];
                        Permission_VIEW = permissions["VIEW"];

                        GetList_BacLuong();
                        GetList_ChucVu();
                        GetList_ChuyenMon();
                        GetList_DanToc();
                        GetList_PhongBan();
                        GetList_TonGiao();
                        GetList_TrinhDo();
                        Get_NhanVien_ByMaNhanVien();
                    });

                    loading_Task.Start();
                    await loading_Task;
                }
                );

            ExitCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    CloseDetailWindow();
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
                        var result = new NhanVienDAO().Delete_NhanVien(MaNhanVien);
                        if (result)
                        {
                            ShowMessageBoxCustom("Xóa nhân viên thành công.", CommonConstant.Success_ICon);
                            CloseDetailWindow();
                        }
                        else
                        {
                            ShowMessageBoxCustom("Xóa nhân viên thất bại.", CommonConstant.Error_ICon);
                        }
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
                        GioiTinh = Nam_Checked ? "Nam" : "Nữ";
                        GioiTinh = Nu_Checked ? "Nữ" : "Nam";

                        NhanVien nv = new NhanVien()
                        {
                            maNhanVien = MaNhanVien,
                            tenNhanVien = HoTen,
                            gioiTinh = GioiTinh,
                            ngaySinh = Convert.ToDateTime(NgaySinh),
                            CCCD = Cccd,
                            dienThoai = DienThoai,
                            noiOHienTai = NoiOHienTai,
                            queQuan = QueQuan,
                            anhThe = Path.GetFileName(AnhThe),
                            maHoSo = MaHoSo,
                            maHopDong = MaHopDong,
                            maChucVu = MaChucVu,
                            maPhong = MaPhong,
                            maTrinhDo = MaTrinhDo,
                            maDanToc = MaDanToc,
                            maTonGiao = MaTonGiao,
                            maChuyenMon = MaChuyenMon,
                        };

                        var result = new NhanVienDAO().Update_NhanVien(nv);

                        if (result)
                        {
                            ShowMessageBoxCustom("Cập nhật nhân viên thành công.", CommonConstant.Success_ICon);
                            CloseDetailWindow();
                        }
                        else
                        {
                            ShowMessageBoxCustom("Cập nhật nhân viên thất bại.", CommonConstant.Error_ICon);
                        }
                    }
                }
                );

            ChooseImageCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    AnhThe = FeatureHelper.ChooseImage();                    
                }
                );
        }

        private void CloseDetailWindow()
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (activeWindow != null)
            {
                activeWindow.Close();
            }
        }

        private void Get_NhanVien_ByMaNhanVien()
        {
            if ( !string.IsNullOrEmpty(MaNhanVien))
            {
                var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);

                if (nv != null)
                {
                    HoTen = nv.tenNhanVien;
                    Nam_Checked = (nv.gioiTinh == "Nam" ? true : false);
                    Nu_Checked = (nv.gioiTinh == "Nữ" ? true : false);
                    NgaySinh = nv.ngaySinh.ToString("dd/MM/yyyy");
                    Cccd = nv.CCCD;
                    DienThoai = nv.dienThoai;
                    NoiOHienTai = nv.noiOHienTai;
                    QueQuan = nv.queQuan;
                    MaChucVu = nv.maChucVu;
                    SelectedChucVu = ListChucVu.Where(x => x.maChucVu == nv.maChucVu).SingleOrDefault();
                    MaPhong = nv.maPhong;
                    SelectedPhongBan = ListPhongBan.SingleOrDefault(x => x.maPhong == nv.maPhong);
                    MaTrinhDo = nv.maTrinhDo;
                    SelectedTrinhDo = ListTrinhDo.SingleOrDefault(x => x.maTrinhDo == nv.maTrinhDo);
                    MaDanToc = nv.maDanToc;
                    SelectedDanToc = ListDanToc.SingleOrDefault(x => x.maDanToc == nv.maDanToc);
                    MaTonGiao = nv.maTonGiao;
                    SelectedTonGiao = ListTonGiao.SingleOrDefault(x => x.maTonGiao == nv.maTonGiao);
                    MaChuyenMon = nv.maChuyenMon;
                    SelectedChuyenMon = ListChuyenMon.SingleOrDefault(x => x.maChuyenMon == nv.maChuyenMon);
                    MaHopDong = (int)nv.maHopDong;
                    //SoHopDong = nv.HopDong.soHopDong;
                    MaHoSo = (int)nv.maHoSo;
                    AnhThe = (nv.anhThe == null) ? "..\\..\\Assets\\NhanVien_Image\\DefaultAvatar.jpg" :
                        AppDomain.CurrentDomain.BaseDirectory + "NhanVien_Image\\" + nv.anhThe;
                }
            }
            else
            {
                ShowMessageBoxCustom("Lấy thông tin nhân viên lỗi", CommonConstant.Error_ICon);
            }
        }

        private void ShowMessageBoxCustom(string msg, string imagePath)
        {
            MessageBox_Custom messageBox_Custom = new MessageBox_Custom();
            messageBox_Custom.MsgBox_Content = msg;

            // Chuyển đổi đường dẫn hình ảnh từ kiểu string sang ImageSource
            ImageSource msgIcon = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            messageBox_Custom.Img_MsgIcon = msgIcon;

            messageBox_Custom.ShowDialog();
        }

        private void GetList_BacLuong()
        {
            try
            {
                var result = new BacLuongDAO().GetList_Luong();

                //DsBacLuong = new ObservableCollection<BacLuong>(result);
            }
            catch (Exception ex) { }
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
            catch (Exception ex) { }
        }

        private void GetList_PhongBan()
        {
            try
            {
                var result = new PhongBanDAO().GetList_PhongBan();

                ListPhongBan = new ObservableCollection<PhongBan>(result);
            }
            catch (Exception ex) { }
        }

        private void GetList_ChucVu()
        {
            try
            {
                var result = new ChucVuDAO().GetListChucVu();

                ListChucVu = new ObservableCollection<ChucVu>(result);
            }
            catch (Exception ex) { }
        }

        private void GetList_DanToc()
        {
            try
            {
                var result = new DanTocDAO().GetList_DanToc();

                ListDanToc = new ObservableCollection<DanToc>(result);
            }
            catch (Exception ex) { }
        }

        private void GetList_TonGiao()
        {
            try
            {
                var result = new TonGiaoDAO().GetList_TonGiao();

                ListTonGiao = new ObservableCollection<TonGiao>(result);
            }
            catch (Exception ex) { }
        }
    }
}
