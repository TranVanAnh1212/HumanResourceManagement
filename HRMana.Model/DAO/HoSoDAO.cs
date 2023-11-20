using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class HoSoDAO
    {
        public int CreateNew_HoSo(HoSo hs)
        {
            try
            {
                if (hs != null)
                {
                    DataProvider.Instance.DBContext.HoSo.Add(hs);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return hs.maHoSo;
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

        public HoSo Get_HoSo_By_MaHoSo(int maHoSo)
        {
            try
            {
                return DataProvider.Instance.DBContext.HoSo.FirstOrDefault(x => x.maHoSo == maHoSo);
            }
            catch
            {
                return null;
            }
        }

        public bool Update_HoSo(HoSo hs)
        {
            try
            {
                if (hs != null)
                {
                    var hoSo = DataProvider.Instance.DBContext.HoSo.Where(x => x.maHoSo == hs.maHoSo).FirstOrDefault();
                    hoSo.soYeuLyLich = hs.soYeuLyLich;
                    hoSo.giayKhaiSinh = hs.giayKhaiSinh;
                    hoSo.soHoKhau = hs.soHoKhau;
                    hoSo.bangTotNghiep = hs.bangTotNghiep;
                    hoSo.giayKhamSK = hs.giayKhamSK;
                    hoSo.anhThe = hs.anhThe;
                    hoSo.tinhTrangHoSo = hs.tinhTrangHoSo;
                    hoSo.hinhThucThanhToanLuong = hs.hinhThucThanhToanLuong;
                    hoSo.soTkNganHang = hs.soTkNganHang;
                    hoSo.nganHang = hs.nganHang;
                    hoSo.maSoThue = hs.maSoThue;
                    hoSo.maSoBHXH = hs.maSoBHXH;

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

        public bool Delete_HoSo(int mahs)
        {
            try
            {
                if (mahs != 0)
                {
                    var HoSo = DataProvider.Instance.DBContext.HoSo.Where(x => x.maHoSo == mahs).FirstOrDefault();

                    DataProvider.Instance.DBContext.HoSo.Remove(HoSo);
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
