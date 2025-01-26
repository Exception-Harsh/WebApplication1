using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TEstController : ApiController
    {
        [HttpGet]
        public DemoResponse TestQuery()
        {
            DemoResponse response = new DemoResponse { IsSuccess = true };
            try
            {
                OracleConnection connection = new OracleConnection("Data Source=XE;User Id=ARBOUR;Password=12345;");
                OracleCommand command = new OracleCommand("select * from demotable", connection);
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataSet results = new DataSet();
                int junk = adapter.Fill(results);

                foreach (DataRow row in results.Tables[0].Rows)
                {
                    Demo d = new Demo();
                    d.Id = Convert.ToInt32(row["ID"]);
                    d.Name = row["NAME"].ToString();
                    d.JoiningDate = Convert.ToDateTime(row["JDATE"]).ToString("yyyy-MM-dd");

                    response.DemoRecords.Add(d);
                }
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public DemoResponse TestCreate()
        {
            Demo obj = new Demo();
            obj.Id = 8;
            obj.Name = "Arbour";
            obj.JoiningDate = "2025-01-30";

            DemoResponse response = new DemoResponse { IsSuccess = true };
            try
            {
                OracleConnection connection = new OracleConnection("Data Source = XE; User Id = ARBOUR; Password = 12345;");
                connection.Open();
                string insertQuery = "INSERT INTO demotable (ID, NAME, JDATE) VALUES (:Id, :Name, :JoiningDate)";
                OracleCommand command = new OracleCommand(insertQuery, connection);

                command.Parameters.Add(new OracleParameter(":id", obj.Id));
                command.Parameters.Add(new OracleParameter(":name", obj.Name));
                command.Parameters.Add(new OracleParameter(":joiningDate", Convert.ToDateTime(obj.JoiningDate)));

                int affectedrow = command.ExecuteNonQuery();
                if (affectedrow == 1)
                {
                    response.Message = "New record created successfully!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to create new record. No rows affected.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}