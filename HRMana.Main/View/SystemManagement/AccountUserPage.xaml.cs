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

namespace HRMana.Main.View.SystemManagement
{
    /// <summary>
    /// Interaction logic for AccountUserPage.xaml
    /// </summary>
    public partial class AccountUserPage : Page
    {
        public AccountUserPage()
        {
            InitializeComponent();

            NotificationEvent.Instance.ShowNotificationRequested += async (sender, e) =>
            {
                Storyboard storyboard = FindResource("AccountWindowNotification") as Storyboard;

                if (storyboard != null)
                {
                    storyboard.Begin();

                    await Task.Delay(TimeSpan.FromSeconds(5));

                    storyboard.Stop();
                }

            };
        }
    }
}
