using HRMana.Common;
using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Main.View.Dialog;
using HRMana.Main.View.Position;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class PositionViewModel : BaseViewModel
    {
        private int _maChucVu;
        private string _tenChucVu;
        private ObservableCollection<PositionViewModel> _positions;
        private PositionViewModel _SelectedPosition;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;
        private string _message;
        private string _fill;
        private bool isEnable;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand CreateNew_ChucVuCommand { get; set; }
        public ICommand Update_ChucVuCommand { get; set; }
        public ICommand Delete_ChucVuCommand { get; set; }
        public ICommand CancelCommandCommand { get; set; }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }


        public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        public ObservableCollection<PositionViewModel> Positions { get => _positions; set { _positions = value; OnPropertyChanged(); } }
        public PositionViewModel SelectedPosition
        {
            get => _SelectedPosition;
            set
            {
                _SelectedPosition = value;
                OnPropertyChanged();

                if (_SelectedPosition != null)
                {
                    MaChucVu = SelectedPosition.MaChucVu;
                    TenChucVu = SelectedPosition.TenChucVu;
                }
            }
        }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }

        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }

        public bool IsEnable { get => isEnable; set { isEnable = value; OnPropertyChanged(); } }

        public PositionViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            PageSize = 20;
            TotalPage = 1;

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    GetList_ChucVu();

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
                        GetList_ChucVu();
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
                        GetList_ChucVu();
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
                    GetList_ChucVu();
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
                    GetList_ChucVu();
                }
                );

            CreateNew_ChucVuCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    var cv = new ChucVu()
                    {
                        maChucVu = MaChucVu,
                        tenChucVu = TenChucVu,
                    };

                    var result = new ChucVuDAO().CreateNew_ChucVu(cv);

                    if (result > 0)
                    {
                        ShowNotification("Thêm mới chức vụ thành công.", "#FF58FF7B");
                        EmptyTextbox();
                        GetList_ChucVu();
                    }
                    else if (result == 0)
                    {
                        ShowNotification("Dữ liệu đưa vào trống.", "#FFFF00");
                    }
                    else
                    {
                        ShowNotification("Có lỗi xảy ra bên phía máy chủ.", "#FFFF5858");

                    }
                }
                );

            CancelCommandCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    EmptyTextbox();
                }
                );

            Update_ChucVuCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPosition == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn sửa chức vụ?";
                        d.Owner = Window.GetWindow(new PositionPage());

                        if (true == d.ShowDialog())
                        {
                            var cv = new ChucVu()
                            {
                                maChucVu = MaChucVu,
                                tenChucVu = TenChucVu,
                            };

                            var result = new ChucVuDAO().Update_ChucVu(cv);

                            if (result)
                            {
                                ShowNotification("Cập nhật chức vụ thành công. ", "#FF58FF7B");
                                EmptyTextbox();
                                GetList_ChucVu();
                            }
                            else
                            {
                                ShowNotification("Cập nhật chức vụ không thành công. ", "#FFFF5858");
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        ShowNotification("Có lỗi xảy ra.", "#FFFF5858");

                    }
                }
                );

            Delete_ChucVuCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPosition == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn xóa?";
                        d.Owner = Window.GetWindow(new PositionPage());

                        if (true == d.ShowDialog())
                        {
                            var nv_cv_Contrain = new ChucVuDAO().GetList_NhanVien_By_MaChucVu(MaChucVu);

                            if (nv_cv_Contrain.Count <= 0)
                            {
                                var result = new ChucVuDAO().Delete_ChucVu(MaChucVu);
                                if (result)
                                {
                                    ShowNotification("Xóa chức vụ thành công. ", "#FF58FF7B");
                                    EmptyTextbox();
                                    GetList_ChucVu();
                                }
                                else
                                {
                                    ShowNotification("Xóa chức vụ không thành công. ", "#FFFF5858");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Có nhân viên thuộc chức vụ này, \n Yêu cầu đảm bảo các nhân viên không thuộc chức vụ này.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Có lỗi xảy ra.", "#FFFF5858");

                    }
                }
                );
        }

        private void GetList_ChucVu()
        {
            try
            {
                var db = DataProvider.Instance.DBContext;
                var result = from cv in db.ChucVu
                             select new PositionViewModel()
                             {
                                 MaChucVu = cv.maChucVu,
                                 TenChucVu = cv.tenChucVu,
                             };

                TotalRecord = result.Count();
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                Positions = new ObservableCollection<PositionViewModel>(result.OrderBy(x => x.TenChucVu).Skip((Page - 1) * PageSize).Take(PageSize).ToList());
            }
            catch (Exception ex) {
                ShowNotification("Có lỗi khi lấy danh sách chức vụ.", "#FFFF5858");
            }
        }

        private void EmptyTextbox()
        {
            SelectedPosition = null;
            MaChucVu = 0;
            TenChucVu = string.Empty;
        }

        private void ShowNotification(string message, string fill)
        {
            Message = message;
            Fill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }

    }
}
