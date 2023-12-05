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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRMana.Main.View.SystemManagement
{
    /// <summary>
    /// Interaction logic for AccountAuthenticationPage.xaml
    /// </summary>
    public partial class AccountAuthenticationPage : Page
    {
        public AccountAuthenticationPage()
        {
            InitializeComponent();

            txtbl_TenQuyen.Visibility = Visibility.Collapsed;
            txtbl_MaQuyen.Visibility = Visibility.Collapsed;
            txtbl_HanhDong.Visibility = Visibility.Collapsed;
        }

        private void txt_MaQuyen_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length <= 0)
            {
                txtbl_MaQuyen.Visibility = Visibility.Visible;
                txtbl_MaQuyen.Text = "Mã quyền không được bỏ trống";
                btn_AddQuyen.IsEnabled = false;
            }
            else
            {
                if (txt.Text.Length > 10)
                {
                    txtbl_MaQuyen.Visibility = Visibility.Visible;
                    txtbl_MaQuyen.Text = "Mã quyền không được dài quá 10 ký tự";
                    btn_AddQuyen.IsEnabled = false;
                }
                else
                {
                    txtbl_MaQuyen.Visibility = Visibility.Collapsed;
                    txtbl_MaQuyen.Text = "Mã quyền không được bỏ trống";
                    btn_AddQuyen.IsEnabled = true;
                }
            }
        }

        private void txt_TenQuyen_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length <= 0)
            {
                txtbl_TenQuyen.Visibility = Visibility.Visible;
                btn_AddQuyen.IsEnabled = false;
            }
            else
            {
                txtbl_TenQuyen.Visibility = Visibility.Collapsed;
                btn_AddQuyen.IsEnabled = true;
            }
        }

        private bool Check_CkbIsSelected()
        {
            return ckb_Add.IsChecked.Value && ckb_Del.IsChecked.Value && ckb_Edit.IsChecked.Value && ckb_Muser.IsChecked.Value && ckb_View.IsChecked.Value;
        }

    }
}
