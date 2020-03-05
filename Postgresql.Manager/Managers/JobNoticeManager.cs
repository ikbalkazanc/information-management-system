using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
using Postgresql.Services.DatabaseServices;
using Entities;

namespace Postgresql.Manager.Managers
{
    public class JobNoticeManager
    {

        private static JobNoticeManager _jobNoticeManager;
        private static object lockObject = new object();
        private JobNoticeManager() { }

        public static JobNoticeManager CreateAsJobManager()
        {
            lock (lockObject)
                return _jobNoticeManager ?? (_jobNoticeManager = new JobNoticeManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        private NoticeServices_Job noticeservice = new NoticeServices_Job();

        public List<JobNoticeDB> GetJobList()
        {
            
            return noticeservice.getList().Objects.ToList();
        }
        public JobNoticeDB getJob(int id)
        {
            return noticeservice.getId(id).Object;
        }
        public bool AddNotice(JobNoticeDB notice)          
        {
            notice.notice_id = noticeservice.getNextPersonId();
            EntityResult<JobNoticeDB> result = noticeservice.Insert(notice);
            return result.Result;

            
        }
        public bool deleteNoticec(int id)
        {
            EntityResult<JobNoticeDB> result = noticeservice.Delete(id);
            return result.Result;
        }
    }
}
