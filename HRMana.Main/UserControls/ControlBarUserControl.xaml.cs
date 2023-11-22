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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRMana.Main.UserControls
{
    /// <summary>
    /// Interaction logic for ControlBarUserControl.xaml
    /// </summary>
    public partial class ControlBarUserControl : UserControl
    {
        public ControlBarUserControl()
        {
            InitializeComponent();
            normal.Visibility = Visibility.Collapsed;
        }

        private void btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (normal.Visibility == Visibility.Collapsed)
            {
                maximize.Visibility = Visibility.Collapsed;
                normal.Visibility = Visibility.Visible;
            }
            else
            {
                maximize.Visibility = Visibility.Visible;
                normal.Visibility = Visibility.Collapsed;
            }
        }
    }
}
