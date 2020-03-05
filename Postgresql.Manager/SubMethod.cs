using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgresql.Services.DatabaseServices;
using Postgresql.Services;
using Entities;
using System.Runtime;
using Entities.Databases;
namespace Postgresql.Manager
{
    
    public class SubMethod
    {
        SubMethods method = new SubMethods();

        private static SubMethod _SubMethod;
        private static object lockObject = new object();
        private SubMethod() { }

        public static SubMethod CreateAsJobManager()
        {
            lock (lockObject)
                return _SubMethod ?? (_SubMethod = new SubMethod());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }
        public int nameToid(string name)
        {
            return method.nameToID(name);
        }
        public Result AddArray(string table, string col, int value, int set_id)
        {
            return method.addArray(table,col,value,set_id);
        }
        public bool AddArray(string table, string col, string value, int set_id)
        {
            return method.addArray(table, col, value, set_id).Result_;
        }
        public bool Update(string table, string col, string value, int set_id)
        {
            return method.UpdateCol(table, col, value, set_id).Result_;
        }
        public bool RemoveArray(string table, string col, int value, int set_id)
        {
            return method.ArrayRemove(table, col, value, set_id).Result_;
        }
        public List<InternNoticeDB> internnoticelist(int set_id)
        {
            return method.InternNoticeList(set_id).Objects.ToList();
        }
        public List<JobNoticeDB> jobnoticelist(int set_id)
        {
            return method.JobNoticeList( set_id).Objects.ToList();
        }
        public bool isIntern(int set_id)
        {
            return method.isIntern(set_id);
        }
        public int NumberOfGraduateCount()
        {

            return method.countOfGraduateRow().integer; 
        }
        public int NumberOfCompanyCount()
        {

            return method.countOfCompanyRow().integer;
        }
        public int graduateOnCompany(int id)
        {
            return method.graduateOnCompanyRow(id).integer;
        }
        public DateTime graduateWorkLenghtOnCompany(int id)
        {
            DateTime temp = new DateTime(1,1,1);
            
            Result result = new Result();
            result = method.graduateWorkLenghtOnCompanyRow(id);
            for (int i = 0;i < result.ending_date.Count;i++)
            {
                if (result.ending_date[i].Year != 1 && result.ending_date[i].Year != 1)
                {
                    
                    
                    temp = temp.AddDays(Math.Abs(result.ending_date[i].Day - result.start_date[i].Day));
                    temp = temp.AddMonths(Math.Abs(result.ending_date[i].Month - result.start_date[i].Month));
                    temp = temp.AddYears(Math.Abs(result.ending_date[i].Year - result.start_date[i].Year));
                    //DateTime day = result.ending_date[i] - result.start_date[i];
                    
                }
            }
            return temp;
        }
        public bool noticeDeleteWithGraduate(int id)
        {
            return method.deleteNotices(id).Result_;
        }
        public bool follow(int user,int grad)
        {
            return method.follow(user, grad);
        }
        public bool unfollow(int user,int grad)
        {
            return method.unfollow(user, grad);
        }
        public List<int> getFollow(int id)
        {
            return method.getFollow(id);
        }
        public List<int> getFollower(int id)
        {
            return method.getFollower(id);
        }
        public double getGraduateAVG()
        {
            return method.getGraduateAvg().avg;
        }
        public List<JobNoticeDB> getListJobNoticeWithSql(string sql)
        {
            return method.getJobNoticeWithSql(sql).jobnotice;
        }
        public List<InternNoticeDB> getListInternNoticeWithSql(string sql)
        {
            return method.getInternNoticeWithSql(sql).internnotice;
        }
        public List<int> getAllGradID()
        {
            return method.getGradsID();
        }

    }
}
