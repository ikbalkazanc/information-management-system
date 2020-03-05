using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Databases;
using Postgresql.Services.DatabaseServices;

namespace Postgresql.Manager.Managers
{
    public class JobManager
    {
        private static JobManager _jobManager;
        private static object lockObject = new object();
        private JobManager() { }

        public static JobManager CreateAsJobManager()
        {
            lock (lockObject)
                return _jobManager ?? (_jobManager = new JobManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        JobServices jobservice = new JobServices();

        public List<JobDB> GetJobList()
        {
            return jobservice.getList().Objects.ToList();
        }
        public bool JobDelete(int id)
        {
            EntityResult<JobDB> result = jobservice.Delete(id);
            return result.Result;
        }
        public bool AddJob(JobDB job)
        {
            EntityResult<JobDB> result = jobservice.Insert(job);
            return result.Result;
        }
        public bool UpdateSelected(string table, string col, int value, int id)
        {
            EntityResult<JobDB> result = jobservice.UpdateSelected(table, col, value, id);
            return result.Result;
        }
        public int getNextID()
        {
            return jobservice.getNextPersonId();
        }

    }
  }

