using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Windows;

namespace Demo.Controllers
{
    [EnableCors("*", "*", "*")]
    public class DeviceJoinController : ApiController
    {
        [HttpGet]
        [Route("view/allDevice")]
        public List<ContactModel> testData()
        {
            List<ContactModel> results = new List<ContactModel>();
            
          ContactModel result = null;
           
            /*List<Test> TestList = new List<Test>();
            Test test = null;
            */

            SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true");
            SqlCommand command = new SqlCommand("getAllDevice", sqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();

                SqlDataReader reader = command.ExecuteReader();

                

                while (reader.Read())
                {
                    result = new ContactModel();
                    result.IMEI = reader["IMEI"].ToString().Trim();
                    result.SerialNumber = reader["Serial_Number"].ToString().Trim();
                    result.Manufacturer = reader["name_manufacturer"].ToString().Trim();
                    result.Firmware = reader["Firmware"].ToString().Trim();
                    result.GateWay = reader["name_gateway"].ToString().Trim();
                    result.AppPlatID = reader["name_App"].ToString().Trim();
                    result.Model = reader["model"].ToString().Trim();
                    result.CmTypeId = reader["name_Commu"].ToString().Trim();
                    result.statusDevice = reader["status_device"].ToString().Trim();
                    result.contractnumber = reader["ContractID"].ToString().Trim();
                    results.Add(result);
                }

                


                sqlCon.Close();








            }
            return results;
        
    }
    }
}
