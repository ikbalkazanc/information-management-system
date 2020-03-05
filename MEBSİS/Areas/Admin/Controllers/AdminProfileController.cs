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
    public class AdminProfileController : Controller
    {
        private GraduateManager graduatemanagerx = GraduateManager.CreateAsGraduateManager();
        private JobManager jobmanagerx = JobManager.CreateAsJobManager();
        private SubMethod submethod = SubMethod.CreateAsJobManager();
        private CompanyManager companymanagerx = CompanyManager.CreateAsCompanyManager();
        private JobNoticeManager jobnoticex = JobNoticeManager.CreateAsJobManager();
        private InternNoticeManager internnoticex = InternNoticeManager.CreateAsJobManager();

        
        public ActionResult Profil(int grad_id)
        { 
            HomeViewModel<GraduateDB> graduate = new HomeViewModel<GraduateDB>();
            graduate.modelX = graduatemanagerx.getID(grad_id);
            graduate.companyList = companymanagerx.GetCompanyList();
            graduate.internlist = submethod.internnoticelist(grad_id);
            graduate.jobnoticelist = submethod.jobnoticelist(grad_id);
            graduate.modelX.graduate_followx = submethod.getFollow(grad_id);
            graduate.modelX.graduate_followersx = submethod.getFollower(grad_id);
            return View(graduate);

        }


        [HttpPost]
        public ActionResult AddJob(HomeViewModel<GraduateDB> model)
        {
            model.modelX.graduate_view_job_list.job_company.company_id = model.company.company_id;

            model.modelX.graduate_view_job_list.job_id = jobmanagerx.getNextID();
            jobmanagerx.AddJob(model.modelX.graduate_view_job_list);
            submethod.AddArray("Graduate", "graduate_job_fk", model.modelX.graduate_view_job_list.job_id, model.modelX.graduate_id);
            return RedirectToAction("Profil", new { id = model.modelX.graduate_id });
            //DENEMEKTEN KORKUYUM
        }
        public ActionResult Update(HomeViewModel<GraduateDB> model)
        {
            if (model.modelX.graduate_workposition != "")
            {
                submethod.Update("Graduate", "graduate_workposition", model.modelX.graduate_workposition, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_workposition != "")
            {
                submethod.Update("Graduate", "graduate_workfield", model.modelX.graduate_workfield, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_workposition != "")
            {
                submethod.Update("Graduate", "graduate_mail", model.modelX.graduate_mail, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_workposition != "")
            {
                submethod.Update("Graduate", "graduate_phone", model.modelX.graduate_phone, model.modelX.graduate_id);
            }

            if (model.modelX.graduate_language_temp != "")
            {
                submethod.AddArray("Graduate", "graduate_language", model.modelX.graduate_language_temp, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_certificate_temp != "")
            {
                submethod.AddArray("Graduate", "graduate_certificate", model.modelX.graduate_certificate_temp, model.modelX.graduate_id);
            }
            return RedirectToAction("Profil", new { id = model.modelX.graduate_id });

        }
        public ActionResult DeleteJob(int id_grad, int id_job)
        {
            submethod.RemoveArray("Graduate", "graduate_job_fk", id_job, id_grad);
            jobmanagerx.JobDelete(id_job);
            return RedirectToAction("Profil", new { id = id_grad });
            //DENEMEKTEN KORKUYUM

        }
        public ActionResult AddInternNotice(HomeViewModel<GraduateDB> model)
        {
            model.noticeIntern.notice_graduate.graduate_id = model.modelX.graduate_id;
            internnoticex.AddNotice(model.noticeIntern);
            return RedirectToAction("Profil", new { id = model.modelX.graduate_id });

        }
        public ActionResult AddJobNotice(HomeViewModel<GraduateDB> model)
        {
            model.noticeJob.notice_graduate.graduate_id = model.modelX.graduate_id;
            jobnoticex.AddNotice(model.noticeJob);

            return RedirectToAction("Profil", new { id = model.modelX.graduate_id });

        }
    }
}