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
            List<ChucVu> chucVu = null;

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<ChucVu>("exec [dbo].[LayDanhSach_ChucVu]").ToList();

                chucVu = result;
            }
            catch (Exception ex)
            {
                return chucVu;
            }

            return chucVu;
        }

        public List<NhanVien> GetList_NhanVien_By_MaChucVu(int mcv)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                if (mcv > 0)
                {
                    return list = DataProvider.Instance.DBContext.ChucVu.FirstOrDefault(x => x.maChucVu == mcv).NhanVien.ToList();
                }
                else
                {
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }

        public ChucVu Get_ChucVu_By_MaChucVu(int id)
        {
            try
            {
                if (id > 0)
                {
                    return DataProvider.Instance.DBContext.ChucVu.FirstOrDefault(x => x.maChucVu == id);
                }
                else { return null; }
            }
            catch { return null; }
        }

        public ChucVu CreateNew_ChucVu(string tenChucVu)
        {
            ChucVu cv = null;

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
                if (string.IsNullOrEmpty(tenChucVu) && maChucVu == 0)
                {
                    return false;
                }
                else
                {
                    var result = DataProvider.Instance.DBContext.ChucVu.Where(x => x.maChucVu == maChucVu).SingleOrDefault();

                    result.tenChucVu = tenChucVu;

                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete_ChucVu(int maChucBu)
        {
            try
            {
                if (maChucBu == 0)
                {
                    return false;
                }
                else
                {
                    var cv = DataProvider.Instance.DBContext.ChucVu.Where(x => x.maChucVu == maChucBu).SingleOrDefault();

                    DataProvider.Instance.DBContext.ChucVu.Remove(cv);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                    
                }
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
