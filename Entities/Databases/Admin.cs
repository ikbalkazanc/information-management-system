using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class Admin
    {
        public int user_id { get; set; }
        public int admin_id { get; set; }
        public string admin_name { get; set; }
        public string admin_surname { get; set; }
        public string admin_password { get; set; }
    }
}
