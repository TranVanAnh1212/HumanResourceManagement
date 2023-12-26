using HRMana.Main.View.Personnel;
using HRMana.Model.DAO;
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
    public class SearchViewModel : BaseViewModel
    {
        private string _maNhanVien;
        private string _hoTen;
        private DateTime? _ngaySinh;
        private string _gioiTinh;
        private string _queQuan;
        private string _soHopDong;
        private string _soCCCD;
        private string _noiOHienTai;
        private string _DanToc;
        private ObservableCollection<SearchViewModel> _dsSearchResult;
        private SearchViewModel _selectedSearchResult;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
        public string QueQuan { get => _queQuan; set { _queQuan = value; OnPropertyChanged(); } }
        public string SoHopDong { get => _soHopDong; set { _soHopDong = value; OnPropertyChanged(); } }
        public string SoCCCD { get => _soCCCD; set { _soCCCD = value; OnPropertyChanged(); } }

        public ObservableCollection<SearchViewModel> DsSearchResult { get => _dsSearchResult; set { _dsSearchResult = value; OnPropertyChanged(); } }
        public SearchViewModel SelectedSearchResult
        {
            get => _selectedSearchResult;
            set
            {
                _selectedSearchResult = value;
                OnPropertyChanged();

                if (value != null)
                {
                    MaNhanVien = SelectedSearchResult.MaNhanVien;

                    PersonnelDetailsWindow detail = new PersonnelDetailsWindow(MaNhanVien);
                    detail.Show();
                }
            }
        }

        public string GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }
        public string NoiOHienTai { get => _noiOHienTai; set { _noiOHienTai = value; OnPropertyChanged(); } }
        public string DanToc { get => _DanToc; set { _DanToc = value; OnPropertyChanged(); } }

        public SearchViewModel()
        {
            SearchCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    try
                    {
                        var db = DataProvider.Instance.DBContext;
                        var searchResult = from nv in db.NhanVien
                                           select new SearchViewModel()
                                           {
                                               MaNhanVien = nv.maNhanVien,
                                               HoTen = nv.tenNhanVien,
                                               GioiTinh = nv.gioiTinh,
                                               NgaySinh = nv.ngaySinh,
                                               SoCCCD = nv.CCCD.Trim(),
                                               QueQuan = nv.queQuan,
                                               NoiOHienTai = nv.noiOHienTai,
                                               DanToc = nv.DanToc.tenDanToc
                                           };

                        if ( !string.IsNullOrEmpty(MaNhanVien))
                        {
                            searchResult = searchResult.Where(x => x.MaNhanVien.Contains(MaNhanVien));
                        }

                        if (!string.IsNullOrEmpty(HoTen))
                        {
                            searchResult = searchResult.Where(x => x.HoTen.Contains(HoTen));
                        }

                        if (!string.IsNullOrEmpty(QueQuan))
                        {
                            searchResult = searchResult.Where(x => x.HoTen.Contains(QueQuan));
                        }

                        DsSearchResult = new ObservableCollection<SearchViewModel>(searchResult.ToList());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                );

            CancelCommand = new RelayCommand<object>(
                 (param) => true,
                 (param) =>
                 {
                     SelectedSearchResult = null;
                     MaNhanVien = string.Empty;
                     HoTen = string.Empty;
                     NgaySinh = null;
                     QueQuan = string.Empty;
                     SoHopDong = string.Empty;
                     SoCCCD = string.Empty;
                 }
                 );
        }
    }
}
