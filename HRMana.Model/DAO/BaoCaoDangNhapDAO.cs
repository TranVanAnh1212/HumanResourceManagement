using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class BaoCaoDangNhapDAO
    {
        public IEnumerable<BaoCaoDangNhap> GetList_BaoCaoDangNhap()
        {
            try
            {
                return DataProvider.Instance.DBContext.BaoCaoDangNhap.ToList();
            }
            catch
            {
                return null;
            }
        }

        public void Create_BaoCaoDangNhap(int maTaiKhoan, DateTime tgDangNhap, DateTime tgDangXuat)
        {
            try
            {
                var dn = new BaoCaoDangNhap()
                {
                    maTaiKhoan = maTaiKhoan,
                    tgDangNhap = tgDangNhap,
                    tgDangXuat = tgDangXuat
                };

                DataProvider.Instance.DBContext.BaoCaoDangNhap.Add(dn);
                DataProvider.Instance.DBContext.SaveChanges();

            }catch (Exception ex) { }
        }

        public void Create_BaoCaoDangNhap(BaoCaoDangNhap bc)
        {
            try
            {
                if (bc != null)
                {
                    var dn = new BaoCaoDangNhap()
                    {
                        maTaiKhoan = bc.maTaiKhoan,
                        tgDangNhap = bc.tgDangNhap,
                        tgDangXuat = bc.tgDangXuat
                    };

                    DataProvider.Instance.DBContext.BaoCaoDangNhap.Add(dn);
                    DataProvider.Instance.DBContext.SaveChanges();
                }

            }
            catch (Exception ex) { }
        }
    }
}
