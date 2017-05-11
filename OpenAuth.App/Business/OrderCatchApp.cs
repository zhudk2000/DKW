using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using OpenAuth.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.Repository;
using OpenAuth.Repository.Business;

namespace OpenAuth.App.Business
{
    public class OrderCatchApp
    {
        private RepositoryOrderCatch _repo = new RepositoryOrderCatch();

        public string Abc(OrderHeader ord)
        {
            return _repo.abc();
        }

        public Customer GetCustomerByUserAcct(string acct)
        {
            BusinessUtility utl = new BusinessUtility();
            return utl.GetCustomerByLoginUserAcct(acct);
        }

        public String GetCustID_NameByUserAcct(string acct)
        {
            string result = "";
            Customer cust = GetCustomerByUserAcct(acct);
            if (cust != null)
                result = cust.Customer_ID + ";" + cust.Customer_Name;

            return result;
        }

        public string GetNextOrderNumber()
        {
            return _repo.GetNextOrderNumber();
        }
    }
}
