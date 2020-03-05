using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Postgresql.Manager;
using Entities.Databases;
using Entities;
using MEBSİS.Areas.Admin.Models;
using Postgresql.Manager.Managers;

namespace MEBSİS.Areas.Admin.Controllers
{
    public class StatController : Controller
    {
        private SubMethod method = SubMethod.CreateAsJobManager();
        private CompanyManager companymanager = CompanyManager.CreateAsCompanyManager();
        // GET: Admin/Stat
        public ActionResult Stat()
        {
            StatModel stat = new StatModel();
            stat.NumberofGraduateCount = method.NumberOfGraduateCount();
            stat.NumberofCompanyCount = method.NumberOfCompanyCount();

            List<CompanyDB> companylist = new List<CompanyDB>();
            CompanyDB tempcompany = new CompanyDB();
            companylist = companymanager.GetCompanyList();
            foreach (var item in companylist)
            {
                item.numberofgraduate =  method.graduateOnCompany(item.company_id);
                item.GraduateWorkRank = method.graduateWorkLenghtOnCompany(item.company_id);
            }
            for(int i = 0; i < companylist.Count-1; i++)
            {
                for (int j = 0; j < companylist.Count-i-1; j++)
                {
                    if (companylist[j].numberofgraduate <= companylist[j + 1].numberofgraduate)
                    {
                        tempcompany = companylist[j + 1];
                        companylist[j + 1] = companylist[j];
                        companylist[j] = tempcompany;
                    }
                }
            }
            stat.companylist = companylist;

            stat.avgOfGraduate = method.getGraduateAVG();

            return View(stat);
        }
    }
}