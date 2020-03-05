using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class GraduateDB
    {
        public int user_id { get; set; }
        public int graduate_id { get; set; }
        public string graduate_name { get; set; }
        public string graduate_surname { get; set; }

        public string graduate_date_temp { get; set; }
        public DateTime graduate_birth = new DateTime(1, 1, 1);
        public string graduate_country { get; set; }
        public string graduate_city { get; set; }
        public string graduate_fulladress { get; set; }
        public string graduate_school { get; set; }
        public double graduate_school_average { get; set; }

        public List<string> graduate_language = new List<string>();
        public string graduate_language_temp { get; set; }

        public List<string> graduate_certificate = new List<string>();

        public string graduate_certificate_temp { get; set; }

        public List<int> graduate_follow = new List<int>();
        public List<int> graduate_followx { get; set; }

        public List<int> graduate_followers = new List<int>();
        public List<int> graduate_followersx { get; set; }
        public bool graduate_isBanned { get; set; }

        public List<JobDB> graduate_job_list = new List<JobDB>();

        public JobDB graduate_view_job_list {get;set;}

        public string graduate_mail { get; set; }
        public string graduate_phone {get;set;}
        public string graduate_workposition { get; set; }
        public string graduate_workfield { get; set; }
        public string graduate_password { get; set; }

    }
}