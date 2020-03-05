using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace Postgresql.Services
{
    public class StateConnection  
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=mebsisDatabase; User Id=postgres; Password=xl3236825;");
        
        protected NpgsqlConnection connectionOpen()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (NpgsqlException Ex)
            {
                var p =Ex.Message;
                return connection;
            }
        }
        protected NpgsqlConnection connectionClosed()
        {
            if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
            return connection;
        }

    }
}
 