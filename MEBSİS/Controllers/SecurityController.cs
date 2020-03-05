
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Databases;
using Postgresql.Manager.Managers;
using Postgresql.Manager;
using MEBSİS.Models;
using System.Web.Security;

namespace MEBSİS.Controllers
{
   
    public class SecurityController : Controller
    {
        private SecurityManager securitymanager = SecurityManager.CreateAsSecurityManager();
        private GraduateManager graduatemanager = GraduateManager.CreateAsGraduateManager();
        private SubMethod method = SubMethod.CreateAsJobManager();
        // GET: Security
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }
       
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        [Route("")]
        public ActionResult Way()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult LoginOK(LoginView model)
        {
            if (model.graduate)
            {
                
                if (securitymanager.getAdmin(model.id).admin_name == null)
                    return HttpNotFound("Şeçilen id ile kimliğe sahip kimse yok");
                Admin person = securitymanager.getAdmin(model.id);
                if (model.password != person.admin_password)
                    return HttpNotFound("Şifreyi yalnış girdiniz");
                FormsAuthentication.RedirectFromLoginPage(model.id.ToString(), false);
                return RedirectToAction("NoticePage", "Admin/AdminNotice");
            }
            else {
                List<int> gradlist = method.getAllGradID();
                
                if (gradlist.IndexOf(model.id) == -1 )
                    return HttpNotFound("Şeçilen id ile kimliğe sahip kimse yok");
                GraduateDB graduate = graduatemanager.getID(model.id);
                if (model.password != graduate.graduate_password)
                    return HttpNotFound("Şifreyi yalnış girdiniz");
                model.graduate = true;
                model.password = "";
                

                FormsAuthentication.RedirectFromLoginPage(model.id.ToString(), false);
                return RedirectToAction("NoticePage", "Notice", new { model.id }) ;
            }
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        ~SecurityController()
        {
            FormsAuthentication.SignOut();
        }
    }
}