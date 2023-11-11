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
        public List<BacLuong> GetList_Luong()
        {
            List<BacLuong> luong = null;

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<BacLuong>("exec [dbo].[LayDanhSach_BacLuong]");

                luong = result.ToList();
            }
            catch (Exception ex)
            {
                return luong;
            }

            return luong;
        }
    }
}
