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
    public class AdminGraduateController : Controller
    {
       
            private GraduateManager graduatemanagerx = GraduateManager.CreateAsGraduateManager();
            public ActionResult Index()
            {
                return View();
            }
            public ActionResult GraduateList()
            {
                HomeViewModel<GraduateDB> model = new HomeViewModel<GraduateDB>();
                model.modelsX = graduatemanagerx.GetGraduateList();

                return View(model);
            }

            public ActionResult GraduateDelete(int id)
            {
            //submethod.unfollow(user.graduate_id, grad_id);
            if (graduatemanagerx.getID(id).graduate_name == null)
                    return HttpNotFound("Şeçilen id ile kimliğe sahip kimse yok");
                graduatemanagerx.GraduateDelete(id);

                return RedirectToAction("GraduateList", "AdminGraduate", new { area = "Admin" });
            }
            [HttpPost]
            public ActionResult GraduateAdd(HomeViewModel<GraduateDB> graduate)
            {
                graduate.modelX.graduate_id = graduatemanagerx.getNextId();
                graduatemanagerx.AddGraduate(graduate.modelX);

                return RedirectToAction("GraduateList","AdminGraduate",new { area ="Admin"});
            }
        }
    
}