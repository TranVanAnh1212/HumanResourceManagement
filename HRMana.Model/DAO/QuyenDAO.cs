using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRMana.Model.DAO
{
    public class QuyenDAO
    {
        public List<Quyen> GetListQuyen()
        {
            var listQuyen = new List<Quyen>();

            try
            {
                listQuyen = DataProvider.Instance.DBContext.Quyen.ToList();
            }
            catch (Exception ex)
            {
                return listQuyen;
            }

            return listQuyen;
        }

        public List<ChiTietQuyen_Quyen> GetList_CTQQ_By_MaQuyen(string mq)
        {
            var queryResult = DataProvider.Instance.DBContext.ChiTietQuyen_Quyen.Where(x => x.maQuyen.Equals(mq)).ToList();
            return queryResult;
        }

        public bool CreateNew_Quyen(Quyen q)
        {
            try
            {
                DataProvider.Instance.DBContext.Quyen.Add(q);
                DataProvider.Instance.DBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Update_Quyen(Quyen q)
        {
            try
            {
                var quyen = DataProvider.Instance.DBContext.Quyen.FirstOrDefault(x => x.maQuyen.Equals(q.maQuyen));
                quyen.tenQuyen = q.tenQuyen;
                DataProvider.Instance.DBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Delete_Quyen(string maQuyen)
        {
            try
            {
                var quyen = DataProvider.Instance.DBContext.Quyen.FirstOrDefault(x => x.maQuyen.Equals(maQuyen));
                DataProvider.Instance.DBContext.Quyen.Remove(quyen);
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
