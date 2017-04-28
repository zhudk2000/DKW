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
            //
            System.Diagnostics.Debug.WriteLine("1111111");
        }
    }
}
