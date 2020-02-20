using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    public class DeviceController : ApiController
    {
        [HttpGet]
        [Route("device/testData")]
        public List<ContactModel> testData()
        {
            List<ContactModel> results = new List<ContactModel>();
            ContactModel result = null;

            SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=testData;Integrated Security=true");

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                using (SqlDataAdapter a = new SqlDataAdapter(
                     "SELECT * FROM Device", sqlCon))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);

                    foreach (DataRow row in t.Rows)
                    {
                        result = new ContactModel();
                        result.IMEI = row["IMEI"].ToString().Trim();
                        result.SerialNumber = row["SerialNumber"].ToString().Trim();
                        
                        result.Manufacturer = row["Manufacturer"].ToString().Trim();
                        result.IoTPlatID = row["IoTPlatID"].ToString().Trim();
                        result.AppPlatID = row["AppPlatID"].ToString().Trim();
                        result.StatusID = row["StatusID"].ToString().Trim();
                        result.ModelID = row["ModelID"].ToString().Trim();
                        result.CmTypeId = row["CmTypeId"].ToString().Trim();
                        
                        results.Add(result);
                    }

                    sqlCon.Close();
                }
            }
            return results;
        }

        

    }
}
