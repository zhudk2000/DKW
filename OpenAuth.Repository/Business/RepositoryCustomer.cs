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
            int newCustID = GetNextCustID();
            string sqlCust = @"
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
            cmd.CommandText = sqlCust;
            db.NewParaWithValue("customer_id", DbType.String, newCustID.ToString(), ref cmd);
            db.NewParaWithValue("customer_name", DbType.String, entity.Customer_Name, ref cmd);
            db.NewParaWithValue("contacts", DbType.String, entity.Contacts, ref cmd);
            db.NewParaWithValue("contact_tel", DbType.String, entity.Contact_Tel, ref cmd);
            db.NewParaWithValue("contact_mob", DbType.String, entity.Contact_Mob, ref cmd);
            db.NewParaWithValue("cust_address", DbType.String, entity.Customer_Addr, ref cmd);
            db.NewParaWithValue("contact_number", DbType.String, entity.Contract_Num, ref cmd);
            db.NewParaWithValue("Cust_type", DbType.String, entity.Customer_Type, ref cmd);
            db.NewParaWithValue("advance_amount", DbType.Decimal, entity.Advance_Amt, ref cmd);
            db.NewParaWithValue("credit", DbType.Decimal, entity.Credit, ref cmd);
            db.NewParaWithValue("account_method", DbType.String, entity.Amount_Method, ref cmd);
            db.NewParaWithValue("payment_method", DbType.String, entity.Payment_Method, ref cmd);
            db.NewParaWithValue("bank", DbType.String, entity.Bank, ref cmd);
            db.NewParaWithValue("bank_account", DbType.String, entity.Bank_Acct, ref cmd);

            string sqlUser = @"
begin
insert into [user](
    id, Account, Password, Name, Sex, 
    Status, Type, CreateTime, CrateId, customer_id
) values(
    newid(), @Account, @Password, @Name, 0, 
    0, 0, getdate(), NULL, @customer_id
)

insert into Relevance(Id, [Description], [Key], [Status], OperateTime, OperatorId, FirstId, SecondId)
select newid(), '', 'UserRole', 0, getdate(), 0, u.Id, r.Id
from [user] u, [role] r
where u.Account = @Account and r.name = '外部客户下单'
end";
            DbTransaction dt = null;
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                dt = cmd.Connection.BeginTransaction();
                cmd.Transaction = dt;
                cmd.ExecuteNonQuery();

                cmd.CommandText = sqlUser;
                cmd.Parameters.Clear();
                db.NewParaWithValue("Account", DbType.String, entity.User_Account, ref cmd);
                db.NewParaWithValue("Password", DbType.String, entity.User_Password, ref cmd);
                db.NewParaWithValue("Name", DbType.String, entity.User_Name, ref cmd);
                db.NewParaWithValue("customer_id", DbType.String, newCustID.ToString(), ref cmd);
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
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
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
            string sql = "select count(*) from [user] where Account = @acct";

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
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
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
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
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
