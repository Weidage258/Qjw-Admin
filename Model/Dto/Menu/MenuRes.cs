using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Menu
{
    public class MenuRes
    {
        [SugarColumn(IsTreeKey = true)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string FilePath { get; set; }
        public long ParentId { get; set; }
        public string ParentName { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 子级   实体类中如果不映射某个属性.s
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<MenuRes> Children { get; set; }
    }
}
