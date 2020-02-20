using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Demo.Controllers
{
    [EnableCors("*", "*", "*")]
    public class InsertDeviceController : ApiController
    {
        [HttpPost]
        [Route("insert/data")]
        public string InsertDevice([FromBody] ContactModel insert)
        {
            string msg = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=testData;Integrated Security=true"))
                {
                    SqlCommand command = new SqlCommand("insertData", sqlCon);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IMEI", insert.InsertIMEI);
                    command.Parameters.AddWithValue("@SerialNumber", insert.InsertSerialNumber);
                    command.Parameters.AddWithValue("@Manufacturer", insert.InsertManufacturer);
                    command.Parameters.AddWithValue("@IoTPlatID", insert.InsertIoTPlatID);
                    command.Parameters.AddWithValue("@AppPlatID", insert.InsertAppPlatID);
                    command.Parameters.AddWithValue("@StatusID", insert.InsertStatusID);
                    command.Parameters.AddWithValue("@ModelID", insert.InsertModelID);
                    command.Parameters.AddWithValue("@CmTypeID", insert.InsertCmTypeID);

                    sqlCon.Open();
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        msg = "Insert Complete";
                    }
                    else
                    {
                        msg = "Can't Insert values.";
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return msg;
        }
    }
}
