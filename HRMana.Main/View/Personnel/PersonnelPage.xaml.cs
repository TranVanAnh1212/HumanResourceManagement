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

            List<Personel> list = new List<Personel>()
            {
                new Personel(){ID=9, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=1, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=5, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=12, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=1, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=12, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=3, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=3, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=18, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=1, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=1, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=12, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=13, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=13, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=1, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=15, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=18, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=11, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=14, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
                new Personel(){ID=15, Name="Trần Văn A", Age=18, Address="Thôn a - Xã A - Tỉnh A", Department="Tài chính"},
            };

            NhanSu_Lv.ItemsSource = list;
        }


    }

    public class Personel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }
}
