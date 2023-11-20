using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class TrinhDoDAO
    {
        public int Create_TrinhDo(TrinhDo td)
        {
            try
            {
                if (td == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.TrinhDo.Add(td);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return td.maTrinhDo;
                }
            }
            catch
            {
                return -1;
            }
        }

        public List<NhanVien> GetList_NhanVien_By_MaTrinhDo(int mtd)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                if (mtd > 0)
                {
                    return list = DataProvider.Instance.DBContext.TrinhDo.FirstOrDefault(x => x.maTrinhDo == mtd).NhanVien.ToList();
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

        public bool Delete_TrinhDo(int maTrinhDo)
        {
            try
            {
                if (maTrinhDo <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.TrinhDo.FirstOrDefault(x => x.maTrinhDo == maTrinhDo);
                    if (result == null) return false;
                    else
                    {
                        DataProvider.Instance.DBContext.TrinhDo.Remove(result);
                        DataProvider.Instance.DBContext.SaveChanges() ;
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<TrinhDo> GetList_TrinhDo()
        {
            List<TrinhDo> trinhDos = new List<TrinhDo>();

            try
            {
                return trinhDos = DataProvider.Instance.DBContext.TrinhDo.ToList();

            }
            catch
            {
                return trinhDos;
            }

        }

        public bool Update_TrinhDo(TrinhDo td)
        {
            try
            {
                if (td.maTrinhDo <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.TrinhDo.FirstOrDefault(x => x.maTrinhDo == td.maTrinhDo);
                    result.tenTrinhDo = td.tenTrinhDo; 
                    DataProvider.Instance.DBContext.SaveChanges() ;

                    return true;
                }
            }
            catch { return false; }
        }
    }
}
