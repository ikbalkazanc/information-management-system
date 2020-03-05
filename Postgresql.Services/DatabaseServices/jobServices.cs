using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
using Entities;
using Npgsql;
using NpgsqlTypes;




namespace Postgresql.Services.DatabaseServices
{
    public class JobServices : StateConnection, IServices<JobDB> 
    {
        private CompanyServices companyservice = new CompanyServices();
        private SubMethods2 submethodsx = new SubMethods2();
        public Entities.EntityResult<JobDB> getList()
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<JobDB> jobList = new List<JobDB>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\";", connectionOpen()); //connectionOpen connection döndürüyor
               
                NpgsqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    JobDB job = new JobDB();

                    job.job_workfield = reader.GetString(0);
                    
                    NpgsqlDate p1 = reader.GetDate(1);
                    NpgsqlDate p2 = reader.GetDate(2);
                    DateTime start = new DateTime(p1.Year,p1.Month,p1.Day);
                    DateTime ending = new DateTime(p2.Year , p2.Month , p2.Day);
                    job.start_date = start;
                    job.ending_date = ending;

                    job.job_workposition = reader.GetString(3);

                    job.job_company.company_id = reader.GetInt32(4);
                    job.job_company = companyservice.getId(job.job_company.company_id).Object;

                    job.job_id = reader.GetInt32(5);


                    jobList.Add(job);
                }
                result.Objects = jobList; //birden fazla liste dönülüyor
                connectionClosed();

            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {

                result.Result = false;
                result.ErrorText = Ex.Message;

            }

            connectionClosed();
            return result;
        }
        public Entities.EntityResult<JobDB> Insert(JobDB entities)
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Job\" (job_workfield,job_start_date,job_ending_date,job_workposition,job_company_fk,job_id)" +
                    " VALUES(@field,@startdate,@endingdate,@pos,@fk,@id_)", connectionOpen()); //connectionOpen connection döndürüyor
                
                command.Parameters.AddWithValue("@id_", entities.job_id);


                DateTime date3 = new DateTime();
                
                if (entities.ending_temp_date == null)
                {
                    date3 = submethodsx.dateParse("0001-01-01");

                }
                else
                {
                    date3 = submethodsx.dateParse(entities.ending_temp_date);
                }
                NpgsqlDate date2 = new NpgsqlDate(date3);
                
                date3 = submethodsx.dateParse(entities.start_temp_date);
                NpgsqlDate date4 = new NpgsqlDate(date3);

                command.Parameters.AddWithValue("@startdate", date4);
                
                command.Parameters.AddWithValue("@endingdate", date2);
                command.Parameters.AddWithValue("@fk", entities.job_company.company_id);
                command.Parameters.AddWithValue("@pos", entities.job_workposition);
                command.Parameters.AddWithValue("@field", "");
                command.ExecuteNonQuery();
                
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            return result;
        }

        public Entities.EntityResult<JobDB> Delete(int id)
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Job\" WHERE job_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            { 
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            return result;
        }

        public Entities.EntityResult<JobDB> getId(int id)
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            JobDB job = new JobDB();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE job_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                 //workfield   --  start date  --  ending date --- workposition  --  Job fk   -- id
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                

                NpgsqlDate p1 = reader.GetDate(1);
                NpgsqlDate p2 = reader.GetDate(2);
                DateTime start = new DateTime(p1.Year, p1.Month, p1.Day);
                DateTime ending = new DateTime(p2.Year, p2.Month, p2.Day);
                job.start_date = start;
                job.ending_date = ending;

                job.job_workposition = reader.GetString(3);

                job.job_company.company_id = reader.GetInt32(4);
                job.job_id = reader.GetInt32(5);
                job.job_company = companyservice.getId(job.job_company.company_id).Object;
               


            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            result.Object = job;
            return result;
        }

        public Entities.EntityResult<JobDB> Update(JobDB entities)
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            
            

            return result;
        }
        public int getNextPersonId()
        {
            NpgsqlCommand commands = new NpgsqlCommand("SELECT MAX(Job_id) FROM \"Job\"", connectionOpen()); //connectionOpen connection döndürüyor

            commands.ExecuteNonQuery();
            NpgsqlDataReader reader = commands.ExecuteReader();

            reader.Read();
          
            var pin = reader.GetInt32(0);
            connectionClosed();
            return pin + 1;
        }
        public EntityResult<JobDB> UpdateSelected(string table, string colum, int value, int id)
        {
            EntityResult<JobDB> result = new EntityResult<JobDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE @table SET @col = @value  WHERE @tablename = @id; ", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.AddWithValue("@table", "\"" + table + "\"");
                command.Parameters.AddWithValue("@col", colum);
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@tablename", table.ToLower() + "_id");

                command.ExecuteNonQuery();
                connectionClosed();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            return result;
        }


    }
}
