using HRMana.Common.Commons;
using HRMana.Common.Events;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HRMana.Main.View.Personnel
{
    /// <summary>
    /// Interaction logic for CreateNewPersonnelPage.xaml
    /// </summary>
    public partial class CreateNewPersonnelPage : Page
    {
        public CreateNewPersonnelPage()
        {
            InitializeComponent();
            txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;
            //txtbl_BirthdayValidate.Visibility = Visibility.Collapsed;

            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                try
                {
                    Storyboard storyboard = FindResource("CreatePersonnelWindowNotification") as Storyboard;

                    if (storyboard != null)
                    {
                        storyboard.Begin();

                        await Task.Delay(TimeSpan.FromSeconds(5));

                        storyboard.Stop();
                    }
                }
                catch (Exception exep) { }
            };

        }

        private void txt_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length == 0)
            {
                btn_CreateNew.IsEnabled = true;
                txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!StringHelper.IsNumeric(txt.Text))
                {
                    txtbl_PhoneErrorValidate.Text = "Định dạng số điện thoại không đúng!";
                    txtbl_PhoneErrorValidate.Visibility = Visibility.Visible;
                    txtbl_PhoneErrorValidate.Foreground = new SolidColorBrush(Colors.Red);
                    btn_CreateNew.IsEnabled = false;
                }
                else
                {
                    if (txt.Text.Length > 10)
                    {
                        txtbl_PhoneErrorValidate.Visibility = Visibility.Visible;
                        txtbl_PhoneErrorValidate.Foreground = new SolidColorBrush(Colors.Red);
                        btn_CreateNew.IsEnabled = false;
                    }
                    else
                    {
                        btn_CreateNew.IsEnabled = true;
                        txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;

                    }
                }
            }
        }

        //private void txt_Birthday_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox txt = sender as TextBox;

        //    if (txt.Text.Length <= 0)
        //    {
        //        txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //        txtbl_BirthdayValidate.Text = "Ngày sinh không được bỏ trống.";
        //        txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //        btn_CreateNew.IsEnabled = false;
        //    }
        //    else
        //    {

        //        if (!StringHelper.IsValidDate(txt.Text, "dd/MM/yyyy"))
        //        {
        //            txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //            txtbl_BirthdayValidate.Text = "Định dạng ngày tháng năm không đúng.";
        //            txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //            btn_CreateNew.IsEnabled = false;
        //        }
        //        else
        //        {
        //            if (DateTime.Now.Year - Convert.ToDateTime(txt.Text).Year < 18)
        //            {
        //                txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //                txtbl_BirthdayValidate.Text = "Nhân viên phải có số tuổi lớn hơn 18.";
        //                txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //                btn_CreateNew.IsEnabled = false;

        //                return;
        //            }

        //            if (DateTime.Now < Convert.ToDateTime(txt.Text))
        //            {
        //                txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //                txtbl_BirthdayValidate.Text = "Ngày tháng năm sinh phải bé hơn ngày táng hiện tại.";
        //                txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //                btn_CreateNew.IsEnabled = false;

        //                return;
        //            }


        //            btn_CreateNew.IsEnabled = true;
        //            txtbl_BirthdayValidate.Visibility = Visibility.Collapsed;
        //        }
        //    }
        //}

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

        private void Format_HoTen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string inputText = textBox.Text;

                // Chuyển đổi văn bản thành chữ cái đầu viết hoa của mỗi từ
                string result = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText.ToLower());

                // Kiểm tra xem văn bản nhập vào có thay đổi không trước khi gán giá trị mới
                if (result != textBox.Text)
                {
                    // Ngăn không cho sự kiện TextChanged lặp vô hạn khi gán giá trị cho TextBox
                    textBox.TextChanged -= Format_HoTen_TextChanged;

                    // Gán giá trị đã được xử lý vào TextBox
                    textBox.Text = result;

                    // Di chuyển con trỏ đến cuối TextBox
                    textBox.SelectionStart = textBox.Text.Length;

                    // Thêm lại sự kiện TextChanged
                    textBox.TextChanged += Format_HoTen_TextChanged;
                }
            }
        }
    }
}
