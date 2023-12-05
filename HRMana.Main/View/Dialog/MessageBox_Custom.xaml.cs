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
using System.Windows.Shapes;

namespace HRMana.Main.View.Dialog
{
    /// <summary>
    /// Interaction logic for MessageBox_Custom.xaml
    /// </summary>
    public partial class MessageBox_Custom : Window
    {
        public MessageBox_Custom()
        {
            InitializeComponent();
           
        }

        public string MsgBox_Content { get => txtbl_MsgBoxContent.Text; set => txtbl_MsgBoxContent.Text = value; }

        public ImageSource Img_MsgIcon { get => img_MsgIcon.Source; set => img_MsgIcon.Source = value; }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
