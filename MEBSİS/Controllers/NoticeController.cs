using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Databases;
using Postgresql.Manager.Managers;
using MEBSİS.Models;
using Postgresql.Manager;


namespace MEBSİS.Controllers
{
    [Authorize]
    public class NoticeController : Controller
    {
        // GET: Notice
        
        private JobNoticeManager jobnoticemanagerx = JobNoticeManager.CreateAsJobManager();
        private InternNoticeManager internmanagerx = InternNoticeManager.CreateAsJobManager();
        private  SubMethod submethod = SubMethod.CreateAsJobManager();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JobNoticeList()
        {
            HomeViewModel<JobNoticeDB> model = new HomeViewModel<JobNoticeDB>();
            model.modelsX = jobnoticemanagerx.GetJobList();

            return View(model);
        }
        public ActionResult InternNoticeList()
        {
            HomeViewModel<InternNoticeDB> model = new HomeViewModel<InternNoticeDB>();
            model.modelsX = internmanagerx.GetInternList();

            return View(model);
        }
        
        public ActionResult NoticePage(int id)
        {
            NoticeView modelx = new NoticeView();
            modelx.id = id;
            /*
             Buraya takip edilen kişilerin listesi gelicek
             */
            List<int> followers = new List<int>();
            followers = submethod.getFollow(Int32.Parse(User.Identity.Name));
            followers.Add(Int32.Parse(User.Identity.Name));
            foreach (int item in followers)
            {
                modelx.interns.AddRange(submethod.internnoticelist(item));
                modelx.jobs.AddRange(submethod.jobnoticelist(item));
            }           
            return View(modelx);
        }
        [HttpPost]
        public ActionResult NoticePage(NoticeView model)
        {
            int counter1 = 0;
            int counter2 = 0;
            string intern_sql;
            string job_sql;
            List<int> followers = new List<int>();
            List<JobNoticeDB> jobs = new List<JobNoticeDB>();
            List<InternNoticeDB> interns = new List<InternNoticeDB>();
            followers = submethod.getFollow(Int32.Parse(User.Identity.Name));
            followers.Add(Int32.Parse(User.Identity.Name));

            if (model.filter_isIntern)
            {
                 intern_sql = "SELECT * FROM \"Notice\" WHERE \"isIntern\"= true "; //intern için
                if (model.filter_isAutum == true || model.filter_isSpring == true)
                {
                    if (model.filter_isAutum == true && model.filter_isSpring == true) { 
                    
                    }
                    else
                    {
                        if (model.filter_isAutum)
                        {
                            intern_sql = intern_sql + " AND notice_intership_period = 'Güz' ";
                        }
                        else
                        {
                            intern_sql = intern_sql + " AND not notice_intership_period = 'Güz' ";
                        }
                        if (model.filter_isSpring)
                        {
                            intern_sql = intern_sql + " AND notice_intership_period = 'Yaz' ";
                        }
                        else
                        {
                            intern_sql = intern_sql + " AND not notice_intership_period = 'Yaz' ";
                        }
                    }
                }
                if (model.filter_min_day != 0 || model.filter_max_day != 0)
                {
                    if (model.filter_min_day < model.filter_max_day)
                    {
                        intern_sql = intern_sql + "AND notice_intership_lenght_days BETWEEN " + model.filter_min_day + " AND " + model.filter_max_day + " ";
                    }
                    else
                    {
                        intern_sql = intern_sql + "AND notice_intership_lenght_days BETWEEN " + model.filter_max_day + " AND " + model.filter_min_day + " ";
                    }
                }
                if (model.filter_position != null)
                {
                    intern_sql = intern_sql + "AND notice_workfield = '" + model.filter_position + "' ";
                }
                if (followers.Count != 0) {
                    intern_sql = intern_sql + " AND (";
                foreach (var item in followers)
                    {
                        if (!(counter1+1 >= followers.Count))
                        {
                            intern_sql = intern_sql + "\"notice_graduate_id_FK\" = " + item + " OR ";
                            
                        }
                        else
                        {
                            intern_sql = intern_sql + "\"notice_graduate_id_FK\" = " + item + " ) ";
                        }
                        counter1++;
                    }
                }
                else
                {
                    intern_sql = intern_sql + "AND false";
                }

                interns = submethod.getListInternNoticeWithSql(intern_sql);
            }
            if (model.filter_isJob)
            {
                job_sql = "SELECT * FROM \"Notice\" WHERE \"isIntern\"=false "; //iş ilanı için
                if (model.filter_min_salary != 0 || model.filter_max_salary != 0)
                {
                    if (model.filter_min_salary < model.filter_max_salary)
                    {
                        job_sql = job_sql + "AND notice_job_salary BETWEEN " + model.filter_min_salary + " AND " + model.filter_max_salary + " ";
                    }
                    else
                    {
                        job_sql = job_sql + "AND notice_job_salary BETWEEN " + model.filter_max_salary + " AND " + model.filter_min_salary + " ";
                    }
                }
                if (model.filter_position != null)
                {
                    job_sql = job_sql + "AND notice_workfield = '" + model.filter_position + "' ";
                }
                if (followers.Count != 0)
                {
                    job_sql = job_sql + " AND (";
                    foreach (var item in followers)
                    {
                        if (!(counter2+1 >= followers.Count))
                        {
                            job_sql = job_sql + "\"notice_graduate_id_FK\" = " + item + " OR ";

                        }
                        else
                        {
                            job_sql = job_sql + "\"notice_graduate_id_FK\" = " + item + ")";
                        }
                        counter2++;
                    }
                }
                else
                {
                    intern_sql = job_sql + "AND false";
                }
                jobs = submethod.getListJobNoticeWithSql(job_sql);
            }
            model.jobs = jobs;
            model.interns = interns;
           

            return View(model);
        }
        [Route("duvar/{user_id}/{not_id}")]
        public ActionResult NoticeShow(int user_id,int not_id)
        {
            
            NoticeView model = new NoticeView();
            model.id = user_id;
            if (submethod.isIntern(not_id)){
                model.intern = internmanagerx.getIntern(not_id);
            }
            else
            {
                model.job = jobnoticemanagerx.getJob(not_id);
            }
            return View(model);
        }
    }
}