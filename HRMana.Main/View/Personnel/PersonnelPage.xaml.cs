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
    /// Interaction logic for PersonnelPage.xaml
    /// </summary>
    public partial class PersonnelPage : Page
    {
        public PersonnelPage()
        {
            InitializeComponent();
        }

        private void btn_AddnewPersonel_Click(object sender, RoutedEventArgs e)
        {
            NotificationEvent.Instance.RequestShowPage();
        }
    }
}
