using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChuyenCongTac_NhanVienDAO
    {
        public int CreateNew_ChuyenCongTacNhanVien(ChuyenCongTac_NhanVien cct_nv)
        {
            try
            {
                if (cct_nv == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.ChuyenCongTac_NhanVien.Add(cct_nv);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Update_ChuyenCongTacNhanVien(ChuyenCongTac_NhanVien cct_nv)
        {
            try
            {
                if (cct_nv != null)
                {
                    var result = DataProvider.Instance.DBContext.ChuyenCongTac_NhanVien.FirstOrDefault(x => x.soQuyetDinh.Equals(cct_nv.soQuyetDinh) && x.maNhanVien == cct_nv.maNhanVien);

                    if (result == null) return false;
                    else
                    {
                        result.soQuyetDinh = cct_nv.soQuyetDinh;
                        result.maNhanVien = cct_nv.maNhanVien;
                        result.chucVuCu = cct_nv.chucVuCu;
                        result.chucVuMoi = cct_nv.chucVuMoi;
                        result.phongBanCu = cct_nv.phongBanCu;
                        result.phongBanMoi = cct_nv.phongBanMoi;

                        DataProvider.Instance.DBContext.SaveChanges();

                        return true;
                    }
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

        public bool Delete_ChuyenCongTacNhanVien(string soQuyetDinh, string maNhanVien)
        {
            if (soQuyetDinh != null && !string.IsNullOrEmpty(maNhanVien))
            {
                var result = DataProvider.Instance.DBContext
                    .ChuyenCongTac_NhanVien
                    .FirstOrDefault(x => x.soQuyetDinh.Equals(soQuyetDinh) && x.maNhanVien.Equals(maNhanVien));

                if (result != null)
                {
                    DataProvider.Instance.DBContext.ChuyenCongTac_NhanVien.Remove(result);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
    }
}
