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
    public class ProfileEmployeeViewModel : BaseViewModel
    {
        #region Khai báo biến
        private string _maNhanVien;
        private string _hoTen;
        private ObservableCollection<NhanVien> _dsNhanVien;
        private NhanVien _selectedNhanVien;
        private int _maChucVu;
        private string _tenChucVu;
        private int _maPhong;
        private string _tenPhong;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private string _TNV_Search;
        private int _maTrinhDo;
        private string _tenTrinhDo;
        private int _maChuyenMon;
        private string _tenChuyenMon;
        private int _maHoSo;
        private string _soYeuLyLich;
        private string _giayKhaiSinh;
        private string _soHoKhau;
        private string _bangTotNghiep;
        private string _giayKhamSK;
        private string _anhThe;
        private string _tinhTrangHoSo;
        private string _hinhThucThanhToanLuong;
        private string _soTkNganHang;
        private string _nganHang;
        private string _maSoThue;
        private string _maSoBHXH;
        private decimal _heSoLuong;
        private decimal _luongCoBan;

        private string _soHopDong;
        private int _maHopDong;
        private string _loaiHopDong;
        private string _thoiHanHD;
        private string _NgayBDHD;


        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_HoSoCommand { get; set; }
        public ICommand Update_HoSoCommand { get; set; }
        public ICommand Delete_HoSoCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }


        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        public int MaPhong { get => _maPhong; set { _maPhong = value; OnPropertyChanged(); } }
        public string TenPhong { get => _tenPhong; set { _tenPhong = value; OnPropertyChanged(); } }

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
                    HoTen = SelectedNhanVien.tenNhanVien;
                    TenChucVu = SelectedNhanVien.ChucVu.tenChucVu;
                    TenPhong = SelectedNhanVien.PhongBan.tenPhong;
                    TenTrinhDo = SelectedNhanVien.TrinhDo.tenTrinhDo;
                    TenChuyenMon = SelectedNhanVien.ChuyenMon.tenChuyenMon;
                    
                    var hd = new HopDongDAO().Get_HopDong_By_MaHopDong(SelectedNhanVien.maHopDong.Value);
                    if (hd != null)
                    {
                        MaHopDong = hd.maHopDong;
                        SoHopDong = hd.soHopDong;
                        LoaiHopDong = hd.loaiHopDong;
                        NgayBDHD = hd.ngayKyHD.ToString();
                        ThoiHanHD = hd.thoiHanHD;
                    }
                    else
                    {
                        EmptyHopDong();
                    }

                    var hs = new HoSoDAO().Get_HoSo_By_MaHoSo(SelectedNhanVien.maHoSo.Value);
                    if (hs != null)
                    {
                        MaHoSo = hs.maHoSo;
                        SoYeuLyLich = hs.soYeuLyLich;
                        GiayKhaiSinh = hs.giayKhaiSinh;
                        SoHoKhau = hs.soHoKhau;
                        BangTotNghiep = hs.bangTotNghiep;
                        GiayKhamSK = hs.giayKhamSK;
                        AnhThe = hs.anhThe;
                        TinhTrangHoSo = hs.tinhTrangHoSo;
                        HinhThucThanhToanLuong = hs.hinhThucThanhToanLuong;
                        SoTkNganHang = hs.soTkNganHang;
                        NganHang = hs.nganHang;
                        MaSoThue = hs.maSoThue;
                        MaSoBHXH = hs.maSoBHXH;
                    }
                    else
                    {
                        EmptyHoSo();
                    }

                }
            }
        }
        public string TNV_Search
        {
            get => _TNV_Search;
            set
            {
                _TNV_Search = value;
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

        public int MaTrinhDo { get => _maTrinhDo; set { _maTrinhDo = value; OnPropertyChanged(); } }
        public string TenTrinhDo { get => _tenTrinhDo; set { _tenTrinhDo = value; OnPropertyChanged(); } }
        public int MaChuyenMon { get => _maChuyenMon; set { _maChuyenMon = value; OnPropertyChanged(); } }
        public string TenChuyenMon { get => _tenChuyenMon; set { _tenChuyenMon = value; OnPropertyChanged(); } }
        public int MaHoSo { get => _maHoSo; set { _maHoSo = value; OnPropertyChanged(); } }
        public string SoYeuLyLich { get => _soYeuLyLich; set { _soYeuLyLich = value; OnPropertyChanged(); } }
        public string GiayKhaiSinh { get => _giayKhaiSinh; set { _giayKhaiSinh = value; OnPropertyChanged(); } }
        public string SoHoKhau { get => _soHoKhau; set { _soHoKhau = value; OnPropertyChanged(); } }
        public string BangTotNghiep { get => _bangTotNghiep; set { _bangTotNghiep = value; OnPropertyChanged(); } }
        public string GiayKhamSK { get => _giayKhamSK; set { _giayKhamSK = value; OnPropertyChanged(); } }
        public string AnhThe { get => _anhThe; set { _anhThe = value; OnPropertyChanged(); } }
        public string TinhTrangHoSo
        {
            get => _tinhTrangHoSo;
            set
            {
                _tinhTrangHoSo = value;
                OnPropertyChanged();

            }
        }
        public string HinhThucThanhToanLuong { get => _hinhThucThanhToanLuong; set { _hinhThucThanhToanLuong = value; OnPropertyChanged(); } }
        public string SoTkNganHang { get => _soTkNganHang; set { _soTkNganHang = value; OnPropertyChanged(); } }
        public string NganHang { get => _nganHang; set { _nganHang = value; OnPropertyChanged(); } }
        public string MaSoThue { get => _maSoThue; set { _maSoThue = value; OnPropertyChanged(); } }
        public string MaSoBHXH { get => _maSoBHXH; set { _maSoBHXH = value; OnPropertyChanged(); } }
        public decimal HeSoLuong { get => _heSoLuong; set { _heSoLuong = value; OnPropertyChanged(); } }
        public decimal LuongCoBan { get => _luongCoBan; set { _luongCoBan = value; OnPropertyChanged(); } }
        public string SoHopDong { get => _soHopDong; set { _soHopDong = value; OnPropertyChanged(); } }
        public int MaHopDong { get => _maHopDong; set { _maHopDong = value; OnPropertyChanged(); } }
        public string LoaiHopDong { get => _loaiHopDong; set { _loaiHopDong = value; OnPropertyChanged(); } }
        public string ThoiHanHD { get => _thoiHanHD; set { _thoiHanHD = value; OnPropertyChanged(); } }
        public string NgayBDHD { get => _NgayBDHD; set { _NgayBDHD = value; OnPropertyChanged(); } }

        #endregion

        public ProfileEmployeeViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_NhanVien();

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

            Create_HoSoCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (SelectedNhanVien != null)
                        {
                            var hso = new HoSo()
                            {
                                soYeuLyLich = SoYeuLyLich.Trim(),
                                giayKhaiSinh = GiayKhaiSinh.Trim(),
                                soHoKhau = SoHoKhau.Trim(),
                                bangTotNghiep = BangTotNghiep.Trim(),
                                giayKhamSK = GiayKhamSK.Trim(),
                                anhThe = AnhThe.Trim(),
                                tinhTrangHoSo = TinhTrangHoSo.Trim(),
                                hinhThucThanhToanLuong = HinhThucThanhToanLuong.Trim(),
                                soTkNganHang = SoTkNganHang.Trim(),
                                nganHang = NganHang.Trim(),
                                maSoThue = MaSoThue.Trim(),
                                maSoBHXH = MaSoBHXH.Trim(),
                            };

                            var result = new HoSoDAO().CreateNew_HoSo(hso);

                            if (result < 0)
                                ShowMessageBoxCustom("Thêm mới thất bại, Có lỗi từ máy chủ!", CommonConstant.Error_ICon);
                            else if (result == 0)
                                ShowMessageBoxCustom("Thêm mới thất bại, Dữ liệu trống!", CommonConstant.Warning_ICon);
                            else
                            {
                                ShowMessageBoxCustom("Thêm mới thành công!", CommonConstant.Success_ICon);

                                var nv = new NhanVien()
                                {
                                    maNhanVien = MaNhanVien,
                                    maHoSo = result,
                                };

                                var update_mhs_nv = new NhanVienDAO().Update_MaHoSo(nv);

                                EmptyField();
                            }
                        }
                        else
                        {
                            ShowMessageBoxCustom("Nhân viên không tồn tại, hãy thêm mới nhân viên trước!", CommonConstant.Error_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            Update_HoSoCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedNhanVien == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn cập nhật hồ sơ?";

                        if (true == d.ShowDialog())
                        {
                            var hso = new HoSo()
                            {
                                soYeuLyLich = SoYeuLyLich.Trim(),
                                giayKhaiSinh = GiayKhaiSinh.Trim(),
                                soHoKhau = SoHoKhau.Trim(),
                                bangTotNghiep = BangTotNghiep.Trim(),
                                giayKhamSK = GiayKhamSK.Trim(),
                                anhThe = AnhThe.Trim(),
                                tinhTrangHoSo = TinhTrangHoSo.Trim(),
                                hinhThucThanhToanLuong = HinhThucThanhToanLuong.Trim(),
                                soTkNganHang = SoTkNganHang.Trim(),
                                nganHang = NganHang.Trim(),
                                maSoThue = MaSoThue.Trim(),
                                maSoBHXH = MaSoBHXH.Trim(),
                            };

                            var result = new HoSoDAO().Update_HoSo(hso);

                            if (result)
                                ShowMessageBoxCustom("Cập nhật hồ sơ thất bại!", CommonConstant.Error_ICon);
                            else
                            {
                                ShowMessageBoxCustom("Cập nhật hồ sơ thành công!", CommonConstant.Success_ICon);
                                EmptyField();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            Delete_HoSoCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedNhanVien == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn xóa hồ sơ?";

                        if (true == d.ShowDialog())
                        {

                            var result = new HoSoDAO().Delete_HoSo(MaHoSo);

                            if (result)
                                ShowMessageBoxCustom("Xóa hồ sơ thất bại!", CommonConstant.Error_ICon);
                            else
                            {
                                ShowMessageBoxCustom("Xóa hồ sơ thành công!", CommonConstant.Success_ICon);
                                EmptyField();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    EmptyField();
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

        private void EmptyField()
        {
            SelectedNhanVien = null;
            MaNhanVien = string.Empty;
            HoTen = string.Empty;
            MaChucVu = 0;
            MaPhong = 0;
            TenChucVu = string.Empty;
            TenPhong = string.Empty;
            MaTrinhDo = 0;
            TenTrinhDo = string.Empty;
            MaChuyenMon = 0;
            TenChuyenMon = string.Empty;
            MaHoSo = 0;
            SoYeuLyLich = GiayKhaiSinh = SoHoKhau = BangTotNghiep =
                GiayKhamSK = AnhThe = TinhTrangHoSo = HinhThucThanhToanLuong =
                SoTkNganHang = NganHang = MaSoThue = MaSoBHXH = string.Empty;
            HeSoLuong = LuongCoBan = MaHopDong = 0;
            SoHopDong = LoaiHopDong = ThoiHanHD = NgayBDHD = string.Empty;
        }

        private void EmptyHoSo()
        {
            
            MaHoSo = 0;
            SoYeuLyLich = GiayKhaiSinh = SoHoKhau = BangTotNghiep =
                GiayKhamSK = AnhThe = TinhTrangHoSo = HinhThucThanhToanLuong =
                SoTkNganHang = NganHang = MaSoThue = MaSoBHXH = string.Empty;
            HeSoLuong = LuongCoBan = MaHopDong = 0;
        }

        private void EmptyHopDong()
        {
            SoHopDong = LoaiHopDong = ThoiHanHD = NgayBDHD = string.Empty;
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
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
