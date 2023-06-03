using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Menu
{
    public class MenuAdd
    {
        public string Name { get; set; }
        public string Index { get; set; }
        public string FilePath { get; set; }
        public long ParentId { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }

    }
}
