using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
using Entities;
using Npgsql;
using NpgsqlTypes;
using Npgsql.PostgresTypes;
using Npgsql.TypeMapping;
using Npgsql.Schema;
using Postgresql.Services;



namespace Postgresql.Services.DatabaseServices
{

    public class GraduateServices: StateConnection  , IServices<GraduateDB>
    {
        JobServices jobservicesx = new JobServices();
        SubMethods2 submethodx = new SubMethods2();
        
        public Entities.EntityResult<GraduateDB> getList()
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<GraduateDB> graduateList = new List<GraduateDB>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Graduate\";", connectionOpen()); 
                //id--name--surname--birth--country--city--fulladdress--school--average--language--certificate--follow--followers--isbanned--jobfk
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GraduateDB graduate = new GraduateDB();
                    graduate.graduate_id = reader.GetInt32(0);
                    graduate.graduate_name = reader.GetString(1);
                    graduate.graduate_surname = reader.GetString(2);

                    NpgsqlDate date = reader.GetDate(3);
                    DateTime date2 = new DateTime(date.Year, date.Month, date.Day);
                    graduate.graduate_birth = date2;

                    graduate.graduate_country = reader.GetString(4);
                    graduate.graduate_city = reader.GetString(5);
                    graduate.graduate_fulladress = reader.GetString(6);
                    graduate.graduate_school = reader.GetString(7);

                    graduate.graduate_school_average = reader.GetDouble(8);

                    SubMethods method = new SubMethods();
                    graduate.graduate_language = method.postgreGetArray_string("Graduate", "graduate_language", graduate.graduate_id);
                    graduate.graduate_certificate = method.postgreGetArray_string("Graduate", "graduate_certificate", graduate.graduate_id);
                    graduate.graduate_follow = method.postgreGetArray_int("Graduate", "graduate_follow", graduate.graduate_id);
                    graduate.graduate_followers = method.postgreGetArray_int("Graduate", "graduate_followers", graduate.graduate_id);

                    graduate.graduate_isBanned = reader.GetBoolean(13);

                    graduate.graduate_job_list = method.postgreGetArray_JobDB("Graduate", "graduate_job_fk",graduate.graduate_id);
                    graduate.graduate_phone = reader.GetString(15);
                    graduate.graduate_mail = reader.GetString(16);
                    graduate.graduate_workfield = reader.GetString(17);
                    graduate.graduate_workposition = reader.GetString(18);
                    graduate.graduate_password = reader.GetString(19);    

