using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class TonGiaoDAO
    {
        public List<TonGiao> GetList_TonGiao()
        {
            List<TonGiao> list = null;

            try
            {
                list = DataProvider.Instance.DBContext.Database.SqlQuery<TonGiao>("exec [dbo].[LayDanhSach_TonGiao]").ToList();
            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
    }
}
