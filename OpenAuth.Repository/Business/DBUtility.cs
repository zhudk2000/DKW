using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace OpenAuth.Repository.Business
{
    public class DBUtility
    {
        public DBUtility() { }

        public void NewParaWithValue(string paraName, DbType paraType, Object paraValue, ref DbCommand cmd)
        {
            DbParameter para1 = cmd.CreateParameter();
            para1.DbType = paraType;
            para1.ParameterName = paraName;
            para1.Value = paraValue;
            cmd.Parameters.Add(para1);

            //return para1;
        }
    }
}
