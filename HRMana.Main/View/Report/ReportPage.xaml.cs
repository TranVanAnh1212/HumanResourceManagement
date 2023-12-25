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

namespace HRMana.Main.View.Report
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            btn_UnChooseDepartment.Visibility = Visibility.Collapsed;
            btn_TKLNV_UnChooseDepartment.Visibility = Visibility.Collapsed;
        }

        private void cb_TKNV_ChooseDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem != null)
            {
                btn_UnChooseDepartment.Visibility = Visibility.Visible;
            }
            else
            {
                btn_UnChooseDepartment.Visibility = Visibility.Collapsed;
            }
        }

        private void cb_TKLNV_ChooseDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem != null)
            {
                btn_TKLNV_UnChooseDepartment.Visibility = Visibility.Visible;
            }
            else
            {
                btn_TKLNV_UnChooseDepartment.Visibility = Visibility.Collapsed;

            }

        }
    }
}
