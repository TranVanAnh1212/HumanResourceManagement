using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChucVuDAO
    {
        public List<ChucVu> GetListChucVu()
        {
            List<ChucVu> chucVu = new List<ChucVu>();

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<ChucVu>("exec [dbo].[LayDanhSach_ChucVu]");

                chucVu = result.ToList();
            }
            catch (Exception ex)
            {
                return chucVu;
            }

            return chucVu;
        }

        public ChucVu CreateNew_ChucVu(string tenChucVu)
        {
            var cv = new ChucVu();

            if (!string.IsNullOrEmpty(tenChucVu))
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<ChucVu>($"exec [dbo].[TaoMoi_ChucVu] N'{tenChucVu}'");

                cv = result.SingleOrDefault();
            }

            return cv;
        }

        public bool Update_ChucVu(int maChucVu, string tenChucVu)
        {
            try
            {
                var result = DataProvider.Instance.DBContext.ChucVu.Where(x => x.maChucVu == maChucVu).SingleOrDefault();

                result.tenChucVu = tenChucVu;

                DataProvider.Instance.DBContext.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
