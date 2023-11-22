using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class DanTocDAO
    {
        public int Create_DanToc(DanToc dt)
        {
            try
            {
                if (dt == null) { return 0; }
                else
                {
                    DataProvider.Instance.DBContext.DanToc.Add(dt);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return dt.maDanToc;
                }
            }
            catch
            {
                return -1;
            }
        }

        public List<NhanVien> GetList_NhanVien_By_MaDanToc(int mdt)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                if (mdt > 0)
                {
                    return list = DataProvider.Instance.DBContext.DanToc.FirstOrDefault(x => x.maDanToc == mdt).NhanVien.ToList();
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

        public bool Delete_DanToc(int maDanToc)
        {
            try
            {
                if (maDanToc <= 0) { return false; }
                else
                {
                    var dt = DataProvider.Instance.DBContext.DanToc.FirstOrDefault(x => x.maDanToc == maDanToc);

                    DataProvider.Instance.DBContext.DanToc.Remove(dt);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<DanToc> GetList_DanToc()
        {
            List<DanToc> list = null;

            try
            {
                list = DataProvider.Instance.DBContext.DanToc.ToList();
            }
            catch(Exception ex)
            {
                return list;
            }

            return list;
        }

        public bool Update_DanToc(DanToc dt)
        {
            try
            {
                if (dt.maDanToc <= 0) { return false; }
                else
                {
                    var result = DataProvider.Instance.DBContext.DanToc.FirstOrDefault(x => x.maDanToc == dt.maDanToc);
                    result.tenDanToc = dt.tenDanToc;
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
