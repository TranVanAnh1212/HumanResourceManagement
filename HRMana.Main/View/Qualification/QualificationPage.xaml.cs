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

namespace HRMana.Main.View.Qualification
{
    /// <summary>
    /// Interaction logic for QualificationPage.xaml
    /// </summary>
    public partial class QualificationPage : Page
    {
        public QualificationPage()
        {
            InitializeComponent();
            txtbl_TenTrinhDoValidate.Visibility = Visibility.Visible;
        }


        private void UpperCaseFirstChar(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text;

            if (textBox.Text.Length <= 0)
            {
                txtbl_TenTrinhDoValidate.Visibility = Visibility.Visible;
                btn_Add.IsEnabled = false;
            }
            else
            {
                txtbl_TenTrinhDoValidate.Visibility = Visibility.Visible;
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
