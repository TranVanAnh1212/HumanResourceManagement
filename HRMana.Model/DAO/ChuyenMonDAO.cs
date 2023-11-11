using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChuyenMonDAO
    {
        public List<ChuyenMon> GetListChuyenMon()
        {
            List<ChuyenMon> chuyenMon = null;

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<ChuyenMon>("exec [dbo].[LayDanhSach_ChuyenMon]");

                chuyenMon = result.ToList();
            }
            catch (Exception ex)
            {
                return chuyenMon;
            }

            return chuyenMon;
        }
    }
}
