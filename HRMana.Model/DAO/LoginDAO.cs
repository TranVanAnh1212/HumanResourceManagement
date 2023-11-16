
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class LoginDAO
    {
        public TaiKhoan CheckLogin(string usename, string password)
        {
            TaiKhoan taiKhoan = null;

            if (string.IsNullOrEmpty(usename) && string.IsNullOrEmpty(password))
            {
                return taiKhoan;
            }
            else
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<TaiKhoan>($"exec DangNhap_Proc '{usename}', '{password}'");

                if (result.Count() > 0)
                {
                    taiKhoan = result.FirstOrDefault();
                    return taiKhoan;
                }

            }

            return taiKhoan;
        }

        public TaiKhoan Get_TaiKhoan_By_MaTK(int maTK)
        {
            TaiKhoan taiKhoan = null;

            if (maTK <= 0)
            {
                return taiKhoan;
            }
            else
            {
                var result = DataProvider.Instance.DBContext.TaiKhoan.Where(x => x.maTaiKhoan == maTK);

                if (result.Count() > 0)
                {
                    taiKhoan = result.FirstOrDefault();
                    return taiKhoan;
                }

            }

            return taiKhoan;
        }
    }
}
