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
    public class Luk_Controller : ApiController
    {
        ContactModel result = null; 
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true");
        
        //LUK_Application
        [HttpGet]
        [Route("lukUp/application")]
        public List<ContactModel> Luk_application()
        {
            List<ContactModel> results = new List<ContactModel>();
            SqlCommand command = new SqlCommand("application", sqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new ContactModel();
                    result.applicationid = reader["ApplicationID"].ToString().Trim();
                    result.name = reader["Name"].ToString().Trim();
                    result.version = reader["Version"].ToString().Trim();
                    result.description = reader["Description"].ToString().Trim();
                    results.Add(result);
                }
                sqlCon.Close();

            }
            return results;
        }


        //LUK_Communication_Media_Type
        [HttpGet]
        [Route("lukUp/communication")]
        public List<ContactModel> Luk_communication()
        {
            List<ContactModel> results = new List<ContactModel>();
            SqlCommand command = new SqlCommand("communication_media_type", sqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new ContactModel();
                    result.communication = reader["Communication_Media_TypeID"].ToString().Trim();
                    result.name = reader["Name"].ToString().Trim();
                    result.description = reader["Description"].ToString().Trim();
                    results.Add(result);
                }
                sqlCon.Close();

            }
            return results;
        }


        //LUK_contact_package
        [HttpGet]
        [Route("lukUp/contact_package")]
        public List<ContactModel> Luk_contact_package()
        {
            List<ContactModel> results = new List<ContactModel>();
            SqlCommand command = new SqlCommand("contact_package", sqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new ContactModel();
                    result.no = reader["no"].ToString().Trim();
                    result.contactnumber = reader["contractnumber"].ToString().Trim();
                    result.packageid = reader["packageid"].ToString().Trim();
                    result.description = reader["Description"].ToString().Trim();
                    results.Add(result);
                }
                sqlCon.Close();

            }
            return results;
        }


        //LUK_Department
        [HttpGet]
        [Route("lukUp/department")]
        public List<ContactModel> Luk_department()
        {
            List<ContactModel> results = new List<ContactModel>();
            SqlCommand command = new SqlCommand("procedure_name", sqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new ContactModel();
                    result.departmentid = reader["departmentid"].ToString().Trim();
                    result.name = reader["name"].ToString().Trim();
                    result.description = reader["Description"].ToString().Trim();
                    results.Add(result);
                }
                sqlCon.Close();

            }
            return results;
        }





    }
}
