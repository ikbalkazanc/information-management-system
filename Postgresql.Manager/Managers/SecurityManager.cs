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

        public class SecurityManager
        {
            private static SecurityManager _securitymanager;
            private static object lockObject = new object();
            private SecurityManager() { }

            public static SecurityManager CreateAsSecurityManager()
            {
                lock (lockObject)
                    return _securitymanager ?? (_securitymanager = new SecurityManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
            }
            private SecurityServices securityservices = new SecurityServices();
            public Admin getAdmin(int id)
            {
            return securityservices.getAdmin(id).Object;
             }

        }
    
}
