using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class TrinhDoDAO
    {
        public List<TrinhDo> GetList_TrinhDo()
        {
            List<TrinhDo> trinhDos = null;

            try
            {
                var result = DataProvider.Instance.DBContext.Database.SqlQuery<TrinhDo>("exec [dbo].[LayDanhSach_TrinhDo]");

                trinhDos = result.ToList();
            }
            catch (Exception ex)
            {
                return trinhDos;
            }

            return trinhDos;

        }
    }
}
