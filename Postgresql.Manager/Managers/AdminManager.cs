using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgresql.Services.DatabaseServices;
using Entities;
using Entities.Databases;

namespace Postgresql.Manager.Managers
{
    public class AdminManager
    {
        private static AdminManager _AdminManager;
        private static object lockObject = new object();
        private AdminManager() { }

        public static AdminManager CreateAsAdminManager()
        {
            lock (lockObject)
                return _AdminManager ?? (_AdminManager = new AdminManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        AdminSevices service = new AdminSevices();

        public List<Admin> GetCompanyList()
        {
            return service.getList().Objects.ToList();
        }
        public bool AddCompany(Admin company)
        {
            company.admin_id = service.getNextPersonId();
            EntityResult<Admin> result = service.Insert(company);
            return result.Result;
        }
        public Admin getID(int id)
        {
            EntityResult<Admin> result = service.getId(id);
            return result.Object;
        }
        public bool CompanyDelete(int id)
        {
            EntityResult<Admin> result = service.Delete(id);
            return result.Result;
        }
        public bool UpdateSelected(string table, string col, int value, int id)
        {
            EntityResult<Admin> result = service.UpdateSelected(table, col, value, id);
            return result.Result;
        }
    }
}
