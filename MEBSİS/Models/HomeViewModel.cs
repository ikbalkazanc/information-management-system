using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Databases;

namespace MEBSİS.Models
{
    public class HomeViewModel<T> where T : class
    {
        
       
        
        public T modelX { get; set; }
        public List<T> modelsX { get; set; }

        public CompanyDB company { get; set; }
        public List<CompanyDB> companyList { get; set; }
        public InternNoticeDB noticeIntern { get; set; }
        public JobNoticeDB noticeJob { get; set; }
        public List<InternNoticeDB> internlist { get; set; }
        public List<JobNoticeDB> jobnoticelist { get; set; }
        
        public LoginView userx { get; set; }
    }
}