using System;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Common;
using System.Data;
using System.Text;
/// <summary>
/// 客户管理后台与数据库交互部分
/// </summary>
namespace OpenAuth.Repository
{
    public class CustomerRepository : BaseRepository<Resource>
    {

        public void Add(CustomerInfo entity)
        {
            if (entity != null)
            {
                List<CustomerInfo> arr = new List<CustomerInfo>();
                arr.Add(entity);
                BatchAdd(arr.ToArray());
            }
        }

        public void BatchAdd(CustomerInfo[] entities)
        {
            //base.GetDbCommandObject
            if (entities.Length > 0)
            {
                string sql =
                    @"insert into CustomerInfo(customerid, customercode, customername, customeraddr) 
                        values(@cid, @ccd, @cnm, @cad)";

                using (DbCommand cmd = base.GetDbCommandObject())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    DbParameter para1 = cmd.CreateParameter();
                    para1.ParameterName = "cid";
                    para1.DbType = DbType.String;

                    DbParameter para2 = cmd.CreateParameter();
                    para2.ParameterName = "ccd";
                    para2.DbType = DbType.String;

                    DbParameter para3 = cmd.CreateParameter();
                    para3.ParameterName = "cnm";
                    para3.DbType = DbType.String;

                    DbParameter para4 = cmd.CreateParameter();
                    para4.ParameterName = "cad";
                    para4.DbType = DbType.String;

                    cmd.Parameters.Add(para1);
                    cmd.Parameters.Add(para2);
                    cmd.Parameters.Add(para3);
                    cmd.Parameters.Add(para4);

                    cmd.Connection.Open();

                    foreach (CustomerInfo cust in entities)
                    {
                        cmd.Parameters["cid"].Value = System.Guid.NewGuid().ToString();
                        cmd.Parameters["ccd"].Value = cust.CustomerCode;
                        cmd.Parameters["cnm"].Value = cust.CustomerName;
                        cmd.Parameters["cad"].Value = cust.CustomerAddr;
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Dispose();
                }
            }
        }

        public void Delete(CustomerInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(List<string> idList)
        {
            StringBuilder sb = new StringBuilder("delete from CustomerInfo where customerid in (");
            StringBuilder sql_id = new StringBuilder("");

            foreach (string id in idList)
            {
                if (sql_id.Length > 1)
                    sql_id.Append(", ");
                sql_id.Append("'").Append(id).Append("'");
            }
            sb.Append(sql_id).Append(")");

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public IQueryable<CustomerInfo> Find(Expression<Func<CustomerInfo, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CustomerInfo> Find(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<CustomerInfo, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public CustomerInfo FindSingle(Expression<Func<CustomerInfo, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(Expression<Func<CustomerInfo, bool>> exp = null)
        {
            return 200;
            //throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<CustomerInfo, bool>> exp)
        {
            throw new NotImplementedException();
        }

        //public int GetAllCustomerCount()
        //{
        //    int result = 0;
        //    using (DbCommand cmd = base.GetDbCommandObject())
        //    {
        //        string sql = "select count(*) from CustomerInfo";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sql;
        //        result = int.Parse(cmd.ExecuteScalar().ToString());
        //    }
        //    return result;
        //}
        public IEnumerable<CustomerInfo> LoadCustomerInfo(int pageindex, int pagesize)
        {
            return LoadCustomerInfo("", "", pageindex, pagesize);
        }

        public IEnumerable<CustomerInfo> LoadCustomerInfo(string custCode, string custName, int pageindex, int pagesize)
        {
            List<CustomerInfo> custs = new List<CustomerInfo>();
            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.Connection.Open();
                string sql = "select customerid, customercode, customername, customeraddr from CustomerInfo where 1 = 1 ";
                if (custCode != "")
                {
                    sql = sql + " and upper(customercode) like @ccd ";
                    DbParameter para = cmd.CreateParameter();
                    para.ParameterName = "ccd";
                    para.DbType = DbType.String;
                    para.Value = "%" + custCode.ToUpper() + "%";
                    cmd.Parameters.Add(para);
                }
                if (custName != "")
                {
                    sql = sql + " and upper(customercode) like @cnm ";
                    DbParameter para = cmd.CreateParameter();
                    para.ParameterName = "cnm";
                    para.DbType = DbType.String;
                    para.Value = "%" + custName.ToUpper() + "%";
                    cmd.Parameters.Add(para);
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                DbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CustomerInfo cust = new CustomerInfo();
                    cust.CustomerID = dr["customerid"].ToString();
                    cust.CustomerCode = dr["customercode"].ToString();
                    cust.CustomerName = dr["customername"].ToString();
                    cust.CustomerAddr = dr["customeraddr"].ToString();

                    custs.Add(cust);
                }
                cmd.Dispose();
            }
            return custs;
        }

        public IEnumerable<CustomerInfo> QueryCustomerInfo(int pageindex, int pagesize, string criteria)
        {
            return null;
        }

        public void Update(CustomerInfo cust)
        {
            string sql =
                    @"update CustomerInfo set customercode = @ccd, customername = @cnm, customeraddr = @cad 
                      where customerid = @cid";

            using (DbCommand cmd = base.GetDbCommandObject())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();

                DbParameter para1 = cmd.CreateParameter();
                para1.ParameterName = "cid";
                para1.DbType = DbType.String;

                DbParameter para2 = cmd.CreateParameter();
                para2.ParameterName = "ccd";
                para2.DbType = DbType.String;

                DbParameter para3 = cmd.CreateParameter();
                para3.ParameterName = "cnm";
                para3.DbType = DbType.String;

                DbParameter para4 = cmd.CreateParameter();
                para4.ParameterName = "cad";
                para4.DbType = DbType.String;

                cmd.Parameters.Add(para1);
                cmd.Parameters.Add(para2);
                cmd.Parameters.Add(para3);
                cmd.Parameters.Add(para4);

                cmd.Connection.Open();

                cmd.Parameters["cid"].Value = cust.CustomerID;
                cmd.Parameters["ccd"].Value = cust.CustomerCode;
                cmd.Parameters["cnm"].Value = cust.CustomerName;
                cmd.Parameters["cad"].Value = cust.CustomerAddr;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public void Update(Expression<Func<CustomerInfo, object>> identityExp, CustomerInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<CustomerInfo, bool>> where, Expression<Func<CustomerInfo, CustomerInfo>> entity)
        {
            throw new NotImplementedException();
        }
    }
}
