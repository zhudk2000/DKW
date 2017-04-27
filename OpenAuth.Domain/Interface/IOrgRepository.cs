﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain.Interface
{
    public  interface IOrgRepository :IRepository<Org>
    {
        IEnumerable<Org> LoadOrgs();

        IEnumerable<Org> LoadByUser(Guid userId);

        IEnumerable<Org> LoadByRole(Guid roleId);

        /// <summary>
        ///  获取包括自己在内的全部子部门
        /// </summary>
        /// <param name="orgId">部门ID</param>
        IEnumerable<Org> GetSubOrgs(Guid orgId);
    }
}
