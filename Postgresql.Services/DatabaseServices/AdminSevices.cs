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

        public class AdminSevices : StateConnection, IServices<Admin>
        {
            public Entities.EntityResult<Admin> getList()
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                result.Result = true;
                result.ErrorText = "Sucsess";
                try
                {
                    List<Admin> adminList = new List<Admin>();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Admin\"", connectionOpen()); //connectionOpen connection döndürüyor
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Admin admin = new Admin();
                        admin.admin_id = reader.GetInt32(0);
                        admin.admin_name = reader.GetString(1);
                        admin.admin_surname = reader.GetString(2);
                        adminList.Add(admin);
                    }
                    result.Objects = adminList; //birden fazla liste dönülüyor
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
            public Entities.EntityResult<Admin> Insert(Admin entities)
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                result.Result = true;
                result.ErrorText = "Sucsess";
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Admin\" (admin_id,admin_name,admin_surname,admin_password) VALUES(@id_,@name_,@surname,@pass)", connectionOpen()); //connectionOpen connection döndürüyor
                    command.Parameters.AddWithValue("@id_", entities.admin_id);
                    command.Parameters.AddWithValue("@name_", entities.admin_name);
                    command.Parameters.AddWithValue("@surname", entities.admin_surname);
                    command.Parameters.AddWithValue("@pass", entities.admin_password);

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
            public Entities.EntityResult<Admin> Delete(int id)
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                result.Result = true;
                result.ErrorText = "Sucsess";
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Admin\" WHERE admin_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
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

            public Entities.EntityResult<Admin> getId(int id)
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                Admin admin = new Admin();
                result.Result = true;
                result.ErrorText = "Sucsess";

                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Admin\" WHERE admin_id=@ID;", connectionOpen()); //connectionOpen connection döndürüyor
                    command.Parameters.Add(new NpgsqlParameter("@ID", id));
                    command.ExecuteNonQuery(); //zannımca sorguyu gönderiyor
                    NpgsqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    admin.admin_id = reader.GetInt32(0);
                    admin.admin_name = reader.GetString(1);
                    admin.admin_surname = reader.GetString(2);


                }
                catch (NpgsqlException Ex)//npgsqlexception pgadminden dönen erroru döner
                {
                    result.Result = false;
                    result.ErrorText = Ex.Message;
                }
                connectionClosed();
                result.Object = admin;
                return result;
            }

            public Entities.EntityResult<Admin> Update(Admin entities)
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                result.Result = true;
                result.ErrorText = "Sucsess";

                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("UPDATE \"Admin\" SET admin_name = @name_, admin_surname = @country_, admin_password = @city_", connectionOpen()); //connectionOpen connection döndürüyor
                    
                    command.Parameters.AddWithValue("@name_", entities.admin_name);
                    command.Parameters.AddWithValue("@country_", entities.admin_surname);
                    command.Parameters.AddWithValue("@city_", entities.admin_password);
                   
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
                NpgsqlCommand commands = new NpgsqlCommand("SELECT MAX(admin_id) FROM \"Admin\"", connectionOpen()); //connectionOpen connection döndürüyor

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
                return nextID + 1;
            }
            public EntityResult<Admin> UpdateSelected(string table, string colum, int value, int id)
            {
                EntityResult<Admin> result = new EntityResult<Admin>();
                result.Result = true;
                result.ErrorText = "Sucsess";

                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("UPDATE @table SET @col = @value  WHERE @tablename = @id; ", connectionOpen()); //connectionOpen connection döndürüyor
                    command.Parameters.AddWithValue("@table", "\"" + table + "\"");
                    command.Parameters.AddWithValue("@col", colum);
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@id", id.ToString());
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
