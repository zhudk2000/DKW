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

        public void SaveOrderCatch(OrderHeader view)
        {
            string ord_id = GetNextOrderNumber();
            string sqlH =
@"insert into order_head(
    order_id, customer_id, customer_name, contacts, 
    contact_tel, contact_address, order_date, contract_id, 
    sales_name
  )values(
    '" + ord_id + @"', @customer_id, @customer_name, @contacts, 
    @contact_tel, @contact_address, @order_date, @contract_id, 
    @sales_name
  )";
            string sqlD =
@"insert into order_details(
    order_line_id, order_id, category_id, quantity, 
    unit_uom, unit_quantity, unit_price, amount, 
    spec, service_item, storage
  ) values(
    @order_line_id, '" + ord_id + @"', @category_id, @quantity, 
    @unit_uom, @unit_quantity, @unit_price, @amount, 
    @spec, @service_item, @storage
  )";
            DbCommand cmd = base.GetDbCommandObject();
            DbTransaction dt = null;
            cmd.CommandText = sqlH;
            cmd.CommandType = CommandType.Text;
            DBUtility db = new DBUtility();
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                dt = cmd.Connection.BeginTransaction();
                cmd.Transaction = dt;

                db.NewParaWithValue("customer_id", DbType.String, view.Customer_id, ref cmd);
                db.NewParaWithValue("customer_name", DbType.String, view.Customer_name, ref cmd);
                db.NewParaWithValue("contacts", DbType.String, view.Contacts, ref cmd);
                db.NewParaWithValue("contact_tel", DbType.String, view.Contact_tel, ref cmd);
                db.NewParaWithValue("contact_address", DbType.String, view.Contact_address, ref cmd);
                db.NewParaWithValue("order_date", DbType.DateTime, view.Order_date, ref cmd);
                db.NewParaWithValue("contract_id", DbType.String, view.Contract_id == null ? DBNull.Value.ToString() : view.Contract_id, ref cmd);
                db.NewParaWithValue("sales_name", DbType.String, view.Sales_name == null ? DBNull.Value.ToString() : view.Sales_name, ref cmd);

                cmd.ExecuteNonQuery();

                foreach (OrderDetail od in view.orderDetail) {
                    cmd.Parameters.Clear();
                    cmd.CommandText = sqlD;

                    db.NewParaWithValue("order_line_id", DbType.String, od.Order_line_id, ref cmd);
                    db.NewParaWithValue("category_id", DbType.String, od.Category_id == null ? DBNull.Value.ToString() : od.Category_id, ref cmd);
                    db.NewParaWithValue("quantity", DbType.Decimal, od.Quantity, ref cmd);
                    db.NewParaWithValue("unit_uom", DbType.String, od.Qnit_uom == null ? DBNull.Value.ToString() : od.Qnit_uom, ref cmd);
                    db.NewParaWithValue("unit_quantity", DbType.Decimal, od.Unit_quantity == null ? 0 : od.Unit_quantity, ref cmd);
                    db.NewParaWithValue("unit_price", DbType.Decimal, od.Unit_price, ref cmd);
                    db.NewParaWithValue("amount", DbType.Decimal, od.Amount, ref cmd);
                    db.NewParaWithValue("spec", DbType.String, od.Spec == null ? DBNull.Value.ToString() : od.Spec, ref cmd);
                    db.NewParaWithValue("service_item", DbType.String, od.Service_item, ref cmd);
                    db.NewParaWithValue("storage", DbType.Decimal, od.Storage, ref cmd);

                    cmd.ExecuteNonQuery();
                }

                dt.Commit();
            }
            catch (Exception e) {
                if (dt != null) dt.Rollback();
                throw;
            }
            finally
            {
                if (dt != null) dt.Dispose();
                cmd.Dispose();
            }
        }
    }
}
