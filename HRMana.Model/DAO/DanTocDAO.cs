using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class DanTocDAO
    {
        public List<DanToc> GetList_DanToc()
        {
            List<DanToc> list = null;

            try
            {
                list = DataProvider.Instance.DBContext.Database.SqlQuery<DanToc>("exec [dbo].[LayDanhSach_DanToc]").ToList();
            }
            catch(Exception ex)
            {
                return list;
            }

            return list;
        }
    }
}
