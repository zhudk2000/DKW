using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain.Interface
{
    public interface ICustomerRepository : IRepository<CustomerInfo>
    {
        IEnumerable<CustomerInfo> LoadCustomerInfo(int pageindex, int pagesize);
        IEnumerable<CustomerInfo> QueryCustomerInfo(int pageindex, int pagesize, string criteria);
    }
}
