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
    }
}
