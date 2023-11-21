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
    }
}
