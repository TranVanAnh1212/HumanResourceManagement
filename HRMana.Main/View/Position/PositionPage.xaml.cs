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

namespace HRMana.Main.View.Position
{
    /// <summary>
    /// Interaction logic for PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Page
    {
        public PositionPage()
        {
            InitializeComponent();
            txtbl_TenChucVuValidate.Visibility = Visibility.Visible;

            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                try
                {
                    Storyboard stb = FindResource("PositionPageNotification") as Storyboard;

                    if (stb != null)
                    {
                        stb.Begin();

                        await Task.Delay(TimeSpan.FromSeconds(3));

                        stb.Stop();
                    }
                }
                catch (Exception exep) { }
            };

        }

        private void UpperCaseFirstChar(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text;

            if (textBox.Text.Length <= 0)
            {
                txtbl_TenChucVuValidate.Visibility = Visibility.Visible;
                btn_Add.IsEnabled = false;
            }
            else
            {
                txtbl_TenChucVuValidate.Visibility = Visibility.Collapsed;
                btn_Add.IsEnabled = true;
            }

            if (!string.IsNullOrEmpty(value))
            {
                textBox.Text = char.ToUpper(value[0]) + value.Substring(1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
}
