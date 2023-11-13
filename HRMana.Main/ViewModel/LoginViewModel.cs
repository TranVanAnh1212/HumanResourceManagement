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
            LoginCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    var checkLogin = new LoginDAO().CheckLogin(UserName, Password);

                    if (checkLogin != null)
                    {
                        // Thêm báo cáo đăng nhập
                        CommonConstant.taiKhoanDN = checkLogin;
                        BaoCaoDangNhap bcdn = new BaoCaoDangNhap
                        {
                            maTaiKhoan = CommonConstant.taiKhoanDN.maTaiKhoan,
                            tgDangNhap = DateTime.Now
                        };
                        CommonConstant.baoCaoDN = bcdn;

                        var window = Application.Current.MainWindow;
                        window.Hide();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }
                    else
                    {
                        NotificationEvent.Instance.ReqquestShowNotification();
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
