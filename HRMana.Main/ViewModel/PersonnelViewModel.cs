using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Main.View.Personnel;
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
using System.Windows.Threading;

namespace HRMana.Main.ViewModel
{
    public class PersonnelViewModel : BaseViewModel
    {
        #region Khai báo biến
        private string _maNhanVien;
        private string _hoTen;
        private string _gioiTinh;
        private DateTime _ngaySinh;
        private string _cccd;
        private int _maChucVu;
        private string _tenChucVu;
        private int _maPhong;
        private string _tenPhong;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;
        private string _message;
        private string _fill;
        private int _maTrinhDo;
        private string _tentrinhDo;
        private double _bacLuong;
        private ObservableCollection<PersonnelViewModel> _dsNhanVien;
        private PersonnelViewModel _selectedNhanVien;
        private ObservableCollection<ChucVu> _dsChucVu;
        private ObservableCollection<PhongBan> _dsPhongBan;
        private ObservableCollection<TrinhDo> _dsTrinhDo;
        private ObservableCollection<BacLuong> _dsBacLuong;
        private ChucVu _selectedChucVu;
        private PhongBan _selectedPhongBan;
        private TrinhDo _selectedTrinhDo;
        private BacLuong _selectedBacLuong;
        private string _permission_ADD;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand ShowPersonnelDetailsCommand { get; set; }

        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public string GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }
        public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        public int MaPhong { get => _maPhong; set { _maPhong = value; OnPropertyChanged(); } }
        public string TenPhong { get => _tenPhong; set { _tenPhong = value; OnPropertyChanged(); } }
        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }
        public ObservableCollection<PersonnelViewModel> DsNhanVien { get => _dsNhanVien; set { _dsNhanVien = value; OnPropertyChanged(); } }
        public PersonnelViewModel SelectedNhanVien
        {
            get => _selectedNhanVien;
            set
            {
                _selectedNhanVien = value;
                OnPropertyChanged();

                if (SelectedNhanVien != null)
                {
                    MaNhanVien = SelectedNhanVien.MaNhanVien;
                    HoTen = SelectedNhanVien.HoTen;
                    GioiTinh = SelectedNhanVien.GioiTinh;
                    MaChucVu = SelectedNhanVien.MaChucVu;
                    TenChucVu = SelectedNhanVien.TenChucVu;
                    MaPhong = SelectedNhanVien.MaPhong;
                    TenPhong = SelectedNhanVien.TenPhong;
                    Cccd = SelectedNhanVien.Cccd;

                    PersonnelDetailsWindow pd = new PersonnelDetailsWindow(SelectedNhanVien.MaNhanVien);
                    pd.Show();
                }
            }
        }

        public string Cccd { get => _cccd; set { _cccd = value; OnPropertyChanged(); } }

        public ObservableCollection<ChucVu> DsChucVu { get => _dsChucVu; set { _dsChucVu = value; OnPropertyChanged(); } }
        public ObservableCollection<PhongBan> DsPhongBan { get => _dsPhongBan; set { _dsPhongBan = value; OnPropertyChanged(); } }

        public ObservableCollection<TrinhDo> DsTrinhDo { get => _dsTrinhDo; set { _dsTrinhDo = value; OnPropertyChanged(); } }

        public int MaTrinhDo { get => _maTrinhDo; set { _maTrinhDo = value; OnPropertyChanged(); } }
        public string TentrinhDo { get => _tentrinhDo; set { _tentrinhDo = value; OnPropertyChanged(); } }
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
                }
            }
        }
        public PhongBan SelectedPhongBan
        {
            get => _selectedPhongBan;
            set
            {
                _selectedPhongBan = value;
                OnPropertyChanged();

                if (SelectedPhongBan != null)
                {
                    MaPhong = SelectedPhongBan.maPhong;
                }
            }
        }
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
                }
            }
        }
        public double BacLuong { get => _bacLuong; set { _bacLuong = value; OnPropertyChanged(); } }
        public ObservableCollection<BacLuong> DsBacLuong { get => _dsBacLuong; set { _dsBacLuong = value; OnPropertyChanged(); } }
        public BacLuong SelectedBacLuong
        {
            get => _selectedBacLuong;
            set
            {
                _selectedBacLuong = value;
                OnPropertyChanged();

                if (_selectedBacLuong != null)
                {
                    BacLuong = Convert.ToDouble(SelectedBacLuong.heSoLuong);
                }

            }
        }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }

        public DateTime NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        #endregion


        public PersonnelViewModel()
        {
            Initialize();
        }

        private void PersonnelViewModel_LoadData(object sender, EventArgs e)
        {
            GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
        }

        private void Initialize()
        {
            Page = 1;
            TotalPage = 1;
            PageSize = 20;

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                            GetList_ChucVu();
                            GetList_PhongBan();
                            GetList_TrinhDo();
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
                        }));
                    });
                    GetData_Thread.IsBackground = true;
                    GetData_Thread.Start();
                }
                );

            IncreasePageCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (Page == TotalPage) return false;

                    return true;
                },
                (param) =>
                {
                    if (Page < TotalPage)
                    {
                        Page += 1;
                        GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                    }
                }
                );

            ReducePageCommand = new RelayCommand<Object>(
                (param) =>
                {
                    if (Page == 1) return false;

                    return true;
                },
                (param) =>
                {
                    if (Page > 1)
                    {
                        Page -= 1;
                        GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                    }
                }
                );

            BackToStartCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (Page == 1) return false;

                    return true;
                },
                (param) =>
                {
                    Page = 1;
                    GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                }
                );

            GoToEndCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (Page == TotalPage) return false;

                    return true;
                },
                (param) =>
                {
                    Page = TotalPage;
                    GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    EmptyField();
                }
                );

            FilterCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                }
                );

            RefeshCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    SelectedNhanVien = null;
                    SelectedChucVu = null;
                    SelectedPhongBan = null;
                    SelectedTrinhDo = null;
                    MaPhong = 0;
                    MaChucVu = 0;
                    MaTrinhDo = 0;
                    GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
                }
                );

            //ShowPersonnelDetailsCommand = new RelayCommand<object>(
            //    (param) => true,
            //    (param) =>
            //    {
            //        if (SelectedNhanVien != null)
            //        {
            //            var id = SelectedNhanVien.MaNhanVien;

            //            //if (detailtNhanVien != null)
            //            //{
            //            //    detailtNhanVien(id);
            //            //}


            //            //OnDataCommunicationEventArgs(new CustomEventArgs(id));
            //            //if (IDCommunicationSelectedNhanVien != null)
            //            //{
            //            //    IDCommunicationSelectedNhanVien(id);
            //            //}

            //            // - ctor
            //            //PersonnelDetailsViewModel viewModel = new PersonnelDetailsViewModel(id);

            //            PersonnelDetailsWindow pd = new PersonnelDetailsWindow();
            //            pd.ShowDialog();
            //        }
            //    }
            //    );
        }

        private void GetList_BacLuong()
        {
            try
            {
                var result = new BacLuongDAO().GetList_Luong();

                DsBacLuong = new ObservableCollection<BacLuong>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_TrinhDo()
        {
            try
            {
                var result = new TrinhDoDAO().GetList_TrinhDo();

                DsTrinhDo = new ObservableCollection<TrinhDo>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_PhongBan()
        {
            try
            {
                var result = new PhongBanDAO().GetList_PhongBan();

                DsPhongBan = new ObservableCollection<PhongBan>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetList_ChucVu()
        {
            try
            {
                var result = new ChucVuDAO().GetListChucVu();

                DsChucVu = new ObservableCollection<ChucVu>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public void GetList_NhanVien(int maChucVu, int maPhongBan, int maTrinhDo)
        {
            try
            {
                //var result = new NhanVienDAO().GetList_NhanVien();

                var db = DataProvider.Instance.DBContext;
                IQueryable<PersonnelViewModel> result = from nv in db.NhanVien
                                                        select new PersonnelViewModel()
                                                        {
                                                            MaNhanVien = nv.maNhanVien,
                                                            HoTen = nv.tenNhanVien,
                                                            GioiTinh = nv.gioiTinh,
                                                            NgaySinh = nv.ngaySinh,
                                                            MaChucVu = nv.maChucVu,
                                                            TenChucVu = nv.ChucVu.tenChucVu,
                                                            MaPhong = nv.maPhong,
                                                            TenPhong = nv.PhongBan.tenPhong,
                                                            Cccd = nv.CCCD,
                                                        };

                if (maChucVu > 0)
                {
                    result = result.Where(x => x.MaChucVu == maChucVu);
                }

                if (maPhongBan > 0)
                {
                    result = result.Where(x => x.MaPhong == maPhongBan);
                }

                if (maTrinhDo > 0)
                {
                    result = result.Where(x => x.MaTrinhDo == maTrinhDo);
                }

                TotalRecord = result.Count();
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsNhanVien = new ObservableCollection<PersonnelViewModel>(result.OrderBy(x => x.HoTen).Skip((Page - 1) * PageSize).Take(PageSize).ToList());
            }
            catch { }
        }


        private void EmptyField()
        {
            SelectedChucVu = null;
            SelectedPhongBan = null;
            SelectedTrinhDo = null;
            SelectedNhanVien = null;
            SelectedBacLuong = null;
            MaTrinhDo = 0;
            MaChucVu = 0;
            MaPhong = 0;
            GetList_NhanVien(MaChucVu, MaPhong, MaTrinhDo);
        }
    }
}
