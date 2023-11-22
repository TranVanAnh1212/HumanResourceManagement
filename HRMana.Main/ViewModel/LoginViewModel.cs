using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _notifyContent;
        private string _notifyFill;
        private string _notifyIcon;
        private string _userName;
        private string _password;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand HideNotifyCommand { get; set; }
        public ICommand PasswordChangeCommand { get; set; }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }

        public LoginViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            LoadWindowCommand = new RelayCommand<Window>(
                (param) => true,
                (param) =>
                {
                    CommonConstant.DsChiTietQuyen = new ChiTietQuyenDAO().GetList_ChiTietQuyen();
                }
                );

            LoginCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    try
                    {
                        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                        {
                            Login loginWindow = new Login();
                            loginWindow.MessageSnackbar = "Tên tài khoản và mật khẩu không được để trống!";
                            NotificationEvent.Instance.ReqquestShowNotification();

                        }
                        else
                        {
                            // Mã hóa mật khẩu nhập vào
                            string pass_base64_encode = StringHelper.Base64Encode(Password);
                            string pass_md5_hash = StringHelper.MD5Hash(pass_base64_encode);

                            // Ktra tài khoản
                            var checkLogin = new LoginDAO().CheckLogin(UserName, pass_md5_hash);

                            if (checkLogin != null)
                            {
                                //Thêm vào biến cục bộ
                                CommonConstant.taiKhoanDN = new LoginDAO().Get_TaiKhoan_By_MaTK(checkLogin.maTaiKhoan);
                                CommonConstant.DsQuyenCuaTKDN = CommonConstant.taiKhoanDN.Quyen.ChiTietQuyen_Quyen.ToList();

                                // Thêm báo cáo đăng nhập
                                BaoCaoDangNhap bcdn = new BaoCaoDangNhap
                                {
                                    maTaiKhoan = CommonConstant.taiKhoanDN.maTaiKhoan,
                                    tgDangNhap = DateTime.Now
                                };
                                CommonConstant.baoCaoDN = bcdn;

                                // Hiển thị form chính
                                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                                window.Hide();
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.ShowDialog();
                            }
                            else
                            {
                                NotificationEvent.Instance.ReqquestShowNotification();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            PasswordChangeCommand = new RelayCommand<PasswordBox>(
                (param) => { return true; },
                (param) =>
                {
                    Password = param.Password;
                }
                );

            HideNotifyCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    NotificationEvent.Instance.ReqquestHideNotification();
                }
                );
        }
    }
}
