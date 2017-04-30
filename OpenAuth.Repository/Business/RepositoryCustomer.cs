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
/// <summary>
/// 客户管理后台与数据库交互部分
/// </summary>
namespace OpenAuth.Repository.Business
{
    public class RepositoryCustomer : BaseRepository<Resource>
    {
        public void Add(Customer entity)
        {
            string sql = @"
insert into customer(
    customer_id, customer_name, contacts, 
    contact_tel, contact_mob, cust_address, contact_number, 
    Cust_type, advance_amount, credit, 
    account_method, payment_method, bank, bank_account
) values(
    @customer_id, @customer_name, @contacts, 
    @contact_tel, @contact_mob, @cust_address, @contact_number, 
    @cust_type, @advance_amount, @credit, 
    @account_method, @payment_method, @bank, @bank_account
)";
            DBUtility db = new DBUtility();
            DbCommand cmd = base.GetDbCommandObject();
            cmd.CommandText = sql;
            db.NewParaWithValue("customer_id", DbType.String, GetNextCustID(), ref cmd);

            DbTransaction dt = null;
            try
            {
                cmd.Connection.Open();
                dt = cmd.Connection.BeginTransaction();
                cmd.ExecuteNonQuery();

                dt.Commit();
            }
            catch (Exception e) {
                if (dt != null) dt.Rollback();
                throw;
            }
            finally
            {
                if (dt != null) dt = null;
                cmd.Dispose();
            }
            //System.Diagnostics.Debug.WriteLine("1111111");
        }

        public int GetNextCustID()
        {
            int result = 0;
            string sql = "select max(convert(int, customer_id)) as t from customer";

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.Connection.Open();
                    string s = cmd.ExecuteScalar().ToString();
                    if (s != string.Empty)
                        result = int.Parse(s.Trim()) + 1;
                    else
                        result = 1;
                }
                catch (Exception e) { throw; }
                finally
                {
                    cmd.Dispose();
                }
            }
            return result;
        }

        public int IsUseridExist(string usrName)
        {
            int result = 0;
            string sql = "select count(*) from user where Account = @acct";

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                DbParameter para1 = cmd.CreateParameter();
                para1.ParameterName = "acct";
                para1.DbType = DbType.String;
                para1.Value = usrName;
                cmd.Parameters.Add(para1);

                try
                {
                    cmd.Connection.Open();
                    result = int.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception e) { }
                finally
                {
                    cmd.Dispose();
                }
            }

            return result;
        }

        public int IsCustomerNameExist(string custName)
        {
            int result = 0;
            string sql = "select count(*) from customer where customer_name = @cName";

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                
                DbParameter para1 = cmd.CreateParameter();
                para1.ParameterName = "cName";
                para1.DbType = DbType.String;
                para1.Value = custName;
                cmd.Parameters.Add(para1);

                try
                {
                    cmd.Connection.Open();
                    result = int.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception e) { }
                finally {
                    cmd.Dispose();
                }
            }

                return result;
        }


    }
}
