using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAuth.Repository.Business;

namespace OpenAuth.App.Business
{
    public class BusinessUserApp
    {
        private RepositorBsUser _repo = new RepositorBsUser();
        public String ChangePasswordByAccount(String account, String newPass)
        {
            return _repo.ChangePasswordByAccount(account, newPass);
        }
    }
}
