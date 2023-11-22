using HRMana.Common.Commons;
using HRMana.Common.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            txtbl_EmailCNValidate.Visibility = Visibility.Collapsed;
            txtbl_EmailNBValidate.Visibility = Visibility.Collapsed;
            txtbl_BirthdayValidate.Visibility = Visibility.Collapsed;

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

        private void txt_LuongOffer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 0)
            {
                double value = 0;
                double.TryParse(txt_LuongOffer.Text, out value);
                tb.Text = value.ToString("N0");
                tb.CaretIndex = tb.Text.Length;
            }
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

        private void txt_EmailCN_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length == 0)
            {
                btn_CreateNew.IsEnabled = true;
                txtbl_EmailCNValidate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!StringHelper.IsValidEmail(txt.Text))
                {
                    txtbl_EmailCNValidate.Visibility = Visibility.Visible;
                    txtbl_EmailCNValidate.Foreground = new SolidColorBrush(Colors.Red);
                    btn_CreateNew.IsEnabled = false;
                }
                else
                {
                    btn_CreateNew.IsEnabled = true;
                    txtbl_EmailCNValidate.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void txt_EmailNB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length == 0)
            {
                btn_CreateNew.IsEnabled = true;
                txtbl_EmailNBValidate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!StringHelper.IsValidEmail(txt.Text))
                {
                    txtbl_EmailNBValidate.Visibility = Visibility.Visible;
                    txtbl_EmailNBValidate.Foreground = new SolidColorBrush(Colors.Red);
                    btn_CreateNew.IsEnabled = false;
                }
                else
                {
                    btn_CreateNew.IsEnabled = true;
                    txtbl_EmailNBValidate.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void txt_Birthday_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (!StringHelper.IsValidDate(txt.Text, "dd/MM/yyyy"))
            {
                txtbl_BirthdayValidate.Visibility = Visibility.Visible;
                txtbl_BirthdayValidate.Foreground = new SolidColorBrush(Colors.Red);
                btn_CreateNew.IsEnabled = false;
            }
            else
            {
                btn_CreateNew.IsEnabled = true;
                txtbl_BirthdayValidate.Visibility = Visibility.Collapsed;
            }
        }
    }
}
