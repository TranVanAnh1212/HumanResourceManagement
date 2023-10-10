using HRMana.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class LoginViewModel
    {
        private string _notifyContent;
        private string _notifyFill;
        private string _notifyIcon;
        private string _userName;
        private string _password;

        public ICommand LoginCommand { get; set; }
        public ICommand HideNotifyCommand { get; set; }

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
                    var window = Application.Current.MainWindow;
                    window.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
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
