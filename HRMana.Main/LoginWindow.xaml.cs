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
using System.Windows.Shapes;

namespace HRMana.Main
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                try
                {
                    Storyboard notificationStoryboard = FindResource("NotificationStoryboard") as Storyboard;

                    if (notificationStoryboard != null)
                    {
                        notificationStoryboard.Begin();

                        await Task.Delay(TimeSpan.FromSeconds(3));

                        notificationStoryboard.Stop();
                    }
                }
                catch (Exception) { }
            };

            NotificationEvent.Instance.HideNotificationRequested += Instance_HideNotificationRequested;
        }

        private void Instance_HideNotificationRequested(object sender, EventArgs e)
        {
            try
            {
                Storyboard notificationStoryboard = FindResource("NotificationStoryboard") as Storyboard;

                if (notificationStoryboard != null)
                {
                    notificationStoryboard.Stop();
                }
            }
            catch (Exception) { }
        }
    }
}
