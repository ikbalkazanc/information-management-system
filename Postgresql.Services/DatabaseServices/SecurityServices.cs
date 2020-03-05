using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Databases;
using Npgsql;

namespace Postgresql.Services.DatabaseServices
{
    public class SecurityServices : StateConnection
    {
        public Entities.EntityResult<Admin> getAdmin(int id)
        {
            Admin temp = new Admin();
            EntityResult<Admin> result = new EntityResult<Admin>();
            result.ErrorText = "sucsses";
            result.Result = true;
            try
            {
                string sql = "SELECT * FROM \"Admin\" WHERE admin_id = " + id + ";";
                NpgsqlCommand command = new NpgsqlCommand(sql, connectionOpen());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    temp.admin_id = reader.GetInt32(0);
                    temp.admin_name = reader.GetString(1);
                    temp.admin_surname = reader.GetString(2);
                    temp.admin_password = reader.GetString(3);
                    result.Object = temp;
                }
                else
                {
                    result.Object = temp;
                }
            }
            catch(NpgsqlException Ex)
            {
                result.ErrorText = Ex.Message;
                result.Result = false;
            }
            connectionClosed();
            return result;
        }
    }
}
