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
    public class AdminCompanyController : Controller
    {
        
        private CompanyManager companymanagerx = CompanyManager.CreateAsCompanyManager();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CompanyList()
        {
            HomeViewModel<CompanyDB> model = new HomeViewModel<CompanyDB>();
            model.modelsX = companymanagerx.GetCompanyList();

            return View(model);
        }

        [HttpPost]
        public ActionResult CompanyAdd(HomeViewModel<CompanyDB> model)
        {
            companymanagerx.AddCompany(model.modelX);
            return RedirectToAction("CompanyList", "AdminCompany", new { area = "Admin" });
        }
        [HttpPost]
        public ActionResult CompanyAddfromProfile(HomeViewModel<GraduateDB> model)
        {
            companymanagerx.AddCompany(model.company);
            return RedirectToAction("Profil", "Profile", new { id = model.modelX.graduate_id });
        }
        public ActionResult CompanyDelete(int id)
        {
            companymanagerx.CompanyDelete(id);
            return RedirectToAction("CompanyList", "AdminCompany", new { area = "Admin" });
        }
    }
}