using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    public class testController : ApiController
    {
        [HttpPost]
        [Route("test/data")]
        public string InsertDevice([FromBody] ContactModel insert)
        {
            string msg = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=testData;Integrated Security=true"))
                {
                    SqlCommand command = new SqlCommand("testtest", sqlCon);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", insert.InsertEmail);
                    command.Parameters.AddWithValue("@Name", insert.InsertName);
                    

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
