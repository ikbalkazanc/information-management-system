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
    public class AdminJobController : Controller
    {
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