
﻿using HRMana.Common.Commons;
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

namespace HRMana.Main.View.Department
{
    /// <summary>
    /// Interaction logic for DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        public DepartmentPage()
        {
            InitializeComponent();
        }

        private void UpperCaseFirstChar(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text;

            if (!string.IsNullOrEmpty(value))
            {
                textBox.Text = char.ToUpper(value[0]) + value.Substring(1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void txt_dienThoai_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;

            if (!StringHelper.IsPhoneNumber(txt.Text))
            {
                txt.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 4, 4));
                txt.CaretBrush = new SolidColorBrush(Color.FromArgb(100, 255, 4, 4));
                txt.SelectionBrush = new SolidColorBrush(Color.FromArgb(100, 255, 4, 4));
            }
            else
            {
                txt.BorderBrush = new SolidColorBrush(Color.FromArgb(54, 0, 0, 0));
                txt.CaretBrush = new SolidColorBrush(Color.FromArgb(100, 103, 58, 183));
                txt.SelectionBrush = new SolidColorBrush(Color.FromArgb(100, 179, 157, 219));

            }
        }
    }
}
