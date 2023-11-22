using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class TonGiaoDAO
    {
        public int Create_TonGiao(TonGiao tg)
        {
            try
            {
                if (tg == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.TonGiao.Add(tg);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return tg.maTonGiao;
                }
            }
            catch
            {
                return -1;
            }
        }

        public List<NhanVien> GetList_NhanVien_By_MaTonGiao(int mtg)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                if (mtg > 0)
                {
                    return list = DataProvider.Instance.DBContext.TonGiao.FirstOrDefault(x => x.maTonGiao == mtg).NhanVien.ToList();
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

        public bool Delete_TonGiao(int maTonGiao)
        {
            try
            {
                if (maTonGiao <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.TonGiao.FirstOrDefault(x => x.maTonGiao == maTonGiao);
                    if (result == null) return false;
                    else
                    {
                        DataProvider.Instance.DBContext.TonGiao.Remove(result);
                        DataProvider.Instance.DBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<TonGiao> GetList_TonGiao()
        {
            List<TonGiao> list = null;

            try
            {
                list = DataProvider.Instance.DBContext.TonGiao.ToList();
            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public bool Update_TonGiao(TonGiao cm)
        {
            try
            {
                if (cm.maTonGiao <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.TonGiao.FirstOrDefault(x => x.maTonGiao == cm.maTonGiao);
                    result.tenTonGiao = cm.tenTonGiao;
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch { return false; }
        }
    }
}
