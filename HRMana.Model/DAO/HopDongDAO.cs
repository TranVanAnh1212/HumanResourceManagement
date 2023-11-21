using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class HopDongDAO
    {
        public List<HopDong> GetList_HopDong()
        {
            List<HopDong> list = new List<HopDong>();

            try
            {
                return list = DataProvider.Instance.DBContext.HopDong.ToList();
            }
            catch
            {
                return list;
            }

        }

        public int CreateNew_HopDong(HopDong hd)
        {
            try
            {
                if (hd != null)
                {
                    DataProvider.Instance.DBContext.HopDong.Add(hd);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return hd.maHopDong;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }

        public HopDong Get_HopDong_By_MaHopDong(int maHopDong)
        {
            try
            {
                return DataProvider.Instance.DBContext.HopDong.FirstOrDefault(x => x.maHopDong == maHopDong);
            }
            catch
            {
                return null;
            }
        }

        public bool Update_HopDong(HopDong hd)
        {
            try
            {
                if (hd != null)
                {
                    var hopdong = DataProvider.Instance.DBContext.HopDong.Where(x => x.maHopDong == hd.maHopDong).FirstOrDefault();
                    hopdong.soHopDong = hd.soHopDong;
                    hopdong.ngayKyHD = hd.ngayKyHD;
                    hopdong.ngayKetThucHD = hd.ngayKetThucHD;
                    hopdong.loaiHopDong = hd.loaiHopDong;
                    hopdong.thoiHanHD = hd.thoiHanHD;
                    hopdong.tinhTrangChuKi = hd.tinhTrangChuKi;                    
                    
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

        public bool Delete_HopDong(int mahd)
        {
            try
            {
                if (mahd != 0)
                {
                    var hopdong = DataProvider.Instance.DBContext.HopDong.Where(x => x.maHopDong == mahd).FirstOrDefault();

                    DataProvider.Instance.DBContext.HopDong.Remove(hopdong);
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
