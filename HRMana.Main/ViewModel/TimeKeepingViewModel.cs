using HRMana.Common.Commons;
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

namespace HRMana.Main.ViewModel
{
    public class TimeKeepingViewModel : BaseViewModel
    {
        private int _maChamCong;
        private int _maNhanVien;
        private string _tenNhanVien;
        private decimal _heSoLuong;
        private string _luongCoBan;
        private int _soNgayCong;
        private string _ungTruoc;
        private string _conLai;
        private int _soNghiPhep;
        private int _soNgayTangCa;
        private string _luongTangCa;
        private string _phuCapCongViec;
        private ObservableCollection<BacLuong> _DsBacLuong;
        private BacLuong _selectedBacLuong;
        private ObservableCollection<NhanVien> _dsNhanVien;
        private NhanVien _selectedNhanVien;
        private string _tnv_Search;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_ChamCongCommand { get; set; }
        public ICommand Update_ChamCongCommand { get; set; }
        public ICommand Delete_ChamCongCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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

                    Get_ChamCong(MaNhanVien);
                }
            }
        }

        public int MaChamCong { get => _maChamCong; set { _maChamCong = value; OnPropertyChanged(); } }
        public int MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string TenNhanVien { get => _tenNhanVien; set { _tenNhanVien = value; OnPropertyChanged(); } }
        public decimal HeSoLuong { get => _heSoLuong; set { _heSoLuong = value; OnPropertyChanged(); } }
        public string LuongCoBan { get => _luongCoBan; set { _luongCoBan = value; OnPropertyChanged(); } }
        public int SoNgayCong { get => _soNgayCong; set { _soNgayCong = value; OnPropertyChanged(); } }
        public string UngTruoc { get => _ungTruoc; set { _ungTruoc = value; OnPropertyChanged(); } }
        public string ConLai { get => _conLai; set { _conLai = value; OnPropertyChanged(); } }
        public int SoNghiPhep { get => _soNghiPhep; set { _soNghiPhep = value; OnPropertyChanged(); } }
        public int SoNgayTangCa { get => _soNgayTangCa; set { _soNgayTangCa = value; OnPropertyChanged(); } }
        public string LuongTangCa { get => _luongTangCa; set { _luongTangCa = value; OnPropertyChanged(); } }
        public string PhuCapCongViec { get => _phuCapCongViec; set { _phuCapCongViec = value; OnPropertyChanged(); } }

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

        public TimeKeepingViewModel()
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
                            GetList_BacLuong();
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

        private void Delete_ChamCong()
        {
            try
            {
                var result = new ChamCongDAO().Delete_ChamCong(MaChamCong);

                if (result)
                {
                    MessageBox.Show("Xóa chấm công thành công!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Information);
                    EmptyField();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Update_ChamCong()
        {
            try
            {
                var cc = new ChamCong()
                {
                    maChamCong = MaChamCong,
                    maNhanVien = MaNhanVien,
                    heSoLuong = SelectedBacLuong.heSoLuong,
                    SoNgayCong = SoNgayCong,
                    soNgayTangCa = SoNgayTangCa,
                    ungTruocLuong = StringHelper.ConvertSalary(UngTruoc),
                    conLai = StringHelper.ConvertSalary(ConLai),
                    nghiPhep = SoNghiPhep,
                    luongTangCa = StringHelper.ConvertSalary(LuongTangCa),
                    phuCapCongViec = StringHelper.ConvertSalary(PhuCapCongViec),
                };

                var result = new ChamCongDAO().Update_ChamCong(cc);

                if (result)
                {
                    MessageBox.Show("Cập nhật chấm công thành công!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Information);
                    EmptyField();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Create_ChamCong()
        {
            try
            {
                if (SelectedNhanVien == null)
                {
                    MessageBox.Show("Chưa chọn nhân viên, mời chọn một nhân viên!", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    var cc = new ChamCong()
                    {
                        maNhanVien = MaNhanVien,
                        heSoLuong = SelectedBacLuong.heSoLuong,
                        SoNgayCong = SoNgayCong,
                        soNgayTangCa = SoNgayTangCa,
                        ungTruocLuong = StringHelper.ConvertSalary(UngTruoc),
                        conLai = StringHelper.ConvertSalary(ConLai),
                        nghiPhep = SoNghiPhep,
                        luongTangCa = StringHelper.ConvertSalary(LuongTangCa),
                        phuCapCongViec = StringHelper.ConvertSalary(PhuCapCongViec),
                    };

                    var result = new ChamCongDAO().Create_ChamCong(cc);

                    if (result == 0)
                    {
                        MessageBox.Show("Chấm công đang trống!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else if (result < 0)
                    {
                        MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        MessageBox.Show($"Thêm mới chấm công cho nhân viên {TenNhanVien} thành công!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Information);
                        EmptyField();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Get_ChamCong(int mnv)
        {
            try
            {
                var result = new ChamCongDAO().Get_ChamCong_By_MaNhanVien(mnv);

                if (result.maChamCong != 0)
                {
                    MaNhanVien = result.maNhanVien;
                    MaChamCong = result.maChamCong;
                    TenNhanVien = result.NhanVien.tenNhanVien;
                    SelectedBacLuong = DsBacLuong.SingleOrDefault(x => x.heSoLuong == result.heSoLuong);
                    HeSoLuong = result.heSoLuong;
                    LuongCoBan = result.BacLuong.luongCoBan.ToString();
                    SoNgayCong = Convert.ToInt32(result.SoNgayCong);
                    SoNgayTangCa = (int)result.soNgayTangCa;
                    UngTruoc = result.ungTruocLuong.ToString();
                    ConLai = result.conLai.ToString();
                    SoNghiPhep = (int)result.nghiPhep;
                    SoNgayTangCa = (int)result.soNgayTangCa;
                    PhuCapCongViec = result.phuCapCongViec.ToString();
                }
                else
                {
                    MessageBox.Show($"Nhân viên {TenNhanVien} không có chấm công!", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    EmptyField_MNV();
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
            MaNhanVien = 0;
            MaChamCong = 0;
            TenNhanVien = string.Empty;
            HeSoLuong = 0;
            LuongCoBan = string.Empty;
            SoNgayCong = 0;
            SoNgayTangCa = 0;
            UngTruoc = string.Empty;
            ConLai = string.Empty;
            SoNghiPhep = 0;
            SoNgayTangCa = 0;
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
            SoNgayTangCa = 0;
            UngTruoc = string.Empty;
            ConLai = string.Empty;
            SoNghiPhep = 0;
            SoNgayTangCa = 0;
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

    }
}
