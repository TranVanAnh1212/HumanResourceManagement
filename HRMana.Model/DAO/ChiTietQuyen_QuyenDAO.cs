using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRMana.Model.DAO
{
    public class ChiTietQuyen_QuyenDAO
    {
        public bool CreateNew_ChiTietQuyen_Quyen(ChiTietQuyen_Quyen ctqq)
        {
            try
            {
                DataProvider.Instance.DBContext.ChiTietQuyen_Quyen.Add(ctqq);
                DataProvider.Instance.DBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Delete_CTQQ (string maQuyen, string maCTQ)
        {
            try
            {
                var ctqq = DataProvider.Instance.DBContext.ChiTietQuyen_Quyen.FirstOrDefault(x => x.maQuyen.Equals(maQuyen) && x.maChitietQuyen.Equals(maCTQ));

                DataProvider.Instance.DBContext.ChiTietQuyen_Quyen.Remove(ctqq);
                DataProvider.Instance.DBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
