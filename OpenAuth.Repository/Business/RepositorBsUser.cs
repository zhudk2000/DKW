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
    public class RepositorBsUser : BaseRepository<Resource>
    {
        public String ChangePasswordByAccount(String account, String newPass)
        {
            //
            string sql = @"update [dbo].[User] set password = @newP where account = @acct";
            DbCommand cmd = base.GetDbCommandObject();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            DBUtility db = new DBUtility();

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                db.NewParaWithValue("newP", DbType.String, newPass, ref cmd);
                db.NewParaWithValue("acct", DbType.String, account, ref cmd);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw e; }
            finally { cmd.Dispose(); }

            StringBuilder sb = new StringBuilder();
            sb.Append("user account:").Append(account);
            BusinessUtility bu = new BusinessUtility();
            bu.WriteDataChangeLog("U", "User.PASSWORD", sb.ToString(), "", "自助");

            return "done";
        }
    }
}
