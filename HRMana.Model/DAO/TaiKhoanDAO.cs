using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class TaiKhoanDAO
    {
        public async Task<IEnumerable<TaiKhoan>> GetListTaiKhoanAsync()
        {
            IEnumerable<TaiKhoan> listTaiKhoan = await DataProvider.Instance.DBContext.TaiKhoan.ToListAsync();
            return listTaiKhoan;
        }

        public List<TaiKhoan> GetListTaiKhoan()
        {
            var listTaiKhoan = DataProvider.Instance.DBContext.TaiKhoan.ToList();
            return listTaiKhoan;
        }

        public TaiKhoan Get_TaiKhoan_By_MaTaiKhoan(int mtk)
        {
            return DataProvider.Instance.DBContext.TaiKhoan.FirstOrDefault(x => x.maTaiKhoan == mtk);
        }

        public bool ChangePassword (TaiKhoan tk)
        {
            try
            {
                if (tk != null)
                {
                    var result = DataProvider.Instance.DBContext.TaiKhoan.FirstOrDefault(x => x.maTaiKhoan == tk.maTaiKhoan);

                    if (result != null)
                    {
                        result.matKhau = tk.matKhau;
                        DataProvider.Instance.DBContext.SaveChanges();

                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public TaiKhoan Create_TaiKhoan(string tenTaiKhoan, string matKhau, string maQuyen, bool trangThai)
        {
            var taiKhoan = new TaiKhoan();

            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(maQuyen))
            {
                return taiKhoan;
            }
            else
            {
                try
                {
                    taiKhoan = DataProvider.Instance.DBContext.Database.SqlQuery<TaiKhoan>($"EXEC [dbo].[TaoMoiTaiKhoan] '{tenTaiKhoan}', '{matKhau}', {maQuyen}, {trangThai}").FirstOrDefault();
                    return taiKhoan;
                }
                catch (Exception ex)
                {

                }
            }

            return taiKhoan;
        }

        public bool Delete_TaiKhoan(int maTaiKhoan)
        {
            if (maTaiKhoan == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    //var result = DataProvider.Instance.DBContext.Database.SqlQuery<TaiKhoan>($"exec XoaTaiKhoan {maTaiKhoan}").Count();

                    //if (result > 0)
                    //{
                    //    return true;
                    //}
                    //else return false;

                    var result = DataProvider.Instance.DBContext.TaiKhoan.Where(x => x.maTaiKhoan == maTaiKhoan).FirstOrDefault();
                    DataProvider.Instance.DBContext.TaiKhoan.Remove(result);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;

                } catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Block_TaiKhoan(int maTaiKhoan)
        {
            if ( maTaiKhoan == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    var result = DataProvider.Instance.DBContext.TaiKhoan.Where(x => x.maTaiKhoan == maTaiKhoan).FirstOrDefault();
                    result.trangThai = false;
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                catch ( Exception ex )
                {
                    return false;
                }
            }
        }

        public bool Unblock_TaiKhoan(int maTaiKhoan)
        {
            if (maTaiKhoan == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    var result = DataProvider.Instance.DBContext.TaiKhoan.Where(x => x.maTaiKhoan == maTaiKhoan).FirstOrDefault();
                    result.trangThai = true;
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
