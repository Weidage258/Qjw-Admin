using Model.Dto.Role;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IRoleService
    {
        bool Add(RoleAdd role, long userId);
        bool Edit(RoleEdit role, long userId);
        bool Del(long id);
        bool BatchDel(string ids);
        PageInfo GetRoles(RoleReq req);
        RoleRes GetRoleById(long id);
        public string Test(string a);
    }
}
