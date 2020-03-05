using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Databases;
using Entities;
using Postgresql.Services;
using Npgsql;

namespace Postgresql.Services
{
    public class SubMethods2 : StateConnection
    {
        public DateTime dateParse(string date)
        {

            int year = Int32.Parse(date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString());
            int month = Int32.Parse(date[5].ToString() + date[6].ToString());
            int day = Int32.Parse(date[8].ToString() + date[9].ToString());
            DateTime datex = new DateTime(year, month, day);
            return datex;
        }
        public Result deleteJobWithCompanyID(int companyid)
        {
            Result result = new Result();

            result.ErrorText = "succses";
            result.Result_ = true;
            try
            {
                string sql = "DELETE FROM \"Job\" WHERE \"job_company_fk\" = '" + companyid + "'";
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
    }
}
