using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.User
{
    public class UserEdit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
    }
}
