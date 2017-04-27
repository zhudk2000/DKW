using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.Repository;

namespace OpenAuth.App
{
    public class CustomerApp
    {
        private CustomerRepository _repository = new CustomerRepository();
        //
        public void Add(CustomerInfo cust)
        {
            _repository.Add(cust);
        }

        public GridData LoadCustomerInfo(int pageindex, int pagesize)
        {
            if (pageindex < 1) pageindex = 1;  //TODO:如果列表为空新增加一个用户后，前端会传一个0过来，奇怪？？
            IEnumerable<CustomerInfo> custs;
            int records = _repository.GetCount();
            
            custs = _repository.LoadCustomerInfo(pageindex, pagesize);

            return new GridData
            {
                records = records,
                total = (int)Math.Ceiling((double)records / pagesize),
                rows = custs,
                page = pageindex
            };
        } 

        public GridData LoadCustomerInfo(string custCode, string custName, int pageindex, int pagesize)
        {
            if (pageindex < 1) pageindex = 1;  //TODO:如果列表为空新增加一个用户后，前端会传一个0过来，奇怪？？
            IEnumerable<CustomerInfo> custs;
            int records = _repository.GetCount();

            custs = _repository.LoadCustomerInfo(custCode, custName, pageindex, pagesize);

            return new GridData
            {
                records = records,
                total = (int)Math.Ceiling((double)records / pagesize),
                rows = custs,
                page = pageindex
            };
        }

        public void Delete(string[] ids)
        {
            _repository.Delete(new List<string>(ids));
        }

        public void Update(CustomerInfo cust)
        {
            _repository.Update(cust);
        }
    }
}
