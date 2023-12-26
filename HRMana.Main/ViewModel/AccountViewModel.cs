using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Main.View.Dialog;
using HRMana.Main.View.SystemManagement;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    internal class AccountViewModel : BaseViewModel
    {
        private string _message;
        private string _fill;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;

        private int _maTaiKhoan;
        private string _tenTaiKhoan;
        private string _matKhau;
        private string _trangThaiTaiKhoan;
        private string _quyenTaiKhoan;
        private string _maQuyen;
        private ObservableCollection<AccountViewModel> _listTaiKhoan;
        private ObservableCollection<Quyen> _listQuyen;
        private Quyen _selectedQuyen;
        private AccountViewModel _selectedTaiKhoan;

        public ICommand LoadTaiKhoanPageCommand { get; set; }
        public ICommand CanCelCommand { get; set; }
        public ICommand CreateNew_TaiKhoan { get; set; }
        public ICommand Delete_TaiKhoan { get; set; }
        public ICommand Block_TaiKhoan { get; set; }
        public ICommand Unblock_TaiKhoan { get; set; }

        public string TenTaiKhoan { get => _tenTaiKhoan; set { _tenTaiKhoan = value; OnPropertyChanged(); } }
        public string MatKhau { get => _matKhau; set { _matKhau = value; OnPropertyChanged(); } }
        public string TrangThaiTaiKhoan { get => _trangThaiTaiKhoan; set { _trangThaiTaiKhoan = value; OnPropertyChanged(); } }
        public string QuyenTaiKhoan { get => _quyenTaiKhoan; set { _quyenTaiKhoan = value; OnPropertyChanged(); } }
        public string MaQuyen { get => _maQuyen; set { _maQuyen = value; } }
        public ObservableCollection<AccountViewModel> ListTaiKhoan { get => _listTaiKhoan; set { _listTaiKhoan = value; OnPropertyChanged(); } }
        public ObservableCollection<Quyen> ListQuyen { get => _listQuyen; set { _listQuyen = value; OnPropertyChanged(); } }

        public Quyen SelectedQuyen
        {
            get
            {
                return _selectedQuyen;
            }
            set
            {
                _selectedQuyen = value;
                OnPropertyChanged();

                if (SelectedQuyen != null)
                {
                    MaQuyen = SelectedQuyen.maQuyen;
                    QuyenTaiKhoan = SelectedQuyen.tenQuyen;
                }
            }
        }

        public AccountViewModel SelectedTaiKhoan
        {
            get => _selectedTaiKhoan;
            set
            {
                _selectedTaiKhoan = value;
                OnPropertyChanged();

                if (SelectedTaiKhoan != null)
                {
                    MaTaiKhoan = SelectedTaiKhoan.MaTaiKhoan;
                    TenTaiKhoan = SelectedTaiKhoan.TenTaiKhoan;
                    MaQuyen = SelectedTaiKhoan.MaQuyen;
                    TrangThaiTaiKhoan = SelectedTaiKhoan.TrangThaiTaiKhoan;
                    //MatKhau = SelectedTaiKhoan.MatKhau;
                    SelectedQuyen = ListQuyen.FirstOrDefault(x => x.maQuyen == SelectedTaiKhoan.MaQuyen);
                }
            }
        }
        public int MaTaiKhoan { get => _maTaiKhoan; set { _maTaiKhoan = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }

        public AccountViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            GetListQuyen();

            LoadTaiKhoanPageCommand = new RelayCommand<Object>(
                (param) => { return true; },
                async (param) =>
                {
                    Task loading_Task = new Task(() =>
                    {
                        GetListTaiKhoan();

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
                    });

                    loading_Task.Start();
                    await loading_Task;
                }
                );

            CanCelCommand = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedTaiKhoan == null || string.IsNullOrEmpty(TenTaiKhoan) || string.IsNullOrEmpty(MatKhau) || SelectedQuyen == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    EmptyTextbox();
                }
                );

            CreateNew_TaiKhoan = new RelayCommand<Object>(
                (param) =>
                {

                    if (string.IsNullOrEmpty(TenTaiKhoan) || string.IsNullOrEmpty(MatKhau) || SelectedQuyen == null)
                        return false;
                    else
                        return true;
                },
                (param) =>
                {
                    try
                    {
                        string pass_base64_encode = StringHelper.Base64Encode(MatKhau);
                        string pass_md5_hash = StringHelper.MD5Hash(pass_base64_encode);

                        var result = new TaiKhoanDAO().Create_TaiKhoan(TenTaiKhoan, pass_md5_hash, MaQuyen, true);

                        if (result != null)
                        {
                            ShowNotification("Thêm mới tài khoản thành công", "#FF58FF7B");
                            GetListTaiKhoan();
                            EmptyTextbox();
                        }
                        else
                        {
                            ShowNotification("Xóa tài khoản không thành công", "#FFFF5858");

                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Có lỗi xảy ra.", "#FFFF5858");
                    }
                }
                );

            Delete_TaiKhoan = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedTaiKhoan == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow dialog = new DialogWindow();
                        dialog.DialogMessage = "Bạn có chắc muốn xóa";
                        dialog.Owner = Window.GetWindow(new AccountUserPage());

                        /*                    
                        so sánh true với dialog.ShowDialog() if sẽ được thực thi nếu dialog.ShowDialog() là true
                         */
                        if (true == dialog.ShowDialog())
                        {
                            var result = new TaiKhoanDAO().Delete_TaiKhoan(SelectedTaiKhoan.MaTaiKhoan);

                            if (result)
                            {
                                ShowNotification("Xóa tài khoản thành công", "#FF58FF7B");
                                GetListTaiKhoan();
                                EmptyTextbox();
                            }
                            else
                            {
                                ShowNotification("Xóa tài khoản không thành công", "#FFFF5858");

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Có lỗi xảy ra.", "#FFFF5858");

                    }
                }
                );

            Block_TaiKhoan = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedTaiKhoan == null || SelectedTaiKhoan.TrangThaiTaiKhoan.Equals(CommonConstant.TrangThaiTaiKhoan_Khoa)) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn khóa tài khoản này.";
                        d.Owner = Window.GetWindow(new AccountUserPage());

                        if (true == d.ShowDialog())
                        {
                            var result = new TaiKhoanDAO().Block_TaiKhoan(SelectedTaiKhoan.MaTaiKhoan);

                            if (result)
                            {
                                ShowNotification("Khóa tài khoản thành công", "#FF58FF7B");
                                GetListTaiKhoan();
                                EmptyTextbox();
                            }
                            else
                            {
                                ShowNotification("Khóa tài khoản không thành công", "#FFFF5858");

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Có lỗi xảy ra.", "#FFFF5858");

                    }

                }
                );

            Unblock_TaiKhoan = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedTaiKhoan == null || SelectedTaiKhoan.TrangThaiTaiKhoan.Equals(CommonConstant.TrangThaiTaiKhoan_HoatDong)) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn khóa tài khoản này.";
                        d.Owner = Window.GetWindow(new AccountUserPage());

                        if (true == d.ShowDialog())
                        {
                            var result = new TaiKhoanDAO().Unblock_TaiKhoan(SelectedTaiKhoan.MaTaiKhoan);

                            if (result)
                            {
                                ShowNotification("Mở khóa tài khoản thành công", "#FF58FF7B");
                                GetListTaiKhoan();
                                EmptyTextbox();
                            }
                            else
                            {
                                ShowNotification("Mở khóa tài khoản không thành công", "#FFFF5858");

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

        public void GetListQuyen()
        {
            ListQuyen = new ObservableCollection<Quyen>(new QuyenDAO().GetListQuyen());
        }

        private void GetListTaiKhoan()
        {
            var db = DataProvider.Instance.DBContext;

            var query = from taiKhoan in db.TaiKhoan
                        select new AccountViewModel()
                        {
                            MaTaiKhoan = taiKhoan.maTaiKhoan,
                            TenTaiKhoan = taiKhoan.tenTaiKhoan,
                            TrangThaiTaiKhoan = taiKhoan.trangThai == true ? CommonConstant.TrangThaiTaiKhoan_HoatDong : CommonConstant.TrangThaiTaiKhoan_Khoa,
                            MaQuyen = taiKhoan.maQuyen,
                            MatKhau = taiKhoan.matKhau,
                            QuyenTaiKhoan = taiKhoan.Quyen.tenQuyen,
                        };

            var result = query.ToList();

            ListTaiKhoan = new ObservableCollection<AccountViewModel>(result);
        }

        private void EmptyTextbox()
        {
            MaTaiKhoan = 0;
            TenTaiKhoan = string.Empty;
            TrangThaiTaiKhoan = string.Empty;
            MatKhau = string.Empty;
            MaQuyen = string.Empty;
            QuyenTaiKhoan = string.Empty;
            SelectedQuyen = null;
            SelectedTaiKhoan = null;
        }

        private void ShowNotification(string message, string fill)
        {
            Message = message;
            Fill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }
    }
}
