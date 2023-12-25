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

namespace HRMana.Main.View.TimeKeeping
{
    /// <summary>
    /// Interaction logic for TimeKeepingPage.xaml
    /// </summary>
    public partial class TimeKeepingPage : Page
    {
        public TimeKeepingPage()
        {
            InitializeComponent();
        }

        private void Format_Salary_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 0)
            {
                double value = 0;
                double.TryParse(tb.Text, out value);
                tb.Text = value.ToString("N0");
                tb.CaretIndex = tb.Text.Length;
            }
        }
    }
}
