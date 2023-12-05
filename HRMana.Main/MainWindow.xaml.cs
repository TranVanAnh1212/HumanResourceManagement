
﻿using HRMana.Common.Events;
using HRMana.Main.View.Contract;
using HRMana.Main.View.Department;
using HRMana.Main.View.Expertise;
using HRMana.Main.View.Home;
using HRMana.Main.View.Informations;
using HRMana.Main.View.People;
using HRMana.Main.View.Personnel;
using HRMana.Main.View.Position;
using HRMana.Main.View.Profile;
using HRMana.Main.View.Qualification;
using HRMana.Main.View.Religion;
using HRMana.Main.View.Report;
using HRMana.Main.View.Salary;
using HRMana.Main.View.Search;
using HRMana.Main.View.SystemManagement;
using HRMana.Main.View.TimeKeeping;
using HRMana.Main.View.WorkingRotation;
using System;
using System.Windows;
using System.Windows.Controls;

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

        private void TimeKeeping_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new TimeKeepingPage());
        }

        private void department_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new DepartmentPage());

        }

        private void contract_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ContractPage());

        }

        private void qualificationItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new QualificationPage());
        }

        private void expertiseItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ExpertisePage());
        }

        private void peopleItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new PeoplePage());
        }

        private void religionItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ReligionPage());
        }

        private void salaryItem_Click(object sender, RoutedEventArgs e)
        {
            Directional(new SalaryPage());
        }

        private void quantification_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new QualificationPage());

        }

        private void search_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new SearchPage());

        }

        private void changedPassword_Item_Click(object sender, RoutedEventArgs e)
        {
            var changePassWindow = new ChangedPasswordWindow();
            changePassWindow.ShowDialog();
        }

        private void profile_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ProfileEmployeePage());
        }

        private void ExportReport_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new ReportPage());
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

        private void about_Item_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutWindow();
            about.ShowDialog();
        }

        private void contactTechnician_Item_Click(object sender, RoutedEventArgs e)
        {
            var contact = new ContactTechnicianWindow();
            contact.ShowDialog();
        }

        private void accountAuthentication_Item_Click(object sender, RoutedEventArgs e)
        {
            Directional(new AccountAuthenticationPage());
        }
    }
}
