using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
using Entities;
using Postgresql.Services.DatabaseServices;

namespace Postgresql.Manager.Managers
{
    public class CompanyManager
    {
        private static CompanyManager _CompanyManager;
        private static object lockObject = new object();
        private CompanyManager() { }

        public static CompanyManager CreateAsCompanyManager()
        {
            lock (lockObject)
                return _CompanyManager ?? (_CompanyManager = new CompanyManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        CompanyServices companyservicex = new CompanyServices();

        public List<CompanyDB> GetCompanyList()
        {
            return companyservicex.getList().Objects.ToList();
        }
        public bool AddCompany(CompanyDB company)
        {
            company.company_id = companyservicex.getNextPersonId();
            EntityResult<CompanyDB> result = companyservicex.Insert(company);
            return result.Result;
        }
        public bool CompanyDelete(int id)
        {
            EntityResult<CompanyDB> result = companyservicex.Delete(id);
            return result.Result;
        }
        public bool UpdateSelected(string table,string col,int value,int id)
        {
            EntityResult<CompanyDB> result = companyservicex.UpdateSelected(table,col,value,id);
            return result.Result;
        }

    }
}
