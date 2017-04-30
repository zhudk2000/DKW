using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using OpenAuth.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.Repository.Business;

namespace OpenAuth.App.Business
{
    public class CustomerApplication
    {
        private RepositoryCustomer _repo = new RepositoryCustomer();
        public void Add(Customer cust)
        {
            _repo.Add(cust);
        }

        public int IsCustomerNameExist(string custName)
        {
            return _repo.IsCustomerNameExist(custName);
        }

        public int IsUseridExist(string usrName)
        {
            return _repo.IsUseridExist(usrName);
        }
    }
}
