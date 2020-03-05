using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Databases;

namespace MEBSİS.Areas.Admin.Models
{
    public class StatModel
    {
        public int NumberofGraduateCount { get; set; }
        public int NumberofCompanyCount { get; set; }
        public List<CompanyDB> companylist = new List<CompanyDB>();
        public double avgOfGraduate { get; set; }
    }
}