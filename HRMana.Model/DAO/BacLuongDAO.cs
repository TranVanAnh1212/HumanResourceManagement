using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class BacLuongDAO
    {
        public decimal Create_BacLuong(BacLuong bl)
        {
            try
            {
                if (bl == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.BacLuong.Add(bl);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return bl.heSoLuong;
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool Delete_BacLuong(decimal hsl)
        {
            try
            {
                if (hsl <= 0)
                {
                    return false;
                }
                else
                {
                    var result = DataProvider.Instance.DBContext.BacLuong.FirstOrDefault(x => x.heSoLuong == hsl);
                    DataProvider.Instance.DBContext.BacLuong.Remove(result);
                    DataProvider.Instance.DBContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<ChamCong> GetCount_ChamCong_By_MaBacLuong(decimal hsl)
        {
            List<ChamCong> list = new List<ChamCong>();
            try
            {
                if (hsl <= 0)
                {
                    return list;
                }
                else
                {
                    var result = DataProvider.Instance.DBContext.ChamCong.Where(x => x.heSoLuong == hsl);
                    
                    return list = result.ToList();
                }
            }
            catch
            {
                return list;
            }
        }

        public BacLuong Get_BacLuong_By_HeSoLuong(decimal hsl)
        {
            return DataProvider.Instance.DBContext.BacLuong.FirstOrDefault(x => x.heSoLuong == hsl);
        }

        public List<BacLuong> GetList_Luong()
        {
            List<BacLuong> luong = new List<BacLuong>();

            try
            {
                return luong = DataProvider.Instance.DBContext.BacLuong.ToList();
            }
            catch (Exception ex)
            {
                return luong;
            }
        }

        public bool Update_BacLuong(decimal hsl, BacLuong bl)
        {
            try
            {
                if (bl.heSoLuong <= 0)
                {
                    return false;
                }
                else
                {
                    var result = DataProvider.Instance.DBContext.BacLuong.FirstOrDefault(x => x.heSoLuong == hsl);
                    //result.heSoLuong = bl.heSoLuong;
                    result.luongCoBan = bl.luongCoBan;
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
