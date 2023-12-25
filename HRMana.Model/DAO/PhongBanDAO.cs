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
    public class PhongBanDAO
    {
        public List<PhongBan> GetList_PhongBan()
        {
            return DataProvider.Instance.DBContext.PhongBan.ToList();
        }

        public async Task<List<PhongBan>> GetList_PhongBanAsync()
        {
            return await DataProvider.Instance.DBContext.PhongBan.ToListAsync();

        }

        public PhongBan Get_PhongBan_By_MaPhongBan(int id)
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

        public List<NhanVien> GetList_NhanVien_By_MaPhongBan(int mpb)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                if (mpb > 0)
                {
                    return list = DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong == mpb).NhanVien.ToList();
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

        public bool Delete_PhongBan(int mpb)
        {
            try
            {
                if (mpb > 0)
                {
                    var result = DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong == mpb);
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
