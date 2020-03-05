using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using Entities.Databases;
using Postgresql.Manager.Managers;
using System.Web.Security;

namespace MEBSİS.Areas.Admin.Controllers
{
    public class AdminSecurityController : Controller
    {
        private SecurityManager securitymanager = SecurityManager.CreateAsSecurityManager();
        private GraduateManager graduatemanager = GraduateManager.CreateAsGraduateManager();
        // GET: Security
       
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Security",new { area=""});
        }
        ~AdminSecurityController()
        {
            FormsAuthentication.SignOut();
        }
    }
}