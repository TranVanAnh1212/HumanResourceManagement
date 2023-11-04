using HRMana.Common.Commons;
using HRMana.Common.Events;
using HRMana.Main.View.Dialog;
using HRMana.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _message;
        private string _fill;

        public ICommand ExitCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            Initialize();
        }

        public void ShowMessage(string message, string fill)
        {
            Message = message;
            Fill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }

        private void Initialize()
        {
            ExitCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn thoát?";

                    if (true == d.ShowDialog())
                    {
                        if (CommonConstant.baoCaoDN != null)
                        {
                            CommonConstant.baoCaoDN.tgDangXuat = DateTime.Now;

                            new BaoCaoDangNhapDAO().Create_BaoCaoDangNhap(CommonConstant.baoCaoDN.maTaiKhoan, CommonConstant.baoCaoDN.tgDangNhap, CommonConstant.baoCaoDN.tgDangXuat);

                            CommonConstant.baoCaoDN = null;
                        }

                        Application.Current.Shutdown();
                    }
                }
                );

            LogoutCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn đăng xuất?";

                    if (true == d.ShowDialog())
                    {
                        if (CommonConstant.baoCaoDN != null)
                        {
                            CommonConstant.baoCaoDN.tgDangXuat = DateTime.Now;

                            new BaoCaoDangNhapDAO().Create_BaoCaoDangNhap(CommonConstant.baoCaoDN.maTaiKhoan, CommonConstant.baoCaoDN.tgDangNhap, CommonConstant.baoCaoDN.tgDangXuat);

                            CommonConstant.baoCaoDN = null;
                        }

                        // Kiểm tra window đang hiển thị
                        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                        window.Close();
                        Login login = new Login();
                        login.Show();
                    }
                }
                );
        }
    }
}
