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
    public class UpdateDeviceController : ApiController
    {
        [HttpPut]
        [Route("update/device")]
        public string UpdateDevice([FromBody] ContactModel update)
        {
            string msg = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true"))
                {
                    SqlCommand command = new SqlCommand("updateDevice", sqlCon);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IMEI", update.InsertIMEI);
                    command.Parameters.AddWithValue("@Serial_Number", update.InsertSerialNumber);
                    command.Parameters.AddWithValue("@ManufacturerID", update.InsertManufacturer);
                    command.Parameters.AddWithValue("@Firmware", update.InsertFirmware);
                    command.Parameters.AddWithValue("@GateWayID", update.InsertGateWayID);
                    command.Parameters.AddWithValue("@ApplicationID", update.InsertAppID);
                    command.Parameters.AddWithValue("@ModelID", update.InsertModelID);
                    command.Parameters.AddWithValue("@Communication_Media_TypeID", update.CmTypeId);
                    command.Parameters.AddWithValue("@Device_StatusID", update.statusDevice);
                    command.Parameters.AddWithValue("@ContractID", update.contractnumber);

                    sqlCon.Open();
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        msg = "Update Complete";
                    }
                    else
                    {
                        msg = "Can't Update values.";
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
