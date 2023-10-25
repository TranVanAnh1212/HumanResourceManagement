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
        public ICommand ExitCommand { get; set; }
        public ICommand LogoutCommand { get; set; }


        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            ExitCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirm == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                }
                );

            LogoutCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông báo đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirm == MessageBoxResult.Yes)
                    {
                        var window = Application.Current.MainWindow;
                        window.Hide();
                        Login login = new Login();
                        login.Show();
                    }
                }
                );
        }
    }
}
