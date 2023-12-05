using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using HRMana.Common.Commons;
using HRMana.Main.View.Dialog;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace HRMana.Main.ViewModel
{
    public class PeopleViewModel : BaseViewModel
    {
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private int _maDanToc;
        private string _tenDanToc;
        private ObservableCollection<DanToc> _dsDanToc;
        private DanToc _selectedDanToc;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_DanTocCommand { get; set; }
        public ICommand Update_DanTocCommand { get; set; }
        public ICommand Delete_DanTocCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }
        public int MaDanToc { get => _maDanToc; set { _maDanToc = value; OnPropertyChanged(); } }
        public string TenDanToc { get => _tenDanToc; set { _tenDanToc = value; OnPropertyChanged(); } }

        public ObservableCollection<DanToc> DsDanToc { get => _dsDanToc; set { _dsDanToc = value; OnPropertyChanged(); } }
        public DanToc SelectedDanToc
        {
            get => _selectedDanToc;
            set
            {
                _selectedDanToc = value;
                OnPropertyChanged();

                if (SelectedDanToc != null)
                {
                    MaDanToc = SelectedDanToc.maDanToc;
                    TenDanToc = SelectedDanToc.tenDanToc;
                }
            }
        }

        public PeopleViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            TotalPage = 1;
            PageSize = 15;

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
                        GetList_DanToc();
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
                        GetList_DanToc();
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
                    GetList_DanToc();
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
                    GetList_DanToc();
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
                            GetList_DanToc();

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
                (param) => true,
                (param) =>
                {
                    EmptyField();
                }
                );

            Create_DanTocCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedDanToc != null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(TenDanToc))
                        {
                            var cm = new DanToc()
                            {
                                maDanToc = MaDanToc,
                                tenDanToc = TenDanToc,
                            };

                            var result = new DanTocDAO().Create_DanToc(cm);

                            if (result < 0)
                            {
                                ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);
                            }
                            else if (result == 0)
                            {
                                ShowMessageBoxCustom("Dữ liệu đang bị rỗng.", CommonConstant.Warning_ICon);
                            }
                            else
                            {
                                ShowMessageBoxCustom("Thêm mới dân tộc thành công", CommonConstant.Success_ICon);
                                GetList_DanToc();
                                EmptyField();
                            }
                        }
                        else
                        {
                            ShowMessageBoxCustom("Tên dân tộc không được bỏ trống", CommonConstant.Warning_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );


            Update_DanTocCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedDanToc == null)
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
                            var cm = new DanToc()
                            {
                                maDanToc = MaDanToc,
                                tenDanToc = TenDanToc,
                            };

                            var result = new DanTocDAO().Update_DanToc(cm);

                            if (!result)
                            {
                                ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);

                            }
                            else
                            {
                                ShowMessageBoxCustom("Cập nhật dân tộc thành công", CommonConstant.Success_ICon);
                                GetList_DanToc();
                                EmptyField();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                );

            Delete_DanTocCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedDanToc == null)
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
                            var dt_nv_Constrain = new DanTocDAO().GetList_NhanVien_By_MaDanToc(MaDanToc);
                            if (dt_nv_Constrain.Count <= 0)
                            {
                                var result = new DanTocDAO().Delete_DanToc(MaDanToc);

                                if (!result)
                                {
                                    ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);

                                }
                                else
                                {
                                    ShowMessageBoxCustom("Xóa dân tộc dân tộc thành công", CommonConstant.Success_ICon);

                                    GetList_DanToc();
                                    EmptyField();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Có nhân viên là dân tộc này, \n Yêu cầu đảm bảo các nhân viên không là dân tộc này.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                );
        }

        private void EmptyField()
        {
            SelectedDanToc = null;
            MaDanToc = 0;
            TenDanToc = string.Empty;
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

        private void GetList_DanToc()
        {
            try
            {
                var result = new DanTocDAO().GetList_DanToc();

                TotalRecord = result.Count;
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsDanToc = new ObservableCollection<DanToc>(result.OrderBy(x => x.tenDanToc).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
