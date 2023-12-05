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
    public class ExpertiseViewModel : BaseViewModel
    {
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private int _maChuyenMon;
        private string _tenChuyenMon;
        private ObservableCollection<ChuyenMon> _dsChuyenMon;
        private ChuyenMon _selectedChuyenMon;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_ChuyenMonCommand { get; set; }
        public ICommand Update_ChuyenMonCommand { get; set; }
        public ICommand Delete_ChuyenMonCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }
        public int MaChuyenMon { get => _maChuyenMon; set { _maChuyenMon = value; OnPropertyChanged(); } }
        public string TenChuyenMon { get => _tenChuyenMon; set { _tenChuyenMon = value; OnPropertyChanged(); } }

        public ObservableCollection<ChuyenMon> DsChuyenMon { get => _dsChuyenMon; set { _dsChuyenMon = value; OnPropertyChanged(); } }
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

        public ExpertiseViewModel()
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
                        GetList_ChuyenMon();
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
                        GetList_ChuyenMon();
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
                    GetList_ChuyenMon();
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
                    GetList_ChuyenMon();
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
                            GetList_ChuyenMon();

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

            Create_ChuyenMonCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedChuyenMon != null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(TenChuyenMon))
                        {
                            var cm = new ChuyenMon()
                            {
                                maChuyenMon = MaChuyenMon,
                                tenChuyenMon = TenChuyenMon,
                            };

                            var result = new ChuyenMonDAO().Create_ChuyenMon(cm);

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
                                ShowMessageBoxCustom("Thêm mới chuyên môn thành công", CommonConstant.Success_ICon);
                                GetList_ChuyenMon();
                                EmptyField();
                            }
                        }
                        else
                        {
                            ShowMessageBoxCustom("Tên chuyên môn không được bỏ trống.", CommonConstant.Warning_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );


            Update_ChuyenMonCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedChuyenMon == null)
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
                            var cm = new ChuyenMon()
                            {
                                maChuyenMon = MaChuyenMon,
                                tenChuyenMon = TenChuyenMon,
                            };

                            var result = new ChuyenMonDAO().Update_ChuyenMon(cm);

                            if (!result)
                            {
                                ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);
                            }
                            else
                            {
                                ShowMessageBoxCustom("Cập nhật chuyên môn thành công", CommonConstant.Success_ICon);
                                GetList_ChuyenMon();
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

            Delete_ChuyenMonCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedChuyenMon == null)
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
                            var listNV_Constrain = new ChuyenMonDAO().GetCount_NhanVien_By_MaChuyenMon(MaChuyenMon);
                            if (listNV_Constrain.Count > 0)
                            {
                                MessageBox.Show($"Có {listNV_Constrain.Count} nhân viên đang thuộc chuyên môn này, \n Yêu cầu không có nhân viên nào thuộc trình độ này.", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                var result = new ChuyenMonDAO().Delete_ChuyenMon(MaChuyenMon);

                                if (!result)
                                {
                                    ShowMessageBoxCustom("Có lỗi xảy ra ở máy chủ", CommonConstant.Error_ICon);

                                }
                                else
                                {
                                    ShowMessageBoxCustom("Xóa chuyên môn thành công", CommonConstant.Success_ICon);
                                    GetList_ChuyenMon();
                                    EmptyField();
                                }
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
            SelectedChuyenMon = null;
            MaChuyenMon = 0;
            TenChuyenMon = string.Empty;
        }

        private void GetList_ChuyenMon()
        {
            try
            {
                var result = new ChuyenMonDAO().GetListChuyenMon();

                TotalRecord = result.Count;
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsChuyenMon = new ObservableCollection<ChuyenMon>(result.OrderBy(x => x.tenChuyenMon).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
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
