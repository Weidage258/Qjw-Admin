using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.User
{
    public class UserReq
    {
        public string Name { get; set; }
        public string NickName { get; set; } 
        public int UserType { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
