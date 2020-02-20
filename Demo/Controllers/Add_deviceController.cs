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
    public class Add_deviceController : ApiController
    {
        [HttpPost]
        [Route("insert/device")]
        public HttpResponseMessage InsertDevice([FromBody] ContactModel addDevice)
        {
            string msg = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true"))
                {
                    SqlCommand command = new SqlCommand("AddNewDevice", sqlCon);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IMEI", addDevice.InsertIMEI);
                    command.Parameters.AddWithValue("@Serial_Number", addDevice.InsertSerialNumber);
                    command.Parameters.AddWithValue("@ManufacturerID", addDevice.InsertManufacturer);
                    command.Parameters.AddWithValue("@Firmware", addDevice.InsertFirmware);
                    command.Parameters.AddWithValue("@GateWayID", addDevice.InsertGateWayID);
                    command.Parameters.AddWithValue("@ApplicationID", addDevice.InsertAppID);
                    command.Parameters.AddWithValue("@ModelID", addDevice.InsertModelID);
                    command.Parameters.AddWithValue("@Communication_Media_TypeID", addDevice.CmTypeId);
                    command.Parameters.AddWithValue("@Device_StatusID", addDevice.statusDevice);
                    command.Parameters.AddWithValue("@ContractNumber", addDevice.contractnumber);
                    

                    sqlCon.Open();
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        msg = "Add New Device Complete";
                    }
                    else
                    {
                        msg = "Can't Add New Device.";
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(msg) };
        }

       
    }
}
