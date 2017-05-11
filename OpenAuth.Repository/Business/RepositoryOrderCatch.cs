using System;
using OpenAuth.Domain.Business;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Common;
using System.Data;
using System.Text;
using OpenAuth.Repository;

namespace OpenAuth.Repository.Business
{
    public class RepositoryOrderCatch : BaseRepository<Resource>
    {
        public string abc()
        {
            return "ABC";
        }

        public string GetNextOrderNumber()
        {
            string result = "0001";
            string sql =
@"select convert(varchar, getdate(), 112) + right('0000' + convert(varchar, convert(numeric, isnull(max(right(order_id, 4)), '0')) + 1), 4) 
from order_head 
where order_date >= left(convert(varchar, getdate(), 120), 10) 
	and order_date < left(convert(varchar, dateadd(d, 1, getdate()), 120), 10)";

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                try
                {
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    result = cmd.ExecuteScalar().ToString();
                }
                catch (Exception e) { throw; }
                finally
                {
                    cmd.Dispose();
                }
            }
            return result;
        }
    }
}
