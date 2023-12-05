
using HRMana.Common.Commons;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HRMana.Main.View.Department
{
    /// <summary>
    /// Interaction logic for DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        public DepartmentPage()
        {
            InitializeComponent();
            txtbl_PhoneValidate.Visibility = Visibility.Collapsed;
        }

        private void UpperCaseFirstChar(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text;

            if (!string.IsNullOrEmpty(value))
            {
                textBox.Text = char.ToUpper(value[0]) + value.Substring(1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void txt_dienThoai_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;

            if (string.IsNullOrEmpty(txt.Text))
            {
                btn_CreateNew.IsEnabled = true;
                btn_Update.IsEnabled = true;
                btn_Delete.IsEnabled = true;
                txtbl_PhoneValidate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!StringHelper.IsPhoneNumber(txt.Text))
                {
                    btn_CreateNew.IsEnabled = false;
                    btn_Update.IsEnabled = false;
                    btn_Delete.IsEnabled = false;
                    txtbl_PhoneValidate.Text = "Định dạng số điện thoại không đúng!";
                    txtbl_PhoneValidate.Visibility = Visibility.Visible;
                    txtbl_PhoneValidate.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (txt.Text.Length > 11)
                    {
                        btn_CreateNew.IsEnabled = false;
                        btn_Update.IsEnabled = false;
                        btn_Delete.IsEnabled = false;
                        txtbl_PhoneValidate.Text = "Số điện thoại chỉ dài 10 chứ số";
                        txtbl_PhoneValidate.Visibility = Visibility.Visible;
                        txtbl_PhoneValidate.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        txtbl_PhoneValidate.Visibility = Visibility.Collapsed;
                        btn_CreateNew.IsEnabled = true;
                        btn_Update.IsEnabled = true;
                        btn_Delete.IsEnabled = true;
                    }
                }
            }
        }
    }
}
