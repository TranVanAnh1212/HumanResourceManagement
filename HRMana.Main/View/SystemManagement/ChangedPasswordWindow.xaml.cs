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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRMana.Main.View.SystemManagement
{
    /// <summary>
    /// Interaction logic for ChangedPassword.xaml
    /// </summary>
    public partial class ChangedPasswordWindow : Window
    {
        public ChangedPasswordWindow()
        {
            InitializeComponent();

            //txtbl_ConfirmPassword.Visibility = Visibility.Collapsed;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void txt_NewPass_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox txt = sender as TextBox;

        //    if (txt.Text.Length < 6)
        //    {
        //        // Đổi màu sắc thành màu đỏ (ví dụ: #FF0000 là mã HEX cho màu đỏ)
        //        txtbl_passwordRule.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //    }
        //    else
        //    {
        //        txtbl_passwordRule.Foreground = new SolidColorBrush(Color.FromRgb(42, 181, 45));
        //    }

        //}

        //private void txt_ConfirmPass_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox txt = sender as TextBox;

        //    if (txt.Text.Length > 0)
        //        txtbl_ConfirmPassword.Visibility = Visibility.Visible;

        //    if (!txt_NewPass.Text.Equals(txt.Text))
        //    {
        //        txtbl_ConfirmPassword.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //    }
        //    else
        //    {
        //        txtbl_ConfirmPassword.Visibility = Visibility.Collapsed;
        //        txtbl_ConfirmPassword.Foreground = new SolidColorBrush(Color.FromRgb(42, 181, 45));
        //    }


        //}
    }
}
