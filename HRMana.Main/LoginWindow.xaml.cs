using HRMana.Common.Events;
using HRMana.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRMana.Main
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string MessageSnackbar { get => txtbl_MessageSnackbar.Text; set => txtbl_MessageSnackbar.Text = value; }

        public Login()
        {
            InitializeComponent();
            txt_UserName.Focus();

            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                try
                {
                    Storyboard notificationStoryboard = FindResource("NotificationStoryboard") as Storyboard;

                    if (notificationStoryboard != null)
                    {
                        notificationStoryboard.Begin();

                        await Task.Delay(TimeSpan.FromSeconds(3));

                        notificationStoryboard.Stop();
                    }
                }
                catch (Exception) { }
            };

            NotificationEvent.Instance.HideNotificationRequested += Instance_HideNotificationRequested;
        }

        private void Instance_HideNotificationRequested(object sender, EventArgs e)
        {
            try
            {
                Storyboard notificationStoryboard = FindResource("NotificationStoryboard") as Storyboard;

                if (notificationStoryboard != null)
                {
                    notificationStoryboard.Stop();
                }
            }
            catch (Exception) { }
        }

        private void txt_UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_Password.Focus();
            }
        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is LoginViewModel viewModel)
                {
                    viewModel.LoginCommand.Execute(null);
                }
            }
        }

        private void Grid_Top_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void LoginWindow_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        // Kiểm tra nếu thanh Taskbar được nhấn và di chuột lên trên đầu tiên của màn hình
        //        if (e.GetPosition(this).Y <= 0 && Mouse.Captured == null)
        //        {
        //            // Phóng to cửa sổ
        //            this.WindowState = WindowState.Maximized;
        //        }
        //    }
        //}
    }
}
