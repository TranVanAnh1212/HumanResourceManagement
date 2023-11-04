using HRMana.Main.View.Contract;
using HRMana.Main.View.Department;
using HRMana.Main.View.Home;
using HRMana.Main.View.Personnel;
using HRMana.Main.View.SystemManagement;
using HRMana.Main.View.TimeKeeping;
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

namespace HRMana.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Directional(Page page)
        {
            mainFrame.Navigate(page);
        }


        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Directional(new HomePage());
        }

        private void listPersonel_Click(object sender, RoutedEventArgs e)
        {
            Directional(new PersonnelPage());
        }

        private void GoHome_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new HomePage());
        }

        private void createAccUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new CreateAccountUserPage());
        }

        private void updateDepartment_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new DepartmentPage());
        }

        private void updateContract_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ContractPage());
        }

        private void timeKeeping_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new TimeKeepingPage());
        }
    }
}
