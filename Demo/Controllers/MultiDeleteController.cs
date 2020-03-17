﻿using System;
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
    public class MultiDeleteController : ApiController
    {
        [HttpDelete]
        [Route("delete/multi")]
        public string Delete(string contractID)
        {
            string msg = "";
            try
            {

                int result = 0;
                SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true");
                SqlCommand command = new SqlCommand("MultiDelete", sqlCon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@ContractID", contractID);
                sqlCon.Open();
                result = command.ExecuteNonQuery();
                int i = result;
                if (i > 0)
                {
                    msg = "Delete Complete";
                }
                else
                {
                    msg = "Can't Delete This Record.";
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return msg;
        }

    }
}

