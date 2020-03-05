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
    public class InternNoticeManager
    {
        private static InternNoticeManager _InternNoticeManager;
        private static object lockObject = new object();
        private InternNoticeManager() { }

        public static InternNoticeManager CreateAsJobManager()
        {
            lock (lockObject)
                return _InternNoticeManager ?? (_InternNoticeManager = new InternNoticeManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        NoticeServices_Intern noticeservice = new NoticeServices_Intern();
        GraduateServices graduateservices = new GraduateServices();

        public List<InternNoticeDB> GetInternList()
        {

            return noticeservice.getList().Objects.ToList();
        }
        public InternNoticeDB getIntern(int id)
        {
            return noticeservice.getId(id).Object;
        }
        public bool AddNotice(InternNoticeDB notice)
        {
            notice.notice_id = noticeservice.getNextPersonId();
            EntityResult<InternNoticeDB> result = noticeservice.Insert(notice);         
            return result.Result;
        }
        public bool deleteNoticec(int id)
        {
            EntityResult<InternNoticeDB> result = noticeservice.Delete(id);
            return result.Result;
        }
    }
}
