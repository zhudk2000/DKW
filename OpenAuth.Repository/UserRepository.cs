﻿using OpenAuth.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityFramework.Extensions;
using OpenAuth.Domain;

namespace OpenAuth.Repository
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        public IEnumerable<User> LoadUsers(int pageindex, int pagesize)
        {
            return Context.Users.OrderBy(u => u.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
        }

        public IEnumerable<User> LoadInOrgs(params Guid[] orgId)
        {
            var result = from user in Context.Users
                     where (
                         Context.Relevances.Where(uo => orgId.Contains(uo.SecondId) && uo.Key =="UserOrg")
                         .Select(u => u.FirstId)
                         .Distinct()
                     )
                     .Contains(user.Id)
                select user;
            return result;

        }

        public int GetUserCntInOrgs(params Guid[] orgIds)
        {
            return LoadInOrgs(orgIds).Count();
        }

        public IEnumerable<User> LoadInOrgs(int pageindex, int pagesize, params Guid[] orgIds)
        {
            return LoadInOrgs(orgIds).OrderBy(u =>u.Id).Skip((pageindex -1)*pagesize).Take(pagesize);
        }

    }
}
