using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Role
{
    public class RoleRes
    {
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public int Order { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary> 
        public bool IsEnable { get; set; } 
        /// <summary>
        /// 描述
        /// </summary> 
        public string Description { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary> 
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary> 
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary> 
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary> 
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary> 
        public int IsDeleted { get; set; }
    }
}
