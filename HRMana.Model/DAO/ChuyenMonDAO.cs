using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChuyenMonDAO
    {
        public List<ChuyenMon> GetListChuyenMon()
        {
            List<ChuyenMon> chuyenMon = null;

            try
            {
                chuyenMon = DataProvider.Instance.DBContext.ChuyenMon.ToList();
            }
            catch (Exception ex)
            {
                return chuyenMon;
            }

            return chuyenMon;
        }

        public List<NhanVien> GetCount_NhanVien_By_MaChuyenMon(int mcm)
        {
            List<NhanVien> result = new List<NhanVien>();

            try
            {
                var cm = DataProvider.Instance.DBContext.ChuyenMon.FirstOrDefault(x => x.maChuyenMon == mcm);
                return result = cm.NhanVien.ToList();
            }
            catch
            {
                return result;
            }
        }

        public int Create_ChuyenMon(ChuyenMon cm)
        {
            try
            {
                if (cm == null) { return 0; }
                else
                {
                    DataProvider.Instance.DBContext.ChuyenMon.Add(cm);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return cm.maChuyenMon;

                }
            }
            catch
            {
                return -1;
            }
        }

        public bool Update_ChuyenMon(ChuyenMon cm)
        {
            try
            {
                if (cm.maChuyenMon <= 0) { return false; }
                else
                {
                    var result = DataProvider.Instance.DBContext.ChuyenMon.FirstOrDefault(x => x.maChuyenMon == cm.maChuyenMon);
                    result.tenChuyenMon = cm.tenChuyenMon;
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete_ChuyenMon(int mcm)
        {
            try
            {
                if (mcm < 0) { return false; }
                else
                {
                    var result = DataProvider.Instance.DBContext.ChuyenMon.FirstOrDefault(x => x.maChuyenMon == mcm);
                    if (result != null)
                    {
                        DataProvider.Instance.DBContext.ChuyenMon.Remove(result);
                        DataProvider.Instance.DBContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch
            {
                return false;
            }
        }
    }
}
