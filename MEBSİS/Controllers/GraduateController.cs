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
    public class GraduateController : Controller
    {
        // GET: Graduate
        
        private GraduateManager graduatemanagerx = GraduateManager.CreateAsGraduateManager();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GraduateList(int user_id)
        {
            HomeViewModel<GraduateDB> model = new HomeViewModel<GraduateDB>();
            model.modelsX = graduatemanagerx.GetGraduateList();

            return View(model);
        }
       
        public ActionResult GraduateDelete(int id)
        {
            if (graduatemanagerx.getID(id).graduate_name == null)
                return HttpNotFound("Şeçilen id ile kimliğe sahip kimse yok");
            graduatemanagerx.GraduateDelete(id);
       
            return RedirectToAction("GraduateList");
        }
        [HttpPost]
        public ActionResult GraduateAdd(HomeViewModel<GraduateDB> graduate)
        {
            graduate.modelX.graduate_id = graduatemanagerx.getNextId();
            graduatemanagerx.AddGraduate(graduate.modelX);
           
            return RedirectToAction("GraduateList");
        }
    }
}