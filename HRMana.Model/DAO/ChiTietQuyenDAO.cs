using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChiTietQuyenDAO
    {
        public List<Chitiet_Quyen> GetList_ChiTietQuyen()
        {
            try
            {
                return DataProvider.Instance.DBContext.Chitiet_Quyen.ToList();
            }
            catch {
                return null;
            }
        }
    }
}
