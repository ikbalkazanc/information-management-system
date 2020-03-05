using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class JobDB
    {
        public int user_id { get; set; }
        public int job_id { get; set; }
      
        public DateTime start_date = new DateTime(1,1,1);
       public string start_temp_date { get; set; }

        public DateTime ending_date = new DateTime(1,1,1);
        public string ending_temp_date { get; set; }
        //public int job_start_year { get; set; }
        //public int job_start_month{ get; set; }
        //public int job_start_day { get; set; }
        //public int job_ending_year { get; set; }
        //public int job_ending_month { get; set; }
        //public int job_ending_day { get; set; }
        public string job_workfield { get; set; }
        public string job_workposition { get; set; }

        public CompanyDB job_company = new CompanyDB();
        public CompanyDB job_company_view { get; set; }
        
        //public CompanyDB job_company { get; set; }
    }
}
