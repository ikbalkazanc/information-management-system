using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Databases;
using Postgresql.Manager.Managers;
using MEBSİS.Models;
using System.Web.Mvc;

namespace MEBSİS.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        // GET: Job
        private JobManager jobmanagerx = JobManager.CreateAsJobManager();
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JobList()
        {
            HomeViewModel<JobDB> model = new HomeViewModel<JobDB>();
            model.modelsX = jobmanagerx.GetJobList();

            return View(model);
        }
        public ActionResult JobAdd(JobDB job)
        {
            return View();
        }
    }
}