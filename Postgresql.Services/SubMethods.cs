using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Postgresql.Services.DatabaseServices;
using Postgresql.Services;
using Entities.Databases;
using Entities;
using NpgsqlTypes;



namespace Postgresql.Services
{
    public class SubMethods : StateConnection
    {
        private JobServices jobservice = new JobServices();
        private GraduateServices graduateservice = new GraduateServices();
        public List<string> postgreGetArray_string(string tablename, string coloum, int id)
        {

            List<string> temp = new List<string>();
            int count = 1;
            NpgsqlCommand command = new NpgsqlCommand("SELECT " + coloum.ToString() + "[" + count.ToString() + "]" + " FROM \"" + tablename.ToString() +
                "\" WHERE \"" + tablename.ToString().ToLower() + "_id\" =" + id.ToString() + ";", connectionOpen());
            do
            {
                string v = "SELECT " + coloum.ToString() + "[" + count.ToString() + "]" + " FROM \"" + tablename.ToString() + "\" WHERE \"" + tablename.ToString().ToLower() + "_id\" =" + id.ToString() + ";";
                command.CommandText = v;
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.IsDBNull(0))
                {
                    connectionClosed();
                    return temp;
                }
                temp.Add(reader.GetString(0));
                count++;
                reader.Close();

            }
            while (true);

        }
        public List<int> postgreGetArray_int(string tablename, string coloum, int id)
        {
            List<int> temp = new List<int>();
            int count = 1;
            NpgsqlCommand command = new NpgsqlCommand("SELECT " + coloum.ToString() + "[" + count.ToString() + "]" + " FROM \"" + tablename.ToString() + "\"  WHERE \"" + tablename.ToString().ToLower() + "_id\" =" + id.ToString() + ";", connectionOpen());
            do
            {
                string v = "SELECT " + coloum.ToString() + "[" + count.ToString() + "]" + " FROM \"" + tablename.ToString() + "\"  WHERE \"" + tablename.ToString().ToLower() + "_id\" =" + id.ToString() + ";";
                command.CommandText = v;
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.IsDBNull(0))
                {
                    connectionClosed();
                    return temp;
                }
                temp.Add(reader.GetInt32(0));
                count++;
                reader.Close();
            }
            while (true);

        }

        public List<JobDB> postgreGetArray_JobDB(string tablename, string coloum, int id)
        {
            List<JobDB> temp = new List<JobDB>();
            List<int> temp_int = new List<int>();
            int count = 0;
            temp_int = postgreGetArray_int(tablename, coloum, id);

            while (temp_int.Count > count)
            {
                temp.Add(jobservice.getId(temp_int[count]).Object);
                count++;
            }


            connectionClosed();
            return temp;
        }

        public int nameToID(string name)
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Company\" WHERE company_name='@name';", connectionOpen());
            command.Parameters.AddWithValue("@name", name);
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            var p = reader.GetInt32(0);
            connectionClosed();
            return p;
        }
        public Result addArray(string table, string col, int value, int set_id)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {
                string sql = "UPDATE \"" + table + "\" SET " + col + " = array_append(" + col + ",'" + value + "') WHERE " + table.ToLower() + "_id = " + set_id + " ;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result_ = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public Result addArray(string table, string col, string value, int set_id)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {

                string sql = "UPDATE \"" + table + "\" SET " + col + " = array_append(" + col + ",'" + value.ToString() + "') WHERE " + table.ToLower() + "_id = " + set_id + " ;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result_ = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public Result dropArray(string table, string col, int value, int set_id)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {
                string sql = "UPDATE \"" + table + "\" SET " + col + " = array_remove(" + col + ",'" + value + "') WHERE " + table.ToLower() + "_id = " + set_id + " ;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result_ = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public Result UpdateCol(string table, string col, string value, int set_id)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {

                string sql = "UPDATE \"" + table + "\" SET " + col + "= '" + value + "' WHERE " + table.ToLower() + "_id = " + set_id + " ;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result_ = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public Result ArrayRemove(string table, string col, int value, int set_id)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {

                string sql = "UPDATE \"" + table + "\" SET " + col + " = array_remove(" + col + ",'" + value + "') WHERE " + table.ToLower() + "_id = " + set_id + " ;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result_ = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public EntityResult<JobNoticeDB> JobNoticeList(int grad_id)
        {
            EntityResult<JobNoticeDB> result = new EntityResult<JobNoticeDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                DatabaseSort("Notice", "notice_date");
                List<JobNoticeDB> notices = new List<JobNoticeDB>();
                string sql = "SELECT * FROM \"Notice\" WHERE \"notice_graduate_id_FK\" = " + grad_id + " AND \"isIntern\" = 'false';";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    JobNoticeDB notice = new JobNoticeDB();
                    notice.notice_id = reader.GetInt32(0);

                    //notice.notice_graduate = jobservice.getId(reader.GetInt32(1));
                    notice.notice_graduate.graduate_id = reader.GetInt32(1);
                    notice.notice_graduate = graduateservice.getId(notice.notice_graduate.graduate_id).Object;
                    notice.notice_title = reader.GetString(2);
                    notice.notice_text = reader.GetString(3);

                    NpgsqlDate date1 = reader.GetDate(4);
                    DateTime date2 = new DateTime(date1.Year, date1.Month, date1.Day);
                    notice.notice_date = date2;

                    notice.notice_workfield = reader.GetString(5);
                    notice.notice_job_salary = reader.GetInt32(8);
                    notice.isIntern = reader.GetBoolean(9);


                    notices.Add(notice);
                }
                result.Objects = notices; //birden fazla liste dönülüyor
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public EntityResult<InternNoticeDB> InternNoticeList(int grad_id)
        {
            EntityResult<InternNoticeDB> result = new EntityResult<InternNoticeDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                DatabaseSort("Notice", "notice_date");
                List<InternNoticeDB> notices = new List<InternNoticeDB>();
                string sql = "SELECT * FROM \"Notice\" WHERE \"notice_graduate_id_FK\" = '" + grad_id + "' AND \"isIntern\" = 'true';;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    InternNoticeDB notice = new InternNoticeDB();

                    notice.notice_id = reader.GetInt32(0);

                    //notice.notice_graduate = jobservice.getId(reader.GetInt32(1));
                    notice.notice_graduate.graduate_id = reader.GetInt32(1);
                    notice.notice_graduate = graduateservice.getId(notice.notice_graduate.graduate_id).Object;
                    notice.notice_title = reader.GetString(2);
                    notice.notice_text = reader.GetString(3);

                    NpgsqlDate date1 = reader.GetDate(4);
                    DateTime date2 = new DateTime(date1.Year, date1.Month, date1.Day);
                    notice.notice_date = date2;

                    notice.notice_workfield = reader.GetString(5);
                    notice.notice_intern_period = reader.GetString(6);
                    notice.notice_intern_lenght_days = reader.GetInt32(7);
                    notice.isIntern = reader.GetBoolean(9);

                    notices.Add(notice);
                }
                result.Objects = notices; //birden fazla liste dönülüyor
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                connectionClosed();
                return result;
            }
            connectionClosed();

            return result;
        }
        public void DatabaseSort(string table, string col)
        {
            string sql = "SELECT * FROM \"" + table + "\" ORDER BY " + col + " DESC";
            NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
            command.ExecuteNonQuery();
            connectionClosed();
        }
        public bool isIntern(int grad_id)
        {

            DatabaseSort("Notice", "notice_date");
            string sql = "SELECT * FROM \"Notice\" WHERE notice_id = '" + grad_id + "';";
            NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool p = reader.GetBoolean(9);
            connectionClosed();
            return p;
        }

        public void deleteNotice(int id)
        {
            string sql = "DELETE FROM \"Notice\" WHERE notice_id = '" + id + "' ";
            NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
            command.ExecuteNonQuery();
            connectionClosed();

        }
        public Result countOfGraduateRow()
        {
            Result result = new Result();
            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "SELECT COUNT(graduate_id) FROM \"Graduate\" ";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result.integer = reader.GetInt32(0);
            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;

        }
        public Result countOfCompanyRow()
        {
            Result result = new Result();
            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "SELECT COUNT(company_id) FROM \"Company\" ";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result.integer = reader.GetInt32(0);
            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;

        }
        public Result graduateOnCompanyRow(int companyid)
        {
            Result result = new Result();
            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "SELECT COUNT(job_id) FROM \"Job\" WHERE job_company_fk = '" + companyid + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                result.integer = reader.GetInt32(0);

            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;
        }
        public Result graduateWorkLenghtOnCompanyRow(int companyid)
        {
            Result result = new Result();

            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "SELECT job_start_date,job_ending_date FROM \"Job\" WHERE job_company_fk = '" + companyid + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime temp1 = new DateTime(reader.GetDate(0).Year, reader.GetDate(0).Month, reader.GetDate(0).Day);

                    result.start_date.Add(temp1);
                    DateTime temp2 = new DateTime(reader.GetDate(1).Year, reader.GetDate(1).Month, reader.GetDate(1).Day);
                    result.ending_date.Add(temp2);
                }
            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;
        }
        public Result deleteNotices(int graduateid)  //mezuna bağlı ilanları siler
        {
            Result result = new Result();

            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "DELETE FROM \"Notice\" WHERE \"notice_graduate_id_FK\" = '" + graduateid + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                command.ExecuteNonQuery();

            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;
        }

        public bool follow(int user, int grad)
        {
            addArray("Graduate", "graduate_followers", user, grad);
            return addArray("Graduate", "graduate_follow", grad, user).Result_;
        }
        public bool unfollow(int user, int grad)
        {
            dropArray("Graduate", "graduate_followers", user, grad);
            return dropArray("Graduate", "graduate_follow", grad, user).Result_;
        }
        public List<int> getFollow(int id)
        {
            return postgreGetArray_int("Graduate", "graduate_follow", id);
        }
        public List<int> getFollower(int id)
        {
            return postgreGetArray_int("Graduate", "graduate_followers", id);
        }
        public Result getGraduateAvg()
        {
            Result result = new Result();

            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "SELECT AVG(graduate_gradeaverage) FROM \"Graduate\" ";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result.avg = reader.GetDouble(0);

            }
            catch (NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result_ = false;
                connectionClosed();
                return result;
            }
            connectionClosed();
            return result;
        }

        public Result getInternNoticeWithSql(string sql)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<InternNoticeDB> noticeList = new List<InternNoticeDB>();
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen());
                //id   --  graduateid--  title -- text -- date -- workfield--intershipperiod--intershiplenght--jobsalary---isIntern
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    InternNoticeDB notice = new InternNoticeDB();

                    notice.notice_id = reader.GetInt32(0);

                    //notice.notice_graduate = jobservice.getId(reader.GetInt32(1));
                    notice.notice_graduate.graduate_id = reader.GetInt32(1);
                    notice.notice_graduate = graduateservice.getId(notice.notice_graduate.graduate_id).Object;
                    notice.notice_title = reader.GetString(2);
                    notice.notice_text = reader.GetString(3);

                    NpgsqlDate date1 = reader.GetDate(4);
                    DateTime date2 = new DateTime(date1.Year, date1.Month, date1.Day);
                    notice.notice_date = date2;

                    notice.notice_workfield = reader.GetString(5);
                    notice.notice_intern_period = reader.GetString(6);
                    notice.notice_intern_lenght_days = reader.GetInt32(7);
                    notice.isIntern = reader.GetBoolean(9);

                    noticeList.Add(notice);
                }
                result.internnotice = noticeList; //birden fazla liste dönülüyor
                connectionClosed();

            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {

                result.Result_ = false;
                result.ErrorText = Ex.Message;

            }

            connectionClosed();
            return result;
        }
        public Result getJobNoticeWithSql(string sql)
        {
            Result result = new Result();
            result.Result_ = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<JobNoticeDB> noticeList = new List<JobNoticeDB>();
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen()); //connectionOpen connection döndürüyor
                //id   --  graduateid--  title -- text -- date -- workfield--intershipperiod--intershiplenght--jobsalary---isIntern
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    JobNoticeDB notice = new JobNoticeDB();
                    notice.notice_id = reader.GetInt32(0);

                    //notice.notice_graduate = jobservice.getId(reader.GetInt32(1));
                    notice.notice_graduate.graduate_id = reader.GetInt32(1);
                    notice.notice_graduate = graduateservice.getId(notice.notice_graduate.graduate_id).Object;
                    notice.notice_title = reader.GetString(2);
                    notice.notice_text = reader.GetString(3);

                    NpgsqlDate date1 = reader.GetDate(4);
                    DateTime date2 = new DateTime(date1.Year, date1.Month, date1.Day);
                    notice.notice_date = date2;

                    notice.notice_workfield = reader.GetString(5);
                    notice.notice_job_salary = reader.GetInt32(8);
                    notice.isIntern = reader.GetBoolean(9);


                    noticeList.Add(notice);
                }
                result.jobnotice = noticeList; //birden fazla liste dönülüyor
                connectionClosed();

            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {

                result.Result_ = false;
                result.ErrorText = Ex.Message;

            }

            connectionClosed();
            return result;
        }
        public List<int> getGradsID()
        {
            List<int> temp = new List<int>();
            NpgsqlCommand command = new NpgsqlCommand("SELECT graduate_id FROM \"Graduate\"", connectionOpen());
           NpgsqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
             
                temp.Add(reader.GetInt32(0));
                
                
            }
            reader.Close();
            connectionClosed();
            return temp;

        }

    }
}

    

