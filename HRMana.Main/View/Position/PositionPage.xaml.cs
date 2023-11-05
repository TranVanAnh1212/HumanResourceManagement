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
    }
}
