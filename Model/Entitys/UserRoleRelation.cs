using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRoleRelation : IBase
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long UserId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }
    }
}
