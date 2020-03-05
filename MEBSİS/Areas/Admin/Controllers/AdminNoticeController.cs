using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Databases;
using Postgresql.Manager.Managers;
using Postgresql.Manager;
using MEBSİS.Models;

namespace MEBSİS.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminNoticeController : Controller
    {

        private JobNoticeManager jobnoticemanagerx = JobNoticeManager.CreateAsJobManager();
        private InternNoticeManager internmanagerx = InternNoticeManager.CreateAsJobManager();
        private SubMethod submethod = SubMethod.CreateAsJobManager();
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
        public ActionResult NoticePage()
        {
            NoticeView model = new NoticeView();
            List<int> followers = new List<int>();
            followers = submethod.getAllGradID();
            

            foreach (int item in followers)
            {
                model.interns.AddRange(submethod.internnoticelist(item));
                model.jobs.AddRange(submethod.jobnoticelist(item));
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult NoticePage(NoticeView model)
        {
            int counter1 = 0;
            int counter2 = 0;
            string intern_sql;
            string job_sql;
            List<JobNoticeDB> jobs = new List<JobNoticeDB>();
            List<InternNoticeDB> interns = new List<InternNoticeDB>();

            if (model.filter_isIntern)
            {
                intern_sql = "SELECT * FROM \"Notice\" WHERE \"isIntern\"= true "; //intern için
                if (model.filter_isAutum == true || model.filter_isSpring == true)
                {
                    if (model.filter_isAutum == true && model.filter_isSpring == true)
                    {

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
               
                jobs = submethod.getListJobNoticeWithSql(job_sql);
            }
            model.jobs = jobs;
            model.interns = interns;


            return View(model);
        }
        public ActionResult NoticeShow(int id)
        {
            NoticeView model = new NoticeView();
            if (submethod.isIntern(id))
            {
                model.intern = internmanagerx.getIntern(id);
            }
            else
            {
                model.job = jobnoticemanagerx.getJob(id);
            }
            return View(model);
        }
    }
}