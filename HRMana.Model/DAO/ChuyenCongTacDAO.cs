using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{

    public class ChuyenCongTacDAO
    {
        public List<ChuyenCongTac> GetList_ChuyenCongtac()
        {
            List<ChuyenCongTac> chuyenCongTacs = new List<ChuyenCongTac>();

            try
            {
                return chuyenCongTacs = DataProvider.Instance.DBContext.ChuyenCongTac.ToList();

            }
            catch (Exception ex)
            {
                return chuyenCongTacs;
            }
        }

        public int CreateNew_ChuyenCongTac(ChuyenCongTac cct)
        {
            try
            {
                if (cct == null)
                {
                    return 0;
                }
                else
                {
                    DataProvider.Instance.DBContext.ChuyenCongTac.Add(cct);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return 1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Update_ChuyenCongtac(ChuyenCongTac cct)
        {
            try
            {
                if (cct == null) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.ChuyenCongTac.FirstOrDefault(x => x.soQuyetDinh.Equals(cct.soQuyetDinh));

                    if (result == null) { return false; }
                    else
                    {
                        result.soQuyetDinh = cct.soQuyetDinh;
                        result.ngayQuyetDinh = cct.ngayQuyetDinh;
                        result.thoiGianThiHanh = cct.thoiGianThiHanh;

                        DataProvider.Instance.DBContext.SaveChanges();

                        return true;
                    }
                };
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete_ChuyenCongTac(string soQuyetDinh)
        {
            try
            {
                if (string.IsNullOrEmpty(soQuyetDinh)) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.ChuyenCongTac.FirstOrDefault(x => x.soQuyetDinh.Equals(soQuyetDinh));

                    if (result == null) { return false; }
                    else
                    {
                        DataProvider.Instance.DBContext.ChuyenCongTac.Remove(result);
                        DataProvider.Instance.DBContext.SaveChanges();

                        return true;
                    }
                };
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
