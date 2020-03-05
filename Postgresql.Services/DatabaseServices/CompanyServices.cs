using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;

using Entities;
using Npgsql;

namespace Postgresql.Services.DatabaseServices
{
    public class CompanyServices : StateConnection, IServices<CompanyDB>
    {
        private SubMethods2 method = new SubMethods2();
        public Entities.EntityResult<CompanyDB> getList()
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<CompanyDB> companyList = new List<CompanyDB>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Company\"", connectionOpen()); //connectionOpen connection döndürüyor
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CompanyDB company = new CompanyDB();
                    company.company_id = reader.GetInt32(0);
                    company.company_name = reader.GetString(1);
                    company.company_country = reader.GetString(2);
                    company.company_city = reader.GetString(3);
                    company.company_fullAddress = reader.GetString(4);
                    companyList.Add(company);
                }
                result.Objects = companyList; //birden fazla liste dönülüyor
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
        public Entities.EntityResult<CompanyDB> Insert(CompanyDB entities)
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Company\" (company_id,company_name,company_country,company_city,\"company_fullAddress\") VALUES(@id_,@name_,@country_,@city_,@fulladdress_)", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.AddWithValue("@id_", entities.company_id);
                command.Parameters.AddWithValue("@name_", entities.company_name);
                command.Parameters.AddWithValue("@country_", entities.company_country);
                command.Parameters.AddWithValue("@city_", entities.company_city);
                command.Parameters.AddWithValue("@fulladdress_", entities.company_fullAddress);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            return result;
        }
        public Entities.EntityResult<CompanyDB> Delete(int id)
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Company\" WHERE company_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                method.deleteJobWithCompanyID(id);
                
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

        public Entities.EntityResult<CompanyDB> getId(int id)
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            CompanyDB company = new CompanyDB();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Company\" WHERE company_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.Add(new NpgsqlParameter("@ID", id));
                command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                
                company.company_id = reader.GetInt32(0);
                company.company_name = reader.GetString(1);
                company.company_country = reader.GetString(2);
                company.company_fullAddress = reader.GetString(3);
                company.company_city = reader.GetString(4);


            }
            catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
            }
            connectionClosed();
            result.Object = company;
            return result;
        }

        public Entities.EntityResult<CompanyDB> Update(CompanyDB entities)
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE personedb SET company_name = @name_, company_country = @country_, company_city = @city_,company_fulladdress = @fulladdress_ WHERE id_ =@id_; ", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.AddWithValue("@id_", entities.company_id);
                command.Parameters.AddWithValue("@name_", entities.company_name);
                command.Parameters.AddWithValue("@country_", entities.company_country);
                command.Parameters.AddWithValue("@city_", entities.company_city);
                command.Parameters.AddWithValue("@fulladdress_", entities.company_fullAddress);
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
        public int getNextPersonId()
        {
            NpgsqlCommand commands = new NpgsqlCommand("SELECT MAX(company_id) FROM \"Company\"", connectionOpen()); //connectionOpen connection döndürüyor

            commands.ExecuteNonQuery();
            NpgsqlDataReader reader = commands.ExecuteReader();

            reader.Read();

            //if (reader.GetInt32(0) != null) //buraya bir daha bakarsın
            //{
            //    connectionClosed();
            //    return 0;
            //}
            var nextID = reader.GetInt32(0);


            connectionClosed();
            return nextID+1;
        }
        public EntityResult<CompanyDB> UpdateSelected(string table,string colum, int value, int id)
        {
            EntityResult<CompanyDB> result = new EntityResult<CompanyDB>();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE @table SET @col = @value  WHERE @tablename = @id; ", connectionOpen()); //connectionOpen connection döndürüyor
                command.Parameters.AddWithValue("@table", "\""+table+"\"");
                command.Parameters.AddWithValue("@col",colum );
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@id", id.ToString());
                command.Parameters.AddWithValue("@tablename", table.ToLower()+"_id");

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
