using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Databases;
using Entities;
using Postgresql.Services.DatabaseServices;
using System.Threading.Tasks;

namespace Postgresql.Manager.Managers
{
    public class GraduateManager
    {
        private static GraduateManager _GraduateManager;
        private static object lockObject = new object();
        private GraduateManager() { }

        public static GraduateManager CreateAsGraduateManager()
        {
            lock (lockObject)
                return _GraduateManager ?? (_GraduateManager = new GraduateManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        GraduateServices graduatemanagerx = new GraduateServices();

        public List<GraduateDB> GetGraduateList()
        {
            return graduatemanagerx.getList().Objects.ToList();
        }
        public bool GraduateDelete(int id)
        {
            EntityResult<GraduateDB> result = graduatemanagerx.Delete(id);
            return result.Result;
        }
        public GraduateDB getID(int id)
        {
            EntityResult<GraduateDB> result = graduatemanagerx.getId(id);
            return result.Object;
        }
        public int getNextId()
        {
            return graduatemanagerx.getNextId();
        }
        public bool AddGraduate(GraduateDB graduate)
        {
            graduate.graduate_id =  graduatemanagerx.getNextId();
            EntityResult<GraduateDB> result = graduatemanagerx.Insert(graduate);
            return result.Result;
        }
    }
}
