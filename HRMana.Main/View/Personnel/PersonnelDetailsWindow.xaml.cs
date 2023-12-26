using HRMana.Common.Commons;
using HRMana.Main.ViewModel;
using HRMana.Model.DAO;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HRMana.Main.View.Personnel
{
    /// <summary>
    /// Interaction logic for PersonnelDetailsWindow.xaml
    /// </summary>
    public partial class PersonnelDetailsWindow : Window
    {
        public PersonnelDetailsWindow()
        {
            InitializeComponent();
            txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;
        }

        public PersonnelDetailsWindow(string maNhanVien)
        {
            InitializeComponent();
            this.DataContext = new PersonnelDetailsViewModel(maNhanVien);
        }

        private void txt_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length == 0)
            {
                btn_Update.IsEnabled = true;
                txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!StringHelper.IsNumeric(txt.Text))
                {
                    txtbl_PhoneErrorValidate.Text = "Định dạng số điện thoại không đúng!";
                    txtbl_PhoneErrorValidate.Visibility = Visibility.Visible;
                    txtbl_PhoneErrorValidate.Foreground = new SolidColorBrush(Colors.Red);
                    btn_Update.IsEnabled = false;
                }
                else
                {
                    if (txt.Text.Length > 11)
                    {
                        txtbl_PhoneErrorValidate.Visibility = Visibility.Visible;
                        txtbl_PhoneErrorValidate.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    }
                    else
                    {
                        txtbl_PhoneErrorValidate.Visibility = Visibility.Collapsed;
                    }
                }
            }

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

        private void PsnlDetailWindow_Closing(object sender, CancelEventArgs e)
        {
            PersonnelPage p = new PersonnelPage();
            ((PersonnelViewModel)p.DataContext).SelectedNhanVien = null;
        }

        //private void txt_Birthday_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox txt = sender as TextBox;

        //    if (txt.Text.Length <= 0)
        //    {
        //        txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //        txtbl_BirthdayValidate.Text = "Ngày sinh không được bỏ trống.";
        //        txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //        btn_Update.IsEnabled = false;
        //    }
        //    else
        //    {
        //        if (!StringHelper.IsValidDate(txt.Text, "dd/MM/yyyy"))
        //        {
        //            txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //            txtbl_BirthdayValidate.Text = "Định dạng ngày tháng năm không đúng.";
        //            txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //            btn_Update.IsEnabled = false;
        //        }
        //        else
        //        {

        //            if (DateTime.Now.Year - Convert.ToDateTime(txt.Text).Year < 18)
        //            {
        //                txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //                txtbl_BirthdayValidate.Text = "Nhân viên phải có số tuổi lớn hơn 18.";
        //                txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //                btn_Update.IsEnabled = false;

        //                return;
        //            }

        //            if (DateTime.Now < Convert.ToDateTime(txt.Text))
        //            {
        //                txtbl_BirthdayValidate.Visibility = Visibility.Visible;
        //                txtbl_BirthdayValidate.Text = "Ngày tháng năm sinh phải bé hơn ngày táng hiện tại.";
        //                txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
        //                btn_Update.IsEnabled = false;

        //                return;
        //            }


        //            btn_Update.IsEnabled = true;
        //            txtbl_BirthdayValidate.Visibility = Visibility.Collapsed;
        //        }

        //    }
        //}
    }
}
