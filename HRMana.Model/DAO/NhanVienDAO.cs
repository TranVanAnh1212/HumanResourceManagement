using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class NhanVienDAO
    {
        public List<NhanVien> GetList_NhanVien()
        {
            List<NhanVien> nv = null;

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<NhanVien>("exec [dbo].[LayDanhSach_NhanVien]");

                if (result.Count() > 0)
                {
                    nv = result.ToList();
                }
            }
            catch (Exception ex)
            {
                return nv;
            }
            return nv;
        }

        public NhanVien Get_NhanVien_By_MaNhanVien(int maNhanVien)
        {
            NhanVien nv = null;

            try
            {
                nv = DataProvider.Instance.DBContext.NhanVien.SingleOrDefault(x => x.maNhanVien == maNhanVien);
            }
            catch (Exception ex)
            {
                return nv = null;
            }

            return nv;
        }

        public int CreateNew_NhanVien(NhanVien nhanVien)
        {
            try
            {
                if (nhanVien == null)
                {
                    return -1;
                }
                else
                {
                    DataProvider.Instance.DBContext.NhanVien.Add(nhanVien);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return nhanVien.maNhanVien;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
