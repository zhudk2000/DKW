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
    }
}
