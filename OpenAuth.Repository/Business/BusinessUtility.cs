using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using OpenAuth.Domain.Business;
using OpenAuth.Domain;

namespace OpenAuth.Repository.Business
{
    public class BusinessUtility : BaseRepository<Resource>
    {
        public BusinessUtility() { }

        public Customer GetCustomerByLoginUserAcct(string acct)
        {
            Customer cust = null;
            string sql = @"select * from customer c where customer_id in (select customer_id from [user] where account = @acct)";
            DBUtility db = new DBUtility();
            DbCommand cmd = base.GetDbCommandObject();

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            db.NewParaWithValue("acct", DbType.String, acct, ref cmd);
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                DbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cust = new Customer();
                    cust.Customer_ID = dr["CUSTOMER_ID"].ToString();
                    cust.Customer_Name = dr["CUSTOMER_NAME"].ToString();
                    cust.Contacts = dr["CONTACTS"].ToString();
                    cust.Contact_Tel = dr["CONTACT_TEL"].ToString();
                    cust.Contact_Mob = dr["CONTACT_MOB"].ToString();
                    cust.Customer_Addr = dr["CUST_ADDRESS"].ToString();
                    cust.Contract_Num = dr["CONTACT_NUMBER"].ToString();
                    cust.Customer_Type = int.Parse(dr["CUST_TYPE"].ToString());
                    cust.Advance_Amt = float.Parse(dr["ADVANCE_AMOUNT"].ToString());
                    cust.Credit = float.Parse(dr["CREDIT"].ToString());
                    cust.Amount_Method = dr["ACCOUNT_METHOD"].ToString();
                    cust.Payment_Method = dr["PAYMENT_METHOD"].ToString();
                    cust.Bank = dr["BANK"].ToString();
                    cust.Bank_Acct = dr["BANK_ACCOUNT"].ToString();
                }
            }
            catch (Exception e) { throw; }
            finally { cmd.Dispose(); }

            return cust;

        }

        public void WriteDataChangeLog(string logType, string tabName, string dataFrom, string dataTo, string changeUser)
        {
            DBUtility db = new DBUtility();
            DbCommand cmd = base.GetDbCommandObject();
            string sql =
@"insert into data_change_log(log_type, table_name, changed_time, changed_by, data_from, data_to)
values(@log_type, @table_name, getdate(), @changed_by, @data_from, @data_to )";
            cmd.CommandText = sql;
            db.NewParaWithValue("log_type", DbType.String, logType, ref cmd);
            db.NewParaWithValue("table_name", DbType.String, tabName, ref cmd);
            db.NewParaWithValue("changed_by", DbType.String, changeUser, ref cmd);
            db.NewParaWithValue("data_from", DbType.String, dataFrom, ref cmd);
            db.NewParaWithValue("data_to", DbType.String, dataTo, ref cmd);

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw; }
            finally { cmd.Dispose(); }
        }
    }
}
