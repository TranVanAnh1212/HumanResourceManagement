using Aspose.Words.Drawing;
using HRMana.Common.Commons;
using HRMana.Main.View.Dialog;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HRMana.Main.ViewModel
{
    public class DepartmentViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Khai báo biến

        private int _maPhong;
        private string _tenPhong;
        private string _sdt;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;
        private ObservableCollection<PhongBan> _dsPhongBan;
        private PhongBan _selectedPhongBan;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_PhongBanCommand { get; set; }
        public ICommand Update_PhongBanCommand { get; set; }
        public ICommand Delete_PhongBanCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }
        public ObservableCollection<PhongBan> DsPhongBan { get => _dsPhongBan; set { _dsPhongBan = value; OnPropertyChanged(); } }
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
                    TenPhong = SelectedPhongBan.tenPhong.Trim();
                    Sdt = SelectedPhongBan.dienThoai.Trim();
                }
            }
        }

        public int MaPhong { get => _maPhong; set { _maPhong = value; OnPropertyChanged(); } }
        public string TenPhong { get => _tenPhong; set { _tenPhong = value; OnPropertyChanged(); } }
        public string Sdt { get => _sdt; set { _sdt = value; OnPropertyChanged(); } }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                var err = "";

                switch(columnName)
                {
                    case "TenPhong":
                        if (string.IsNullOrEmpty(TenPhong))
                            err = "Tên phòng không được bỏ trống";
                        break;
                }

                return err;
            }
        }

        #endregion

        public DepartmentViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            TotalPage = 1;
            PageSize = 10;

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
                        GetList_PhongBan();
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
                        GetList_PhongBan();
                    }
                }
                );

            BackToStartCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (Page <= 1) return false;

                    return true;
                },
                (param) =>
                {
                    Page = 1;
                    GetList_PhongBan();
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
                    GetList_PhongBan();
                }
                );

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_PhongBan();

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

            Create_PhongBanCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (string.IsNullOrEmpty(TenPhong) || SelectedPhongBan != null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        var pb = new PhongBan()
                        {
                            tenPhong = TenPhong.Trim(),
                            dienThoai = Sdt.Trim()
                        };

                        var result = new PhongBanDAO().CreateNew_PhongBan(pb);

                        if (result > 0)
                        {

                            ShowMessageBoxCustom("Thêm mới phòng ban thành công.", CommonConstant.Success_ICon);

                            EmptyField();
                            GetList_PhongBan();
                        }
                        else if (result == 0)
                        {
                            ShowMessageBoxCustom("Dữ liệu đưa vào rỗng.", CommonConstant.Warning_ICon);
                        }
                        else
                        {
                            ShowMessageBoxCustom("Có lỗi xảy ra khi thêm phòng ban mới vào máy chủ.", CommonConstant.Error_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                );

            Update_PhongBanCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPhongBan == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn cập nhật?";

                    if (true == d.ShowDialog())
                    {
                        try
                        {

                            var result = new PhongBanDAO().Update_PhongBan(MaPhong, TenPhong.Trim(), Sdt.Trim());

                            if (result)
                            {
                                ShowMessageBoxCustom("Cập nhật phòng ban thành công.", CommonConstant.Success_ICon);
                                EmptyField();
                                GetList_PhongBan();
                            }
                            else
                            {
                                ShowMessageBoxCustom("Có lỗi xảy ra khi cập nhật phòng ban ở máy chủ.", CommonConstant.Error_ICon);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                );

            Delete_PhongBanCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPhongBan == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn xóa?";

                    if (true == d.ShowDialog())
                    {
                        try
                        {
                            var pb_nv_Constrain = new PhongBanDAO().GetList_NhanVien_By_MaPhongBan(MaPhong);

                            if (pb_nv_Constrain.Count <= 0)
                            {
                                var result = new PhongBanDAO().Delete_PhongBan(MaPhong);

                                if (result)
                                {
                                    ShowMessageBoxCustom("Xóa phòng ban thành công.", CommonConstant.Success_ICon);
                                    EmptyField();
                                    GetList_PhongBan();
                                }
                                else
                                {
                                    ShowMessageBoxCustom("Có lỗi xảy ra khi xóa phòng ban ở máy chủ.", CommonConstant.Error_ICon);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Có nhân viên thuộc phòng ban này, \n Yêu cầu đảm bảo các nhân viên không thuộc phòng ban này.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
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
        }

        private void EmptyField()
        {
            SelectedPhongBan = null;
            MaPhong = 0;
            TenPhong = string.Empty;
            Sdt = string.Empty;
        }

        private void GetList_PhongBan()
        {
            try
            {
                var result = new PhongBanDAO().GetList_PhongBan();

                TotalRecord = result.Count();
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsPhongBan = new ObservableCollection<PhongBan>(result.OrderBy(x => x.tenPhong).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy dữ liệu", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