                    //graduate.graduate_language = reader.GetFieldValue<int>(9);
                    //graduate.graduate_certificate = reader.GetInt32(0);
                    //graduate.graduate_follow = reader.GetInt32(0);
                    //graduate.graduate_followers = reader.GetInt32(0);
                    //graduate.graduate_isBanned = reader.GetInt32(0);
                    //graduate.graduate_job_list = reader.GetInt32(0);
                    graduateList.Add(graduate);
                }
                result.Objects = graduateList; //birden fazla liste dönülüyor
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
        public Entities.EntityResult<GraduateDB> Insert(GraduateDB entities)
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Graduate\" " +
                    "(graduate_id,graduate_name,graduate_surname,graduate_birth,graduate_country,graduate_city,graduate_fullAddress,graduate_school,graduate_gradeaverage,graduate_language," +
                    "graduate_certificate,graduate_follow,graduate_followers,\"graduate_isBanned\",graduate_job_fk,graduate_phone,graduate_mail,graduate_workfield,graduate_workposition,graduate_password)" +
                    "VALUES(@id,@name,@surname,@birth,@country,@city,@fulladdress,@school,@average,@language,@certificate,@follow,@followers,@ban,@job_fk,@phone,@mail,@field,@position,@pass)", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.AddWithValue("@id", entities.graduate_id);
                command.Parameters.AddWithValue("@name", entities.graduate_name);

                DateTime date3=new DateTime();
                date3 = submethodx.dateParse(entities.graduate_date_temp);
                NpgsqlDate date2 =new NpgsqlDate(date3);                             
               
                command.Parameters.AddWithValue("@surname", entities.graduate_surname);
                command.Parameters.AddWithValue("@birth", date2);
                command.Parameters.AddWithValue("@country", entities.graduate_country);
                command.Parameters.AddWithValue("@city", entities.graduate_city);
                command.Parameters.AddWithValue("@fulladdress", entities.graduate_fulladress);
                command.Parameters.AddWithValue("@school", entities.graduate_school);
                command.Parameters.AddWithValue("@average", entities.graduate_school_average);
                command.Parameters.AddWithValue("@language", entities.graduate_language);
                command.Parameters.AddWithValue("@certificate", entities.graduate_certificate);
                command.Parameters.AddWithValue("@follow", entities.graduate_follow);         
                command.Parameters.AddWithValue("@followers", entities.graduate_followers);
                command.Parameters.AddWithValue("@ban", false);
                List<int> job_fk = new List<int>();
               
                command.Parameters.AddWithValue("@job_fk",job_fk);
                command.Parameters.AddWithValue("@phone", "");
                command.Parameters.AddWithValue("@mail", "");
                command.Parameters.AddWithValue("@field", "");
                command.Parameters.AddWithValue("@position", "");
                command.Parameters.AddWithValue("@pass", entities.graduate_password);

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

        public Entities.EntityResult<GraduateDB> Delete(int id)
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Graduate\" WHERE graduate_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                SubMethods method = new SubMethods();
                List<JobDB> job = new List<JobDB>();
                
              
                job = method.postgreGetArray_JobDB("Graduate", "graduate_job_fk",id);
                method.deleteNotices(id);
                foreach(var item in job)
                {
                    jobservicesx.Delete(item.job_id);
                }
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
       public Entities.EntityResult<GraduateDB> getId(int id)
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();
            GraduateDB graduate = new GraduateDB();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Graduate\" WHERE graduate_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                                                                                                                        //workfield   --  start date  --  ending date --- workposition  --  Job fk   -- id
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                graduate.graduate_id = reader.GetInt32(0);
                graduate.graduate_name = reader.GetString(1);
                graduate.graduate_surname = reader.GetString(2);

                NpgsqlDate date = reader.GetDate(3);
                DateTime date2 = new DateTime(date.Year, date.Month, date.Day);
                graduate.graduate_birth = date2;

                graduate.graduate_country = reader.GetString(4);
                graduate.graduate_city = reader.GetString(5);
                graduate.graduate_fulladress = reader.GetString(6);
                graduate.graduate_school = reader.GetString(7);

                graduate.graduate_school_average = reader.GetDouble(8);

                SubMethods method = new SubMethods();
                graduate.graduate_language = method.postgreGetArray_string("Graduate", "graduate_language", graduate.graduate_id);
                graduate.graduate_certificate = method.postgreGetArray_string("Graduate", "graduate_certificate", graduate.graduate_id);
                graduate.graduate_follow = method.postgreGetArray_int("Graduate", "graduate_follow", graduate.graduate_id);
                graduate.graduate_followers = method.postgreGetArray_int("Graduate", "graduate_followers", graduate.graduate_id);

                graduate.graduate_isBanned = reader.GetBoolean(13);

                graduate.graduate_job_list = method.postgreGetArray_JobDB("Graduate", "graduate_job_fk", graduate.graduate_id);
                graduate.graduate_phone = reader.GetString(15);
                graduate.graduate_mail = reader.GetString(16);
                graduate.graduate_workfield = reader.GetString(17);
                graduate.graduate_workposition = reader.GetString(18);
                graduate.graduate_password = reader.GetString(19);
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            result.Object = graduate;
            return result;
        }

        public Entities.EntityResult<GraduateDB> Update(GraduateDB entities)
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();



            return result;
        }
        public int getNextId()
        {
            NpgsqlCommand commands = new NpgsqlCommand("SELECT MAX(graduate_id) FROM \"Graduate\"", connectionOpen()); //connectionOpen connection döndürüyor

            commands.ExecuteNonQuery();
            NpgsqlDataReader reader = commands.ExecuteReader();

            reader.Read();
          
           var pin =  reader.GetInt32(0);
            connectionClosed();
            return pin+1;
           
        }
        public EntityResult<GraduateDB> UpdateSelected(string table, string colum, int value, int id)
        {
            EntityResult<GraduateDB> result = new EntityResult<GraduateDB>();
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
