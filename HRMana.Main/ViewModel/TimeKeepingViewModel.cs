using HRMana.Common.Commons;
using HRMana.Main.View.Dialog;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace HRMana.Main.ViewModel
{
    public class TimeKeepingViewModel : BaseViewModel
    {
        private int _thang;
        private int _nam;
        private int _maChamCong;
        private string _maNhanVien;
        private string _tenNhanVien;
        private decimal _heSoLuong;
        private string _luongCoBan;
        private int _soNgayCong;
        private string _ungTruoc;
        private string _conLai;
        private int _soNghiPhep;
        private int _soGioTangCa;
        private string _luongTangCa;
        private string _phuCapCongViec;
        private ObservableCollection<BacLuong> _DsBacLuong;
        private BacLuong _selectedBacLuong;
        private ObservableCollection<NhanVien> _dsNhanVien;
        private NhanVien _selectedNhanVien;
        private string _tnv_Search;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private List<int> _dsThang;
        private List<int> _dsNam;
        private string _tongLuong;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_ChamCongCommand { get; set; }
        public ICommand Update_ChamCongCommand { get; set; }
        public ICommand Delete_ChamCongCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }


        public ObservableCollection<NhanVien> DsNhanVien { get => _dsNhanVien; set { _dsNhanVien = value; OnPropertyChanged(); } }
        public NhanVien SelectedNhanVien
        {
            get => _selectedNhanVien;
            set
            {
                _selectedNhanVien = value;
                OnPropertyChanged();

                if (SelectedNhanVien != null)
                {
                    MaNhanVien = SelectedNhanVien.maNhanVien;
                    TenNhanVien = SelectedNhanVien.tenNhanVien;

                    Thang = DateTime.Now.Month;
                    Nam = DateTime.Now.Year;

                    //Get_ChamCong(MaNhanVien);
                }
            }
        }

        public int MaChamCong { get => _maChamCong; set { _maChamCong = value; OnPropertyChanged(); } }
        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string TenNhanVien { get => _tenNhanVien; set { _tenNhanVien = value; OnPropertyChanged(); } }
        public decimal HeSoLuong { get => _heSoLuong; set { _heSoLuong = value; OnPropertyChanged(); } }
        public string LuongCoBan
        {
            get => _luongCoBan; set
            {
                _luongCoBan = value; OnPropertyChanged();
                if (!string.IsNullOrEmpty(value))
                {
                    var tongLuong = Convert.ToInt32(TinhTongLuong(LuongCoBan, SoNgayCong, SoGioTangCa, LuongTangCa, PhuCapCongViec));

                    TongLuong = tongLuong.ToString();
                }
            }
        }
        public int SoNgayCong
        {
            get => _soNgayCong; set
            {
                _soNgayCong = value; OnPropertyChanged();
                if (value != 0)
                {
                    var tongLuong = Convert.ToInt32(TinhTongLuong(LuongCoBan, SoNgayCong, SoGioTangCa, LuongTangCa, PhuCapCongViec));

                    TongLuong = tongLuong.ToString();

                }
            }
        }
        public string UngTruoc
        {
            get => _ungTruoc;
            set
            {
                _ungTruoc = value;
                OnPropertyChanged();

                if (!string.IsNullOrEmpty(value))
                {
                    var tl = StringHelper.ConvertSalary(TongLuong);
                    var ut = StringHelper.ConvertSalary(value);
                    var cl = Convert.ToInt32(tl - ut);

                    ConLai = cl.ToString();

                }
            }
        }
        public string ConLai { get => _conLai; set { _conLai = value; OnPropertyChanged(); } }
        public int SoNghiPhep { get => _soNghiPhep; set { _soNghiPhep = value; OnPropertyChanged(); } }
        public int SoGioTangCa
        {
            get => _soGioTangCa;
            set
            {
                _soGioTangCa = value; OnPropertyChanged();
                if (value != 0)
                {
                    decimal lcb = StringHelper.ConvertSalary(LuongCoBan);
                    var ltc = Convert.ToInt32(TinhLuongTangCa(lcb, SoGioTangCa));

                    LuongTangCa = ltc.ToString();

                    var tongLuong = Convert.ToInt32(TinhTongLuong(LuongCoBan, SoNgayCong, SoGioTangCa, LuongTangCa, PhuCapCongViec));

                    TongLuong = tongLuong.ToString();
                }
            }
        }
        public string LuongTangCa
        {
            get => _luongTangCa;
            set
            {
                _luongTangCa = value; OnPropertyChanged();
                if (!string.IsNullOrEmpty(value))
                {
                    var tongLuong = Convert.ToInt32(TinhTongLuong(LuongCoBan, SoNgayCong, SoGioTangCa, LuongTangCa, PhuCapCongViec));

                    TongLuong = tongLuong.ToString();
                }
            }
        }
        public string PhuCapCongViec
        {
            get => _phuCapCongViec;
            set
            {
                _phuCapCongViec = value; OnPropertyChanged();
                if (!string.IsNullOrEmpty(value))
                {
                    var tongLuong = Convert.ToInt32(TinhTongLuong(LuongCoBan, SoNgayCong, SoGioTangCa, LuongTangCa, PhuCapCongViec));

                    TongLuong = tongLuong.ToString();
                }
            }
        }

        public ObservableCollection<BacLuong> DsBacLuong { get => _DsBacLuong; set { _DsBacLuong = value; OnPropertyChanged(); } }
        public BacLuong SelectedBacLuong
        {
            get => _selectedBacLuong;
            set
            {
                _selectedBacLuong = value;
                OnPropertyChanged();

                if (SelectedBacLuong != null)
                {
                    HeSoLuong = Convert.ToInt32(SelectedBacLuong.heSoLuong);
                    LuongCoBan = SelectedBacLuong.luongCoBan.ToString();

                }
            }
        }

        public string Tnv_Search
        {
            get => _tnv_Search;
            set
            {
                _tnv_Search = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(value))
                {
                    GetList_NhanVien();
                }
                else
                {
                    GetList_NhanVien(value);
                }
            }
        }

        public int Thang
        {
            get => _thang; set
            {
                _thang = value;
                OnPropertyChanged();

                if (SelectedNhanVien != null)
                {
                    Get_ChamCong(MaNhanVien, _thang, _nam);
                }
            }
        }
        public int Nam
        {
            get => _nam;
            set
            {
                _nam = value;
                OnPropertyChanged();

                if (SelectedNhanVien != null)
                {
                    Get_ChamCong(MaNhanVien, _thang, _nam);
                }
            }
        }

        public List<int> DsThang { get => _dsThang; set => _dsThang = value; }
        public List<int> DsNam { get => _dsNam; set => _dsNam = value; }
        public string TongLuong
        {
            get => _tongLuong;
            set
            {
                _tongLuong = value;
                OnPropertyChanged();
            }
        }

        public TimeKeepingViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            DsThang = new List<int>()
            {
                1, 2, 3, 4, 5,6 ,7 , 8 , 9 , 10 , 11 , 12
            };
            DsNam = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = 1900; year <= currentYear; year++)
            {
                DsNam.Add(year);
            }
            DsNam = DsNam.OrderByDescending(x => x).ToList();

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_NhanVien();
                            GetList_BacLuong();

                            // Xét quyền của tài khoản
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
                        }));
                    });
                    GetData_Thread.IsBackground = true;
                    GetData_Thread.Start();
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    EmptyField();
                }
                );

            Create_ChamCongCommand = new RelayCommand<object>(
                (param) =>
                {

                    if (MaChamCong != 0)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    Create_ChamCong();
                }
                );

            Update_ChamCongCommand = new RelayCommand<object>(
                (param) =>
                {

                    if (SelectedNhanVien == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    Update_ChamCong();
                }
                );

            Delete_ChamCongCommand = new RelayCommand<object>(
                (param) =>
                {

                    if (SelectedNhanVien == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    Delete_ChamCong();
                }
                );
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

        private void Delete_ChamCong()
        {
            DialogWindow d = new DialogWindow();
            d.DialogMessage = "Bạn có chắc muốn xóa?";

            if (true == d.ShowDialog())
            {
                try
                {
                    var result = new ChamCongDAO().Delete_ChamCong(MaChamCong);

                    if (result)
                    {
                        ShowMessageBoxCustom("Xóa chấm công thành công!", CommonConstant.Success_ICon);
                        EmptyField();
                    }
                    else
                    {
                        ShowMessageBoxCustom("Đã xảy ra lỗi!", CommonConstant.Error_ICon);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void Update_ChamCong()
        {
            DialogWindow d = new DialogWindow();
            d.DialogMessage = "Bạn có chắc muốn cập nhật?";

            if (true == d.ShowDialog())
            {
                try
                {
                    var cc = new ChamCong()
                    {
                        maChamCong = MaChamCong,
                        maNhanVien = MaNhanVien,
                        heSoLuong = SelectedBacLuong.heSoLuong,
                        SoNgayCong = SoNgayCong,
                        soGioTangCa = SoGioTangCa,
                        ungTruocLuong = StringHelper.ConvertSalary(UngTruoc),
                        tongNhan = StringHelper.ConvertSalary(TongLuong),
                        conLai = StringHelper.ConvertSalary(ConLai),
                        nghiPhep = SoNghiPhep,
                        luongTangCa = StringHelper.ConvertSalary(LuongTangCa),
                        phuCapCongViec = StringHelper.ConvertSalary(PhuCapCongViec),
                    };

                    var result = new ChamCongDAO().Update_ChamCong(cc);

                    if (result)
                    {
                        ShowMessageBoxCustom("Cập nhật chấm công thành công!", CommonConstant.Success_ICon);
                        EmptyField();
                    }
                    else
                    {
                        ShowMessageBoxCustom("Đã xảy ra lỗi!", CommonConstant.Error_ICon);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void Create_ChamCong()
        {
            try
            {
                if (SelectedNhanVien == null)
                {
                    ShowMessageBoxCustom("Chưa chọn nhân viên, mời chọn một nhân viên!", CommonConstant.Warning_ICon);
                    return;
                }
                else
                {
                    var cc = new ChamCong()
                    {
                        thang = Thang,
                        nam = Nam,
                        maNhanVien = MaNhanVien,
                        heSoLuong = SelectedBacLuong.heSoLuong,
                        SoNgayCong = SoNgayCong,
                        soGioTangCa = SoGioTangCa,
                        ungTruocLuong = StringHelper.ConvertSalary(UngTruoc),
                        tongNhan = StringHelper.ConvertSalary(TongLuong),
                        conLai = StringHelper.ConvertSalary(ConLai),
                        nghiPhep = SoNghiPhep,
                        luongTangCa = StringHelper.ConvertSalary(LuongTangCa),
                        phuCapCongViec = StringHelper.ConvertSalary(PhuCapCongViec),
                    };

                    var result = new ChamCongDAO().Create_ChamCong(cc);

                    if (result == 0)
                    {
                        ShowMessageBoxCustom("Chấm công đang trống!", CommonConstant.Error_ICon);

                    }
                    else if (result < 0)
                    {
                        ShowMessageBoxCustom("Đã xảy ra lỗi!", CommonConstant.Error_ICon);

                    }
                    else
                    {
                        ShowMessageBoxCustom($"Thêm mới chấm công cho nhân viên {TenNhanVien} thành công!", CommonConstant.Success_ICon);
                        EmptyField();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Get_ChamCong(string mnv, int thang, int nam)
        {
            try
            {
                var result = new ChamCongDAO().Get_ChamCong_By_MaNhanVien(mnv, thang, nam);

                if (result.maChamCong != 0)
                {
                    _thang = result.thang;
                    _nam = result.nam;
                    MaNhanVien = result.maNhanVien;
                    MaChamCong = result.maChamCong;
                    TenNhanVien = result.NhanVien.tenNhanVien;
                    SelectedBacLuong = DsBacLuong.SingleOrDefault(x => x.heSoLuong == result.heSoLuong);
                    HeSoLuong = result.heSoLuong;
                    LuongCoBan = result.BacLuong.luongCoBan.ToString();
                    SoNgayCong = Convert.ToInt32(result.SoNgayCong);
                    SoGioTangCa = (int)result.soGioTangCa;
                    UngTruoc = result.ungTruocLuong.ToString();
                    TongLuong = result.tongNhan.ToString();
                    ConLai = result.conLai.ToString();
                    SoNghiPhep = (int)result.nghiPhep;
                    //SoGioTangCa = (int)result.soGioTangCa;
                    PhuCapCongViec = result.phuCapCongViec.ToString();
                }
                else
                {
                    EmptyField_ChamCong();
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void GetList_BacLuong()
        {
            try
            {
                var result = new BacLuongDAO().GetList_Luong();

                DsBacLuong = new ObservableCollection<BacLuong>(result);
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmptyField()
        {
            SelectedBacLuong = null;
            SelectedNhanVien = null;
            Thang = DateTime.Now.Month;
            Nam = DateTime.Now.Month;
            MaNhanVien = string.Empty;
            MaChamCong = 0;
            TenNhanVien = string.Empty;
            HeSoLuong = 0;
            LuongCoBan = string.Empty;
            SoNgayCong = 0;
            SoGioTangCa = 0;
            UngTruoc = string.Empty;
            TongLuong = string.Empty;
            ConLai = string.Empty;
            SoNghiPhep = 0;
            SoGioTangCa = 0;
            PhuCapCongViec = string.Empty;
            LuongTangCa = string.Empty;
        }

        private void EmptyField_ChamCong()
        {
            SelectedBacLuong = null;
            MaChamCong = 0;
            HeSoLuong = 0;
            LuongCoBan = string.Empty;
            SoNgayCong = 0;
            SoGioTangCa = 0;
            UngTruoc = string.Empty;
            ConLai = string.Empty;
            SoNghiPhep = 0;
            SoGioTangCa = 0;
            PhuCapCongViec = string.Empty;
            LuongTangCa = string.Empty;
        }

        private void EmptyField_MNV()
        {
            SelectedBacLuong = null;
            MaChamCong = 0;
            HeSoLuong = 0;
            LuongCoBan = string.Empty;
            SoNgayCong = 0;
            SoGioTangCa = 0;
            UngTruoc = string.Empty;
            ConLai = string.Empty;
            SoNghiPhep = 0;
            SoGioTangCa = 0;
            PhuCapCongViec = string.Empty;
            LuongTangCa = string.Empty;
        }

        private void GetList_NhanVien()
        {
            try
            {
                var result = new NhanVienDAO().GetList_NhanVien();

                DsNhanVien = new ObservableCollection<NhanVien>(result.OrderBy(x => x.tenNhanVien));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra, {ex.Message}");
            }
        }

        private void GetList_NhanVien(string tnv)
        {
            try
            {
                var result = new NhanVienDAO().GetList_NhanVien(tnv);

                DsNhanVien = new ObservableCollection<NhanVien>(result.OrderBy(x => x.tenNhanVien));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra, {ex.Message}");
            }
        }

        private decimal TinhTongLuong(string luongCoBan, int soNgayCong, int soGioTangCa, string luongTangCa, string phuCapCongViec)
        {
            decimal tongLuong = 0;

            decimal lcb = 0;
            decimal ltc = 0;
            decimal pccv = 0;
            decimal utl = 0;

            if (!string.IsNullOrEmpty(luongCoBan))
            {
                lcb = StringHelper.ConvertSalary(luongCoBan);
            }

            if (!string.IsNullOrEmpty(luongTangCa))
            {
                ltc = StringHelper.ConvertSalary(luongTangCa);
            }

            if (!string.IsNullOrEmpty(phuCapCongViec))
            {
                pccv = StringHelper.ConvertSalary(phuCapCongViec);
            }

            tongLuong = (lcb + pccv) / 26 * soNgayCong + TinhLuongTangCa(lcb, soGioTangCa);

            return tongLuong;
        }

        private decimal TinhLuongTangCa(decimal luongCoBan, int soGioTangCa)
        {
            return (luongCoBan / 26 / 8) * soGioTangCa;
        }

    }
}
