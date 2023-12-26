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

namespace HRMana.Main.View.Contract
{
    /// <summary>
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        public ContractPage()
        {
            InitializeComponent();
            //txtbl_SoHopDongValidate.Visibility = Visibility.Collapsed;


            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                Storyboard storyboard = FindResource("ContractWindowNotification") as Storyboard;

                if (storyboard != null)
                {
                    storyboard.Begin();

                    await Task.Delay(TimeSpan.FromSeconds(5));

                    storyboard.Stop();
                }

            };
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

        //private void txt_SoHopDong_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;

        //    if (textBox.Text.Length <= 0) 
        //    {
        //        txtbl_SoHopDongValidate.Visibility = Visibility.Visible;
        //        btn_Add.IsEnabled = false;
        //    }
        //    else
        //    {
        //        txtbl_SoHopDongValidate.Visibility = Visibility.Collapsed;
        //        btn_Add.IsEnabled = true;
        //    }
        //}

        private void cb_KhongThoiHan_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_KhongThoiHan.IsChecked == true)
            {
                txt_THHD.IsEnabled = false;
                dpNgayKTHD.IsEnabled = false;
            }

            if (rdb_CoThoiHan.IsChecked == true)
            {
                txt_THHD.IsEnabled = true;
                dpNgayKTHD.IsEnabled = true;
            }
        }
    }
}
