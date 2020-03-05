using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class CompanyDB
    {
        public int user_id { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_country { get; set; }
        public string company_city { get; set; }
        public string company_fullAddress { get; set; }
        public int numberofgraduate { get; set; }
        public DateTime GraduateWorkRank { get; set; }
        

    }
}
