using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Databases;
using Postgresql.Manager.Managers;
using MEBSİS.Models;

namespace MEBSİS.Controllers
{
    [Authorize]
    public class CompanyController : Controller
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
            return RedirectToAction("CompanyList");
        }
        [HttpPost]
        public ActionResult CompanyAddfromProfile(HomeViewModel<GraduateDB> model)
        {
            companymanagerx.AddCompany(model.company);
            return RedirectToAction("profil","Profile", new { user_id = model.userx.id,grad_id = model.modelX.graduate_id  });
        }
        public ActionResult CompanyDelete(int id)
        {
            companymanagerx.CompanyDelete(id);
            return RedirectToAction("CompanyList");
        }
    }
}