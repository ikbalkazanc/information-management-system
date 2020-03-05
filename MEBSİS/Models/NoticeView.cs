using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Entities.Databases;
namespace MEBSİS.Models
{
    public class NoticeView : LoginView
    {
        public List<InternNoticeDB> interns = new List<InternNoticeDB>();
        public List<JobNoticeDB> jobs = new List<JobNoticeDB>();
        public InternNoticeDB intern = new InternNoticeDB();
        public JobNoticeDB job = new JobNoticeDB();
        
        public bool filter_isIntern { get; set; }
        public bool filter_isJob { get; set; }
        public int filter_min_salary { get; set; }
        public int filter_max_salary { get; set; }
        public bool filter_isAutum { get; set; }
        public bool filter_isSpring { get; set; }
        public int filter_min_day { get; set; }
        public int filter_max_day { get; set; }
        public string filter_position { get; set; }
    }

}