<<<<<<< HEAD
﻿using HRMana.Common.Events;
=======
﻿using HRMana.Main.View.Contract;
using HRMana.Main.View.Department;
>>>>>>> Nguyen-Viet-Anh
using HRMana.Main.View.Home;
using HRMana.Main.View.Personnel;
using HRMana.Main.View.Position;
using HRMana.Main.View.SystemManagement;
<<<<<<< HEAD
using HRMana.Main.View.WorkingRotation;
=======
using HRMana.Main.View.TimeKeeping;
>>>>>>> Nguyen-Viet-Anh
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

            NotificationEvent.Instance.ShowPageRequested += Instance_ShowPageRequested;
        }

        private void Instance_ShowPageRequested(object sender, EventArgs e)
        {
            try
            {
                Directional(new CreateNewPersonnelPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Directional(Page page)
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
            Directional(new AccountUserPage());
        }

        private void positionItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new PositionPage());
        }

        private void workingRotationItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new WorkingRotationPage());
        }

        private void createPersonnel_item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new CreateNewPersonnelPage());

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
