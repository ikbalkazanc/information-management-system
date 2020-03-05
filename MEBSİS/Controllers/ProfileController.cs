using Entities.Databases;
using MEBSİS.Models;
using Postgresql.Manager;
using Postgresql.Manager.Managers;
using System.Web.Mvc;
using System;

namespace MEBSİS.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        private GraduateManager graduatemanagerx = GraduateManager.CreateAsGraduateManager();
        private JobManager jobmanagerx = JobManager.CreateAsJobManager();
        private SubMethod submethod = SubMethod.CreateAsJobManager();
        private CompanyManager companymanagerx = CompanyManager.CreateAsCompanyManager();
        private JobNoticeManager jobnoticex = JobNoticeManager.CreateAsJobManager();
        private InternNoticeManager internnoticex = InternNoticeManager.CreateAsJobManager();
        public ActionResult Index()
        {
            return View();
        }
        [Route("profil/{user_id}/{grad_id}")]
        public ActionResult Profil(int user_id,int grad_id)
        {
            LoginView login = new LoginView();
            HomeViewModel<GraduateDB> graduate = new HomeViewModel<GraduateDB>();
            graduate.modelX = graduatemanagerx.getID(grad_id);
            graduate.companyList = companymanagerx.GetCompanyList();
            graduate.internlist = submethod.internnoticelist(grad_id);
            graduate.jobnoticelist = submethod.jobnoticelist(grad_id);
            graduate.modelX.graduate_followx = submethod.getFollow(grad_id);
            graduate.modelX.graduate_followersx = submethod.getFollower(grad_id);
            login.id = user_id;
            graduate.userx = login;
            return View(graduate);
        }
        

        [HttpPost]
        public ActionResult AddJob(HomeViewModel<GraduateDB> model)
        {
            model.modelX.graduate_view_job_list.job_company.company_id = model.company.company_id;
            
            model.modelX.graduate_view_job_list.job_id = jobmanagerx.getNextID();
            jobmanagerx.AddJob(model.modelX.graduate_view_job_list);
            submethod.AddArray("Graduate", "graduate_job_fk", model.modelX.graduate_view_job_list.job_id, model.modelX.graduate_id);
            return RedirectToAction("profil",new {user_id = model.userx.id, grad_id=model.modelX.graduate_id});
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

            if (model.modelX.graduate_language_temp != null)
            {
                submethod.AddArray("Graduate", "graduate_language", model.modelX.graduate_language_temp, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_certificate_temp != null)
            {
                submethod.AddArray("Graduate", "graduate_certificate", model.modelX.graduate_certificate_temp, model.modelX.graduate_id);
            }
            if (model.modelX.graduate_password != "")
            {
                submethod.Update("Graduate", "graduate_password", model.modelX.graduate_password, model.modelX.graduate_id);
            }
            return RedirectToAction("profil", new { user_id=model.userx.id,grad_id=model.modelX.graduate_id }
            );
            
        }
        [Route("deletenotice/{id_grad}/{id_not}")]
        public ActionResult DeleteNotice(int id_grad, int id_not)
        {

            jobnoticex.deleteNoticec(id_not);
            return RedirectToAction("profil", new { user_id = id_grad, grad_id = id_grad });
        }
        [Route("deletejob/{id_grad}/{id_job}")]
        public ActionResult DeleteJob(int id_grad,int id_job)
        {
            submethod.RemoveArray("Graduate", "graduate_job_fk", id_job, id_grad);
            jobmanagerx.JobDelete(id_job);
            return RedirectToAction("profil", new { user_id = id_grad,grad_id = id_grad }); 
        }
        
        public ActionResult AddInternNotice(HomeViewModel<GraduateDB> model)
        {
            model.noticeIntern.notice_graduate.graduate_id = model.modelX.graduate_id;
            internnoticex.AddNotice(model.noticeIntern);
            return RedirectToAction("profil", new { user_id = model.userx.id, grad_id = model.modelX.graduate_id });
        }
        public ActionResult AddJobNotice(HomeViewModel<GraduateDB> model)
            {
            model.noticeJob.notice_graduate.graduate_id = model.modelX.graduate_id;
            jobnoticex.AddNotice(model.noticeJob);
            return RedirectToAction("profil", new { user_id = model.userx.id, grad_id = model.modelX.graduate_id });
        }
        public ActionResult follow(int grad_id)
        {
            GraduateDB user = new GraduateDB();
          
            if (User.Identity.Name != "")
            {
                user = graduatemanagerx.getID(Int32.Parse(User.Identity.Name));
                submethod.follow(user.graduate_id,grad_id);
            }
            return RedirectToAction("profil", new { user_id = user.graduate_id, grad_id = grad_id });

        }
        public ActionResult unfollow(int grad_id)
        {
            GraduateDB user = new GraduateDB();

            if (User.Identity.Name != "")
            {
                user = graduatemanagerx.getID(Int32.Parse(User.Identity.Name));

                submethod.unfollow(user.graduate_id, grad_id);
            }
            return RedirectToAction("profil", new { user_id = user.graduate_id, grad_id = grad_id });
        }

    }
}