using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRMana.Model.DAO
{
    public class PhongBanDAO
    {
        public List<PhongBan> GetList_PhongBan()
        {
            List<PhongBan> phongBans = new List<PhongBan>();

            try
            {
                phongBans = DataProvider.Instance.DBContext.PhongBan.OrderBy(x => x.tenPhong).ToList();
            }
            catch (Exception ex)
            {
                return phongBans;
            }

            return phongBans;
        }

        public PhongBan Get_PhongBan_By_MaPhongBan(int id)
        {
            try
            {
                if (id > 0)
                {
                    return DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong == id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int CreateNew_PhongBan(PhongBan p)
        {
            try
            {
                if (p == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.PhongBan.Add(p);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return p.maPhong;
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool Update_PhongBan(int mpb, string tpb, string sdt)
        {
            try
            {
                if (mpb != 0 && !string.IsNullOrEmpty(tpb))
                {
                    var result = DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong == mpb);
                    result.tenPhong = tpb.Trim();
                    result.dienThoai = sdt.Trim();

                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete_PhongBan(PhongBan p)
        {
            try
            {
                if (p != null)
                {
                    var result = DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong == p.maPhong);
                    DataProvider.Instance.DBContext.PhongBan.Remove(result);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
