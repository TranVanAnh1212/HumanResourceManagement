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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using HRMana.Common.Commons;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace HRMana.Main.ViewModel
{
    public class ReligionViewModel : BaseViewModel
    {
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private int _maTonGiao;
        private string _tenTonGiao;
        private ObservableCollection<TonGiao> _dsTonGiao;
        private TonGiao _selectedTonGiao;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_TonGiaoCommand { get; set; }
        public ICommand Update_TonGiaoCommand { get; set; }
        public ICommand Delete_TonGiaoCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }
        public int MaTonGiao { get => _maTonGiao; set { _maTonGiao = value; OnPropertyChanged(); } }
        public string TenTonGiao { get => _tenTonGiao; set { _tenTonGiao = value; OnPropertyChanged(); } }

        public ObservableCollection<TonGiao> DsTonGiao { get => _dsTonGiao; set { _dsTonGiao = value; OnPropertyChanged(); } }
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

        public ReligionViewModel()
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
                        GetList_TonGiao();
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
                        GetList_TonGiao();
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
                    GetList_TonGiao();
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
                    GetList_TonGiao();
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
                            GetList_TonGiao();

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

            Create_TonGiaoCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedTonGiao != null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(TenTonGiao))
                        {
                            var cm = new TonGiao()
                            {
                                maTonGiao = MaTonGiao,
                                tenTonGiao = TenTonGiao,
                            };

                            var result = new TonGiaoDAO().Create_TonGiao(cm);

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
                                ShowMessageBoxCustom("Thêm mới tôn giáo thành công", CommonConstant.Success_ICon);
                                GetList_TonGiao();
                                EmptyField();
                            }
                        }
                        else
                        {
                            ShowMessageBoxCustom("Tên tôn giáo không được bỏ trống.", CommonConstant.Warning_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );


            Update_TonGiaoCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedTonGiao == null)
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
                            var cm = new TonGiao()
                            {
                                maTonGiao = MaTonGiao,
                                tenTonGiao = TenTonGiao,
                            };

                            var result = new TonGiaoDAO().Update_TonGiao(cm);

                            if (!result)
                            {
                                ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);

                            }
                            else
                            {
                                ShowMessageBoxCustom("Cập nhật tôn giáo thành công", CommonConstant.Success_ICon);
                                GetList_TonGiao();
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

            Delete_TonGiaoCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedTonGiao == null)
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
                            var tg_nv_Constrain = new TonGiaoDAO().GetList_NhanVien_By_MaTonGiao(MaTonGiao);
                            if (tg_nv_Constrain.Count <= 0)
                            {
                                var result = new TonGiaoDAO().Delete_TonGiao(MaTonGiao);

                                if (!result)
                                {
                                    ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);

                                }
                                else
                                {
                                    ShowMessageBoxCustom("Xóa tôn giáo thành công", CommonConstant.Success_ICon);
                                    GetList_TonGiao();
                                    EmptyField();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Có nhân viên là tôn giáo này, \n Yêu cầu đảm bảo các nhân viên không là tôn giáo này.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

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
            SelectedTonGiao = null;
            MaTonGiao = 0;
            TenTonGiao = string.Empty;
        }

        private void GetList_TonGiao()
        {
            try
            {
                var result = new TonGiaoDAO().GetList_TonGiao();

                TotalRecord = result.Count;
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsTonGiao = new ObservableCollection<TonGiao>(result.OrderBy(x => x.tenTonGiao).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
