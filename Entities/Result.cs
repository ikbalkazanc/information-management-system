using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
namespace Entities
{
    public class Result
    {
        public bool Result_ { get; set; }
        public string ErrorText { get; set; }
       
        public int integer { get; set; }
        public List<DateTime> start_date = new List<DateTime>();
        public List<DateTime> ending_date = new List<DateTime>();
        public double avg { get; set; }
        public List<JobNoticeDB> jobnotice { get; set; }
        public List<InternNoticeDB> internnotice { get; set; }
    }
}
